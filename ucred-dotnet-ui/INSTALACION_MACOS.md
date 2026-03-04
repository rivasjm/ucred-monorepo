# 🍎 Guía de Instalación - macOS con Apple Silicon

## Requisitos del Sistema

- macOS 11.0 (Big Sur) o superior
- Procesador Apple Silicon (M1, M2, M3, etc.)
- Al menos 2 GB de espacio libre en disco
- Conexión a internet

## Instalación Paso a Paso

### 1. Verificar la Arquitectura del Procesador

Abre Terminal y ejecuta:

```bash
uname -m
```

Si muestra `arm64`, tienes Apple Silicon. Si muestra `x86_64`, tienes Intel.

### 2. Instalar Homebrew (Gestor de Paquetes)

Si no tienes Homebrew instalado:

```bash
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
```

Después de la instalación, sigue las instrucciones en pantalla para añadir Homebrew al PATH.

Verifica la instalación:
```bash
brew --version
```

### 3. Instalar .NET SDK 8.0

#### Opción A: Usando Homebrew (Recomendado)

```bash
# Instalar .NET SDK
brew install --cask dotnet-sdk

# Verificar instalación
dotnet --version
```

#### Opción B: Descarga Manual

1. Ve a https://dotnet.microsoft.com/download/dotnet/8.0
2. Descarga ".NET SDK 8.0.x - macOS Arm64 Installer"
3. Abre el archivo `.pkg` descargado
4. Sigue el asistente de instalación

### 4. Verificar la Instalación de .NET

```bash
# Ver version
dotnet --version

# Ver información completa
dotnet --info

# Ver SDKs instalados
dotnet --list-sdks

# Ver runtimes instalados
dotnet --list-runtimes
```

Deberías ver algo como:
```
8.0.xxx
```

### 5. Instalar Git (Si no lo tienes)

Git suele venir preinstalado en macOS, pero si no:

```bash
# Verificar si está instalado
git --version

# Si no está, instalarlo con Homebrew
brew install git
```

Configurar Git:
```bash
git config --global user.name "Tu Nombre"
git config --global user.email "tu.email@ejemplo.com"
```

### 6. Instalar un Editor de Código (Opcional pero Recomendado)

#### Visual Studio Code

```bash
brew install --cask visual-studio-code
```

O descarga desde: https://code.visualstudio.com/

**Extensiones recomendadas para VS Code:**
- C# Dev Kit (microsoft.csdevkit)
- .NET Extension Pack (ms-dotnettools.vscode-dotnet-pack)
- GitHub Actions (github.vscode-github-actions)

#### Rider (Alternativa de JetBrains)

```bash
brew install --cask rider
```

### 7. Clonar o Navegar al Proyecto

```bash
# Ya deberías estar en:
cd /Users/juan/Developer/teaching/ucred-gh-actions

# Verificar que los archivos están ahí
ls -la
```

## Probar la Instalación

### Paso 1: Restaurar Dependencias

```bash
dotnet restore CalculadoraDemo.sln
```

Esto descargará todos los paquetes de NuGet necesarios, incluyendo Avalonia UI.

### Paso 2: Compilar el Proyecto

```bash
dotnet build CalculadoraDemo.sln
```

Si todo está bien, verás:
```
Build succeeded.
    0 Warning(s)
    0 Error(s)
```

### Paso 3: Ejecutar las Pruebas

```bash
dotnet test CalculadoraDemo.sln
```

Deberías ver:
```
Passed! - Failed: 0, Passed: X, Skipped: 0, Total: X
```

### Paso 4: Ejecutar la Aplicación

```bash
dotnet run --project src/Calculadora/Calculadora.csproj
```

Se abrirá una ventana con la aplicación de calculadora. Deberías ver:
- Un display grande en la parte superior
- Botones numéricos (0-9)
- Botones de operaciones (+, -, ×, /)
- Botón C para limpiar
- Botón = para calcular

## Solución de Problemas Comunes

### Problema 1: "command not found: dotnet"

**Causa**: .NET SDK no está en el PATH

**Solución**:
```bash
# Añadir al PATH manualmente
echo 'export PATH="$PATH:/usr/local/share/dotnet"' >> ~/.zshrc
source ~/.zshrc
```

### Problema 2: Error de arquitectura ARM64/x86_64

**Causa**: Versión incorrecta de .NET SDK instalada

**Solución**: Asegúrate de instalar la versión ARM64 (Apple Silicon)

