# 🔧 Configuración de Azure Container Apps

Esta guía te ayudará a configurar el despliegue automático a Azure Container Apps.

## ✅ Requisitos Previos

- Cuenta de Azure (Azure for Students da $100 gratis)
- Azure CLI instalado en tu máquina
- Permisos de administrador en tu suscripción de Azure

## 📦 Paso 1: Instalar Azure CLI

```bash
# En macOS con Homebrew
brew install azure-cli

# Verificar instalación
az --version
```

## 🔐 Paso 2: Login en Azure

```bash
# Login interactivo (abrirá tu navegador)
az login

# Verificar tu suscripción activa
az account show

# Si tienes múltiples suscripciones:
az account list --output table
az account set --subscription "NOMBRE_O_ID_DE_TU_SUSCRIPCION"
```

## 🏗️ Paso 3: Crear Recursos en Azure

Ejecuta estos comandos en tu terminal. **Ajusta las variables según prefieras:**

```bash
# ========================================
# VARIABLES - Ajusta según tus preferencias
# ========================================
RESOURCE_GROUP="rg-ucred-todo-api"
LOCATION="westeurope"              # O "eastus", "northeurope", etc.
ACR_NAME="ucredtodoapi"            # Solo alfanumérico, único globalmente
CONTAINER_APP_ENV="ucred-env"
CONTAINER_APP_NAME="ucred-todo-api"

# ========================================
# 1. Crear Resource Group
# ========================================
echo "📦 Creando Resource Group..."
az group create \
  --name $RESOURCE_GROUP \
  --location $LOCATION

# ========================================
# 2. Crear Azure Container Registry
# ========================================
echo "🐳 Creando Container Registry..."
az acr create \
  --resource-group $RESOURCE_GROUP \
  --name $ACR_NAME \
  --sku Basic \
  --admin-enabled true

echo "✅ Container Registry creado: $ACR_NAME.azurecr.io"

# ========================================
# 3. Crear Container Apps Environment
# ========================================
echo "🌍 Creando Container Apps Environment..."
az containerapp env create \
  --name $CONTAINER_APP_ENV \
  --resource-group $RESOURCE_GROUP \
  --location $LOCATION

# ========================================
# 4. Crear Container App
# ========================================
echo "🚀 Creando Container App..."
az containerapp create \
  --name $CONTAINER_APP_NAME \
  --resource-group $RESOURCE_GROUP \
  --environment $CONTAINER_APP_ENV \
  --image mcr.microsoft.com/azuredocs/containerapps-helloworld:latest \
  --target-port 8080 \
  --ingress external \
  --min-replicas 0 \
  --max-replicas 1

echo "✅ Recursos creados exitosamente!"
```

## 🔑 Paso 4: Obtener Credenciales

Ejecuta estos comandos para obtener las credenciales que necesitarás en GitHub:

```bash
# ========================================
# Obtener credenciales del ACR
# ========================================
echo "🔐 Obteniendo credenciales de Container Registry..."

ACR_USERNAME=$(az acr credential show --name $ACR_NAME --query username -o tsv)
ACR_PASSWORD=$(az acr credential show --name $ACR_NAME --query "passwords[0].value" -o tsv)

echo ""
echo "================================"
echo "ACR_LOGIN_SERVER:"
echo "${ACR_NAME}.azurecr.io"
echo ""
echo "ACR_USERNAME:"
echo "$ACR_USERNAME"
echo ""
echo "ACR_PASSWORD:"
echo "$ACR_PASSWORD"
echo "================================"
echo ""

# ========================================
# Crear Service Principal para GitHub
# ========================================
echo "👤 Creando Service Principal..."

SUBSCRIPTION_ID=$(az account show --query id -o tsv)

SP_OUTPUT=$(az ad sp create-for-rbac \
  --name "github-actions-ucred-todo-api" \
  --role contributor \
  --scopes /subscriptions/$SUBSCRIPTION_ID/resourceGroups/$RESOURCE_GROUP \
  --sdk-auth)

echo ""
echo "================================"
echo "AZURE_CREDENTIALS:"
echo "$SP_OUTPUT"
echo "================================"
echo ""

# ========================================
# Resumen
# ========================================
echo "✅ Setup completado!"
echo ""
echo "📋 Ahora configura estos GitHub Secrets:"
echo "1. AZURE_CREDENTIALS = (JSON del Service Principal arriba)"
echo "2. ACR_LOGIN_SERVER = ${ACR_NAME}.azurecr.io"
echo "3. ACR_USERNAME = $ACR_USERNAME"
echo "4. ACR_PASSWORD = (password mostrado arriba)"
echo "5. RESOURCE_GROUP = $RESOURCE_GROUP"
echo "6. CONTAINER_APP_NAME = $CONTAINER_APP_NAME"
```

