/**
 * auth-helper.ts
 * Corre este script UNA VEZ para obtener tus tokens de Mendeley.
 * Uso: npm run auth
 */
import express from "express";
import axios from "axios";
import * as fs from "fs";
import * as path from "path";
import * as dotenv from "dotenv";

dotenv.config();

const PORT = 3333;
const CLIENT_ID = process.env.MENDELEY_CLIENT_ID!;
const CLIENT_SECRET = process.env.MENDELEY_CLIENT_SECRET!;
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

  const app = express();

  const authUrl =
    `${AUTH_URL}?client_id=${CLIENT_ID}` +
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
    const { default: open } = await import("open");
    await open(authUrl);
  } catch {
    console.log("(Abre la URL manualmente en tu navegador)");
  }

  const server = app.listen(PORT, () => {
    console.log(`\n🌐 Servidor de callback en http://localhost:${PORT}`);
  });

  app.get("/callback", async (req, res) => {
    const code = req.query.code as string;

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

      const response = await axios.post(TOKEN_URL, params.toString(), {
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

      const updateOrAdd = (content: string, key: string, value: string): string => {
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
    } catch (error) {
      console.error("❌ Error al obtener tokens:", error);
      res.send("❌ Error al obtener tokens. Revisa la consola.");
      server.close();
    }
  });
}

startAuthFlow();
