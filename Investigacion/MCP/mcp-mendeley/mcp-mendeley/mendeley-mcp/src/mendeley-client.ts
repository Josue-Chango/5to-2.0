import axios, { AxiosInstance } from "axios";
import * as fs from "fs";
import * as path from "path";
import * as dotenv from "dotenv";

dotenv.config();

const BASE_URL = "https://api.mendeley.com";
const TOKEN_URL = "https://api.mendeley.com/oauth/token";
const ENV_PATH = path.join(__dirname, "../.env");

export interface MendeleyDocument {
  id: string;
  title: string;
  type: string;
  authors?: Array<{ first_name: string; last_name: string }>;
  year?: number;
  abstract?: string;
  source?: string;
  doi?: string;
  keywords?: string[];
  tags?: string[];
  read?: boolean;
  starred?: boolean;
  file_attached?: boolean;
  created?: string;
  last_modified?: string;
  folder_uuids?: string[];
}

export interface MendeleyFolder {
  id: string;
  name: string;
  parent_id?: string;
  created?: string;
}

export interface MendeleyFile {
  id: string;
  document_id: string;
  file_name: string;
  mime_type: string;
  size: number;
  filehash: string;
  created?: string;
}

export interface MendeleyAnnotation {
  id: string;
  document_id: string;
  text: string;
  type: string;
  created?: string;
  last_modified?: string;
}

class MendeleyClient {
  private client: AxiosInstance;
  private accessToken: string;
  private refreshToken: string;

  constructor() {
    this.accessToken = process.env.MENDELEY_ACCESS_TOKEN || "";
    this.refreshToken = process.env.MENDELEY_REFRESH_TOKEN || "";

    this.client = axios.create({
      baseURL: BASE_URL,
      headers: {
        Authorization: `Bearer ${this.accessToken}`,
      },
    });

    // Interceptor para renovar token automáticamente
    this.client.interceptors.response.use(
      (response) => response,
      async (error) => {
        if (error.response?.status === 401 && this.refreshToken) {
          await this.refreshAccessToken();
          error.config.headers["Authorization"] = `Bearer ${this.accessToken}`;
          return this.client.request(error.config);
        }
        throw error;
      }
    );
  }

  private async refreshAccessToken(): Promise<void> {
    const params = new URLSearchParams({
      grant_type: "refresh_token",
      refresh_token: this.refreshToken,
      redirect_uri: process.env.MENDELEY_REDIRECT_URI || "",
    });

    const credentials = Buffer.from(
      `${process.env.MENDELEY_CLIENT_ID}:${process.env.MENDELEY_CLIENT_SECRET}`
    ).toString("base64");

    const response = await axios.post(TOKEN_URL, params.toString(), {
      headers: {
        Authorization: `Basic ${credentials}`,
        "Content-Type": "application/x-www-form-urlencoded",
      },
    });

    this.accessToken = response.data.access_token;
    if (response.data.refresh_token) {
      this.refreshToken = response.data.refresh_token;
    }

    // Actualizar el header del cliente
    this.client.defaults.headers["Authorization"] =
      `Bearer ${this.accessToken}`;

    // Guardar en .env
    this.updateEnvFile();
  }

  private updateEnvFile(): void {
    try {
      let envContent = fs.readFileSync(ENV_PATH, "utf8");
      envContent = envContent.replace(
        /MENDELEY_ACCESS_TOKEN=.*/,
        `MENDELEY_ACCESS_TOKEN=${this.accessToken}`
      );
      if (this.refreshToken) {
        envContent = envContent.replace(
          /MENDELEY_REFRESH_TOKEN=.*/,
          `MENDELEY_REFRESH_TOKEN=${this.refreshToken}`
        );
      }
      fs.writeFileSync(ENV_PATH, envContent);
    } catch {
      // Si no puede escribir, continúa igual
    }
  }

  // ── DOCUMENTOS ──────────────────────────────────────────────────────────────

  async listDocuments(params?: {
    limit?: number;
    folder_id?: string;
    sort?: string;
    order?: "asc" | "desc";
    authored?: boolean;
  }): Promise<MendeleyDocument[]> {
    const query: Record<string, string | number | boolean> = {
      limit: params?.limit || 200,
      view: "all",
    };
    if (params?.folder_id) query.folder_id = params.folder_id;
    if (params?.sort) query.sort = params.sort;
    if (params?.order) query.order = params.order;
    if (params?.authored !== undefined) query.authored = params.authored;

    const response = await this.client.get("/documents", {
      params: query,
      headers: { Accept: "application/vnd.mendeley-document.1+json" },
    });
    return response.data;
  }

