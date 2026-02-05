# 👨‍🏫 Guía del Instructor

## Plan de Clase Sugerido

### Sesión 1: Introducción (2 horas)

#### Parte 1: Fundamentos de .NET (45 min)
1. Explicar qué es .NET y para qué sirve
2. Mostrar la instalación de .NET SDK en diferentes sistemas
3. Demostrar la creación de un proyecto simple
4. Explicar la estructura de una solución .NET

**Demo en vivo:**
```bash
dotnet --version
dotnet new console -n HolaMundo
cd HolaMundo
dotnet run
```

#### Parte 2: El Proyecto Calculadora (45 min)
1. Mostrar la estructura del proyecto
2. Ejecutar la aplicación
3. Revisar el código de `CalculadoraSimple.cs`
4. Explicar los conceptos de compilación y ejecución

**Actividad práctica:**
- Los estudiantes ejecutan la aplicación
- Experimentan con diferentes operaciones
- Identifican las partes del código

#### Parte 3: Pruebas Unitarias (30 min)
1. ¿Por qué son importantes las pruebas?
2. Introducción a xUnit
3. Anatomía de una prueba unitaria
4. Ejecutar las pruebas del proyecto

**Conceptos clave:**
- Arrange, Act, Assert
- Pruebas positivas y negativas
- Validación de excepciones

### Sesión 2: GitHub Actions Básico (2 horas)

#### Parte 1: Conceptos de CI/CD (30 min)
1. ¿Qué es CI/CD y por qué es importante?
2. Problemas que resuelve
3. Alternativas: Jenkins, GitLab CI, Travis CI
4. Por qué GitHub Actions

**Material de apoyo:**
- Diagrama de flujo de CI/CD
- Ejemplos de fallos que previene

#### Parte 2: Primer Workflow (45 min)
1. Revisar `simple.yml` línea por línea
2. Explicar la sintaxis YAML
3. Conceptos: events, jobs, steps
4. Subir el proyecto a GitHub

**Demo paso a paso:**
```bash
# Inicializar repositorio
git init
git add .
git commit -m "Initial commit: Calculadora con pruebas"

# Crear repositorio en GitHub (mostrar en navegador)
git remote add origin https://github.com/usuario/repo.git
git push -u origin main
```

#### Parte 3: Ver Actions en Acción (45 min)
1. Navegar a la pestaña Actions en GitHub
2. Observar la ejecución del workflow
3. Revisar logs detallados
4. Interpretar errores y éxitos

**Ejercicio práctico:**
- Introducir un error intencional
- Hacer push y ver cómo falla el workflow
- Corregir el error y verificar que pasa

### Sesión 3: GitHub Actions Avanzado (2 horas)

#### Parte 1: Workflows Complejos (45 min)
1. Revisar `ci-cd.yml`
2. Jobs paralelos
3. Matrix strategy
4. Artefactos

**Conceptos avanzados:**
- Dependencias entre jobs
- Estrategias de caché
- Optimización de tiempos

#### Parte 2: Prácticas Avanzadas (45 min)
1. Secretos y variables de entorno
2. Workflows condicionales
3. Workflows programados
4. Badges de estado

#### Parte 3: Ejercicios Prácticos (30 min)
Los estudiantes trabajan en los ejercicios del archivo `EJERCICIOS.md`

## Estrategias Pedagógicas

### Enfoque Incremental
1. **Empezar simple**: Mostrar `simple.yml` antes que `ci-cd.yml`
2. **Construir sobre conocimiento**: Cada concepto se basa en el anterior
3. **Práctica inmediata**: Después de cada concepto, ejercicio práctico

### Aprendizaje Activo
- Hacer que los estudiantes ejecuten los comandos, no solo observar
- Pair programming para los ejercicios
- Code reviews entre estudiantes

### Debugging como Aprendizaje
- Introducir errores intencionalmente
- Enseñar a leer logs de error
- Practicar el ciclo: error → debug → fix → verify

## Puntos Clave para Enfatizar

### 1. La Importancia de las Pruebas Automáticas
> "Si no está en un test automatizado, se romperá en producción"

### 2. CI/CD Ahorra Tiempo y Dinero
- Detecta errores temprano
- Libera a los desarrolladores de tareas repetitivas
- Mejora la calidad del software

### 3. GitHub Actions es Declarativo
- Describes QUÉ quieres, no CÓMO hacerlo
- YAML es legible por humanos
- Fácil de versionar y revisar

### 4. Empezar Simple, Escalar Gradualmente
- No necesitas todos los features el primer día
- Añade complejidad solo cuando la necesites
- Mejor un workflow simple que funciona que uno complejo que no

## Errores Comunes de Estudiantes

### 1. Sintaxis YAML Incorrecta
**Problema**: Indentación incorrecta
```yaml
# ❌ Incorrecto
jobs:
build:
  runs-on: ubuntu-latest

# ✅ Correcto
jobs:
  build:
    runs-on: ubuntu-latest
```

**Solución**: Usar validador de YAML online

### 2. No Hacer Checkout del Código
**Problema**: Olvidar `actions/checkout@v4`

**Síntoma**: "No such file or directory" en pasos posteriores

### 3. Usar Versiones Incorrectas
**Problema**: `dotnet-version: 8` en lugar de `8.0.x`

**Solución**: Revisar documentación de cada action

### 4. No Especificar el Runner
**Problema**: Olvidar `runs-on`

**Error**: "Required field 'runs-on' is missing"

### 5. Paths Incorrectos
**Problema**: Rutas relativas mal especificadas

**Solución**: Usar `pwd` y `ls` en steps de debug

## Evaluación Sugerida

### Criterios de Evaluación (100 puntos)

