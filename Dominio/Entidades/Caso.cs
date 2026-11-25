using Dominio.Interface;

namespace Dominio.Entidades;

public class Caso : iValidable
{
    private static int s_ultId = 0;
    private int _id = s_ultId++;
    private string _nombre;
    private string _descripcion;
    private bool _cerrado;
    private Sospechoso _sospechosoPrincipal;
    private Investigador _investigador;
    public List<Evidencia> _evidencias;

    public Caso(string nombre, string descripcion, bool cerrado, Sospechoso sospechosoPrincipal,
        Investigador investigador, List<Evidencia> evidencias)
    {
        _nombre = nombre;
        _descripcion = descripcion;
        _cerrado = cerrado;
        _sospechosoPrincipal = sospechosoPrincipal;
        _investigador = investigador;
        _evidencias = evidencias;
    }

    public int Id
    {
        get { return _id; }
    }
    
    public string Nombre
    {
        get { return _nombre; }
        set { _nombre = value; }
    }

    public string Descripcion
    {
        get { return _descripcion; }
        set { _descripcion = value; }
        
    }

    public bool Cerrado
    {
        get { return _cerrado; }
        set { _cerrado = value; }
    }

    public Sospechoso SospechosoPrincipal
    {
        get { return _sospechosoPrincipal; }
        set { _sospechosoPrincipal = value; }
    }

    public Investigador Investigador
    {
        get { return _investigador; }
        set { _investigador = value; }
    }

    public List<Evidencia> Evidencias
    {
        get { return _evidencias; }
        set { _evidencias = value; }
    }

    public List<Evidencia> ObtenerEvidencias()
    {
        return _evidencias;
    }

    public int CalcularPesoEvidencias()
    {
        int pesoTotal = 0;

        foreach (Evidencia evidencia in _evidencias)
        {
            pesoTotal += evidencia.CalcularPeso();
        }

        return pesoTotal;
    }

    public string CalcularRecomendacion()
    {
        int peso = CalcularPesoEvidencias();
        
        if(peso < 30)
        {
            return $"DESESTIMADO: El caso {_nombre} carece de fundamentos. El peso de la prueba es {peso} y deberia ser desestimado finalizando la investigacion en contra de {_sospechosoPrincipal.Nombre}";
        }
        else if(peso >= 30 && peso <= 70)
        {
            return $"CONTINUAR INVESTIGANDO: El caso {_nombre} parece ir por el camino correcto, pero necesita mas investigacion. El peso de la prueba es {peso} pero aun no hay informacion suficiente para imputar a {_sospechosoPrincipal.Nombre}";
        }

            return $"IMPUTACION INMINENTE: El caso {_nombre} esta resuelto. El peso de la prueba es {peso} y deberia imputarse a {_sospechosoPrincipal.Nombre} inmediatamente";
    }

    public void AltaEvidencia(Evidencia e)
    {
        if (e == null) throw new Exception("Evidencia null");
        e.Validar();
        
            if (_evidencias.Contains(e)) throw new Exception("Evidencia ya existe");
        
        _evidencias.Add(e);
    }

    public void Validar()
    {
        ValidarNombre();
        ValidarDescripcion();
        ValidarSospechosoPrincipal();
        ValidarInvestigador();
    }

    private void ValidarNombre()
    {
        if(string.IsNullOrEmpty(_nombre)) throw new Exception("El nombre no puede ser vacio");
    }

    private void ValidarDescripcion()
    {
        if(string.IsNullOrEmpty(_descripcion)) throw new Exception("La descripcion no puede ser vacia");   
    }

    private void ValidarSospechosoPrincipal()
    {
        if(_sospechosoPrincipal == null) throw new Exception("El sospechoso principal no puede ser vacio");   
    }

    private void ValidarInvestigador()
    {
        if(_investigador == null) throw new Exception("El investigador no puede ser vacio");
        if(!_investigador.EsDetective()) throw new Exception("El investigador no es detective");
    }
    
    public string ObtenerMailInv()
    {
        return _investigador.Email;
    }

    public override bool Equals(object? obj)
    {
        if(obj == null) return false;

        if (obj is Caso c)
        {
            Caso? otro = obj as Caso;
            return otro != null && _id == otro._id;
        }
        return false;
    }

    public override string ToString()
    {
        return $"Id: {_id}, Nombre: {_nombre}, Descripcion: {_descripcion}, Nombre Investigador: {_investigador.Nombre}";
    }
}