  async getDocument(documentId: string): Promise<MendeleyDocument> {
    const response = await this.client.get(`/documents/${documentId}`, {
      params: { view: "all" },
      headers: { Accept: "application/vnd.mendeley-document.1+json" },
    });
    return response.data;
  }

  async searchDocuments(
    query: string,
    params?: { limit?: number; min_year?: number; max_year?: number }
  ): Promise<MendeleyDocument[]> {
    const response = await this.client.get("/search/catalog", {
      params: {
        query,
        limit: params?.limit || 200,
        ...(params?.min_year && { min_year: params.min_year }),
        ...(params?.max_year && { max_year: params.max_year }),
        view: "all",
      },
      headers: { Accept: "application/vnd.mendeley-document.1+json" },
    });
    return response.data;
  }

  async createDocument(
    document: Partial<MendeleyDocument>
  ): Promise<MendeleyDocument> {
    const response = await this.client.post("/documents", document, {
      headers: {
        Accept: "application/vnd.mendeley-document.1+json",
        "Content-Type": "application/vnd.mendeley-document.1+json",
      },
    });
    return response.data;
  }

  async updateDocument(
    documentId: string,
    updates: Partial<MendeleyDocument>
  ): Promise<MendeleyDocument> {
    const response = await this.client.patch(
      `/documents/${documentId}`,
      updates,
      {
        headers: {
          Accept: "application/vnd.mendeley-document.1+json",
          "Content-Type": "application/vnd.mendeley-document.1+json",
        },
      }
    );
    return response.data;
  }

  async deleteDocument(documentId: string): Promise<void> {
    await this.client.delete(`/documents/${documentId}`);
  }

  // ── CARPETAS ─────────────────────────────────────────────────────────────────

  async listFolders(): Promise<MendeleyFolder[]> {
    const response = await this.client.get("/folders", {
      headers: { Accept: "application/vnd.mendeley-folder.1+json" },
    });
    return response.data;
  }

  async createFolder(name: string, parentId?: string): Promise<MendeleyFolder> {
    const response = await this.client.post(
      "/folders",
      { name, ...(parentId && { parent_id: parentId }) },
      {
        headers: {
          Accept: "application/vnd.mendeley-folder.1+json",
          "Content-Type": "application/vnd.mendeley-folder.1+json",
        },
      }
    );
    return response.data;
  }

  async addDocumentToFolder(
    folderId: string,
    documentId: string
  ): Promise<void> {
    await this.client.post(
      `/folders/${folderId}/documents`,
      { id: documentId },
      {
        headers: {
          "Content-Type": "application/vnd.mendeley-document.1+json",
        },
      }
    );
  }

  // ── ARCHIVOS ──────────────────────────────────────────────────────────────────

  async listFiles(documentId?: string): Promise<MendeleyFile[]> {
    const response = await this.client.get("/files", {
      params: documentId ? { document_id: documentId } : {},
      headers: { Accept: "application/vnd.mendeley-file.1+json" },
    });
    return response.data;
  }

  async uploadFile(
    documentId: string,
    fileBuffer: Buffer,
    fileName: string
  ): Promise<MendeleyFile> {
    const response = await this.client.post("/files", fileBuffer, {
      headers: {
        Accept: "application/vnd.mendeley-file.1+json",
        "Content-Type": "application/pdf",
        "Content-Disposition": `attachment; filename="${fileName}"`,
        Link: `<https://api.mendeley.com/documents/${documentId}>; rel="document"`,
      },
    });
    return response.data;
  }

  async downloadFile(fileId: string): Promise<Buffer> {
    const response = await this.client.get(`/files/${fileId}`, {
      responseType: "arraybuffer",
      headers: { Accept: "application/pdf" },
    });
    return Buffer.from(response.data);
  }

  // ── ANOTACIONES ──────────────────────────────────────────────────────────────

  async listAnnotations(documentId?: string): Promise<MendeleyAnnotation[]> {
    const response = await this.client.get("/annotations", {
      params: documentId ? { document_id: documentId } : { limit: 50 },
      headers: { Accept: "application/vnd.mendeley-annotation.1+json" },
    });
    return response.data;
  }

  // ── PERFIL ────────────────────────────────────────────────────────────────────

  async getProfile(): Promise<Record<string, unknown>> {
    const response = await this.client.get("/profiles/me");
    return response.data;
  }
}

export const mendeleyClient = new MendeleyClient();
