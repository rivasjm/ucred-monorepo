using System;
using System.Globalization;
using System.Windows.Input;
using Calculadora.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Calculadora.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly CalculadoraSimple _calculadora = new();
    
    [ObservableProperty]
    private string _display = "0";
    
    private double _primerNumero;
    private string? _operacionActual;
    private bool _nuevoNumero = true;

    [RelayCommand]
    private void AgregarNumero(string numero)
    {
        if (_nuevoNumero)
        {
            Display = numero;
            _nuevoNumero = false;
        }
        else
        {
            Display = Display == "0" ? numero : Display + numero;
        }
    }

    [RelayCommand]
    private void AgregarDecimal()
    {
        if (_nuevoNumero)
        {
            Display = "0.";
            _nuevoNumero = false;
        }
        else if (!Display.Contains('.'))
        {
            Display += ".";
        }
    }

    [RelayCommand]
    private void EjecutarOperacion(string operacion)
    {
        if (!string.IsNullOrEmpty(_operacionActual) && !_nuevoNumero)
        {
            CalcularResultado();
        }
        
        _primerNumero = double.Parse(Display, CultureInfo.InvariantCulture);
        _operacionActual = operacion;
        _nuevoNumero = true;
    }

    [RelayCommand]
    private void CalcularResultado()
    {
        if (string.IsNullOrEmpty(_operacionActual)) return;

        try
        {
            double segundoNumero = double.Parse(Display, CultureInfo.InvariantCulture);
            double resultado = _operacionActual switch
            {
                "+" => _calculadora.Sumar(_primerNumero, segundoNumero),
                "-" => _calculadora.Restar(_primerNumero, segundoNumero),
                "*" => _calculadora.Multiplicar(_primerNumero, segundoNumero),
                "/" => _calculadora.Dividir(_primerNumero, segundoNumero),
                _ => segundoNumero
            };

            Display = resultado.ToString(CultureInfo.InvariantCulture);
            _operacionActual = null;
            _nuevoNumero = true;
        }
        catch (DivideByZeroException)
        {
            Display = "Error: División por cero";
            _operacionActual = null;
            _nuevoNumero = true;
        }
        catch (Exception ex)
        {
            Display = $"Error: {ex.Message}";
            _operacionActual = null;
            _nuevoNumero = true;
        }
    }

    [RelayCommand]
    private void Limpiar()
    {
        Display = "0";
        _primerNumero = 0;
        _operacionActual = null;
        _nuevoNumero = true;
    }
}
