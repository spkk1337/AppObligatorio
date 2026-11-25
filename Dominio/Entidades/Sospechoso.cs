using Dominio.Interface;

namespace Dominio.Entidades;

public class Sospechoso : iValidable
{
    private string _nombre;
    private string _cedula;
    private DateTime _fechaNacimiento;
    private bool _antecedentes;
    
    public Sospechoso(string nombre, string cedula, DateTime fechaNacimiento, bool antecedentes)
    {
        _nombre = nombre;
        _cedula = cedula;
        _fechaNacimiento = fechaNacimiento;
        _antecedentes = antecedentes;
    }
    
    public string Nombre{
        get { return _nombre; }
        set { _nombre = value; }
    }
    public string Cedula{
        get { return _cedula; }
        set { _cedula = value; }
    }

    public DateTime FechaNacimiento
    {
        get { return _fechaNacimiento; }
        set { _fechaNacimiento = value; }
    }
    public bool Antecedentes{
        get { return _antecedentes; }
        set { _antecedentes = value; }
    }

    public int obtenerEdad()
    {
        DateTime fechaActual = DateTime.Today;
        DateTime fechaSospechoso = _fechaNacimiento;
        int edad = fechaActual.Year - fechaSospechoso.Year;
        if (fechaActual < fechaSospechoso.AddYears(edad)) //Si la fecha actual es anterior al cumpleaños de este año,se resta 1 a la edad
        {
            edad--;
        }
        return edad;
    }

    public void Validar()
    {
        ValidarNombre();
        ValidarCedula();
        ValidarFechaNacimiento();
    }

    private void ValidarNombre()
    {
        if(string.IsNullOrEmpty(_nombre)) throw new Exception("El nombre no puede estar vacio");
    }

    private void ValidarCedula()
    {
        if(string.IsNullOrEmpty(_cedula)) throw new Exception("La cedula no puede vacío");
        
        if(_cedula.Length != 8) throw new Exception("La cedula deber tener 8 digitos");
    }

    private void ValidarFechaNacimiento()
    {
        if (_fechaNacimiento > DateTime.Today) throw new Exception("La fecha de nacimiento no puede ser futura");

        if (!(_fechaNacimiento > new DateTime(1900, 1, 1))) throw new Exception("Fecha inválida");
    }
    
    public override bool Equals(object? obj)
    {
        if(obj == null) return false;

        if (obj is Sospechoso s)
        {
            Sospechoso? otro = obj as Sospechoso;
            return otro != null && _cedula == otro._cedula;
        }
        return false;
    }
    
    public override string ToString()
    {
        return $"Nombre: {_nombre}, cedula: {_cedula}, Fecha nacimiento: {_fechaNacimiento}, Antecedentes:{(_antecedentes ? "Si" : "No")}";
    }
}