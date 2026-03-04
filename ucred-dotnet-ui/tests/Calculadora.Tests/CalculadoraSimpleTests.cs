using Xunit;
using Calculadora.Models;

namespace Calculadora.Tests;

/// <summary>
/// Pruebas unitarias para la clase CalculadoraSimple
/// Estas pruebas se ejecutarán automáticamente en GitHub Actions
/// </summary>
public class CalculadoraSimpleTests
{
    private readonly CalculadoraSimple _calculadora;
    
    public CalculadoraSimpleTests()
    {
        _calculadora = new CalculadoraSimple();
    }
    
    [Fact]
    public void Sumar_DosNumerosPositivos_RetornaResultadoCorrecto()
    {
        // Arrange
        double a = 5;
        double b = 3;
        double esperado = 8;
        
        // Act
        double resultado = _calculadora.Sumar(a, b);
        
        // Assert
        Assert.Equal(esperado, resultado);
    }
    
    [Fact]
    public void Sumar_NumerosNegativos_RetornaResultadoCorrecto()
    {
        // Arrange
        double a = -5;
        double b = -3;
        double esperado = -8;
        
        // Act
        double resultado = _calculadora.Sumar(a, b);
        
        // Assert
        Assert.Equal(esperado, resultado);
    }
    
    [Fact]
    public void Restar_DosNumeros_RetornaResultadoCorrecto()
    {
        // Arrange
        double a = 10;
        double b = 3;
        double esperado = 7;
        
        // Act
        double resultado = _calculadora.Restar(a, b);
        
        // Assert
        Assert.Equal(esperado, resultado);
    }
    
    [Fact]
    public void Multiplicar_DosNumeros_RetornaResultadoCorrecto()
    {
        // Arrange
        double a = 4;
        double b = 5;
        double esperado = 20;
        
        // Act
        double resultado = _calculadora.Multiplicar(a, b);
        
        // Assert
        Assert.Equal(esperado, resultado);
    }
    
    [Fact]
    public void Multiplicar_PorCero_RetornaCero()
    {
        // Arrange
        double a = 100;
        double b = 0;
        double esperado = 0;
        
        // Act
        double resultado = _calculadora.Multiplicar(a, b);
        
        // Assert
        Assert.Equal(esperado, resultado);
    }
    
    [Fact]
    public void Dividir_DosNumeros_RetornaResultadoCorrecto()
    {
        // Arrange
        double a = 10;
        double b = 2;
        double esperado = 5;
        
        // Act
        double resultado = _calculadora.Dividir(a, b);
        
        // Assert
        Assert.Equal(esperado, resultado);
    }
    
    [Fact]
    public void Dividir_PorCero_LanzaExcepcion()
    {
        // Arrange
        double a = 10;
        double b = 0;
        
        // Act & Assert
        Assert.Throws<DivideByZeroException>(() => _calculadora.Dividir(a, b));
    }
    
    [Theory]
    [InlineData(2, 2, 4)]
    [InlineData(0, 5, 5)]
    [InlineData(-3, 3, 0)]
    [InlineData(100, -50, 50)]
    public void Sumar_VariosEjemplos_RetornaResultadosCampectosrrectos(double a, double b, double esperado)
    {
        // Act
        double resultado = _calculadora.Sumar(a, b);
        
        // Assert
        Assert.Equal(esperado, resultado);
    }
    
    [Theory]
    [InlineData(2, 3, 6)]
    [InlineData(-2, 3, -6)]
    [InlineData(-2, -3, 6)]
    [InlineData(0, 100, 0)]
    public void Multiplicar_VariosEjemplos_RetornaResultadosCorrectos(double a, double b, double esperado)
    {
        // Act
        double resultado = _calculadora.Multiplicar(a, b);
        
        // Assert
        Assert.Equal(esperado, resultado);
    }
}
