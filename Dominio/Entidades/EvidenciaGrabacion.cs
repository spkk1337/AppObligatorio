namespace Dominio.Entidades;

public class EvidenciaGrabacion : Evidencia
{
    private int _calidad;
    private bool _infraganti;

    public EvidenciaGrabacion(string descripcion, DateTime fechaEvidencia, int calidad, bool infraganti) : base(descripcion, fechaEvidencia)
    {
        _calidad = calidad;
        _infraganti = infraganti;
    }
    
    public override int CalcularPeso()
    {
        int peso = base.CalcularPeso();
        if(_infraganti)
        {
            peso += 60;
            if(_calidad >= 3)
            {
                peso += 10;
            }
        }
        return peso;
    }

    public override string ToString()
    {
        string mensaje = base.ToString();
        return $"{mensaje}, Calidad: {_calidad}, Infraganti: {_infraganti}";
    }
}