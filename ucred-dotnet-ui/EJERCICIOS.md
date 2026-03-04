# 🎓 Ejercicios para Estudiantes

## Nivel Principiante

### Ejercicio 1: Entender el Workflow Simple
1. Abre `.github/workflows/simple.yml`
2. Identifica cada sección (name, on, jobs, steps)
3. Explica en tus propias palabras qué hace cada paso

### Ejercicio 2: Modificar la Calculadora
1. Añade una función para calcular el módulo (resto de división)
2. Añade pruebas para esta nueva función
3. Verifica que las pruebas pasen localmente: `dotnet test`
4. Haz commit y push para ver el workflow en acción

**Código de ayuda para CalculadoraSimple.cs:**
```csharp
public double Modulo(double a, double b)
{
    if (b == 0)
    {
        throw new DivideByZeroException("No se puede calcular módulo con divisor cero");
    }
    return a % b;
}
```

### Ejercicio 3: Añadir un Paso al Workflow
Modifica `simple.yml` para añadir un paso que muestre un mensaje personalizado:

```yaml
- name: Mensaje de bienvenida
  run: echo "¡Compilación ejecutada por TU_NOMBRE!"
```

## Nivel Intermedio

### Ejercicio 4: Crear un Workflow de Documentación
Crea `.github/workflows/docs.yml` que genere documentación del código:

```yaml
name: Documentación

on:
  push:
    branches: [ main ]

jobs:
  generate-docs:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '8.0.x'
      - name: Generar documentación XML
        run: dotnet build -c Release /p:GenerateDocumentation=true
```

### Ejercicio 5: Añadir Funcionalidad de Raíz Cuadrada
1. Añade un método `RaizCuadrada` a la calculadora
2. El método debe validar que el número no sea negativo
3. Crea al menos 3 pruebas:
   - Raíz de número positivo
   - Raíz de cero
   - Raíz de número negativo (debe lanzar excepción)

### Ejercicio 6: Workflow con Condicionales
Modifica el workflow para que envíe un mensaje diferente si las pruebas fallan:

```yaml
- name: Verificar resultado
  if: success()
  run: echo "✅ ¡Todas las pruebas pasaron!"

- name: Avisar de fallo
  if: failure()
  run: echo "❌ Algunas pruebas fallaron"
```

## Nivel Avanzado

### Ejercicio 7: Implementar Cobertura de Código
1. Instala la herramienta de cobertura:
   ```bash
   dotnet add tests/Calculadora.Tests package coverlet.collector
   ```

2. Crea un workflow que genere reporte de cobertura:

```yaml
- name: Ejecutar pruebas con cobertura
  run: dotnet test --collect:"XPlat Code Coverage"

- name: Generar reporte de cobertura
  uses: danielpalme/ReportGenerator-GitHub-Action@5
  with:
    reports: '**/coverage.cobertura.xml'
    targetdir: 'coveragereport'
    reporttypes: 'HtmlInline;Cobertura'
```

### Ejercicio 8: Workflow de Release Automático
Crea un workflow que:
1. Se active al crear un tag `v*` (ej: v1.0.0)
2. Compile el proyecto en modo Release
3. Publique la aplicación
4. Cree un release de GitHub con el ejecutable

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
      
      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '8.0.x'
      
      - name: Publish
        run: dotnet publish src/Calculadora/Calculadora.csproj -c Release -o ./release
      
      - name: Create Release
        uses: softprops/action-gh-release@v1
        with:
          files: ./release/*
        env:
          GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
```

### Ejercicio 9: Notificaciones por Email
Configura el workflow para enviar un email cuando fallen las pruebas:

```yaml
- name: Enviar notificación
  if: failure()
  uses: dawidd6/action-send-mail@v3
  with:
    server_address: smtp.gmail.com
    server_port: 465
    username: ${{ secrets.EMAIL_USERNAME }}
    password: ${{ secrets.EMAIL_PASSWORD }}
    subject: Fallo en las pruebas
    body: Las pruebas del proyecto han fallado
    to: tu-email@ejemplo.com
```

### Ejercicio 10: Análisis de Seguridad
Añade un job de análisis de seguridad con dependencias:

```yaml
security-scan:
  runs-on: ubuntu-latest
  steps:
    - uses: actions/checkout@v4
    
    - name: Run security scan
      uses: securego/gosec@master
      with:
        args: '-no-fail -fmt sarif -out results.sarif ./...'
    
    - name: Upload SARIF file
      uses: github/codeql-action/upload-sarif@v2
      with:
        sarif_file: results.sarif
```

## Proyecto Final

### Desafío: Sistema Completo de CI/CD

Crea un sistema completo que incluya:

1. **Workflow de Pull Request**
   - Ejecuta pruebas
   - Verifica formato de código
   - Comenta en el PR con resultados

2. **Workflow de Main**
   - Ejecuta todas las pruebas
   - Genera cobertura de código
   - Construye artefactos
   - Despliega a un ambiente de staging (simulado)

3. **Workflow de Release**
   - Construye para múltiples plataformas
   - Genera documentación
   - Crea release en GitHub
   - Genera changelog automático

4. **Workflow Programado**
   - Se ejecuta diariamente
   - Verifica dependencias desactualizadas
   - Ejecuta pruebas de regresión

### Criterios de Evaluación

- ✅ Los workflows se ejecutan correctamente
- ✅ El código está bien documentado
- ✅ Las pruebas cubren al menos el 80% del código
- ✅ Los workflows usan buenas prácticas (caching, versiones específicas, etc.)
- ✅ El README explica claramente todos los workflows

## Recursos de Ayuda

### Comandos Útiles para Debugging

```bash
# Ver logs detallados de las pruebas
dotnet test --logger "console;verbosity=detailed"

# Listar todos los tests
dotnet test --list-tests

# Ejecutar un test específico
dotnet test --filter "FullyQualifiedName~Calculadora.Tests.CalculadoraSimpleTests.Sumar_DosNumeros_RetornaResultadoCorrecto"

# Generar reporte de cobertura
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

### Validar YAML Localmente

Usa herramientas online:
- [YAML Lint](http://www.yamllint.com/)
- [GitHub Actions Validator](https://rhysd.github.io/actionlint/)

O instala actionlint:
```bash
brew install actionlint
actionlint .github/workflows/*.yml
```

### Ejecutar GitHub Actions Localmente

Usa [act](https://github.com/nektos/act):

```bash
# Instalar act
brew install act

# Listar workflows
act -l

# Ejecutar un workflow específico
act push -W .github/workflows/simple.yml

# Ejecutar solo un job
act -j build
```

## Entrega del Proyecto

1. Crea un repositorio en GitHub con el código
2. Asegúrate de que todos los workflows se ejecuten correctamente
3. Añade badges de estado al README
4. Documenta cualquier decisión de diseño
5. Incluye capturas de pantalla de los workflows ejecutándose

## Preguntas de Reflexión

1. ¿Qué ventajas ofrece GitHub Actions sobre la ejecución manual de tests?
2. ¿Cuándo usarías un workflow con matrix strategy?
3. ¿Qué problemas pueden surgir si no versionas las acciones correctamente?
4. ¿Cómo mejorarías el tiempo de ejecución de los workflows?
5. ¿Qué otras herramientas de CI/CD conoces y cómo se comparan?

---

¡Buena suerte con los ejercicios! 🚀