#### Parte 1: Código y Pruebas (40 puntos)
- [10] El código compila sin errores
- [10] Las pruebas pasan correctamente
- [10] Cobertura de código > 80%
- [10] Código bien documentado

#### Parte 2: Workflows (40 puntos)
- [15] Workflow básico funciona correctamente
- [15] Workflow avanzado implementado
- [10] Uso correcto de acciones y sintaxis

#### Parte 3: Documentación (20 puntos)
- [10] README claro y completo
- [5] Badges de estado funcionando
- [5] Comentarios en el código YAML

### Rúbrica Detallada

| Criterio | Excelente (4) | Bueno (3) | Suficiente (2) | Insuficiente (1) |
|----------|---------------|-----------|----------------|------------------|
| Workflows | Múltiples workflows funcionando con features avanzadas | Workflow principal funciona con algunos extras | Workflow básico funciona | Workflow no funciona o no existe |
| Pruebas | >90% cobertura, casos edge incluidos | >70% cobertura, casos principales | >50% cobertura | <50% cobertura o tests no pasan |
| Código | Limpio, bien estructurado, siguiendo convenciones | Funcional con algunas mejoras posibles | Funciona pero necesita refactoring | No compila o muchos errores |
| Documentación | Completa, clara, con ejemplos | Buena pero le faltan detalles | Básica pero suficiente | Incompleta o confusa |

## Recursos Adicionales para Clase

### Diapositivas Sugeridas

1. **¿Qué es CI/CD?**
   - Definición
   - Beneficios
   - Ejemplos del mundo real

2. **Anatomía de GitHub Actions**
   - Diagrama de flujo
   - Componentes principales
   - Ejemplo visual

3. **Demo en Vivo**
   - Hacer un cambio
   - Commit y push
   - Ver el workflow ejecutarse
   - Revisar resultados

### Videos Recomendados
- GitHub Actions Tutorial (Official)
- .NET Testing Best Practices
- CI/CD Explained

### Lecturas Complementarias
- [The twelve-factor app](https://12factor.net/)
- [Continuous Integration de Martin Fowler](https://martinfowler.com/articles/continuousIntegration.html)

## Adaptaciones Según el Nivel

### Para Grupos Avanzados
- Añadir deployment a Azure/AWS
- Implementar Docker containers
- Crear workflows multi-repositorio
- Añadir análisis de seguridad (Dependabot, CodeQL)

### Para Grupos Principiantes
- Enfocarse solo en `simple.yml`
- Más tiempo en fundamentos de .NET
- Ejercicios guiados paso a paso
- Pair programming obligatorio

### Para Clases Cortas (1 hora)
1. Mostrar el proyecto funcionando (10 min)
2. Explicar el workflow simple (20 min)
3. Hacer una demo de push y ejecución (15 min)
4. Ejercicio práctico básico (15 min)

## Troubleshooting Durante la Clase

### Problema: .NET SDK no instala en macOS
**Solución**: Verificar arquitectura (Intel vs ARM)
```bash
arch
# Si muestra arm64, descargar versión ARM64
```

### Problema: Git no configurado
```bash
git config --global user.name "Nombre"
git config --global user.email "email@ejemplo.com"
```

### Problema: Workflow no se ejecuta
1. Verificar que está en `.github/workflows/`
2. Verificar sintaxis YAML
3. Verificar que los eventos están configurados correctamente
4. Revisar la pestaña Actions para ver errores

### Problema: Pruebas fallan en Actions pero pasan localmente
- Diferentes versiones de .NET
- Dependencias de sistema local
- Rutas absolutas en el código
- Timezone o locale differences

## Extensiones del Proyecto

### Idea 1: Calculadora Científica
Añadir funciones:
- Potencias
- Raíces n-simas
- Logaritmos
- Funciones trigonométricas

### Idea 2: API REST
Convertir la calculadora en una API:
- Usar ASP.NET Core
- Endpoints para cada operación
- Pruebas de integración
- Deploy a Azure App Service

### Idea 3: Aplicación Web
- Frontend con Blazor o React
- Backend con .NET
- Workflows separados para frontend y backend
- Deploy automatizado

### Idea 4: Microservicios
- Separar cada operación en un microservicio
- Docker containers
- Kubernetes deployment
- Service mesh

## Preguntas Frecuentes de Estudiantes

**P: ¿Por qué usar GitHub Actions y no Jenkins?**  
R: GitHub Actions está integrado directamente en GitHub, es más fácil de configurar para proyectos pequeños, y es gratis para repos públicos.

**P: ¿Necesito aprobar manualmente cada ejecución?**  
R: No, los workflows se ejecutan automáticamente según los triggers configurados.

**P: ¿Puedo usar GitHub Actions para proyectos privados?**  
R: Sí, pero hay límites de minutos gratuitos (2000/mes para cuentas free).

**P: ¿Qué pasa si mi workflow tarda mucho?**  
R: Tienes un límite de 6 horas por ejecución. Optimiza usando cache y paralelización.

**P: ¿Puedo ejecutar comandos que modifiquen el repositorio?**  
R: Sí, pero necesitas configurar permisos y hacer commit/push de los cambios.

## Checklist Pre-Clase

- [ ] .NET SDK instalado y funcionando
- [ ] Git configurado
- [ ] Cuenta de GitHub lista
- [ ] Proyecto clonado o descargado
- [ ] Slides preparadas
- [ ] Ejemplos de errores comunes listos
- [ ] Plan B si falla internet (ejemplos locales)

## Feedback y Mejora Continua

Al final del curso, pide a los estudiantes:
1. ¿Qué fue lo más útil?
2. ¿Qué fue lo más difícil?
3. ¿Qué añadirías o quitarías?
4. ¿Te sientes preparado para usar esto en un proyecto real?

---

**Nota**: Esta guía es un documento vivo. Adáptala según las necesidades de tu clase.
