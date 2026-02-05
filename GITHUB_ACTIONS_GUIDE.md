# 📘 Guía de GitHub Actions para Estudiantes

## ¿Qué es GitHub Actions?

GitHub Actions es una plataforma de CI/CD (Integración Continua y Entrega Continua) que permite automatizar tareas de desarrollo de software directamente desde tu repositorio de GitHub.

## Conceptos Fundamentales

### 1. Workflow (Flujo de Trabajo)

Un archivo YAML que define qué acciones se ejecutarán automáticamente.

**Ubicación**: `.github/workflows/nombre-workflow.yml`

```yaml
name: Mi Primer Workflow
on: push
jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - name: Mensaje de prueba
        run: echo "¡Hola desde GitHub Actions!"
```

### 2. Events (Eventos)

Eventos que disparan el workflow:

```yaml
on:
  push:                    # Cuando se hace push
    branches: [ main ]     # Solo en la rama main
  pull_request:            # Cuando se crea un PR
  schedule:                # Programado
    - cron: '0 0 * * *'   # Todos los días a medianoche
  workflow_dispatch:       # Ejecución manual
```

### 3. Jobs (Trabajos)

Conjunto de pasos que se ejecutan en el mismo runner:

```yaml
jobs:
  test:
    name: Ejecutar Pruebas
    runs-on: ubuntu-latest
    steps:
      - name: Checkout
        uses: actions/checkout@v4
      - name: Run tests
        run: dotnet test
```

### 4. Steps (Pasos)

Acciones individuales dentro de un job:

```yaml
steps:
  # Paso usando una acción predefinida
  - name: Setup .NET
    uses: actions/setup-dotnet@v4
    with:
      dotnet-version: '8.0.x'
  
  # Paso ejecutando comandos
  - name: Build
    run: dotnet build
```

### 5. Runners

Máquinas virtuales donde se ejecutan los workflows:

- `ubuntu-latest` - Ubuntu Linux (más común)
- `windows-latest` - Windows Server
- `macos-latest` - macOS

## Anatomía del Workflow de Este Proyecto

### Workflow Simple (`simple.yml`)

```yaml
name: Build Simple              # 1. Nombre del workflow

on:                             # 2. Cuándo se ejecuta
  push:
    branches: [ main ]
  pull_request:
    branches: [ main ]

jobs:                           # 3. Trabajos a ejecutar
  build:                        # 4. Nombre del job
    name: Compilar y Probar
    runs-on: ubuntu-latest      # 5. Sistema operativo
    
    steps:                      # 6. Pasos del job
    - name: Descargar código    # 7. Nombre del paso
      uses: actions/checkout@v4 # 8. Acción a usar
    
    - name: Instalar .NET
      uses: actions/setup-dotnet@v4
      with:                     # 9. Parámetros de la acción
        dotnet-version: '8.0.x'
    
    - name: Compilar proyecto
      run: dotnet build         # 10. Comando a ejecutar
```

### Workflow Avanzado (`ci-cd.yml`)

#### Variables de Entorno

```yaml
env:
  DOTNET_VERSION: '8.0.x'
  PROJECT_NAME: 'Calculadora'
```

Uso en los pasos:
```yaml
- name: Configurar .NET ${{ env.DOTNET_VERSION }}
  uses: actions/setup-dotnet@v4
  with:
    dotnet-version: ${{ env.DOTNET_VERSION }}
```

#### Jobs Paralelos

```yaml
jobs:
  build-and-test:     # Se ejecuta en paralelo con...
    runs-on: ubuntu-latest
    steps: [...]
  
  code-analysis:      # ...este job
    runs-on: ubuntu-latest
    steps: [...]
```

#### Matrix Strategy (Estrategia de Matriz)

Ejecuta el mismo job en múltiples configuraciones:

```yaml
strategy:
  matrix:
    os: [ubuntu-latest, windows-latest, macos-latest]
    dotnet-version: ['8.0.x']

runs-on: ${{ matrix.os }}
```

Esto crea 3 jobs paralelos:
- Ubuntu + .NET 8.0
- Windows + .NET 8.0
- macOS + .NET 8.0

#### Artefactos (Artifacts)

Guardar archivos generados:

```yaml
- name: Publicar aplicación
  run: dotnet publish --output ./publish

- name: Subir artefactos
  uses: actions/upload-artifact@v4
  with:
    name: calculadora-app
    path: ./publish
    retention-days: 7
```

#### Condicionales

```yaml
- name: Publicar resultados
  if: always()          # Ejecutar siempre
  uses: dorny/test-reporter@v1

- name: Deploy
  if: success()         # Solo si los pasos anteriores tuvieron éxito
  run: echo "Deploying..."
```

## Acciones Comunes Útiles

### 1. Checkout del Código

```yaml
- uses: actions/checkout@v4
```

### 2. Setup de Lenguajes