```bash
# Desinstalar versión incorrecta
brew uninstall dotnet-sdk

# Reinstalar versión correcta
brew install --cask dotnet-sdk
```

### Problema 3: "No se puede verificar el desarrollador"

**Causa**: macOS bloquea software de desarrolladores no verificados

**Solución**:
1. Ve a Preferencias del Sistema > Privacidad y Seguridad
2. En la pestaña "General", haz clic en "Permitir de todas formas"
3. O ejecuta:
```bash
xattr -r -d com.apple.quarantine /usr/local/share/dotnet
```

### Problema 4: Permisos denegados

**Solución**:
```bash
# Dar permisos de ejecución
chmod +x /usr/local/share/dotnet/dotnet
```

### Problema 5: NuGet no puede descargar paquetes

**Causa**: Problemas de red o certificados

**Solución**:
```bash
# Limpiar caché de NuGet
dotnet nuget locals all --clear

# Intentar de nuevo
dotnet restore
```

## Comandos Útiles para el Curso

### Limpiar el Proyecto

```bash
dotnet clean
```

### Ver la Estructura del Proyecto

```bash
tree -L 3
# O si no tienes tree:
find . -type d -maxdepth 3
```

### Verificar Dependencias de un Proyecto

```bash
dotnet list package
```

### Actualizar Paquetes NuGet

```bash
dotnet list package --outdated
dotnet add package <nombre-paquete> --version <version>
```

### Crear un Nuevo Proyecto .NET

```bash
# Ver plantillas disponibles
dotnet new list

# Crear proyecto de consola
dotnet new console -n MiProyecto

# Crear proyecto de pruebas
dotnet new xunit -n MiProyecto.Tests
```

## Herramientas Adicionales Útiles

### act - Ejecutar GitHub Actions Localmente

```bash
# Instalar act
brew install act

# Listar workflows
act -l

# Ejecutar workflow de push localmente
act push

# Ejecutar trabajo específico
act -j build-and-test
```

### GitHub CLI

```bash
# Instalar GitHub CLI
brew install gh

# Autenticarse
gh auth login

# Ver estado de workflows
gh run list

# Ver detalles de un run
gh run view
```

### dotnet-format (Para análisis de código)

```bash
# Instalar como herramienta global
dotnet tool install -g dotnet-format

# Usar en el proyecto
dotnet format CalculadoraDemo.sln
```

## Verificación Final

Ejecuta este script para verificar que todo está instalado correctamente:

```bash
#!/bin/bash

echo "🔍 Verificando instalación..."
echo ""

echo "✅ Arquitectura del sistema:"
uname -m
echo ""

echo "✅ Versión de macOS:"
sw_vers -productVersion
echo ""

echo "✅ Homebrew:"
brew --version | head -n 1
echo ""

echo "✅ Git:"
git --version
echo ""

echo "✅ .NET SDK:"
dotnet --version
echo ""

echo "✅ Compilando proyecto:"
dotnet build CalculadoraDemo.sln --verbosity quiet
if [ $? eq 0 ]; then
    echo "Compilación exitosa!"
else
    echo "❌ Error en la compilación"
fi
echo ""

echo "✅ Ejecutando pruebas:"
dotnet test CalculadoraDemo.sln --verbosity quiet
if [ $? -eq 0 ]; then
    echo "Todas las pruebas pasaron!"
else
    echo "❌ Algunas pruebas fallaron"
fi
echo ""

echo "🎉 ¡Todo listo! Puedes empezar a trabajar."
```

Guarda esto en un archivo `verificar.sh`, dale permisos y ejecútalo:

```bash
chmod +x verificar.sh
./verificar.sh
```

## Recursos de Ayuda

- **Documentación oficial de .NET**: https://learn.microsoft.com/dotnet/
- **Homebrew**: https://brew.sh/
- **GitHub Actions**: https://docs.github.com/actions
- **Comunidad de .NET**: https://dotnet.microsoft.com/platform/community

## Próximos Pasos

Una vez que todo está instalado:

1. ✅ Ejecuta la aplicación: `dotnet run --project src/Calculadora/Calculadora.csproj`
2. ✅ Juega con la calculadora
3. ✅ Revisa el código fuente
4. ✅ Estudia las pruebas unitarias
5. ✅ Lee el archivo GITHUB_ACTIONS_GUIDE.md
6. ✅ Prepárate para subir el proyecto a GitHub

---

¿Problemas? Revisa la sección de troubleshooting o consulta con el instructor.
