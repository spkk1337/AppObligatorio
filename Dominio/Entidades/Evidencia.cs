using Dominio.Interface;

namespace Dominio.Entidades;

public abstract class Evidencia : iValidable
{
    private static int s_ultId = 0;
    private int _id = s_ultId++;
    private DateTime _fechaEvidencia;
    private string _descripcion;

    public Evidencia(string descripcion, DateTime fechaEvidencia)
    {
        _descripcion = descripcion;
        _fechaEvidencia = fechaEvidencia;
    }
    
    public int Id
    {
        get { return _id; }
    }
    
    public DateTime FechaEvidencia
    {
        get { return _fechaEvidencia; }
        set { _fechaEvidencia = value; }
    }

    public string Descripcion
    {
        get { return _descripcion; }
        set { _descripcion = value; }
    }

    public virtual int CalcularPeso()
    {
        if(_fechaEvidencia > DateTime.Today.AddMonths(-6))
        {
            return 5;
        }
        return 3;
    }

    public void Validar()
    {
        ValidarFechaEvidencia();
        ValidarDescripcion();
    }

    private void ValidarFechaEvidencia()
    {
        if (_fechaEvidencia > DateTime.Now) throw new Exception("Fecha invalida");
    }

    private void ValidarDescripcion()
    {
        if(string.IsNullOrEmpty(_descripcion)) throw new Exception("Descripcion invalida");
    }
    
    public override bool Equals(object? obj)
    {
        Evidencia? otra = obj as Evidencia;
        return otra != null && _id == otra._id;
    }
    
    public override string ToString()
    {
        return $" Id: {_id}, Descripcion:{_descripcion}";
    }
}

