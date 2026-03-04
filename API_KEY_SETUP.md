# Configuración de API Key de TMDb

Esta aplicación requiere una clave API de The Movie Database (TMDb) para funcionar. Sigue estos pasos para obtener tu clave gratuita.

## 📝 Paso 1: Crear una cuenta

1. Ve a https://www.themoviedb.org/
2. Haz clic en "Join TMDb" (Unirse)
3. Completa el formulario de registro con tu email
4. Verifica tu email haciendo clic en el enlace de confirmación

## 🔑 Paso 2: Solicitar API Key

1. Inicia sesión en tu cuenta de TMDb
2. Haz clic en tu avatar (esquina superior derecha)
3. Selecciona **Settings** (Configuración)
4. En el menú lateral, selecciona **API**
5. Haz clic en "Request an API Key" (Solicitar una clave API)
6. Selecciona la opción **Developer** (uso no comercial)
7. Acepta los términos de uso
8. Completa el formulario:
   - **Application Name**: UCRED Movies Browser (o el nombre que prefieras)
   - **Application URL**: http://localhost (para uso personal)
   - **Application Summary**: Movie browser desktop application for educational purposes
9. Haz clic en **Submit**

## ✅ Paso 3: Copiar tu API Key

Una vez aprobada (es inmediato), verás dos claves:

- **API Key (v3 auth)** ← Esta es la que necesitas
- **API Read Access Token (v4 auth)** ← No uses esta

Copia la **API Key (v3 auth)**, que tiene un formato similar a:

```
abc123def456789012345678901234567890abcd
```

## 🖥️ Paso 4: Configurar la variable de entorno

### Linux / macOS

#### Opción 1: Variable temporal (válida solo en la sesión actual)

```bash
export TMDB_API_KEY="tu_api_key_aqui"
java -jar ucred-java-ui-movies-1.0.0.jar
```

#### Opción 2: Variable permanente

Añade al final de tu archivo `~/.bashrc` o `~/.zshrc`:

```bash
echo 'export TMDB_API_KEY="tu_api_key_aqui"' >> ~/.bashrc
source ~/.bashrc
```

O para Zsh:

```bash
echo 'export TMDB_API_KEY="tu_api_key_aqui"' >> ~/.zshrc
source ~/.zshrc
```

### Windows

#### Opción 1: Variable temporal (PowerShell)

```powershell
$env:TMDB_API_KEY="tu_api_key_aqui"
java -jar ucred-java-ui-movies-1.0.0.jar
```

#### Opción 2: Variable temporal (CMD)

```cmd
set TMDB_API_KEY=tu_api_key_aqui
java -jar ucred-java-ui-movies-1.0.0.jar
```

#### Opción 3: Variable permanente (toda la máquina)

1. Abre **Panel de Control** → **Sistema** → **Configuración avanzada del sistema**
2. Haz clic en **Variables de entorno**
3. En "Variables de usuario", haz clic en **Nueva**
4. Nombre: `TMDB_API_KEY`
5. Valor: tu_api_key_aqui
6. Haz clic en **Aceptar** en todas las ventanas
7. **Reinicia** tus aplicaciones para que detecten la nueva variable

#### Opción 4: Variable permanente (PowerShell, solo usuario actual)

```powershell
[System.Environment]::SetEnvironmentVariable('TMDB_API_KEY', 'tu_api_key_aqui', 'User')
```

*Nota: Necesitarás reiniciar PowerShell para que tome efecto*

## ✅ Verificar configuración

Para verificar que la variable está configurada correctamente:

### Linux / macOS

```bash
echo $TMDB_API_KEY
```

### Windows PowerShell

```powershell
echo $env:TMDB_API_KEY
```

### Windows CMD

```cmd
echo %TMDB_API_KEY%
```

Deberías ver tu API key impresa en la consola.

## 🚀 Ejecutar la aplicación

Una vez configurada la variable de entorno, simplemente ejecuta la aplicación:

```bash
java -jar ucred-java-ui-movies-1.0.0.jar
```

O usa el instalador nativo que configurará todo automáticamente (la variable de entorno seguirá siendo necesaria).

## ⚠️ Problemas comunes

### Error: "TMDB_API_KEY environment variable not set"

La aplicación no encuentra la variable de entorno. Verifica que:

1. Has configurado la variable correctamente (revisa los pasos anteriores)
2. Has reiniciado tu terminal/PowerShell después de configurar la variable
3. El nombre de la variable es exactamente `TMDB_API_KEY` (mayúsculas)

### Error: "API request failed: 401"

Tu API key es inválida. Verifica que:

1. Has copiado la API Key (v3 auth) correcta desde TMDb
2. No has copiado espacios adicionales al principio o final
3. La clave no ha expirado (poco probable, suelen ser permanentes)

### Error: "API request failed: 404"

Problema de conectividad o endpoint. Verifica que:

1. Tienes conexión a internet
2. TMDb API está operativo: https://www.themoviedb.org/

## 📚 Más información

- [Documentación oficial de TMDb API](https://developer.themoviedb.org/docs)
- [Términos de uso de TMDb API](https://www.themoviedb.org/documentation/api/terms-of-use)

---

**Nota de seguridad**: Nunca compartas tu API key públicamente ni la subas a repositorios Git. Es personal y te identifica en las llamadas a la API.