```yaml
# .NET
- uses: actions/setup-dotnet@v4
  with:
    dotnet-version: '8.0.x'

# Node.js
- uses: actions/setup-node@v4
  with:
    node-version: '20'

# Python
- uses: actions/setup-python@v5
  with:
    python-version: '3.11'
```

### 3. Cache de Dependencias

```yaml
- uses: actions/cache@v4
  with:
    path: ~/.nuget/packages
    key: ${{ runner.os }}-nuget-${{ hashFiles('**/*.csproj') }}
    restore-keys: |
      ${{ runner.os }}-nuget-
```

### 4. Subir Artefactos

```yaml
- uses: actions/upload-artifact@v4
  with:
    name: mi-artefacto
    path: ./build
```

### 5. Descargar Artefactos

```yaml
- uses: actions/download-artifact@v4
  with:
    name: mi-artefacto
    path: ./download
```

## Secretos y Variables

### Variables de Entorno

```yaml
env:
  MI_VARIABLE: "valor"

steps:
  - name: Usar variable
    run: echo $MI_VARIABLE
```

### Secretos

Configurar en GitHub: Settings > Secrets and variables > Actions

```yaml
steps:
  - name: Usar secreto
    run: echo ${{ secrets.MI_SECRETO }}
    env:
      API_KEY: ${{ secrets.API_KEY }}
```

## Ejercicios Prácticos

### Ejercicio 1: Modificar el Workflow Simple

1. Añade un paso que muestre la fecha y hora
2. Añade un paso que liste los archivos del proyecto

```yaml
- name: Mostrar fecha
  run: date

- name: Listar archivos
  run: ls -la
```

### Ejercicio 2: Crear un Workflow de Release

Crea `.github/workflows/release.yml`:

```yaml
name: Release

on:
  push:
    tags:
      - 'v*'

jobs:
  release:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '8.0.x'
      - name: Publish
        run: dotnet publish -c Release -o ./release
      - name: Create Release
        uses: actions/create-release@v1
        env:
          GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
        with:
          tag_name: ${{ github.ref }}
          release_name: Release ${{ github.ref }}
```

### Ejercicio 3: Añadir Notificaciones

Añade un paso de notificación cuando las pruebas fallen:

```yaml
- name: Notificar fallo
  if: failure()
  run: echo "❌ Las pruebas han fallado!"
```

### Ejercicio 4: Badge de Estado

Añade un badge al README.md:

```markdown
![Build Status](https://github.com/USUARIO/REPO/workflows/CI%2FCD%20Pipeline/badge.svg)
```

## Debugging de Workflows

### Ver Logs Detallados

```yaml
- name: Debug
  run: |
    echo "Runner OS: ${{ runner.os }}"
    echo "GitHub Ref: ${{ github.ref }}"
    echo "GitHub SHA: ${{ github.sha }}"
    env
```

### Usar tmate para Debugging Interactivo

```yaml
- name: Setup tmate session
  if: failure()
  uses: mxschmitt/action-tmate@v3
```

## Mejores Prácticas

### 1. Nombrar Claramente

```yaml
# ❌ Malo
- run: dotnet build

# ✅ Bueno
- name: Compilar proyecto en modo Release
  run: dotnet build --configuration Release
```

### 2. Usar Versiones Específicas de Acciones

```yaml
# ❌ Malo (puede romperse con actualizaciones)
- uses: actions/checkout@main

# ✅ Bueno
- uses: actions/checkout@v4
```

### 3. Minimizar el Tiempo de Ejecución

```yaml
# Usar --no-restore y --no-build cuando sea apropiado
- run: dotnet restore
- run: dotnet build --no-restore
- run: dotnet test --no-build
```

### 4. Mantener los Secretos Seguros

```yaml
# ❌ NUNCA hagas esto
- run: echo "Mi contraseña es password123"

# ✅ Usa secretos
- run: echo "Usando contraseña segura"
  env:
    PASSWORD: ${{ secrets.PASSWORD }}
```

## Recursos Adicionales

- 📚 [Documentación oficial de GitHub Actions](https://docs.github.com/actions)
- 🎯 [Marketplace de GitHub Actions](https://github.com/marketplace?type=actions)
- 💡 [Awesome Actions](https://github.com/sdras/awesome-actions)
- 🔧 [Sintaxis de Workflow](https://docs.github.com/en/actions/reference/workflow-syntax-for-github-actions)

## Preguntas Frecuentes

**P: ¿Cuánto cuesta GitHub Actions?**  
R: Es gratis para repositorios públicos. Para privados, tienes 2000 minutos/mes gratis.

**P: ¿Puedo ejecutar workflows localmente?**  
R: Sí, usando herramientas como [act](https://github.com/nektos/act)

**P: ¿Los workflows pueden modificar archivos del repositorio?**  
R: Sí, pero necesitas hacer commit y push de los cambios.

**P: ¿Puedo cancelar un workflow en ejecución?**  
R: Sí, desde la pestaña Actions en GitHub.

---

¡Experimenta y aprende! La mejor manera de dominar GitHub Actions es practicando. 🚀
