# 🔬 Mendeley MCP Server

Conecta Claude directamente con tu biblioteca de Mendeley. Permite listar, buscar, crear y analizar documentos académicos desde Claude.

## ✨ Herramientas disponibles

| Herramienta | Descripción |
|---|---|
| `mendeley_list_documents` | Lista tu biblioteca completa o por carpeta |
| `mendeley_get_document` | Detalles completos de un documento |
| `mendeley_search_catalog` | Busca en el catálogo público de Mendeley |
| `mendeley_list_folders` | Lista tus carpetas/colecciones |
| `mendeley_create_folder` | Crea una nueva carpeta |
| `mendeley_create_document` | Agrega una referencia manualmente |
| `mendeley_update_document` | Edita metadatos, tags, estado de lectura |
| `mendeley_add_to_folder` | Mueve documentos a carpetas |
| `mendeley_list_files` | Lista los PDFs adjuntos |
| `mendeley_get_annotations` | Lee tus anotaciones y notas |
| `mendeley_get_profile` | Información de tu perfil |
| `mendeley_analyze_library` | Análisis estadístico de tu biblioteca |

---

## 🚀 Instalación paso a paso

### Paso 1 — Registrar tu aplicación en Mendeley

1. Ve a **[https://dev.mendeley.com/myapps.html](https://dev.mendeley.com/myapps.html)**
2. Inicia sesión con tu cuenta de Mendeley
3. Haz clic en **"Register new app"**
4. Rellena el formulario:
   - **Name:** Mi MCP de Investigación (cualquier nombre)
   - **Description:** Integración con Claude
   - **Redirect URIs:** `http://localhost:3333/callback`
5. Guarda el **Client ID** y **Client Secret**

### Paso 2 — Configurar el proyecto

```bash
# Clona o descarga este proyecto
cd mendeley-mcp

# Instala dependencias
npm install

# Copia el archivo de configuración
cp .env.example .env
```

Abre `.env` y pon tus credenciales:

```env
MENDELEY_CLIENT_ID=tu_client_id_aqui
MENDELEY_CLIENT_SECRET=tu_client_secret_aqui
MENDELEY_REDIRECT_URI=http://localhost:3333/callback
```

### Paso 3 — Autenticarse con Mendeley

```bash
npm run auth
```

Esto abrirá tu navegador para autorizar la aplicación. Después de autorizar, los tokens se guardan automáticamente en tu `.env`.

### Paso 4 — Compilar

```bash
npm run build
```

### Paso 5 — Conectar con Claude Desktop (o VS Code + GitHub Copilot)

#### Para Claude Desktop

Edita `~/Library/Application Support/Claude/claude_desktop_config.json` (macOS) o `%APPDATA%\Claude\claude_desktop_config.json` (Windows):

```json
{
  "mcpServers": {
    "mendeley": {
      "command": "node",
      "args": ["/RUTA/ABSOLUTA/mendeley-mcp/dist/index.js"],
      "env": {
        "MENDELEY_CLIENT_ID": "tu_client_id",
        "MENDELEY_CLIENT_SECRET": "tu_client_secret",
        "MENDELEY_ACCESS_TOKEN": "tu_access_token",
        "MENDELEY_REFRESH_TOKEN": "tu_refresh_token",
        "MENDELEY_REDIRECT_URI": "http://localhost:3333/callback"
      }
    }
  }
}
```

#### Para VS Code con GitHub Copilot (MCP)

En tu `settings.json` o `.vscode/mcp.json`:

```json
{
  "mcp": {
    "servers": {
      "mendeley": {
        "command": "node",
        "args": ["/RUTA/ABSOLUTA/mendeley-mcp/dist/index.js"],
        "env": {
          "MENDELEY_CLIENT_ID": "tu_client_id",
          "MENDELEY_CLIENT_SECRET": "tu_client_secret",
          "MENDELEY_ACCESS_TOKEN": "tu_access_token",
          "MENDELEY_REFRESH_TOKEN": "tu_refresh_token",
          "MENDELEY_REDIRECT_URI": "http://localhost:3333/callback"
        }
      }
    }
  }
}
```

---

## 💬 Ejemplos de uso con Claude

Una vez configurado, puedes pedirle a Claude:

```
"Lista todos mis documentos de Mendeley en la carpeta Tesis"

"Busca en el catálogo artículos sobre machine learning aplicado a salud del 2020 al 2024"

"Analiza mi biblioteca completa y dime cuáles son mis autores más citados"

"Crea una carpeta llamada 'Marco Teórico' y mueve ahí los documentos sobre redes neuronales"

"¿Qué anotaciones tengo en el documento con ID xxx?"

"Agrega un nuevo documento: título 'Deep Learning in Medicine', autores Smith J. y Doe A., año 2023, revista Nature Medicine"
```

---

## 🔄 Flujo de trabajo recomendado para proyectos de investigación

1. **Sube tus PDFs a Mendeley** (desktop o web)
2. **Organiza en carpetas** por tema usando `mendeley_create_folder`
3. **Analiza tu biblioteca** con `mendeley_analyze_library` 
4. **Busca literatura faltante** con `mendeley_search_catalog`
5. **Pide a Claude** que genere marco teórico, estado del arte, fichas bibliográficas

---

## 🛠️ Solución de problemas

**Error 401 (Unauthorized):** El token expiró. Corre `npm run auth` de nuevo.

**Error al abrir navegador:** Copia la URL que aparece en la consola y ábrela manualmente.

**"No se encontró el módulo":** Asegúrate de haber corrido `npm run build`.

---

## 📄 Licencia

MIT — Libre para uso personal y académico.
