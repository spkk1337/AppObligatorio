namespace Dominio.Entidades;

public class EvidenciaFisica : Evidencia
{
    private bool _huellasDigitales;

    public EvidenciaFisica(string descripcion, DateTime fechaEvidencia, bool huellasDigitales) : base(descripcion, fechaEvidencia)
    {
        _huellasDigitales = huellasDigitales;
    }



    public override int CalcularPeso()
    {
        int peso = base.CalcularPeso();
        if(_huellasDigitales)
        {
            peso += 40;
        }
        return peso;
    }

    public override string ToString()
    {
        string mensaje = base.ToString();
        return $"{mensaje}, Huellas Digitales: {_huellasDigitales}";
    }
}