## 🔐 Paso 5: Configurar GitHub Secrets

1. Ve a tu repositorio en GitHub:
   ```
   https://github.com/rivasjm/ucred-dotnet-api/settings/secrets/actions
   ```

2. Click en **"New repository secret"** y crea los siguientes secrets:

   | Nombre | Valor | Descripción |
   |--------|-------|-------------|
   | `AZURE_CREDENTIALS` | JSON del Service Principal | JSON completo (incluye `{}`)|
   | `ACR_LOGIN_SERVER` | `ucredtodoapi.azurecr.io` | Tu ACR name + .azurecr.io |
   | `ACR_USERNAME` | Usuario del ACR | Del comando anterior |
   | `ACR_PASSWORD` | Password del ACR | Del comando anterior |
   | `RESOURCE_GROUP` | `rg-ucred-todo-api` | Nombre del resource group |
   | `CONTAINER_APP_NAME` | `ucred-todo-api` | Nombre del container app |

3. Verifica que todos los secrets estén creados correctamente.

## 🚀 Paso 6: Activar el Deployment

Una vez configurados todos los secrets:

1. Haz push de cualquier cambio a la rama `main`
2. Ve a la pestaña **Actions** en GitHub
3. Verás el workflow **"Deploy to Azure"** ejecutándose

**O ejecútalo manualmente:**
1. Ve a Actions → Deploy to Azure
2. Click en "Run workflow"
3. Selecciona la rama `main`
4. Click en "Run workflow"

## 🔍 Verificar el Deployment

Después del primer deployment exitoso:

```bash
# Obtener la URL de tu API
az containerapp show \
  --name $CONTAINER_APP_NAME \
  --resource-group $RESOURCE_GROUP \
  --query properties.configuration.ingress.fqdn \
  -o tsv

# Probar la API
curl https://TU-URL.azurecontainerapps.io/api/todo
```

## 📊 Monitoreo

```bash
# Ver logs de la aplicación
az containerapp logs show \
  --name $CONTAINER_APP_NAME \
  --resource-group $RESOURCE_GROUP \
  --follow

# Ver estado del container app
az containerapp show \
  --name $CONTAINER_APP_NAME \
  --resource-group $RESOURCE_GROUP
```

## 💰 Costos

Con el tier gratuito de Azure Container Apps:
- **180,000 vCPU-seconds/mes gratis**
- **360,000 GiB-s/mes gratis**
- Con min-replicas=0, la app se apaga cuando no hay tráfico (ahorro máximo)
- Azure for Students: $100 de crédito adicional

**Para este proyecto educativo, debería ser completamente GRATIS.**

## 🧹 Limpiar Recursos (Opcional)

Si quieres eliminar todo:

```bash
# Eliminar todo el Resource Group (borra todos los recursos)
az group delete --name $RESOURCE_GROUP --yes --no-wait

# Eliminar el Service Principal
az ad sp delete --id $(az ad sp list --display-name "github-actions-ucred-todo-api" --query [0].id -o tsv)
```

## 🆘 Troubleshooting

### Error: "The subscription is not registered to use namespace 'Microsoft.App'"

```bash
az provider register --namespace Microsoft.App
az provider register --namespace Microsoft.OperationalInsights
```

### Error al crear ACR: "Name already exists"

El nombre del ACR debe ser único globalmente. Cambia `ACR_NAME` a algo único:
```bash
ACR_NAME="ucredtodoapi$(date +%s)"  # Agrega timestamp
```

### Ver errores en Container App

```bash
az containerapp logs show \
  --name $CONTAINER_APP_NAME \
  --resource-group $RESOURCE_GROUP \
  --follow
```

## ✅ Checklist Final

- [ ] Azure CLI instalado
- [ ] Login en Azure completado
- [ ] Resource Group creado
- [ ] Container Registry creado
- [ ] Container Apps Environment creado
- [ ] Container App creado
- [ ] Service Principal creado
- [ ] 6 secrets configurados en GitHub
- [ ] Primer deployment ejecutado
- [ ] API respondiendo correctamente

---

**¡Listo!** Tu API se desplegará automáticamente cada vez que hagas push a `main`. 🚀
