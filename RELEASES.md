# Guía de Releases

Esta guía explica cómo crear releases de la aplicación UCRED Movies Browser.

## 📋 Versionado semántico

El proyecto sigue [Semantic Versioning 2.0.0](https://semver.org/):

```
MAJOR.MINOR.PATCH

- MAJOR: Cambios incompatibles en la API
- MINOR: Nueva funcionalidad compatible con versiones anteriores
- PATCH: Correcciones de bugs
```

Ejemplos:
- `1.0.0` - Release inicial
- `1.1.0` - Nueva funcionalidad (ej: filtros de películas)
- `1.0.1` - Corrección de bugs
- `2.0.0` - Cambio mayor (ej: nueva API de TMDb)

## 🚀 Proceso de release

### 1. Preparar el código

Asegúrate de que:

- [ ] Todos los tests pasan: `mvn test`
- [ ] El código compila sin warnings: `mvn clean compile`
- [ ] La versión en `pom.xml` es correcta
- [ ] `CHANGELOG.md` está actualizado (opcional)
- [ ] Todos los cambios están commiteados

### 2. Actualizar versión en pom.xml

Si es necesario, actualiza la versión en `pom.xml`:

```xml
<groupId>es.unican.ucred</groupId>
<artifactId>ucred-java-ui-movies</artifactId>
<version>1.1.0</version> <!-- Actualiza aquí -->
```

Y también en `release.yml` si es necesario (buscar `--app-version`).

### 3. Crear tag y push

```bash
# Crear tag anotado
git tag -a v1.0.0 -m "Release v1.0.0 - Initial release"

# Push del tag a GitHub
git push origin v1.0.0
```

### 4. GitHub Actions hace el resto

Cuando haces push de un tag que empieza con `v`, automáticamente se:

1. ✅ Compila el JAR ejecutable
2. ✅ Crea instalador Windows (.exe)
3. ✅ Crea instalador macOS (.dmg)
4. ✅ Crea instalador Linux (.deb)
5. ✅ Crea release en GitHub con todos los archivos
6. ✅ Genera notas de release automáticas

Puedes seguir el progreso en: https://github.com/rivasjm/ucred-java-ui-movies/actions

### 5. Verificar release

1. Ve a https://github.com/rivasjm/ucred-java-ui-movies/releases
2. Verifica que aparece el nuevo release
3. Descarga y prueba los instaladores en diferentes plataformas (si es posible)

## 📦 Artefactos generados

Cada release incluye:

| Archivo | Tamaño aprox | Descripción |
|---------|--------------|-------------|
| `ucred-java-ui-movies-1.0.0.jar` | ~5 MB | JAR ejecutable (requiere Java 17+) |
| `UCRED Movies-1.0.0.exe` | ~70 MB | Instalador Windows con JRE embebido |
| `UCRED Movies-1.0.0.dmg` | ~75 MB | Imagen de disco macOS con JRE embebido |
| `ucred-movies_1.0.0_amd64.deb` | ~70 MB | Paquete Debian con JRE embebido |

## 🔄 Actualizar un release existente

Si necesitas reemplazar los artefactos de un release:

```bash
# Eliminar tag local y remoto
git tag -d v1.0.0
git push origin :refs/tags/v1.0.0

# Eliminar release en GitHub (manualmente desde la web)

# Crear tag nuevamente con el mismo nombre
git tag -a v1.0.0 -m "Release v1.0.0 - Updated artifacts"
git push origin v1.0.0
```

**⚠️ Advertencia**: No se recomienda reemplazar releases publicados. Mejor crear un nuevo patch release (ej: `v1.0.1`).

## 🐛 Pre-releases (beta, alpha)

Para crear versiones de prueba:

```bash
# Tag con sufijo
git tag -a v1.1.0-beta.1 -m "Beta release v1.1.0"
git push origin v1.1.0-beta.1
```

Luego marca el release como "pre-release" en GitHub:

1. Ve al release generado
2. Haz clic en "Edit release"
3. Marca "Set as a pre-release"
4. Guarda cambios

## 📝 Notas de release personalizadas

Si quieres personalizar las notas de release:

1. Ve al release en GitHub
2. Haz clic en "Edit release"
3. Modifica el texto del cuerpo
4. Puedes añadir:
   - Capturas de pantalla
   - Lista de cambios
   - Problemas conocidos
   - Instrucciones especiales
5. Guarda cambios

## ✅ Checklist completa de release

```markdown
## Release v1.0.0

- [ ] Tests pasan localmente (`mvn test`)
- [ ] Build exitoso localmente (`mvn package`)
- [ ] Versión actualizada en pom.xml
- [ ] Versión actualizada en release.yml (--app-version)
- [ ] CHANGELOG.md actualizado (si existe)
- [ ] README.md actualizado (si hay cambios)
- [ ] Todos los cambios commiteados
- [ ] Tag creado: `git tag -a v1.0.0 -m "Release v1.0.0"`
- [ ] Tag pusheado: `git push origin v1.0.0`
- [ ] GitHub Actions workflow completado exitosamente
- [ ] Release visible en GitHub
- [ ] Artefactos presentes (JAR, EXE, DMG, DEB)
- [ ] Instalador Windows probado
- [ ] Instalador macOS probado (si es posible)
- [ ] Instalador Linux probado (si es posible)
- [ ] Notas de release personalizadas (opcional)
```

## 🔒 Seguridad

Las API keys de TMDb **NO** están incluidas en los instaladores. Los usuarios deben:

1. Obtener su propia API key de TMDb (gratis)
2. Configurar la variable de entorno `TMDB_API_KEY`
3. Ejecutar la aplicación

Ver [API_KEY_SETUP.md](API_KEY_SETUP.md) para instrucciones.

## 🆘 Problemas comunes

### Error: "jpackage command not found"

jpackage viene incluido en JDK 14+. GitHub Actions usa JDK 17 que lo incluye. No debería ocurrir en el workflow.

### Error: "Permission denied" al crear .exe

Asegúrate de que el workflow tiene permisos de escritura en releases:

```yaml
permissions:
  contents: write
```

### Workflow no se ejecuta

Verifica que el tag empieza con `v` (ej: `v1.0.0`), ya que el trigger es:

```yaml
on:
  push:
    tags:
      - 'v*'
```

---

**Nota**: El primer release puede tardar más (hasta 20-30 minutos) porque cada plataforma debe compilar e incluir el JRE. Releases posteriores son más rápidos gracias al cache de Maven.
