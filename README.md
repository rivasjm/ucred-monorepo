# 🧮 Calculadora Demo - GitHub Actions

[![CI/CD Pipeline](https://github.com/rivasjm/ucred-dotnet-ui/actions/workflows/ci-cd.yml/badge.svg)](https://github.com/rivasjm/ucred-dotnet-ui/actions/workflows/ci-cd.yml)
[![Build Simple](https://github.com/rivasjm/ucred-dotnet-ui/actions/workflows/simple.yml/badge.svg)](https://github.com/rivasjm/ucred-dotnet-ui/actions/workflows/simple.yml)
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/License-Educational-green.svg)](LICENSE)

Proyecto educativo para aprender GitHub Actions con .NET 10

## 📋 Descripción

Este es un proyecto simple en C# que implementa una calculadora con **interfaz gráfica** usando Avalonia UI. El proyecto incluye:

- ✅ Aplicación de escritorio multiplataforma (Windows, macOS, Linux)
- ✅ Interfaz gráfica moderna y amigable
- ✅ Pruebas unitarias con xUnit
- ✅ GitHub Actions para CI/CD
- ✅ Ejemplos de workflows simples y avanzados

### 🖥️ Tecnologías Utilizadas

- **.NET 10.0**: Framework principal
- **Avalonia UI 11.3**: Framework multiplataforma para interfaces gráficas
- **MVVM Pattern**: Arquitectura Model-View-ViewModel
- **xUnit**: Framework de pruebas unitarias
- **CommunityToolkit.Mvvm**: Helpers para MVVM

## 🎯 Objetivo Educativo

El objetivo de este proyecto es enseñar:

1. Cómo estructurar un proyecto .NET con interfaz gráfica
2. Cómo usar Avalonia UI para crear aplicaciones multiplataforma
3. Cómo implementar el patrón MVVM
4. Cómo escribir y ejecutar pruebas unitarias
3. Cómo configurar GitHub Actions para automatizar:
   - Compilación del código
   - Ejecución de pruebas
   - Análisis de código
   - Pruebas multiplataforma

## 🛠️ Requisitos de Instalación (macOS con Apple Silicon)

### 1. Instalar .NET SDK 8.0

```bash
# Opción 1: Usando Homebrew (recomendado)
brew install --cask dotnet-sdk
10.0
# Descarga el instalador para macOS ARM64
```

### 2. Instalar Plantillas de Avalonia (Opcional)

Si quieres crear nuevos proyectos Avalonia:

```bash
dotnet new install Avalonia.Templates
# Visita: https://dotnet.microsoft.com/download/dotnet/8.0
# Descarga el instalador para macOS ARM64
```

### 2. Verificar la Instalación

```bash
dotnet --version
# Debería mostrar 8.0.x o superior

dotnet --info
# Muestra información completa del SDK instalado
```

## 🚀 Cómo Ejecutar el Proyecto

### Compilar la Solución

```

Se abrirá una ventana con la calculadora gráfica.bash
# Desde el directorio raíz del proyecto
dotnet restore
dotnet build
```

### Ejecutar la Aplicación

```bash
dotnet run --project src/Calculadora/Calculadora.csproj
```

### Ejecutar las Pruebas

```bash
# Ejecutar todas las pruebas
dotnet test

# Ejecutar con más detalles
dotnet test --verbosity detailed

# Ejecutar con cobertura de código
dotnet test --collect:"XPlat Code Coverage"
```

## 📁 EstrucModels/
│       │   └── CalculadoraSimple.cs    # Lógica de la calculadora
│       ├── ViewModels/
│       │   ├── MainWindowViewModel.cs  # ViewModel con lógica de UI
│       │   └── ViewModelBase.cs        # Clase base para ViewModels
│       ├── Views/
│       │   ├── MainWindow.axaml        # Interfaz gráfica (XAML)
│       │   └── MainWindow.axaml.cs     # Code-behind
│       ├── App.axaml                   # Configuración de la app
│       ├── App.axaml.cs
│       ├── Program.cs                  # Punto de entrada
│       └── Calculadora.csproj
ucred-gh-actions/
├── .github/
│   └── workflows/
│       ├── ci-cd.yml          # Workflow completo y avanzado
│       └── simple.yml         # Workflow simple para principiantes
├── src/
│   └── Calculadora/
│       ├── Calculadora.csproj
│       ├── Program.cs         # Punto de entrada de la aplicación
│       └── CalculadoraSimple.cs    # Lógica de la calculadora
├── tests/
│   └── Calculadora.Tests/
│       ├── Calculadora.Tests.csproj
│       ├── CalculadoraSimpleTests.cs    # Pruebas unitarias
│       └── Usings.cs
├── .gitignore
├── CalculadoraDemo.sln
└── README.md
```

## 🔄 GitHub Actions Workflows

### Workflow Simple (`simple.yml`)

El workflow más básico que:
- Se ejecuta en cada push/pull request
- Compila el proyecto
- Ejecuta las pruebas

**Ideal para empezar a aprender GitHub Actions**

### Workflow Completo (`ci-cd.yml`)

Un workflow más avanzado que incluye:
- ✅ Compilación y pruebas
- ✅ Análisis de código con dotnet format
- ✅ Pruebas en múltiples plataformas (Ubuntu, Windows, macOS)
- ✅ Generación de reportes de pruebas
- ✅ Creación de artefactos (ejecutables)

### Workflow de Release (`release.yml`)

Un workflow automatizado para crear releases multiplataforma:
- 🚀 Se activa al crear tags con formato `v*.*.*` (ej: `v1.0.0`)
- 🪟 Compila para Windows x64
- 🍎 Compila para macOS (Intel y Apple Silicon)
- 🐧 Compila para Linux x64
- 📦 Empaqueta y publica automáticamente en GitHub Releases

**Ver guía completa**: [RELEASES.md](RELEASES.md)

**Ejemplo rápido:**
```bash
# Crear y publicar un release
git tag -a v1.0.0 -m "Release v1.0.0: Primera versión"
git push origin v1.0.0
# El workflow se ejecuta automáticamente
```

## 📚 Conceptos de GitHub Actions Este Proyecto

### 1. **Triggers (on)**
- `push`: Se ejecuta cuando se hace push
- `pull_request`: Se ejecuta en pull requests
- `workflow_dispatch`: Permite ejecución manual

### 2. **Jobs**
Tareas independientes que pueden ejecutarse en paralelo:
- `build-and-test`: Compilación y pruebas
- `code-analysis`: Análisis de código
- `multi-platform-test`: Pruebas multiplataforma

### 3. **Steps**
Pasos individuales dentro de un job:
- `uses`: Usa una acción predefinida
- `run`: Ejecuta comandos

### 4. **Matrix Strategy**
Ejecuta el mismo job en múltiples configuraciones:
```yaml
strategy:
  matrix:
    os: [ubuntu-latest, windows-latest, macos-latest]
```

### 5. **Artifacts**
Archivos generados que se pueden descargar:
- Ejecutables compilados
- Reportes de pruebas

## 🧪 Pruebas Incluidas

El proyecto incluye pruebas para:
- ✅ Suma de números positivos y negativos
- ✅ Resta
- ✅ Multiplicación (incluyendo por cero)
- ✅ División
- ✅ División por cero (validación de excepciones)
- ✅ Pruebas parametrizadas con múltiples ejemplos

## 📖 Cómo Usar Este Proyecto para Enseñar

### Paso 1: Ejecutar Localmente
1. Instala .NET SDK
2. Compila y ejecuta el proyecto
3. Ejecuta las pruebas

### Paso 2: Subir a GitHub
```bash
git init
git add .
git commit -m "Initial commit"
git branch -M main
git remote add origin <tu-repositorio>
git push -u origin main
```

### Paso 3: Ver GitHub Actions en Acción
1. Ve a la pestaña "Actions" en GitHub
2. Observa cómo se ejecutan los workflows
3. Revisa los logs y resultados

### Paso 4: Experimentar
- Modifica el código y haz push
- Rompe una prueba intencionalmente
- Añade nuevas funcionalidades
- Crea nuevos workflows

## 🎓 Ejercicios Propuestos para Estudiantes

1. **Básico**: Añadir una operación de potencia a la calculadora
2. **Intermedio**: Crear un tercer workflow que solo se ejecute en releases
3. **Avanzado**: Añadir un paso de deployment automático
4. **Desafío**: Implementar badges de estado en el README

## 📝 Notas Adicionales

### Comandos Útiles de .NET

```bash
# Limpiar la solución
dotnet clean

# Ver proyectos en la solución
dotnet sln list

# Añadir un paquete NuGet
dotnet add package <nombre-paquete>

# Crear un nuevo proyecto de pruebas
dotnet new xunit -n MiProyecto.Tests
```

### Recursos de Aprendizaje

- [Documentación oficial de .NET](https://learn.microsoft.com/dotnet/)
- [GitHub Actions Documentation](https://docs.github.com/actions)
- [xUnit Documentation](https://xunit.net/)

## 🤝 Contribuciones

Este es un proyecto educativo. Las contribuciones son bienvenidas para:
- Mejorar la documentación
- Añadir más ejemplos de workflows
- Corregir errores
- Añadir más pruebas

## 📄 Licencia

Este proyecto es de código abierto y está disponible para fines educativos.

---

**Autor**: Proyecto educativo para UCRED  
**Fecha**: Febrero 2026
