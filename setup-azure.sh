#!/bin/bash

# 🚀 Script Rápido para Configurar Azure Container Apps
# Copia y pega estos bloques de comandos en tu terminal

# ========================================
# PASO 1: Instalar Azure CLI (si no lo tienes)
# ========================================
echo "Verificando Azure CLI..."
if ! command -v az &> /dev/null; then
    echo "Instalando Azure CLI..."
    brew install azure-cli
else
    echo "✅ Azure CLI ya está instalado"
fi

# ========================================
# PASO 2: Login en Azure
# ========================================
echo ""
echo "🔐 Iniciando sesión en Azure..."
az login

# Mostrar suscripción activa
echo ""
echo "📋 Tu suscripción activa:"
az account show --output table

# Si necesitas cambiar de suscripción, descomenta y ajusta:
# az account set --subscription "TU_SUSCRIPCION"

# ========================================
# PASO 3: Configurar Variables
# ========================================
echo ""
echo "⚙️ Configurando variables..."

RESOURCE_GROUP="rg-ucred-todo-api"
LOCATION="westeurope"
ACR_NAME="ucredtodoapi"
CONTAINER_APP_ENV="ucred-env"
CONTAINER_APP_NAME="ucred-todo-api"

echo "Resource Group: $RESOURCE_GROUP"
echo "Location: $LOCATION"
echo "ACR Name: $ACR_NAME"
echo "Container App: $CONTAINER_APP_NAME"

read -p "¿Continuar con estos valores? (y/n) " -n 1 -r
echo
if [[ ! $REPLY =~ ^[Yy]$ ]]; then
    echo "Edita las variables al inicio del script y vuelve a ejecutar"
    exit 1
fi

# ========================================
# PASO 4: Crear Recursos
# ========================================
echo ""
echo "🏗️ Creando recursos en Azure..."

# Resource Group
echo "📦 Creando Resource Group..."
az group create \
  --name $RESOURCE_GROUP \
  --location $LOCATION

# Container Registry
echo "🐳 Creando Container Registry..."
az acr create \
  --resource-group $RESOURCE_GROUP \
  --name $ACR_NAME \
  --sku Basic \
  --admin-enabled true

# Container Apps Environment
echo "🌍 Creando Container Apps Environment..."
az containerapp env create \
  --name $CONTAINER_APP_ENV \
  --resource-group $RESOURCE_GROUP \
  --location $LOCATION

# Container App
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

echo ""
echo "✅ Recursos creados exitosamente!"

# ========================================
# PASO 5: Obtener Credenciales
# ========================================
echo ""
echo "🔑 Obteniendo credenciales..."

ACR_USERNAME=$(az acr credential show --name $ACR_NAME --query username -o tsv)
ACR_PASSWORD=$(az acr credential show --name $ACR_NAME --query "passwords[0].value" -o tsv)
SUBSCRIPTION_ID=$(az account show --query id -o tsv)

echo ""
echo "================================================================"
echo "📋 CREDENCIALES PARA GITHUB SECRETS"
echo "================================================================"
echo ""
echo "1️⃣ ACR_LOGIN_SERVER:"
echo "${ACR_NAME}.azurecr.io"
echo ""
echo "2️⃣ ACR_USERNAME:"
echo "$ACR_USERNAME"
echo ""
echo "3️⃣ ACR_PASSWORD:"
echo "$ACR_PASSWORD"
echo ""
echo "4️⃣ RESOURCE_GROUP:"
echo "$RESOURCE_GROUP"
echo ""
echo "5️⃣ CONTAINER_APP_NAME:"
echo "$CONTAINER_APP_NAME"
echo ""

# Service Principal
echo "👤 Creando Service Principal para GitHub Actions..."
SP_OUTPUT=$(az ad sp create-for-rbac \
  --name "github-actions-ucred-todo-api-$(date +%s)" \
  --role contributor \
  --scopes /subscriptions/$SUBSCRIPTION_ID/resourceGroups/$RESOURCE_GROUP \
  --sdk-auth)

echo ""
echo "6️⃣ AZURE_CREDENTIALS:"
echo "$SP_OUTPUT"
echo ""
echo "================================================================"
echo ""

# ========================================
# PASO 6: Guardar Credenciales
# ========================================
echo "💾 Guardando credenciales en archivo..."

cat > azure-secrets.txt << EOF
================================================================
GITHUB SECRETS - Copia estos valores a GitHub
================================================================

Repositorio: https://github.com/rivasjm/ucred-dotnet-api/settings/secrets/actions

1. ACR_LOGIN_SERVER
${ACR_NAME}.azurecr.io

2. ACR_USERNAME
$ACR_USERNAME

3. ACR_PASSWORD
$ACR_PASSWORD

4. RESOURCE_GROUP
$RESOURCE_GROUP

5. CONTAINER_APP_NAME
$CONTAINER_APP_NAME

6. AZURE_CREDENTIALS
$SP_OUTPUT

================================================================
EOF

echo "✅ Credenciales guardadas en: azure-secrets.txt"
echo ""
echo "🎯 SIGUIENTE PASO:"
echo "1. Abre el archivo azure-secrets.txt"
echo "2. Ve a: https://github.com/rivasjm/ucred-dotnet-api/settings/secrets/actions"
echo "3. Crea cada secret con los valores del archivo"
echo "4. Haz push a main y el workflow se ejecutará automáticamente"
echo ""
echo "⚠️  IMPORTANTE: El archivo azure-secrets.txt contiene información sensible."
echo "   Bórralo después de configurar los secrets en GitHub."
