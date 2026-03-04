using Xunit;
using Calculadora.ViewModels;

namespace Calculadora.Tests;

/// <summary>
/// Pruebas unitarias del ViewModel de la ventana principal.
/// Estas pruebas verifican la lógica de la interfaz sin necesidad de renderizar la UI.
/// </summary>
public class MainWindowViewModelTests
{
    [Fact]
    public void Display_Inicial_EsCero()
    {
        // Arrange & Act
        var vm = new MainWindowViewModel();
        
        // Assert
        Assert.Equal("0", vm.Display);
    }

    [Fact]
    public void AgregarNumero_PrimerNumero_ReemplazaCero()
    {
        // Arrange
        var vm = new MainWindowViewModel();
        
        // Act
        vm.AgregarNumeroCommand.Execute("5");
        
        // Assert
        Assert.Equal("5", vm.Display);
    }

    [Fact]
    public void AgregarNumero_VariosDigitos_ConcatenaCorrectamente()
    {
        // Arrange
        var vm = new MainWindowViewModel();
        
        // Act
        vm.AgregarNumeroCommand.Execute("1");
        vm.AgregarNumeroCommand.Execute("2");
        vm.AgregarNumeroCommand.Execute("3");
        
        // Assert
        Assert.Equal("123", vm.Display);
    }

    [Fact]
    public void AgregarDecimal_SinPunto_AgregaPunto()
    {
        // Arrange
        var vm = new MainWindowViewModel();
        vm.AgregarNumeroCommand.Execute("5");
        
        // Act
        vm.AgregarDecimalCommand.Execute(null);
        
        // Assert
        Assert.Equal("5.", vm.Display);
    }

    [Fact]
    public void AgregarDecimal_ConPuntoExistente_NoAgregaOtroPunto()
    {
        // Arrange
        var vm = new MainWindowViewModel();
        vm.AgregarNumeroCommand.Execute("5");
        vm.AgregarDecimalCommand.Execute(null);
        vm.AgregarNumeroCommand.Execute("1");
        
        // Act
        vm.AgregarDecimalCommand.Execute(null);
        
        // Assert
        Assert.Equal("5.1", vm.Display);
    }

    [Fact]
    public void EjecutarOperacion_Suma_AlmacenaOperacion()
    {
        // Arrange
        var vm = new MainWindowViewModel();
        vm.AgregarNumeroCommand.Execute("5");
        
        // Act
        vm.EjecutarOperacionCommand.Execute("+");
        vm.AgregarNumeroCommand.Execute("3");
        vm.CalcularResultadoCommand.Execute(null);
        
        // Assert
        Assert.Equal("8", vm.Display);
    }

    [Fact]
    public void CalcularResultado_Multiplicacion_MuestraResultadoCorrecto()
    {
        // Arrange
        var vm = new MainWindowViewModel();
        
        // Act
        vm.AgregarNumeroCommand.Execute("5");
        vm.AgregarDecimalCommand.Execute(null);
        vm.AgregarNumeroCommand.Execute("1");
        vm.EjecutarOperacionCommand.Execute("*");
        vm.AgregarNumeroCommand.Execute("2");
        vm.CalcularResultadoCommand.Execute(null);
        
        // Assert
        Assert.Equal("10.2", vm.Display);
    }

    [Fact]
    public void CalcularResultado_Division_MuestraResultadoCorrecto()
    {
        // Arrange
        var vm = new MainWindowViewModel();
        
        // Act
        vm.AgregarNumeroCommand.Execute("1");
        vm.AgregarNumeroCommand.Execute("0");
        vm.EjecutarOperacionCommand.Execute("/");
        vm.AgregarNumeroCommand.Execute("2");
        vm.CalcularResultadoCommand.Execute(null);
        
        // Assert
        Assert.Equal("5", vm.Display);
    }

    [Fact]
    public void CalcularResultado_DivisionPorCero_MuestraError()
    {
        // Arrange
        var vm = new MainWindowViewModel();
        
        // Act
        vm.AgregarNumeroCommand.Execute("5");
        vm.EjecutarOperacionCommand.Execute("/");
        vm.AgregarNumeroCommand.Execute("0");
        vm.CalcularResultadoCommand.Execute(null);
        
        // Assert
        Assert.Contains("Error", vm.Display);
    }

    [Fact]
    public void Limpiar_ConNumeros_ReiniciaPantalla()
    {
        // Arrange
        var vm = new MainWindowViewModel();
        vm.AgregarNumeroCommand.Execute("1");
        vm.AgregarNumeroCommand.Execute("2");
        vm.AgregarNumeroCommand.Execute("3");
        
        // Act
        vm.LimpiarCommand.Execute(null);
        
        // Assert
        Assert.Equal("0", vm.Display);
    }

    [Fact]
    public void OperacionesEncadenadas_Multiple_CalculaCorrectamente()
    {
        // Arrange
        var vm = new MainWindowViewModel();
        
        // Act - Simula: 10 + 5 = 15, luego × 2 = 30
        vm.AgregarNumeroCommand.Execute("1");
        vm.AgregarNumeroCommand.Execute("0");
        vm.EjecutarOperacionCommand.Execute("+");
        vm.AgregarNumeroCommand.Execute("5");
        vm.EjecutarOperacionCommand.Execute("*"); // Esto debería calcular 10+5=15
        vm.AgregarNumeroCommand.Execute("2");
        vm.CalcularResultadoCommand.Execute(null);
        
        // Assert
        Assert.Equal("30", vm.Display);
    }

    [Fact]
    public void Resta_NumerosNegativos_ManejaCorrectamente()
    {
        // Arrange
        var vm = new MainWindowViewModel();
        
        // Act
        vm.AgregarNumeroCommand.Execute("5");
        vm.EjecutarOperacionCommand.Execute("-");
        vm.AgregarNumeroCommand.Execute("1");
        vm.AgregarNumeroCommand.Execute("0");
        vm.CalcularResultadoCommand.Execute(null);
        
        // Assert
        Assert.Equal("-5", vm.Display);
    }

    [Theory]
    [InlineData("2", "3", "+", "5")]
    [InlineData("10", "4", "-", "6")]
    [InlineData("7", "3", "*", "21")]
    [InlineData("20", "4", "/", "5")]
    public void OperacionesBasicas_VariosValores_CalculaCorrectamente(
        string num1, string num2, string operacion, string resultadoEsperado)
    {
        // Arrange
        var vm = new MainWindowViewModel();
        
        // Act
        foreach (char c in num1)
            vm.AgregarNumeroCommand.Execute(c.ToString());
        
        vm.EjecutarOperacionCommand.Execute(operacion);
        
        foreach (char c in num2)
            vm.AgregarNumeroCommand.Execute(c.ToString());
        
        vm.CalcularResultadoCommand.Execute(null);
        
        // Assert
        Assert.Equal(resultadoEsperado, vm.Display);
    }
}
