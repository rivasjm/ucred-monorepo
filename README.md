# 🧮 ucred-web

Calculadora científica web con deployment automático a servidor universitario mediante WebDAV.

## 🚀 Tecnologías

- **HTML5** - Estructura semántica
- **CSS3** - Grid layout, animaciones, temas claro/oscuro
- **JavaScript vanilla** - Sin frameworks, código puro
- **GitHub Actions** - CI/CD automático
- **WebDAV** - Deployment a servidor universitario

## ✨ Características

### Calculadora
- ➕ Operaciones básicas (suma, resta, multiplicación, división)
- 📐 Funciones científicas (sin, cos, tan, log, ln, √, x²)
- 🔢 Constantes matemáticas (π, e)
- ⌨️ Soporte de teclado completo
- 🎯 Manejo de errores (división por cero, raíces negativas, etc.)

### Interfaz
- 🌓 Tema claro/oscuro (con persistencia)
- 📜 Historial de operaciones (localStorage)
- 📱 Diseño responsive (móvil, tablet, desktop)
- 🎨 Animaciones suaves
- ♿ Accesibilidad mejorada

## 🏗️ Estructura del Proyecto

```
ucred-web/
├── index.html              # Página principal
├── css/
│   └── styles.css         # Estilos con temas y responsive
├── js/
│   ├── calculator.js      # Lógica de cálculos
│   ├── history.js         # Gestión de historial
│   ├── theme.js           # Cambio de tema
│   └── main.js            # Inicialización y eventos
├── .github/
│   └── workflows/
│       └── deploy.yml     # Deployment automático
└── WEBDAV_SETUP.md        # Guía de configuración

```

## 🚀 Quick Start

### Desarrollo Local

1. **Clonar el repositorio:**
   ```bash
   git clone https://github.com/rivasjm/ucred-web.git
   cd ucred-web
   ```

2. **Abrir en el navegador:**
   ```bash
   # Con Python
   python3 -m http.server 8000
   
   # Con Node.js
   npx serve
   
   # O simplemente abre index.html
   open index.html
   ```

3. **Acceder:**
   - http://localhost:8000

### Deployment Automático

Cada push a `main` despliega automáticamente a tu servidor WebDAV.

**Ver guía de configuración:** [WEBDAV_SETUP.md](WEBDAV_SETUP.md)

## 🎓 Valor Educativo

Este proyecto demuestra:

1. **JavaScript POO** - Clases (Calculator, ThemeManager, HistoryManager)
2. **DOM Manipulation** - Event listeners, dynamic content
3. **LocalStorage** - Persistencia de datos del navegador
4. **CSS Moderno** - Variables CSS, Grid, animaciones
5. **CI/CD** - GitHub Actions con validación y deployment
6. **WebDAV** - Protocolo de transferencia de archivos
7. **Responsive Design** - Media queries, mobile-first

## 🔧 Comandos Útiles

### Validar HTML
```bash
grep -q "<!DOCTYPE html>" index.html && echo "✅ HTML válido"
```

### Validar que existen todos los archivos
```bash
for file in index.html css/styles.css js/calculator.js js/history.js js/theme.js js/main.js; do
  [ -f "$file" ] && echo "✅ $file" || echo "❌ Falta $file"
done
```

### Probar conexión WebDAV
```bash
curl -u "usuario:password" -X PROPFIND \
  "https://disco.unican.es/hcwebdav/Home%20Unican/www"
```

## 📊 GitHub Actions Workflow

El workflow automático incluye:

1. ✅ Validación de estructura HTML
2. ✅ Verificación de archivos CSS
3. ✅ Verificación de archivos JavaScript
4. 📤 Upload a servidor WebDAV
5. 📊 Resumen de deployment

**Tiempo de ejecución:** ~30-60 segundos

## 🌐 Acceso Público

Una vez desplegado, la calculadora estará disponible en:

```
https://personales.unican.es/{tu-usuario}/calculadora/
```

**Ejemplo:** Si tu usuario es `rivas`:
```
https://personales.unican.es/rivas/calculadora/
```

## 🤝 Contribuir

1. Fork el repositorio
2. Crea una rama: `git checkout -b feature/nueva-funcion`
3. Commit: `git commit -am 'feat: Agregar nueva función'`
4. Push: `git push origin feature/nueva-funcion`
5. Abre un Pull Request

## 📝 Roadmap

- [ ] Tests unitarios con Jest
- [ ] Gráficas de funciones
- [ ] Modo de conversión de unidades
- [ ] Calculadora de matrices
- [ ] Export/import de historial
- [ ] PWA (Progressive Web App)

## 📄 Licencia

MIT License - ver [LICENSE](LICENSE) para más detalles

## 👥 Autor

**Juan María Rivas** - [rivasjm](https://github.com/rivasjm)

---

**Proyecto educativo** para la asignatura de GitHub Actions en UCRED 🎓
# Test deployment
