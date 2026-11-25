using Dominio.Interface;

namespace Dominio.Entidades;

public class Investigador : iValidable
{
    private string _email;
    private string _contrasena;
    private string _nombre;
    private Rol _rol;

    public Investigador(string email, string contrasena, string nombre, Rol rol)
    {
        _email = email;
        _contrasena = contrasena;
        _nombre = nombre;
        _rol = rol;
    }

    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }

    public string Contrasena
    {
        get { return _contrasena; }
        set { _contrasena = value; }   
    }
    
    public string Nombre{
        get { return _nombre; }
        set { _nombre = value; }
    }
    public Rol Rol{
        get { return _rol; }
    }

    public bool EsDetective()
    {
        if(_rol == Rol.Detective)
        {
            return true;
        } 
        return false;
    }

    public void Validar()
    {
        ValidarMail();
        ValidarPass();
        ValidarNombre();
    }

    private void ValidarMail()
    {
        if (string.IsNullOrEmpty(_email)) throw new Exception("El mail no puede ser vacio");
        if (!_email.Contains("@")) throw new Exception("El mail debe contener arroba");
    }

    private void ValidarPass()
    {
        if (string.IsNullOrEmpty(_contrasena)) throw new Exception("La contraseña no puede ser vacio");
    }

    private void ValidarNombre()
    {
        if (string.IsNullOrEmpty(_nombre)) throw new Exception("El nombre no puede ser vacio");
    }
    
    public override bool Equals(object? obj)
    {
        Investigador? otra = obj as Investigador;
        return otra != null && _email == otra._email;
    }


    public override string ToString()
    {
        return $"Nombre: {_nombre}, email: {_email}, rol:{_rol}";
    }
}