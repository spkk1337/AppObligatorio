using Dominio.Entidades;

namespace Dominio;

public class Sistema
{
    private List<Investigador> _investigadores;
    private List<Sospechoso> _sospechosos;
    private List<Caso> _casos;

    private static Sistema _instancia;

    public static Sistema Instancia
    {
        get
        {
            if (_instancia == null)
            {
                _instancia = new Sistema();
            }
            return _instancia;
        }
    }

    public List<Investigador> Investigadores
    {
        get { return _investigadores; }
        set { _investigadores = value;}
    }

    public List<Sospechoso> Sospechosos
    {
        get { return _sospechosos; }
        set { _sospechosos = value; }
    }
    
    public List<Caso> Casos 
    {
        get { return _casos; }
        set { _casos = value; }
    }

    public Sistema()
    {
        _investigadores = new List<Investigador>();
        _sospechosos = new List<Sospechoso>();
        _casos = new List<Caso>();
    }

    public void AltaInvestigador(Investigador i)
    {
        if(i == null) throw new Exception("Investigador null");
            if(_investigadores.Contains(i)) throw new Exception("Investigador ya existe");
        
        _investigadores.Add(i);
    }

    public void AltaSospechoso(Sospechoso s)
    {
        if (s == null) throw new Exception("Sospechoso null");
        s.Validar();
        
        if(_sospechosos.Contains(s)) throw new Exception("Sospechoso ya existe");
        
        _sospechosos.Add(s);
    }

    public void AltaCaso(Caso caso)
    {
        if (caso == null) throw new Exception("Caso null");
            caso.Validar();
            
            if(!_investigadores.Contains(caso.Investigador)) throw new Exception("El investigador no existe");
            
            if(!_sospechosos.Contains(caso.SospechosoPrincipal)) throw new Exception("El sospechoso no existe");
        
        _casos.Add(caso);
    }

    public List<Sospechoso> ObtenerSospechososConAntecedentes()
    {
        List <Sospechoso> _sospechososConAntecedentes = new List<Sospechoso>();
        
        foreach (Sospechoso s in _sospechosos)
        {
            if (s.Antecedentes)
            {
                _sospechososConAntecedentes.Add(s);
            }
        }
        return _sospechososConAntecedentes;
    }

    public Sospechoso ObtenerSospechosoXCedula(string cedula)
    {
        foreach (Sospechoso s in _sospechosos)
        {
            if (s.Cedula == cedula)
            {
                return s;
            }
        }
        return null;
    }

    public Investigador ObtenerInvestigadorXMail(string mail)
    {
        foreach (Investigador i in _investigadores)
        {
            if (i.Email == mail)
            {
                return i;
            }
        }
        return null;
    }

    public Caso ObtenerCasoPorId(int id)
    {
        foreach (Caso c in _casos)
        {
            if (c.Id == id)
            {
                return c;
            }
        }
        return null;
    }

    public List<Caso> ObtenerCasosPorInvestigador(string email)
    {
        List<Caso> _casosInvestigador = new List<Caso>();
        if (string.IsNullOrEmpty(email)) throw new Exception("El mail no puede ser vacío");

        foreach (Caso c in _casos)
        {
            if (email == c.ObtenerMailInv())
            {
                _casosInvestigador.Add(c);
            }
        }
        return _casosInvestigador;
    }

    public Investigador Login(string m, string p)
    {
        foreach (Investigador i in _investigadores)
        {
            if (i.Email == m && i.Contrasena == p)
            {
                return i;
            }
        }
        return null;
    }

    public List<Sospechoso> FiltrarSospechososXEdad(int edad)
    {
        List<Sospechoso> retorno = new List<Sospechoso>();
        foreach (Sospechoso s in _sospechosos)
        {
            if (s.obtenerEdad() <= edad)
            {
                retorno.Add(s);
            }
        }
        return retorno;
    }

