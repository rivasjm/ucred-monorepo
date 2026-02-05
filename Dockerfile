# Usar imagen base de .NET SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar archivos de proyecto y restaurar dependencias
COPY ["src/TodoApi/TodoApi.csproj", "src/TodoApi/"]
COPY ["tests/TodoApi.Tests/TodoApi.Tests.csproj", "tests/TodoApi.Tests/"]
RUN dotnet restore "src/TodoApi/TodoApi.csproj"

# Copiar todo el código y compilar
COPY . .
WORKDIR "/src/src/TodoApi"
RUN dotnet build "TodoApi.csproj" -c Release -o /app/build

# Ejecutar tests
WORKDIR /src
RUN dotnet test "tests/TodoApi.Tests/TodoApi.Tests.csproj" -c Release --no-restore

# Publicar la aplicación
WORKDIR "/src/src/TodoApi"
RUN dotnet publish "TodoApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Imagen final con runtime únicamente (más pequeña)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080

# Copiar archivos publicados desde la etapa de build
COPY --from=build /app/publish .

# Configurar ASP.NET Core para usar puerto 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "TodoApi.dll"]
