namespace Dominio.Entidades;

public class EvidenciaTestimonio : Evidencia
{
    private string _nombreTestigo;
    private Credibilidad _credibilidad;

    public EvidenciaTestimonio(string descripcion, DateTime fechaEvidencia, string nombreTestigo, Credibilidad credibilidad) : base(descripcion, fechaEvidencia)
    {
        _nombreTestigo = nombreTestigo;
        _credibilidad = credibilidad;
    }

    public override int CalcularPeso()
    {
        int peso = base.CalcularPeso();
        if(_credibilidad == Credibilidad.Medio || _credibilidad == Credibilidad.Alto)
        {
            peso += 10;
        }
        return peso;
    }

    public static string Evidencia { get; set; }

    public override string ToString()
    {
        string mensaje = base.ToString();
        return $"{mensaje}, Nombre del testigo: {_nombreTestigo}, Credibilidad: {_credibilidad}";
    }
}