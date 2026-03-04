using System;

namespace Calculadora.Models;

/// <summary>
/// Clase que implementa operaciones matemáticas básicas
/// </summary>
public class CalculadoraSimple
{
    /// <summary>
    /// Suma dos números
    /// </summary>
    public double Sumar(double a, double b)
    {
        return a + b;
    }
    
    /// <summary>
    /// Resta dos números
    /// </summary>
    public double Restar(double a, double b)
    {
        return a - b;
    }
    
    /// <summary>
    /// Multiplica dos números
    /// </summary>
    public double Multiplicar(double a, double b)
    {
        return a * b;
    }
    
    /// <summary>
    /// Divide dos números
    /// </summary>
    /// <exception cref="DivideByZeroException">Se lanza cuando el divisor es cero</exception>
    public double Dividir(double a, double b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException("No se puede dividir por cero");
        }
        return a / b;
    }
}
