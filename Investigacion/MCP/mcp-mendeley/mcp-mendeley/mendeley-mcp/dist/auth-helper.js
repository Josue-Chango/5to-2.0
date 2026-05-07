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
/**
 * auth-helper.ts
 * Corre este script UNA VEZ para obtener tus tokens de Mendeley.
 * Uso: npm run auth
 */
const express_1 = __importDefault(require("express"));
const axios_1 = __importDefault(require("axios"));
const fs = __importStar(require("fs"));
const path = __importStar(require("path"));
const dotenv = __importStar(require("dotenv"));
dotenv.config();
const PORT = 3333;
const CLIENT_ID = process.env.MENDELEY_CLIENT_ID;
const CLIENT_SECRET = process.env.MENDELEY_CLIENT_SECRET;
const REDIRECT_URI = `http://localhost:${PORT}/callback`;
const AUTH_URL = "https://api.mendeley.com/oauth/authorize";
const TOKEN_URL = "https://api.mendeley.com/oauth/token";
const ENV_PATH = path.join(__dirname, "../.env");
async function startAuthFlow() {
    if (!CLIENT_ID || !CLIENT_SECRET) {
        console.error("❌ ERROR: Faltan MENDELEY_CLIENT_ID y MENDELEY_CLIENT_SECRET en .env");
        console.log("\n📋 Pasos:");
        console.log("1. Ve a https://dev.mendeley.com/myapps.html");
        console.log("2. Crea una aplicación nueva");
        console.log(`3. Pon como Redirect URI: ${REDIRECT_URI}`);
        console.log("4. Copia el Client ID y Secret en tu archivo .env");
        process.exit(1);
    }
    const app = (0, express_1.default)();
    const authUrl = `${AUTH_URL}?client_id=${CLIENT_ID}` +
        `&redirect_uri=${encodeURIComponent(REDIRECT_URI)}` +
        `&response_type=code` +
        `&scope=all`;
    console.log("\n🔐 AUTENTICACIÓN CON MENDELEY");
    console.log("═══════════════════════════════════════");
    console.log("Abre esta URL en tu navegador:\n");
    console.log(authUrl);
    console.log("\nEsperando autorización...");
    // Intentar abrir el navegador automáticamente
    try {
        const { default: open } = await Promise.resolve().then(() => __importStar(require("open")));
        await open(authUrl);
    }
    catch {
        console.log("(Abre la URL manualmente en tu navegador)");
    }
    const server = app.listen(PORT, () => {
        console.log(`\n🌐 Servidor de callback en http://localhost:${PORT}`);
    });
    app.get("/callback", async (req, res) => {
        const code = req.query.code;
        if (!code) {
            res.send("❌ Error: no se recibió el código de autorización");
            server.close();
            return;
        }
        try {
            const credentials = Buffer.from(`${CLIENT_ID}:${CLIENT_SECRET}`).toString("base64");
            const params = new URLSearchParams({
                grant_type: "authorization_code",
                code,
                redirect_uri: REDIRECT_URI,
            });
            const response = await axios_1.default.post(TOKEN_URL, params.toString(), {
                headers: {
                    Authorization: `Basic ${credentials}`,
                    "Content-Type": "application/x-www-form-urlencoded",
                },
            });
            const { access_token, refresh_token } = response.data;
            // Guardar en .env
            let envContent = "";
            if (fs.existsSync(ENV_PATH)) {
                envContent = fs.readFileSync(ENV_PATH, "utf8");
            }
            const updateOrAdd = (content, key, value) => {
                const regex = new RegExp(`^${key}=.*`, "m");
                if (regex.test(content)) {
                    return content.replace(regex, `${key}=${value}`);
                }
                return content + `\n${key}=${value}`;
            };
            envContent = updateOrAdd(envContent, "MENDELEY_ACCESS_TOKEN", access_token);
            if (refresh_token) {
                envContent = updateOrAdd(envContent, "MENDELEY_REFRESH_TOKEN", refresh_token);
            }
            envContent = updateOrAdd(envContent, "MENDELEY_REDIRECT_URI", REDIRECT_URI);
            fs.writeFileSync(ENV_PATH, envContent);
            console.log("\n✅ ¡AUTENTICACIÓN EXITOSA!");
            console.log("Los tokens han sido guardados en tu archivo .env");
            console.log("\nYa puedes usar el MCP server con: npm start");
            res.send(`
        <html><body style="font-family: sans-serif; text-align: center; padding: 50px;">
          <h1>✅ ¡Autenticación exitosa!</h1>
          <p>Los tokens han sido guardados. Puedes cerrar esta ventana.</p>
          <p><strong>Ya puedes usar tu MCP de Mendeley.</strong></p>
        </body></html>
      `);
            setTimeout(() => {
                server.close();
                process.exit(0);
            }, 2000);
        }
        catch (error) {
            console.error("❌ Error al obtener tokens:", error);
            res.send("❌ Error al obtener tokens. Revisa la consola.");
            server.close();
        }
    });
}
startAuthFlow();
