# 🚀 Guía de Releases

Esta guía explica cómo crear y publicar releases de la aplicación Calculadora.

## 📋 Requisitos Previos

- Código en la rama `main` listo para release
- Todos los tests pasando ✅
- Cambios commiteados y pusheados

## 🎯 Crear un Release

### Opción 1: Comandos Git (Recomendado)

```bash
# 1. Asegúrate de estar en main y actualizado
git checkout main
git pull

# 2. Crear y pushear un tag (ejemplo: v1.0.0)
git tag -a v1.0.0 -m "Release v1.0.0: Primera versión estable"
git push origin v1.0.0
```

### Opción 2: GitHub CLI

```bash
# Crear release directamente desde CLI
gh release create v1.0.0 --title "Calculadora v1.0.0" --notes "Primera versión estable"
```

### Opción 3: Interfaz Web de GitHub

1. Ve a tu repositorio en GitHub
2. Click en "Releases" (lateral derecho)
3. Click en "Create a new release"
4. Click en "Choose a tag" → Escribe `v1.0.0` → "Create new tag"
5. Rellena título y descripción
6. Click en "Publish release"

## 🤖 ¿Qué Sucede Automáticamente?

Cuando creas un tag con formato `v*.*.*`, el workflow `release.yml` se activa automáticamente:

1. ✅ **Crea el GitHub Release** con descripción
2. 🪟 **Compila para Windows** (x64) → `.zip`
3. 🍎 **Compila para macOS Intel** (x64) → `.zip`
4. 🍎 **Compila para macOS Apple Silicon** (arm64) → `.zip`
5. 🐧 **Compila para Linux** (x64) → `.zip`
6. ⬆️ **Sube todos los archivos** al release automáticamente

**Tiempo estimado**: ~5-8 minutos

## 📦 Archivos Generados

Cada release incluirá:

- `Calculadora-v1.0.0-win-x64.zip` - Windows 64-bit
- `Calculadora-v1.0.0-osx-x64.zip` - macOS Intel
- `Calculadora-v1.0.0-osx-arm64.zip` - macOS Apple Silicon (M1/M2/M3)
- `Calculadora-v1.0.0-linux-x64.zip` - Linux 64-bit

## 📝 Versionado Semántico

Sigue el formato `vMAJOR.MINOR.PATCH`:

- **MAJOR** (v**2**.0.0): Cambios incompatibles con versiones anteriores
- **MINOR** (v1.**1**.0): Nueva funcionalidad, compatible hacia atrás
- **PATCH** (v1.0.**1**): Corrección de bugs

### Ejemplos:

```bash
# Primera versión
git tag -a v1.0.0 -m "Release v1.0.0: Primera versión estable"

# Corrección de un bug
git tag -a v1.0.1 -m "Release v1.0.1: Corregir error en división por cero"

# Nueva funcionalidad
git tag -a v1.1.0 -m "Release v1.1.0: Agregar operaciones científicas"

# Cambio mayor
git tag -a v2.0.0 -m "Release v2.0.0: Nueva interfaz rediseñada"
```

## 🔍 Verificar el Release

1. Ve a: `https://github.com/rivasjm/ucred-dotnet-ui/actions`
2. Busca el workflow "Release" en ejecución
3. Una vez completado, ve a: `https://github.com/rivasjm/ucred-dotnet-ui/releases`
4. Verifica que los 4 archivos ZIP estén disponibles

## ✨ Trucos y Consejos

### Ver tags locales
```bash
git tag
```

### Ver tags remotos
```bash
git ls-remote --tags origin
```

### Eliminar un tag (si cometiste un error)
```bash
# Local
git tag -d v1.0.0

# Remoto (¡cuidado!)
git push origin --delete v1.0.0
```

### Crear un pre-release (beta)
```bash
git tag -a v1.0.0-beta.1 -m "Release v1.0.0-beta.1: Versión de prueba"
```

## 🎓 Para Estudiantes

Este workflow de release demuestra:

- ✅ **Compilación multiplataforma**: Un mismo código funciona en Windows, macOS y Linux
- ✅ **Distribución automatizada**: No necesitas compilar manualmente para cada sistema
- ✅ **Versionado**: Control claro de versiones del software
- ✅ **Continuous Delivery**: Desde código hasta ejecutables descargables automáticamente
- ✅ **Jobs paralelos**: Las 4 plataformas se compilan simultáneamente

## 🚨 Solución de Problemas

### El workflow no se ejecuta

- Verifica que el tag tenga formato `v*.*.*` (ejemplo: `v1.0.0`)
- Asegúrate de hacer `git push origin v1.0.0` (no solo `git tag`)

### El release falla

- Revisa que todos los tests pasen en `main`
- Verifica los logs en la pestaña "Actions" de GitHub

### Los archivos no aparecen

- Espera a que todos los jobs terminen (puede tardar varios minutos)
- Si algún job falla, el archivo correspondiente no se subirá
