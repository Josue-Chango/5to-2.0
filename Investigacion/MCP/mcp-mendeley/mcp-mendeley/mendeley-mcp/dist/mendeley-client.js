"use strict";
var __createBinding = (this && this.__createBinding) || (Object.create ? (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    var desc = Object.getOwnPropertyDescriptor(m, k);
    if (!desc || ("get" in desc ? !m.__esModule : desc.writable || desc.configurable)) {
      desc = { enumerable: true, get: function() { return m[k]; } };
    }
    Object.defineProperty(o, k2, desc);
}) : (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    o[k2] = m[k];
}));
var __setModuleDefault = (this && this.__setModuleDefault) || (Object.create ? (function(o, v) {
    Object.defineProperty(o, "default", { enumerable: true, value: v });
}) : function(o, v) {
    o["default"] = v;
});
var __importStar = (this && this.__importStar) || (function () {
    var ownKeys = function(o) {
        ownKeys = Object.getOwnPropertyNames || function (o) {
            var ar = [];
            for (var k in o) if (Object.prototype.hasOwnProperty.call(o, k)) ar[ar.length] = k;
            return ar;
        };
        return ownKeys(o);
    };
    return function (mod) {
        if (mod && mod.__esModule) return mod;
        var result = {};
        if (mod != null) for (var k = ownKeys(mod), i = 0; i < k.length; i++) if (k[i] !== "default") __createBinding(result, mod, k[i]);
        __setModuleDefault(result, mod);
        return result;
    };
})();
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.mendeleyClient = void 0;
const axios_1 = __importDefault(require("axios"));
const fs = __importStar(require("fs"));
const path = __importStar(require("path"));
const dotenv = __importStar(require("dotenv"));
dotenv.config();
const BASE_URL = "https://api.mendeley.com";
const TOKEN_URL = "https://api.mendeley.com/oauth/token";
const ENV_PATH = path.join(__dirname, "../.env");
class MendeleyClient {
    constructor() {
        this.accessToken = process.env.MENDELEY_ACCESS_TOKEN || "";
        this.refreshToken = process.env.MENDELEY_REFRESH_TOKEN || "";
        this.client = axios_1.default.create({
            baseURL: BASE_URL,
            headers: {
                Authorization: `Bearer ${this.accessToken}`,
            },
        });
        // Interceptor para renovar token automáticamente
        this.client.interceptors.response.use((response) => response, async (error) => {
            if (error.response?.status === 401 && this.refreshToken) {
                await this.refreshAccessToken();
                error.config.headers["Authorization"] = `Bearer ${this.accessToken}`;
                return this.client.request(error.config);
            }
            throw error;
        });
    }
    async refreshAccessToken() {
        const params = new URLSearchParams({
            grant_type: "refresh_token",
            refresh_token: this.refreshToken,
            redirect_uri: process.env.MENDELEY_REDIRECT_URI || "",
        });
        const credentials = Buffer.from(`${process.env.MENDELEY_CLIENT_ID}:${process.env.MENDELEY_CLIENT_SECRET}`).toString("base64");
        const response = await axios_1.default.post(TOKEN_URL, params.toString(), {
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
    updateEnvFile() {
        try {
            let envContent = fs.readFileSync(ENV_PATH, "utf8");
            envContent = envContent.replace(/MENDELEY_ACCESS_TOKEN=.*/, `MENDELEY_ACCESS_TOKEN=${this.accessToken}`);
            if (this.refreshToken) {
                envContent = envContent.replace(/MENDELEY_REFRESH_TOKEN=.*/, `MENDELEY_REFRESH_TOKEN=${this.refreshToken}`);
            }
            fs.writeFileSync(ENV_PATH, envContent);
        }
        catch {
            // Si no puede escribir, continúa igual
        }
    }
    // ── DOCUMENTOS ──────────────────────────────────────────────────────────────
    async listDocuments(params) {
        const query = {
            limit: params?.limit || 200,
            view: "all",
        };
        if (params?.folder_id)
            query.folder_id = params.folder_id;
        if (params?.sort)
            query.sort = params.sort;
        if (params?.order)
            query.order = params.order;
        if (params?.authored !== undefined)
            query.authored = params.authored;
        const response = await this.client.get("/documents", {
            params: query,
            headers: { Accept: "application/vnd.mendeley-document.1+json" },
        });
        return response.data;
    }
    async getDocument(documentId) {
        const response = await this.client.get(`/documents/${documentId}`, {
            params: { view: "all" },
            headers: { Accept: "application/vnd.mendeley-document.1+json" },
        });
        return response.data;
    }
    async searchDocuments(query, params) {
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
    async createDocument(document) {
        const response = await this.client.post("/documents", document, {
            headers: {
                Accept: "application/vnd.mendeley-document.1+json",
                "Content-Type": "application/vnd.mendeley-document.1+json",
            },
        });
        return response.data;
    }
    async updateDocument(documentId, updates) {
        const response = await this.client.patch(`/documents/${documentId}`, updates, {
            headers: {
                Accept: "application/vnd.mendeley-document.1+json",
                "Content-Type": "application/vnd.mendeley-document.1+json",
            },
        });
        return response.data;
    }
    async deleteDocument(documentId) {
        await this.client.delete(`/documents/${documentId}`);
    }
    // ── CARPETAS ─────────────────────────────────────────────────────────────────
    async listFolders() {
        const response = await this.client.get("/folders", {
            headers: { Accept: "application/vnd.mendeley-folder.1+json" },
        });
        return response.data;
    }
    async createFolder(name, parentId) {
        const response = await this.client.post("/folders", { name, ...(parentId && { parent_id: parentId }) }, {
            headers: {
                Accept: "application/vnd.mendeley-folder.1+json",
                "Content-Type": "application/vnd.mendeley-folder.1+json",
            },
        });
        return response.data;
    }
    async addDocumentToFolder(folderId, documentId) {
        await this.client.post(`/folders/${folderId}/documents`, { id: documentId }, {
            headers: {
                "Content-Type": "application/vnd.mendeley-document.1+json",
            },
        });
    }
    // ── ARCHIVOS ──────────────────────────────────────────────────────────────────
    async listFiles(documentId) {
        const response = await this.client.get("/files", {
            params: documentId ? { document_id: documentId } : {},
            headers: { Accept: "application/vnd.mendeley-file.1+json" },
        });
        return response.data;
    }
    async uploadFile(documentId, fileBuffer, fileName) {
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
    async downloadFile(fileId) {
        const response = await this.client.get(`/files/${fileId}`, {
            responseType: "arraybuffer",
            headers: { Accept: "application/pdf" },
        });
        return Buffer.from(response.data);
    }
    // ── ANOTACIONES ──────────────────────────────────────────────────────────────
    async listAnnotations(documentId) {
        const response = await this.client.get("/annotations", {
            params: documentId ? { document_id: documentId } : { limit: 50 },
            headers: { Accept: "application/vnd.mendeley-annotation.1+json" },
        });
        return response.data;
    }
    // ── PERFIL ────────────────────────────────────────────────────────────────────
    async getProfile() {
        const response = await this.client.get("/profiles/me");
        return response.data;
    }
}
exports.mendeleyClient = new MendeleyClient();
