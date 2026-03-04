# 🎨 Características de la Interfaz Gráfica

## Vista de la Aplicación

La calculadora tiene una interfaz gráfica moderna y amigable construida con Avalonia UI:

```
╔══════════════════════════════╗
║  🧮 Calculadora - Demo       ║
╠══════════════════════════════╣
║                              ║
║        0                     ║  ← Display
║                              ║
╠══════════════════════════════╣
║  [ C  ] [ / ] [ × ]          ║
║  [ 7  ] [ 8 ] [ 9 ] [ − ]    ║
║  [ 4  ] [ 5 ] [ 6 ] [ + ]    ║
║  [ 1  ] [ 2 ] [ 3 ] [   ]    ║
║  [   0    ] [ . ] [ = ]      ║
╚══════════════════════════════╝
```

## Componentes

### Display
- **Color de fondo**: Negro (#333333)
- **Texto**: Blanco, grande (32px), negrita
- **Alineación**: Derecha (como calculadoras tradicionales)
- **Esquinas redondeadas**: Para un look moderno

### Botones Numéricos (0-9, .)
- **Color**: Gris claro (#E0E0E0)
- **Texto**: Negro, 20px, negrita
- **Función**: Ingresar dígitos y punto decimal

### Botones de Operación (+, -, ×, /)
- **Color**: Naranja (#FFA500)
- **Texto**: Blanco, 20px, negrita
- **Función**: Seleccionar la operación matemática

### Botón Igual (=)
- **Color**: Verde (#4CAF50)
- **Texto**: Blanco, 20px, negrita
- **Tamaño**: Doble altura (ocupa 2 filas)
- **Función**: Calcular el resultado

### Botón Limpiar (C)
- **Color**: Rojo (#F44336)
- **Texto**: Blanco, 20px, negrita
- **Tamaño**: Doble ancho (ocupa 2 columnas)
- **Función**: Reiniciar la calculadora

## Funcionalidades

### Operaciones Básicas
- ➕ **Suma**: Suma dos números
- ➖ **Resta**: Resta el segundo número del primero
- ✖️ **Multiplicación**: Multiplica dos números
- ➗ **División**: Divide el primer número por el segundo

### Características Especiales
- **Decimales**: Soporte para números con punto decimal
- **Operaciones encadenadas**: Puedes realizar múltiples operaciones sin presionar =
- **Manejo de errores**: Muestra mensajes claros cuando hay errores (ej: división por cero)
- **Interfaz responsive**: Los botones se adaptan al tamaño de la ventana

### Flujo de Uso

1. **Ingresar primer número**: Click en los dígitos
2. **Seleccionar operación**: Click en +, -, ×, o /
3. **Ingresar segundo número**: Click en los dígitos
4. **Calcular**: Click en =
5. **Resultado**: Se muestra en el display

### Ejemplo de Uso

```
Usuario hace click: 5 → 0 → + → 3 → =
Display muestra: "5" → "50" → "50" → "3" → "53"
```

## Arquitectura MVVM

La aplicación usa el patrón **Model-View-ViewModel**:

### Model (`CalculadoraSimple.cs`)
- Contiene la lógica matemática pura
- Métodos: `Sumar()`, `Restar()`, `Multiplicar()`, `Dividir()`
- Sin dependencias de UI
- Fácilmente testeable

### View (`MainWindow.axaml`)
- Define la interfaz gráfica en XAML
- Botones, layouts, estilos
- No contiene lógica de negocio
- Se comunica con el ViewModel a través de bindings

### ViewModel (`MainWindowViewModel.cs`)
- Maneja el estado de la UI (`Display`, `_primerNumero`, etc.)
- Implementa comandos para las acciones del usuario
- Usa el Model para realizar cálculos
- Usa `CommunityToolkit.Mvvm` para simplificar el código

### Binding de Datos

```xml
<!-- En MainWindow.axaml -->
<TextBlock Text="{Binding Display}" />
<Button Command="{Binding AgregarNumeroCommand}" CommandParameter="5" />
```

```csharp
// En MainWindowViewModel.cs
[ObservableProperty]
private string _display = "0";

[RelayCommand]
private void AgregarNumero(string numero) { /*...*/ }
```

## Multiplataforma

Gracias a Avalonia UI, la aplicación funciona en:

- ✅ **Windows** (Windows 10/11)
- ✅ **macOS** (Intel y Apple Silicon)
- ✅ **Linux** (Ubuntu, Fedora, etc.)

El código es **exactamente el mismo** para todas las plataformas.

## Tecnologías Utilizadas

- **Avalonia UI 11.3**: Framework de UI multiplataforma
- **CommunityToolkit.Mvvm 8.2**: Helpers para el patrón MVVM
- **.NET 10.0**: Runtime y SDK
- **XAML**: Lenguaje de marcado para la UI

## Ventajas de Avalonia

1. **Multiplataforma nativa**: No usa Electron ni WebView
2. **Performance**: Renderizado nativo y rápido
3. **XAML familiar**: Similar a WPF para desarrolladores .NET
4. **Open Source**: Gratuito y con comunidad activa
5. **Moderno**: Soporte para estilos, animaciones, y temas

## Comparación con Otras Tecnologías

| Característica | Avalonia | WPF | MAUI | Electron |
|---------------|----------|-----|------|----------|
| Windows | ✅ | ✅ | ✅ | ✅ |
| macOS | ✅ | ❌ | ✅ | ✅ |
| Linux | ✅ | ❌ | ❌ | ✅ |
| Tamaño App | Pequeño | Pequeño | Medio | Grande |
| Performance | Excelente | Excelente | Buena | Media |
| XAML | ✅ | ✅ | ✅ | ❌ |

## Para Desarrolladores

### Extender la Calculadora

Para añadir nuevas operaciones:

1. **Añadir método en Model**:
```csharp
public double Potencia(double a, double b) => Math.Pow(a, b);
```

2. **Añadir comando en ViewModel**:
```csharp
[RelayCommand]
private void CalcularPotencia() { /*...*/ }
```

3. **Añadir botón en View**:
```xml
<Button Content="x²" Command="{Binding CalcularPotenciaCommand}"/>
```

### Hot Reload

Durante el desarrollo, puedes usar Hot Reload:
```bash
dotnet watch run --project src/Calculadora/Calculadora.csproj
```

Los cambios en XAML y C# se reflejan automáticamente sin reiniciar.

---

**Nota**: La interfaz está diseñada para ser simple y educativa, perfecta para enseñar conceptos de UI, MVVM, y desarrollo multiplataforma.
