# 🔧 Troubleshooting - Problemas Comunes y Soluciones

## 1. Error: "HttpError: Resource not accessible by integration"

### Descripción del Problema

Al ejecutar el workflow de GitHub Actions, específicamente el paso de `dorny/test-reporter@v1`, se produce el error:

```
##[error]HttpError: Resource not accessible by integration
```

### Causa

Este error ocurre porque el `GITHUB_TOKEN` por defecto en GitHub Actions tiene permisos limitados de solo lectura (`read`) en repositorios privados. El action `test-reporter` necesita permisos de escritura para crear "check runs" en GitHub.

Los permisos predeterminados eran:
```
Contents: read
Metadata: read
Packages: read
```

### Solución

Añadir una sección `permissions` al archivo del workflow para otorgar los permisos necesarios:

```yaml
# En .github/workflows/ci-cd.yml
permissions:
  contents: read          # Leer el código del repositorio
  checks: write          # Crear check runs (necesario para test-reporter)
  pull-requests: write   # Comentar en PRs (opcional pero útil)
```

### Ubicación en el Workflow

La sección `permissions` debe ir después de las variables de entorno y antes de los jobs:

```yaml
name: CI/CD Pipeline

on:
  push:
    branches: [ main, develop ]

env:
  DOTNET_VERSION: '10.0.x'

permissions:       # <-- Aquí
  contents: read
  checks: write
  pull-requests: write

jobs:
  build-and-test:
    # ...
```

### Commit de la Solución

```bash
git add .github/workflows/
git commit -m "Fix: Add necessary permissions for GitHub Actions workflows"
git push
```

### Verificación

Después del push, los workflows se ejecutarán automáticamente con los nuevos permisos. Puedes verificar en:
https://github.com/rivasjm/ucred-gh-actions/actions

---

## 2. Diferencias de .NET entre Local y GitHub Actions

### Problema

Las pruebas pasan localmente pero fallan en GitHub Actions con error:
```
Framework: 'Microsoft.NETCore.App', version '8.0.0' (arm64)
.NET location: /usr/local/share/dotnet/
```

### Causa

- **Local (macOS)**: .NET 10.0.102 (arm64)
- **GitHub Actions**: Múltiples versiones instaladas (8.0.x, 9.0.x, 10.0.x)

El proyecto compilado para `net10.0` no encuentra el runtime correcto.

### Solución

Asegurarse de que:
1. El `TargetFramework` en los `.csproj` coincide con la versión de .NET instalada
2. El workflow especifica la versión correcta:

```yaml
- name: Setup .NET
  uses: actions/setup-dotnet@v4
  with:
    dotnet-version: '10.0.x'
```

---

## 3. Permisos en Repositorios Públicos vs Privados

### Diferencia Importante

**Repositorios Públicos:**
- GITHUB_TOKEN tiene permisos más amplios por defecto
- Muchas actions funcionan sin configuración adicional

**Repositorios Privados:**
- GITHUB_TOKEN tiene permisos restrictivos (solo lectura)
- Necesitas especificar permisos explícitamente

### Mejores Prácticas

Siempre especifica los permisos explícitamente, independientemente del tipo de repositorio:

```yaml
permissions:
  contents: read    # Mínimo necesario
  checks: write     # Si usas test reporters
  pull-requests: write  # Si quieres comentar en PRs
```

---

## 4. Badge de Estado No Se Muestra

### Problema

Los badges en el README aparecen como "unknown" o no se muestran.

### Causas Posibles

1. **Repositorio privado**: Los badges no funcionan en repos privados
2. **Nombre del workflow incorrecto**: Verifica que coincida exactamente
3. **Workflow nunca ejecutado**: El badge aparece después del primer run

### Solución

Para repositorio privado, hacer público:
```bash
gh repo edit rivasjm/ucred-gh-actions --visibility public
```

O usar badges alternativos:
```markdown
![Build](https://img.shields.io/badge/build-passing-brightgreen)
```

---

## 5. Actions No Se Ejecutan Automáticamente

### Verificar

1. **Configuración del repositorio**:
   - Settings > Actions > General
   - Asegurarse que Actions está habilitado

2. **Archivos en la ubicación correcta**:
   ```
   .github/workflows/ci-cd.yml  ✅
   github/workflows/ci-cd.yml   ❌
   ```

3. **Sintaxis YAML correcta**:
   - Usar validador: http://www.yamllint.com/

4. **Eventos correctos**:
   ```yaml
   on:
     push:
       branches: [ main ]  # Verifica que es tu rama principal
   ```

---

## 6. Comandos Útiles para Debugging

### Ver logs de un workflow

```bash
# Listar runs recientes
gh run list --limit 5

# Ver logs de un run específico
gh run view <run-id> --log

# Ver logs del último run fallido
gh run view --log
```

### Verificar permisos actuales

```bash
# Ver configuración del repo
gh repo view --json defaultBranchRef,isPrivate

# Ver workflow runs
gh api repos/rivasjm/ucred-gh-actions/actions/runs
```

### Re-ejecutar un workflow fallido

```bash
# Re-run el último workflow
gh run rerun

# Re-run solo los jobs fallidos
gh run rerun --failed
```

---

## 7. Recursos Adicionales

- **Documentación de Permisos**: https://docs.github.com/en/actions/security-guides/automatic-token-authentication
- **Test Reporter Action**: https://github.com/dorny/test-reporter
- **GitHub Actions Syntax**: https://docs.github.com/en/actions/learn-github-actions/workflow-syntax-for-github-actions

---

## 📝 Historial de Cambios

| Fecha | Problema | Solución | Commit |
|-------|----------|----------|---------|
| 2026-02-05 | HttpError: Resource not accessible | Añadidos permisos `checks: write` | fea5c15 |
| 2026-02-05 | .NET 8.0 → 10.0 | Actualizado TargetFramework | 042f208 |

---

**Nota**: Este documento se actualizará con nuevos problemas y soluciones a medida que surjan durante el uso del proyecto en clase.
