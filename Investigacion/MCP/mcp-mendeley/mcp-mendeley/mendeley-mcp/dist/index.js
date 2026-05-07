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
Object.defineProperty(exports, "__esModule", { value: true });
const index_js_1 = require("@modelcontextprotocol/sdk/server/index.js");
const stdio_js_1 = require("@modelcontextprotocol/sdk/server/stdio.js");
const types_js_1 = require("@modelcontextprotocol/sdk/types.js");
const dotenv = __importStar(require("dotenv"));
const mendeley_client_js_1 = require("./mendeley-client.js");
dotenv.config();
// ── DEFINICIÓN DE HERRAMIENTAS ─────────────────────────────────────────────────
const TOOLS = [
    {
        name: "mendeley_list_documents",
        description: "Lista los documentos de tu biblioteca de Mendeley. Puedes filtrar por carpeta, ordenar por título/año/autor y limitar la cantidad.",
        inputSchema: {
            type: "object",
            properties: {
                limit: {
                    type: "number",
                    description: "Número máximo de documentos a retornar (default: 50, max: 500)",
                },
                folder_id: {
                    type: "string",
                    description: "ID de la carpeta para filtrar documentos",
                },
                sort: {
                    type: "string",
                    enum: ["created", "last_modified", "title", "year", "author"],
                    description: "Campo por el que ordenar",
                },
                order: {
                    type: "string",
                    enum: ["asc", "desc"],
                    description: "Dirección del ordenamiento",
                },
            },
        },
    },
    {
        name: "mendeley_get_document",
        description: "Obtiene los detalles completos de un documento específico de Mendeley incluyendo abstract, autores, año, DOI, tags y más.",
        inputSchema: {
            type: "object",
            properties: {
                document_id: {
                    type: "string",
                    description: "El ID único del documento en Mendeley",
                },
            },
            required: ["document_id"],
        },
    },
    {
        name: "mendeley_search_catalog",
        description: "Busca artículos en el catálogo público de Mendeley (millones de papers). Útil para encontrar literatura relacionada con tu proyecto.",
        inputSchema: {
            type: "object",
            properties: {
                query: {
                    type: "string",
                    description: "Términos de búsqueda (título, autores, palabras clave, etc.)",
                },
                limit: {
                    type: "number",
                    description: "Número de resultados (default: 20)",
                },
                min_year: {
                    type: "number",
                    description: "Año mínimo de publicación",
                },
                max_year: {
                    type: "number",
                    description: "Año máximo de publicación",
                },
            },
            required: ["query"],
        },
    },
    {
        name: "mendeley_list_folders",
        description: "Lista todas las carpetas/colecciones en tu biblioteca de Mendeley con sus IDs.",
        inputSchema: {
            type: "object",
            properties: {},
        },
    },
    {
        name: "mendeley_create_folder",
        description: "Crea una nueva carpeta en tu biblioteca de Mendeley.",
        inputSchema: {
            type: "object",
            properties: {
                name: {
                    type: "string",
                    description: "Nombre de la nueva carpeta",
                },
                parent_id: {
                    type: "string",
                    description: "ID de la carpeta padre (opcional, para subcarpetas)",
                },
            },
            required: ["name"],
        },
    },
    {
        name: "mendeley_create_document",
        description: "Crea un nuevo documento/referencia en tu biblioteca de Mendeley manualmente ingresando los metadatos.",
        inputSchema: {
            type: "object",
            properties: {
                title: { type: "string", description: "Título del documento" },
                type: {
                    type: "string",
                    enum: [
                        "journal_article",
                        "book",
                        "book_section",
                        "conference_proceedings",
                        "thesis",
                        "report",
                        "working_paper",
                        "web_page",
                        "generic",
                    ],
                    description: "Tipo de documento",
                },
                year: { type: "number", description: "Año de publicación" },
                abstract: { type: "string", description: "Resumen del documento" },
                source: {
                    type: "string",
                    description: "Nombre de la revista o fuente",
                },
                doi: { type: "string", description: "DOI del artículo" },
                authors: {
                    type: "array",
                    description: "Lista de autores",
                    items: {
                        type: "object",
                        properties: {
                            first_name: { type: "string" },
                            last_name: { type: "string" },
                        },
                    },
                },
                keywords: {
                    type: "array",
                    items: { type: "string" },
                    description: "Palabras clave",
                },
                tags: {
                    type: "array",
                    items: { type: "string" },
                    description: "Etiquetas personales",
                },
            },
            required: ["title", "type"],
        },
    },
    {
        name: "mendeley_update_document",
        description: "Actualiza los metadatos de un documento existente en Mendeley (título, abstract, tags, etc.).",
        inputSchema: {
            type: "object",
            properties: {
                document_id: {
                    type: "string",
                    description: "ID del documento a actualizar",
                },
                title: { type: "string" },
                abstract: { type: "string" },
                year: { type: "number" },
                tags: { type: "array", items: { type: "string" } },
                keywords: { type: "array", items: { type: "string" } },
                read: { type: "boolean", description: "Marcar como leído" },
                starred: { type: "boolean", description: "Marcar con estrella" },
            },
            required: ["document_id"],
        },
    },
    {
        name: "mendeley_add_to_folder",
        description: "Agrega un documento existente a una carpeta de Mendeley.",
        inputSchema: {
            type: "object",
            properties: {
                folder_id: { type: "string", description: "ID de la carpeta" },
                document_id: { type: "string", description: "ID del documento" },
            },
            required: ["folder_id", "document_id"],
        },
    },
    {
        name: "mendeley_list_files",
        description: "Lista los archivos PDF adjuntos a los documentos de tu biblioteca.",
        inputSchema: {
            type: "object",
            properties: {
                document_id: {
                    type: "string",
                    description: "ID del documento para filtrar (opcional)",
                },
            },
        },
    },
    {
        name: "mendeley_get_annotations",
        description: "Obtiene las anotaciones y notas que has hecho en los PDFs de Mendeley.",
        inputSchema: {
            type: "object",
            properties: {
                document_id: {
                    type: "string",
                    description: "ID del documento (opcional, si no se da retorna todas las anotaciones)",
                },
            },
        },
    },
    {
        name: "mendeley_get_profile",
        description: "Obtiene información de tu perfil de Mendeley (nombre, institución, áreas de investigación).",
        inputSchema: {
            type: "object",
            properties: {},
        },
    },
    {
        name: "mendeley_analyze_library",
        description: "Hace un análisis completo de tu biblioteca: cuenta documentos por tipo, autores frecuentes, años de publicación, tags más usados. Útil para entender tu colección de investigación.",
        inputSchema: {
            type: "object",
            properties: {
                folder_id: {
                    type: "string",
                    description: "Limitar análisis a una carpeta específica (opcional)",
                },
            },
        },
    },
    {
        name: "mendeley_download_pdf",
        description: "Descarga el PDF de un documento de Mendeley y lo guarda localmente para poder analizarlo con Copilot.",
        inputSchema: {
            type: "object",
            properties: {
                file_id: {
                    type: "string",
                    description: "ID del archivo en Mendeley (obtenido con mendeley_list_files)",
                },
                save_path: {
                    type: "string",
                    description: "Ruta local donde guardar el PDF (ej: C:/Users/MEGAPC/Desktop/papers/paper1.pdf)",
                },
            },
            required: ["file_id", "save_path"],
        },
    },
];
// ── SERVIDOR MCP ───────────────────────────────────────────────────────────────
const server = new index_js_1.Server({ name: "mendeley-mcp-server", version: "1.0.0" }, { capabilities: { tools: {} } });
server.setRequestHandler(types_js_1.ListToolsRequestSchema, async () => {
    return { tools: TOOLS };
});
server.setRequestHandler(types_js_1.CallToolRequestSchema, async (request) => {
    const { name, arguments: args } = request.params;
    try {
        switch (name) {
            // ── listar documentos ──────────────────────────────────────────────────
            case "mendeley_list_documents": {
                const docs = await mendeley_client_js_1.mendeleyClient.listDocuments(args);
                const formatted = docs.map((d) => ({
                    id: d.id,
                    title: d.title,
                    authors: d.authors
                        ?.map((a) => `${a.first_name} ${a.last_name}`)
                        .join(", "),
                    year: d.year,
                    type: d.type,
                    source: d.source,
                    tags: d.tags,
                    read: d.read,
                    starred: d.starred,
                    file_attached: d.file_attached,
                }));
                return {
                    content: [
                        {
                            type: "text",
                            text: `📚 **${docs.length} documentos encontrados**\n\n${JSON.stringify(formatted, null, 2)}`,
                        },
                    ],
                };
            }
            // ── obtener documento ──────────────────────────────────────────────────
            case "mendeley_get_document": {
                const doc = await mendeley_client_js_1.mendeleyClient.getDocument(args.document_id);
                return {
                    content: [
                        {
                            type: "text",
                            text: `📄 **${doc.title}**\n\n${JSON.stringify(doc, null, 2)}`,
                        },
                    ],
                };
            }
            // ── buscar en catálogo ─────────────────────────────────────────────────
            case "mendeley_search_catalog": {
                const results = await mendeley_client_js_1.mendeleyClient.searchDocuments(args.query, args);
                const formatted = results.map((d) => ({
                    id: d.id,
                    title: d.title,
                    authors: d.authors
                        ?.map((a) => `${a.first_name} ${a.last_name}`)
                        .join(", "),
                    year: d.year,
                    source: d.source,
                    doi: d.doi,
                    abstract: d.abstract?.substring(0, 300) + (d.abstract && d.abstract.length > 300 ? "..." : ""),
                }));
                return {
                    content: [
                        {
                            type: "text",
                            text: `🔍 **${results.length} resultados para "${args.query}"**\n\n${JSON.stringify(formatted, null, 2)}`,
                        },
                    ],
                };
            }
            // ── listar carpetas ────────────────────────────────────────────────────
            case "mendeley_list_folders": {
                const folders = await mendeley_client_js_1.mendeleyClient.listFolders();
                return {
                    content: [
                        {
                            type: "text",
                            text: `📁 **${folders.length} carpetas**\n\n${JSON.stringify(folders, null, 2)}`,
                        },
                    ],
                };
            }
            // ── crear carpeta ──────────────────────────────────────────────────────
            case "mendeley_create_folder": {
                const folder = await mendeley_client_js_1.mendeleyClient.createFolder(args.name, args.parent_id);
                return {
                    content: [
                        {
                            type: "text",
                            text: `✅ Carpeta creada: **${folder.name}** (ID: ${folder.id})`,
                        },
                    ],
                };
            }
            // ── crear documento ────────────────────────────────────────────────────
            case "mendeley_create_document": {
                const doc = await mendeley_client_js_1.mendeleyClient.createDocument(args);
                return {
                    content: [
                        {
                            type: "text",
                            text: `✅ Documento creado: **${doc.title}** (ID: ${doc.id})`,
                        },
                    ],
                };
            }
            // ── actualizar documento ───────────────────────────────────────────────
            case "mendeley_update_document": {
                const { document_id, ...updates } = args;
                const doc = await mendeley_client_js_1.mendeleyClient.updateDocument(document_id, updates);
                return {
                    content: [
                        {
                            type: "text",
                            text: `✅ Documento actualizado: **${doc.title}**`,
                        },
                    ],
                };
            }
            // ── agregar a carpeta ──────────────────────────────────────────────────
            case "mendeley_add_to_folder": {
                await mendeley_client_js_1.mendeleyClient.addDocumentToFolder(args.folder_id, args.document_id);
                return {
                    content: [
                        {
                            type: "text",
                            text: `✅ Documento agregado a la carpeta exitosamente`,
                        },
                    ],
                };
            }
            // ── listar archivos ────────────────────────────────────────────────────
            case "mendeley_list_files": {
                const files = await mendeley_client_js_1.mendeleyClient.listFiles(args.document_id);
                return {
                    content: [
                        {
                            type: "text",
                            text: `📎 **${files.length} archivos**\n\n${JSON.stringify(files, null, 2)}`,
                        },
                    ],
                };
            }
            // ── anotaciones ───────────────────────────────────────────────────────
            case "mendeley_get_annotations": {
                const annotations = await mendeley_client_js_1.mendeleyClient.listAnnotations(args.document_id);
                return {
                    content: [
                        {
                            type: "text",
                            text: `💬 **${annotations.length} anotaciones**\n\n${JSON.stringify(annotations, null, 2)}`,
                        },
                    ],
                };
            }
            // ── perfil ─────────────────────────────────────────────────────────────
            case "mendeley_get_profile": {
                const profile = await mendeley_client_js_1.mendeleyClient.getProfile();
                return {
                    content: [
                        {
                            type: "text",
                            text: `👤 **Perfil de Mendeley**\n\n${JSON.stringify(profile, null, 2)}`,
                        },
                    ],
                };
            }
            // ── análisis de biblioteca ─────────────────────────────────────────────
            case "mendeley_analyze_library": {
                const docs = await mendeley_client_js_1.mendeleyClient.listDocuments({
                    limit: 500,
                    folder_id: args.folder_id,
                });
                const totalDocs = docs.length;
                const byType = {};
                const byYear = {};
                const authorFreq = {};
                const tagFreq = {};
                let withFiles = 0;
                let read = 0;
                let starred = 0;
                docs.forEach((d) => {
                    // por tipo
                    byType[d.type || "unknown"] = (byType[d.type || "unknown"] || 0) + 1;
                    // por año
                    if (d.year) {
                        byYear[d.year] = (byYear[d.year] || 0) + 1;
                    }
                    // autores
                    d.authors?.forEach((a) => {
                        const key = `${a.first_name} ${a.last_name}`.trim();
                        if (key)
                            authorFreq[key] = (authorFreq[key] || 0) + 1;
                    });
                    // tags
                    d.tags?.forEach((t) => {
                        tagFreq[t] = (tagFreq[t] || 0) + 1;
                    });
                    if (d.file_attached)
                        withFiles++;
                    if (d.read)
                        read++;
                    if (d.starred)
                        starred++;
                });
                // Top 10 autores
                const topAuthors = Object.entries(authorFreq)
                    .sort((a, b) => b[1] - a[1])
                    .slice(0, 10);
                // Top 10 tags
                const topTags = Object.entries(tagFreq)
                    .sort((a, b) => b[1] - a[1])
                    .slice(0, 10);
                const analysis = {
                    resumen: {
                        total_documentos: totalDocs,
                        con_pdf_adjunto: withFiles,
                        leidos: read,
                        marcados_con_estrella: starred,
                    },
                    por_tipo: byType,
                    distribucion_por_año: Object.fromEntries(Object.entries(byYear).sort((a, b) => Number(a[0]) - Number(b[0]))),
                    top_10_autores: topAuthors,
                    top_10_tags: topTags,
                };
                return {
                    content: [
                        {
                            type: "text",
                            text: `📊 **Análisis de tu biblioteca Mendeley**\n\n${JSON.stringify(analysis, null, 2)}`,
                        },
                    ],
                };
            }
            case "mendeley_download_pdf": {
                const fileBuffer = await mendeley_client_js_1.mendeleyClient.downloadFile(args.file_id);
                const savePath = args.save_path;
                // Crear carpeta si no existe
                const fs = await Promise.resolve().then(() => __importStar(require("fs")));
                const path = await Promise.resolve().then(() => __importStar(require("path")));
                fs.mkdirSync(path.dirname(savePath), { recursive: true });
                fs.writeFileSync(savePath, fileBuffer);
                return {
                    content: [
                        {
                            type: "text",
                            text: `✅ PDF descargado en: ${savePath}\nYa puedes usarlo en Copilot con: #file:${savePath}`,
                        },
                    ],
                };
            }
            default:
                return {
                    content: [{ type: "text", text: `❌ Herramienta desconocida: ${name}` }],
                    isError: true,
                };
        }
    }
    catch (error) {
        const msg = error?.response?.data?.message ||
            error?.message ||
            "Error desconocido";
        const status = error?.response?.status;
        let hint = "";
        if (status === 401) {
            hint = "\n\n💡 Tu token puede haber expirado. Corre: npm run auth";
        }
        else if (status === 404) {
            hint = "\n\n💡 El recurso no fue encontrado. Verifica el ID.";
        }
        return {
            content: [
                {
                    type: "text",
                    text: `❌ Error ${status || ""}: ${msg}${hint}`,
                },
            ],
            isError: true,
        };
    }
});
// ── INICIO ─────────────────────────────────────────────────────────────────────
async function main() {
    if (!process.env.MENDELEY_ACCESS_TOKEN) {
        process.stderr.write("⚠️  MENDELEY_ACCESS_TOKEN no configurado.\n" +
            "   Corre primero: npm run auth\n");
    }
    const transport = new stdio_js_1.StdioServerTransport();
    await server.connect(transport);
    process.stderr.write("✅ Mendeley MCP Server iniciado\n");
}
main().catch((err) => {
    process.stderr.write(`Fatal: ${err}\n`);
    process.exit(1);
});
