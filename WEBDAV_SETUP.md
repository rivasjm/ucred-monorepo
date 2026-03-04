# 🌐 Configuración de Deployment WebDAV

Esta guía explica cómo configurar el deployment automático a tu servidor WebDAV de la universidad.

## 📋 Requisitos Previos

1. Acceso a tu servidor WebDAV de la universidad
2. Repositorio en GitHub
3. Permisos de escritura en `Home Unican/www`

## 🔐 Paso 1: Configurar GitHub Secrets

Ve a tu repositorio en GitHub → **Settings** → **Secrets and variables** → **Actions** → **New repository secret**

Añade estos 3 secrets:

### WEBDAV_URL
La URL completa de tu directorio WebDAV donde se subirán los archivos.

**Valor:** `https://disco.unican.es/hcwebdav/Home%20Unican/www`

> **Nota:** Los espacios en la URL deben estar codificados como `%20`

### WEBDAV_USERNAME
Tu nombre de usuario completo de la UC (con @unican.es)

**Valor:** `tu_usuario@unican.es`

**Ejemplo:** Si tu email es `rivas@unican.es`, entonces el username es `rivas@unican.es`

### WEBDAV_PASSWORD
Tu contraseña de la UC

**Valor:** `tu_contraseña`

> ⚠️ **Importante:** Esta es la misma contraseña que usas para el email, Moodle, etc.

## 🎯 Paso 2: Verificar la Configuración

### Probar acceso WebDAV manualmente

Abre un terminal y ejecuta:

```bash
# Listar contenido del directorio
curl -u "TU_USUARIO@unican.es:TU_PASSWORD" -X PROPFIND \
  "https://disco.unican.es/hcwebdav/Home%20Unican/www"

# Si ves una respuesta XML con el listado, ¡funciona! ✅
```

### Crear el directorio (si no existe)

```bash
curl -u "TU_USUARIO@unican.es:TU_PASSWORD" -X MKCOL \
  "https://disco.unican.es/hcwebdav/Home%20Unican/www"
```

## 🚀 Paso 3: Activar Deployment Automático

Una vez configurados los secrets:

1. Haz un commit y push a `main`
2. Ve a **Actions** en GitHub
3. Verás el workflow "Deploy to WebDAV" ejecutándose
4. Espera ~1 minuto a que complete

## 🌐 Paso 4: Acceder a tu Página

Tu calculadora estará disponible en:

```
https://personales.unican.es/{tu-usuario}/calculadora/
```

**Ejemplo:** Si tu usuario es `rivas`, la URL sería:
```
https://personales.unican.es/rivas/calculadora/
```

## 📊 Verificar el Deployment

### Desde GitHub Actions

En el workflow verás:

```
✅ index.html uploaded
✅ css/styles.css uploaded
✅ js/calculator.js uploaded
✅ js/history.js uploaded
✅ js/theme.js uploaded
✅ js/main.js uploaded
🎉 Deployment completado!
```

### Desde WebDAV

Lista los archivos en el servidor:

```bash
curl -u "TU_USUARIO@unican.es:TU_PASSWORD" -X PROPFIND \
  "https://disco.unican.es/hcwebdav/Home%20Unican/www" \
  | grep -o 'href>[^<]*</href'
```

Deberías ver:

- index.html
- css/styles.css
- js/calculator.js
- js/history.js
- js/theme.js
- js/main.js

## 🔧 Troubleshooting

### Error: "401 Unauthorized"

**Problema:** Usuario o contraseña incorrectos

**Solución:**
1. Verifica que los secrets `WEBDAV_USERNAME` y `WEBDAV_PASSWORD` sean correctos
2. Usa tu usuario completo CON el dominio `@unican.es` (ej: `usuario@unican.es`)
3. Verifica que puedas hacer login en https://disco.unican.es/hcwebdav/

### Error: "404 Not Found"

**Problema:** La ruta WebDAV no existe

**Solución:**
1. Verifica que `WEBDAV_URL` apunte a `Home%20Unican/www`
2. Crea el directorio manualmente con MKCOL (ver Paso 2)
3. Contacta con el servicio de informática si el problema persiste

### Error: "403 Forbidden"

**Problema:** No tienes permisos de escritura

**Solución:**
1. Verifica que tienes una cuenta activa de estudiante
2. Contacta con el servicio de informática para solicitar acceso WebDAV
3. Asegúrate de que tu cuenta no esté bloqueada

### La página no se ve bien

**Problema:** Los archivos CSS/JS no cargan

**Solución:**
1. Verifica en el workflow de Actions que todos los archivos se subieron correctamente
2. Abre la consola del navegador (F12) y busca errores 404
3. Comprueba que las rutas relativas en `index.html` sean correctas
4. Asegúrate de que el servidor sirva los archivos con MIME types correctos

### El deployment falla intermitentemente

**Problema:** Timeouts en el servidor WebDAV

**Solución:**
1. El servidor puede estar lento, reintenta el workflow
2. Si persiste, contacta al servicio de informática

## 🔄 Workflow del Deployment

Cada vez que haces push a `main`:

1. GitHub Actions valida HTML, CSS y JS
2. Se conecta al servidor WebDAV con tus credenciales
3. Crea los directorios necesarios
4. Sube todos los archivos (HTML, CSS, JS)
5. Genera un resumen del deployment

**Tiempo estimado:** 30-60 segundos

## 📚 Recursos Adicionales

- [WebDAV Specification](http://www.webdav.org/)
- [GitHub Actions Secrets](https://docs.github.com/en/actions/security-guides/encrypted-secrets)
- [curl WebDAV Guide](https://gist.github.com/madprops/d66d81ba5f2e7321567b4fc9e3e29cbe)

## 🆘 Soporte

Si tienes problemas:

1. Revisa los logs del workflow en GitHub Actions
2. Verifica que puedes acceder manualmente a https://disco.unican.es/hcwebdav/
3. Contacta al servicio de informática de la UC: [informatica@unican.es](mailto:informatica@unican.es)

---

**¡Deployment configurado!** 🎊 Cada push a main desplegará automáticamente tu calculadora.
