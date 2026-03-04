using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.Threading;
using Avalonia.VisualTree;
using Xunit;
using Calculadora.Views;
using Calculadora.ViewModels;

namespace Calculadora.Tests;

/// <summary>
/// Pruebas de integración de la UI usando Avalonia Headless.
/// Estas pruebas simulan interacciones reales del usuario con la interfaz gráfica.
/// </summary>
public class MainWindowUITests
{
    private static MainWindow CreateAndShowWindow()
    {
        var window = new MainWindow
        {
            DataContext = new MainWindowViewModel()
        };
        
        // Forzar la inicialización de la ventana
        window.Show();
        
        // Procesar eventos pendientes
        Dispatcher.UIThread.RunJobs();
        
        return window;
    }
    
    [AvaloniaFact]
    public void MainWindow_AlCrearse_MuestraCero()
    {
        // Arrange & Act
        var window = CreateAndShowWindow();
        
        // Buscar el TextBlock del display
        var display = window.FindControl<TextBlock>("DisplayText");
        
        // Assert
        Assert.NotNull(display);
        Assert.Equal("0", display.Text);
    }

    [AvaloniaFact]
    public void BotonNumero_AlHacerClick_ActualizaDisplay()
    {
        // Arrange
        var window = CreateAndShowWindow();
        var boton5 = window.FindControl<Button>("Boton5");
        var display = window.FindControl<TextBlock>("DisplayText");
        
        // Act
        boton5?.Command?.Execute(boton5.CommandParameter);
        Dispatcher.UIThread.RunJobs();
        
        // Assert
        Assert.Equal("5", display?.Text);
    }

    [AvaloniaFact]
    public void BotonesMultiples_SecuenciaNumeros_ConcatenaCorrectamente()
    {
        // Arrange
        var window = CreateAndShowWindow();
        var display = window.FindControl<TextBlock>("DisplayText");
        
        // Act
        window.FindControl<Button>("Boton1")?.Command?.Execute("1");
        window.FindControl<Button>("Boton2")?.Command?.Execute("2");
        window.FindControl<Button>("Boton3")?.Command?.Execute("3");
        Dispatcher.UIThread.RunJobs();
        
        // Assert
        Assert.Equal("123", display?.Text);
    }

    [AvaloniaFact]
    public void BotonDecimal_AlHacerClick_AgregaPunto()
    {
        // Arrange
        var window = CreateAndShowWindow();
        var display = window.FindControl<TextBlock>("DisplayText");
        
        // Act
        window.FindControl<Button>("Boton5")?.Command?.Execute("5");
        window.FindControl<Button>("BotonDecimal")?.Command?.Execute(null);
        window.FindControl<Button>("Boton1")?.Command?.Execute("1");
        Dispatcher.UIThread.RunJobs();
        
        // Assert
        Assert.Equal("5.1", display?.Text);
    }

    [AvaloniaFact]
    public void CalculadoraSuma_SecuenciaCompleta_MuestraResultadoCorrecto()
    {
        // Arrange
        var window = CreateAndShowWindow();
        var display = window.FindControl<TextBlock>("DisplayText");
        
        // Act - Simula: 5 + 3 =
        window.FindControl<Button>("Boton5")?.Command?.Execute("5");
        window.FindControl<Button>("BotonSumar")?.Command?.Execute("+");
        window.FindControl<Button>("Boton3")?.Command?.Execute("3");
        window.FindControl<Button>("BotonIgual")?.Command?.Execute(null);
        Dispatcher.UIThread.RunJobs();
        
        // Assert
        Assert.Equal("8", display?.Text);
    }

    [AvaloniaFact]
    public void CalculadoraMultiplicacion_ConDecimales_MuestraResultadoCorrecto()
    {
        // Arrange
        var window = CreateAndShowWindow();
        var display = window.FindControl<TextBlock>("DisplayText");
        
        // Act - Simula: 5.1 × 2 =
        window.FindControl<Button>("Boton5")?.Command?.Execute("5");
        window.FindControl<Button>("BotonDecimal")?.Command?.Execute(null);
        window.FindControl<Button>("Boton1")?.Command?.Execute("1");
        window.FindControl<Button>("BotonMultiplicar")?.Command?.Execute("*");
        window.FindControl<Button>("Boton2")?.Command?.Execute("2");
        window.FindControl<Button>("BotonIgual")?.Command?.Execute(null);
        Dispatcher.UIThread.RunJobs();
        
        // Assert
        Assert.Equal("10.2", display?.Text);
    }

    [AvaloniaFact]
    public void BotonLimpiar_DespuesDeOperacion_ReiniciaPantalla()
    {
        // Arrange
        var window = CreateAndShowWindow();
        var display = window.FindControl<TextBlock>("DisplayText");
        
        // Act
        window.FindControl<Button>("Boton9")?.Command?.Execute("9");
        window.FindControl<Button>("Boton9")?.Command?.Execute("9");
        window.FindControl<Button>("BotonLimpiar")?.Command?.Execute(null);
        Dispatcher.UIThread.RunJobs();
        
        // Assert
        Assert.Equal("0", display?.Text);
    }

    [AvaloniaFact]
    public void DivisionPorCero_MuestraError()
    {
        // Arrange
        var window = CreateAndShowWindow();
        var display = window.FindControl<TextBlock>("DisplayText");
        
        // Act - Simula: 5 / 0 =
        window.FindControl<Button>("Boton5")?.Command?.Execute("5");
        window.FindControl<Button>("BotonDividir")?.Command?.Execute("/");
        window.FindControl<Button>("Boton0")?.Command?.Execute("0");
        window.FindControl<Button>("BotonIgual")?.Command?.Execute(null);
        Dispatcher.UIThread.RunJobs();
        
        // Assert
        Assert.NotNull(display?.Text);
        Assert.Contains("Error", display?.Text);
    }

    [AvaloniaFact]
    public void BotonesOperadores_TodosExisten()
    {
        // Arrange
        var window = CreateAndShowWindow();
        
        // Assert - Verificar que todos los botones de operadores existen
        Assert.NotNull(window.FindControl<Button>("BotonSumar"));
        Assert.NotNull(window.FindControl<Button>("BotonRestar"));
        Assert.NotNull(window.FindControl<Button>("BotonMultiplicar"));
        Assert.NotNull(window.FindControl<Button>("BotonDividir"));
        Assert.NotNull(window.FindControl<Button>("BotonIgual"));
        Assert.NotNull(window.FindControl<Button>("BotonLimpiar"));
    }

    [AvaloniaFact]
    public void BotonesNumeros_TodosExisten()
    {
        // Arrange
        var window = CreateAndShowWindow();
        
        // Assert - Verificar que todos los botones numéricos existen (0-9)
        for (int i = 0; i <= 9; i++)
        {
            var boton = window.FindControl<Button>($"Boton{i}");
            Assert.NotNull(boton);
        }
    }
}
