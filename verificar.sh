#!/bin/bash

# Script de verificación para el proyecto Calculadora Demo
# Verifica que todo esté instalado y funcionando correctamente

echo "╔════════════════════════════════════════════════════════════╗"
echo "║  Verificación del Entorno - Calculadora Demo             ║"
echo "╚════════════════════════════════════════════════════════════╝"
echo ""

# Colores para output
GREEN='\033[0;32m'
RED='\033[0;31m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Función para verificar comandos
check_command() {
    if command -v $1 &> /dev/null; then
        echo -e "${GREEN}✅ $2 instalado${NC}"
        if [ ! -z "$3" ]; then
            echo "   Versión: $($3)"
        fi
        return 0
    else
        echo -e "${RED}❌ $2 NO encontrado${NC}"
        return 1
    fi
}

# Verificar sistema
echo "📋 Información del Sistema:"
echo "   OS: $(uname -s)"
echo "   Arquitectura: $(uname -m)"
echo "   macOS: $(sw_vers -productVersion)"
echo ""

# Verificar herramientas
echo "🔧 Herramientas Básicas:"
check_command "git" "Git" "git --version"
check_command "brew" "Homebrew" "brew --version | head -n 1"
echo ""

# Verificar .NET
echo "🔵 .NET SDK:"
if check_command "dotnet" ".NET SDK" "dotnet --version"; then
    echo ""
    echo "   SDKs instalados:"
    dotnet --list-sdks | sed 's/^/   /'
    echo ""
    echo "   Runtimes instalados:"
    dotnet --list-runtimes | sed 's/^/   /'
else
    echo -e "${YELLOW}⚠️  Instala .NET SDK con: brew install --cask dotnet-sdk${NC}"
fi
echo ""

# Verificar estructura del proyecto
echo "📁 Estructura del Proyecto:"
if [ -f "CalculadoraDemo.sln" ]; then
    echo -e "${GREEN}✅ Solución encontrada${NC}"
else
    echo -e "${RED}❌ CalculadoraDemo.sln no encontrado${NC}"
    exit 1
fi

if [ -f "src/Calculadora/Calculadora.csproj" ]; then
    echo -e "${GREEN}✅ Proyecto principal encontrado${NC}"
else
    echo -e "${RED}❌ Proyecto principal no encontrado${NC}"
    exit 1
fi

if [ -f "tests/Calculadora.Tests/Calculadora.Tests.csproj" ]; then
    echo -e "${GREEN}✅ Proyecto de pruebas encontrado${NC}"
else
    echo -e "${RED}❌ Proyecto de pruebas no encontrado${NC}"
    exit 1
fi
echo ""

# Verificar workflows
echo "⚙️  GitHub Actions Workflows:"
if [ -f ".github/workflows/simple.yml" ]; then
    echo -e "${GREEN}✅ simple.yml encontrado${NC}"
else
    echo -e "${RED}❌ simple.yml no encontrado${NC}"
fi

if [ -f ".github/workflows/ci-cd.yml" ]; then
    echo -e "${GREEN}✅ ci-cd.yml encontrado${NC}"
else
    echo -e "${RED}❌ ci-cd.yml no encontrado${NC}"
fi
echo ""

# Restaurar dependencias
echo "📦 Restaurando Dependencias..."
if dotnet restore CalculadoraDemo.sln --verbosity quiet > /dev/null 2>&1; then
    echo -e "${GREEN}✅ Dependencias restauradas${NC}"
else
    echo -e "${RED}❌ Error al restaurar dependencias${NC}"
    exit 1
fi
echo ""

# Compilar proyecto
echo "🔨 Compilando Proyecto..."
if dotnet build CalculadoraDemo.sln --configuration Release --verbosity quiet > /dev/null 2>&1; then
    echo -e "${GREEN}✅ Compilación exitosa${NC}"
else
    echo -e "${RED}❌ Error en la compilación${NC}"
    echo ""
    echo "Detalles del error:"
    dotnet build CalculadoraDemo.sln --configuration Release
    exit 1
fi
echo ""

# Ejecutar pruebas
echo "🧪 Ejecutando Pruebas..."
TEST_OUTPUT=$(dotnet test CalculadoraDemo.sln --configuration Release --verbosity quiet --logger "console;verbosity=minimal" 2>&1)
TEST_EXIT_CODE=$?

if [ $TEST_EXIT_CODE -eq 0 ]; then
    echo -e "${GREEN}✅ Todas las pruebas pasaron${NC}"
    echo "$TEST_OUTPUT" | grep "Passed"
else
    echo -e "${RED}❌ Algunas pruebas fallaron${NC}"
    echo "$TEST_OUTPUT"
    exit 1
fi
echo ""

# Verificar herramientas opcionales
echo "🛠️  Herramientas Opcionales:"
check_command "code" "Visual Studio Code" "code --version | head -n 1"
check_command "gh" "GitHub CLI" "gh --version | head -n 1"
check_command "act" "act (GitHub Actions local)" "act --version"
echo ""

# Resumen final
echo "╔════════════════════════════════════════════════════════════╗"
echo "║                    RESUMEN FINAL                          ║"
echo "╚════════════════════════════════════════════════════════════╝"
echo ""
echo -e "${GREEN}✅ ¡Todo está listo!${NC}"
echo ""
echo "Próximos pasos:"
echo "1. Ejecutar la aplicación:"
echo "   ${YELLOW}dotnet run --project src/Calculadora/Calculadora.csproj${NC}"
echo ""
echo "2. Ejecutar pruebas con detalles:"
echo "   ${YELLOW}dotnet test --verbosity detailed${NC}"
echo ""
echo "3. Revisar la documentación:"
echo "   - README.md - Visión general del proyecto"
echo "   - INSTALACION_MACOS.md - Guía de instalación"
echo "   - GITHUB_ACTIONS_GUIDE.md - Guía de GitHub Actions"
echo "   - EJERCICIOS.md - Ejercicios para estudiantes"
echo "   - GUIA_INSTRUCTOR.md - Guía para el instructor"
echo ""
echo "4. Subir a GitHub (cuando estés listo):"
echo "   ${YELLOW}git init${NC}"
echo "   ${YELLOW}git add .${NC}"
echo "   ${YELLOW}git commit -m \"Initial commit: Calculadora con GitHub Actions\"${NC}"
echo "   ${YELLOW}git branch -M main${NC}"
echo "   ${YELLOW}git remote add origin <tu-repositorio>${NC}"
echo "   ${YELLOW}git push -u origin main${NC}"
echo ""
echo "🎉 ¡Feliz aprendizaje!"
