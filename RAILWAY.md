# 🚀 Deployment con Railway

Railway es una plataforma cloud que facilita el deployment de aplicaciones. Es ideal para proyectos educativos por su simplicidad y plan gratuito.

## ⚡ Deploy en 3 Pasos

### Paso 1: Crear Cuenta en Railway

1. Ve a [railway.app](https://railway.app)
2. Click en "Start a New Project"
3. Login con tu cuenta de GitHub

### Paso 2: Conectar Repositorio

1. En Railway, click en "New Project"
2. Selecciona "Deploy from GitHub repo"
3. Autoriza Railway para acceder a tus repositorios
4. Busca y selecciona: `rivasjm/ucred-dotnet-api`
5. Railway detectará automáticamente el Dockerfile y comenzará el deployment

### Paso 3: Configurar Variables (Opcional)

Railway asignará un puerto automáticamente. Tu aplicación ya está configurada para usar el puerto que Railway asigne (8080).

## 🎉 ¡Listo!

Railway:
- ✅ Construye la imagen Docker automáticamente
- ✅ Ejecuta los tests durante el build
- ✅ Despliega tu API
- ✅ Genera una URL pública tipo: `https://ucred-todo-api-production.up.railway.app`

## 🔄 Deployments Automáticos

Cada push a `main` en GitHub → Deployment automático en Railway

No necesitas configurar nada más, Railway detecta los cambios automáticamente.

## 📊 Monitoreo

En el dashboard de Railway puedes ver:
- 📈 Métricas (CPU, memoria, red)
- 📝 Logs en tiempo real
- 🔄 Estado del deployment
- 🌐 URL de tu API

## 🧪 Probar la API

Una vez desplegada:

```bash
# Reemplaza con tu URL de Railway
URL="https://tu-app.up.railway.app"

# Listar tareas
curl $URL/api/todo

# Crear tarea
curl -X POST $URL/api/todo \
  -H "Content-Type: application/json" \
  -d '{"title":"Mi primera tarea","description":"Desde Railway!"}'

# Obtener tarea específica
curl $URL/api/todo/1
```

## 💰 Plan Gratuito

Railway ofrece:
- 💵 **$5 USD gratis/mes** sin tarjeta
- 🆓 Después de usar los $5, puedes agregar tarjeta
- 📊 Monitoreo incluido
- 🔄 Deployments ilimitados

**Uso estimado para este proyecto:** ~$1-2/mes (si está activo 24/7)

## 🎓 Ventajas para Educación

1. **Simplicidad**: No requiere configuración compleja
2. **Rapidez**: Deploy en minutos, no horas
3. **Visibilidad**: Logs y métricas claros
4. **Real-world**: Mismo workflow que empresas usan
5. **Colaboración**: Fácil compartir URLs con estudiantes

## 🔧 Variables de Entorno (Opcional)

Si necesitas agregar variables de entorno:

1. En Railway, ve a tu proyecto
2. Click en "Variables"
3. Agrega variables (ejemplo):
   - `ASPNETCORE_ENVIRONMENT=Production`
   - `LOG_LEVEL=Information`

## 🆘 Troubleshooting

### El deployment falla

1. Revisa los logs en Railway dashboard
2. Verifica que los tests pasen localmente: `dotnet test`
3. Asegúrate que el Dockerfile sea correcto

### La API no responde

1. Verifica que el puerto sea 8080 (ya configurado en Dockerfile)
2. Revisa logs en Railway para ver errores
3. Asegúrate que Ingress esté en "Public"

### Consumo excesivo de créditos

1. Railway cobra por tiempo de CPU usado
2. Si no necesitas la app 24/7, puedes pausarla manualmente
3. Considera configurar sleep/wakeup automático

## 📚 Recursos

- [Railway Docs](https://docs.railway.app)
- [Railway + .NET Guide](https://docs.railway.app/guides/dotnet)
- [Railway Pricing](https://railway.app/pricing)

---

**¡Deployment completado!** 🎊 Tu API REST está en producción y lista para ser usada en clase.