    public void PrecargarDatos()
    {
        // INVESTIGADORES: 5 FISCALES Y 5 DETECTIVES

        _investigadores.Add(new Investigador("fiscal1@mail.com", "Fiscal123", "Laura Méndez", Rol.Fiscal));
        _investigadores.Add(new Investigador("fiscal2@mail.com", "Fiscal123", "Martín Rodríguez", Rol.Fiscal));
        _investigadores.Add(new Investigador("fiscal3@mail.com", "Fiscal123", "Sofía Fernández", Rol.Fiscal));
        _investigadores.Add(new Investigador("fiscal4@mail.com", "Fiscal123", "Diego Pereira", Rol.Fiscal));
        _investigadores.Add(new Investigador("fiscal5@mail.com", "Fiscal123", "Valentina Silva", Rol.Fiscal));

        _investigadores.Add(new Investigador("detective1@mail.com", "Detective123", "Andrés Gómez", Rol.Detective));
        _investigadores.Add(new Investigador("detective2@mail.com", "Detective123", "Camila Torres", Rol.Detective));
        _investigadores.Add(new Investigador("detective3@mail.com", "Detective123", "Nicolás Castro", Rol.Detective));
        _investigadores.Add(new Investigador("detective4@mail.com", "Detective123", "Lucía Romero", Rol.Detective));
        _investigadores.Add(new Investigador("detective5@mail.com", "Detective123", "Federico Suárez", Rol.Detective));


        // SOSPECHOSOS: 15

        _sospechosos.Add(new Sospechoso("Juan Pérez", "41000001", new DateTime(1990, 3, 12), true));
        _sospechosos.Add(new Sospechoso("Ana Gómez", "41000002", new DateTime(1998, 7, 21), false));
        _sospechosos.Add(new Sospechoso("Pedro Fernández", "41000003", new DateTime(1985, 11, 5), true));
        _sospechosos.Add(new Sospechoso("María López", "41000004", new DateTime(2001, 2, 18), false));
        _sospechosos.Add(new Sospechoso("Carlos Silva", "41000005", new DateTime(1993, 9, 30), true));
        _sospechosos.Add(new Sospechoso("Lucía Martínez", "41000006", new DateTime(1997, 4, 14), false));
        _sospechosos.Add(new Sospechoso("Diego Castro", "41000007", new DateTime(1989, 6, 9), true));
        _sospechosos.Add(new Sospechoso("Valentina Ruiz", "41000008", new DateTime(2000, 12, 2), false));
        _sospechosos.Add(new Sospechoso("Martín Acosta", "41000009", new DateTime(1992, 1, 25), true));
        _sospechosos.Add(new Sospechoso("Sofía Pereira", "41000010", new DateTime(1999, 8, 11), false));
        _sospechosos.Add(new Sospechoso("Nicolás Torres", "41000011", new DateTime(1987, 5, 19), true));
        _sospechosos.Add(new Sospechoso("Camila Díaz", "41000012", new DateTime(1996, 10, 7), false));
        _sospechosos.Add(new Sospechoso("Federico Ramos", "41000013", new DateTime(1991, 3, 28), true));
        _sospechosos.Add(new Sospechoso("Florencia Silva", "41000014", new DateTime(2002, 6, 16), false));
        _sospechosos.Add(new Sospechoso("Gabriel Morales", "41000015", new DateTime(1984, 12, 23), true));


        // CASOS: 10
        // Los investigadores están en las posiciones 5 a 9,
        // que corresponden a los cinco detectives.

        AltaCaso(new Caso(
            "Robo a una joyería",
            "Robo de varias piezas durante la madrugada.",
            false,
            _sospechosos[0],
            _investigadores[5],
            CrearEvidencias("Robo a una joyería")));

        AltaCaso(new Caso(
            "Fraude bancario",
            "Transferencias bancarias sospechosas.",
            false,
            _sospechosos[1],
            _investigadores[6],
            CrearEvidencias("Fraude bancario")));

        AltaCaso(new Caso(
            "Robo de vehículo",
            "Desaparición de un vehículo estacionado.",
            true,
            _sospechosos[2],
            _investigadores[7],
            CrearEvidencias("Robo de vehículo")));

        AltaCaso(new Caso(
            "Falsificación documental",
            "Presunta falsificación de documentos comerciales.",
            false,
            _sospechosos[3],
            _investigadores[8],
            CrearEvidencias("Falsificación documental")));

        AltaCaso(new Caso(
            "Hurto en comercio",
            "Desaparición de mercadería de un local.",
            true,
            _sospechosos[4],
            _investigadores[9],
            CrearEvidencias("Hurto en comercio")));

        AltaCaso(new Caso(
            "Estafa inmobiliaria",
            "Presunto engaño durante una operación inmobiliaria.",
            false,
            _sospechosos[5],
            _investigadores[5],
            CrearEvidencias("Estafa inmobiliaria")));

        AltaCaso(new Caso(
            "Daños a la propiedad",
            "Daños ocasionados en un establecimiento.",
            false,
            _sospechosos[6],
            _investigadores[6],
            CrearEvidencias("Daños a la propiedad")));

        AltaCaso(new Caso(
            "Robo en domicilio",
            "Ingreso no autorizado y sustracción de objetos.",
            true,
            _sospechosos[7],
            _investigadores[7],
            CrearEvidencias("Robo en domicilio")));

        AltaCaso(new Caso(
            "Fraude informático",
            "Acceso no autorizado a un sistema informático.",
            false,
            _sospechosos[8],
            _investigadores[8],
            CrearEvidencias("Fraude informático")));

        AltaCaso(new Caso(
            "Contrabando de mercadería",
            "Presunto ingreso irregular de mercadería.",
            false,
            _sospechosos[9],
            _investigadores[9],
            CrearEvidencias("Contrabando de mercadería")));
    }

    // Crea cuatro evidencias de tres clases diferentes.

    private List<Evidencia> CrearEvidencias(string contexto)
    {
        List<Evidencia> evidencias = new List<Evidencia>();

        evidencias.Add(new EvidenciaFisica(
            "Huella dactilar. Caso: " + contexto,
            DateTime.Now.AddDays(-10),
            true
        ));

        evidencias.Add(new EvidenciaFisica(
            "Objeto recolectado. Caso: " + contexto,
            DateTime.Now.AddDays(-9),
            true
        ));

        evidencias.Add(new EvidenciaGrabacion(
            "Cámara de seguridad. Caso: " + contexto,
            DateTime.Now.AddDays(-8),
            2,
            true
        ));

        evidencias.Add(new EvidenciaTestimonio(
            "Declaración de testigo. Caso: " + contexto,
            DateTime.Now.AddDays(-7),
            "Pedro",
            Credibilidad.Medio
        ));

        return evidencias;
    }


    public List<Caso> ListaDeCasosOrdenados()
    {
        List<Caso> retorno = new List<Caso>();
        foreach (Caso v in _casos)
        {
            retorno.Add(v);
        }
        retorno.Sort(new CasosOrdenAsc());
        return retorno;
    }

    public List<Sospechoso> ListaDeSospechosos()
    {
        List<Sospechoso> retorno = new List<Sospechoso>();
        foreach (Sospechoso s in _sospechosos)
        {
            retorno.Add(s);
        }
        return retorno;
    }
}