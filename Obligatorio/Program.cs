using Dominio;
using Dominio.Entidades;
namespace Obligatorio;

internal class Program
{
    private static Sistema s = new Sistema();
    
    static void Main(string[] args)
    {
        s.PrecargarDatos();
        MostrarMenu();
        bool flag = true;

        while (flag)
        {
            Console.Clear();
            MostrarMenu();
            int nroIngresado = LecturaMenu();
            switch (nroIngresado)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Listar casos con sus evidencias");
                    ListarTodosLosCasos();
                    Console.ReadKey();
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Listar casos de un investigador");
                    ListarCasosxInvestigador();
                    Console.ReadKey();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Dar de alta un sospechoso");
                    AltaSospechoso();
                    Console.ReadKey();
                    break;
                case 4:
                    Console.WriteLine("Listar sospechosos con antecedentes");
                    ListarSospechososConAntecedentes();
                    Console.ReadKey();
                    break;
                case 0:
                    flag = false;
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Ingrese una opcion valida");
                    Console.WriteLine("Presione cualquier tecla para volver al menu");
                    Console.ReadKey();
                    break;
            }
        }

    }
    
    static void MostrarMenu()
    {
        Console.WriteLine("Sistema Investigacion");
        Console.WriteLine("1- Listar todos los casos con sus evidencias");
        Console.WriteLine("2- Listar casos de un investigador (por email)");
        Console.WriteLine("3- Alta de sospechoso");
        Console.WriteLine("4- Listar sospechosos con antecedentes");
        Console.WriteLine("0- Salir");
        Console.WriteLine("Opción: ");
    }

    static int LecturaMenu()
    {
        int valor = 0;
        bool flag = false;
        while (!flag)
        {
            try
            {
                valor = int.Parse(Console.ReadLine());
                flag = true;
            }
            catch (Exception ex)
            {
                Console.Clear();
                MostrarMenu();
                Console.WriteLine("Ingrese un valor valido");
            }
        }

        return valor;
    }

    static void ListarTodosLosCasos()
    {
        foreach (Caso c in s.Casos)
        {
            Console.WriteLine($"Caso, Id: {c.Id}, Nombre: {c.Nombre}");
            foreach (Evidencia e in c.Evidencias)
            {
                Console.WriteLine($"Evidencia, Id: {e.Id}, Descripcion: {e.Descripcion}");
            }
        }
    }

    static void ListarCasosxInvestigador()
    {
        Console.WriteLine("Ingrese el mail del investigador:");
        string email =  Console.ReadLine();
        
        List<Caso> casos = s.ObtenerCasosPorInvestigador(email);

        foreach (Caso c in casos)
        {
            Console.WriteLine($"{c.ToString()}");
        }
    }

    static void AltaSospechoso()
        { 
            Console.Write("Alta Sospechoso");

            Console.WriteLine("Nombre:");
            string nombre = Console.ReadLine();
        
            Console.WriteLine("Cedula");
            string cedula = Console.ReadLine();
        
            Console.WriteLine("Fecha de nacimiento (dd/mm/aaaa):");
            string fechadeNacimiento = Console.ReadLine();
        
            DateTime.TryParse(fechadeNacimiento, out DateTime fechaSalida);
        
            Console.WriteLine("Antecedentes: Si(1) - No(0)");
            int.TryParse(Console.ReadLine(), out int decision);

            bool antecedentes = false;
            bool decisionValida = false;
            while (!decisionValida)
            {
                if (decision == 1)
                {
                    antecedentes = true;
                    decisionValida = true;
                }
                else if (decision == 0)
                {
                    antecedentes = false;
                    decisionValida = true;
                }
                else
                {
                    Console.WriteLine("Ingrese una decision valida");
                }
            }

            Sospechoso sospechoso = new Sospechoso(nombre, cedula, fechaSalida, antecedentes);
            s.AltaSospechoso(sospechoso);
            Console.WriteLine(sospechoso.ToString());
        }

    static void ListarSospechososConAntecedentes()
    {
        List<Sospechoso> listaSospechosos = s.ObtenerSospechososConAntecedentes();

        foreach (Sospechoso sosp in listaSospechosos)
        {
            Console.WriteLine($"{sosp.ToString()}");
        }
    }
    
}