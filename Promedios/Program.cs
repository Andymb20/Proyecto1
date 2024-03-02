using System;
using System.Collections.Generic;

class Program

    // se agrega en listas
{
    static List<Estudiante> estudiantes = new List<Estudiante>();

    static void Main(string[] args)
    {
        int opcion;
        do
        {
            MostrarMenu();
            opcion = LeerOpcion();
            ProcesarOpcion(opcion);
        } while (opcion != 7);
    }

    static void MostrarMenu()
    {
        Console.WriteLine("Menú Principal");
        Console.WriteLine("1- Inicializar Vectores");
        Console.WriteLine("2- Incluir Estudiantes");
        Console.WriteLine("3- Consultar Estudiantes");
        Console.WriteLine("4- Modificar Estudiantes");
        Console.WriteLine("5- Eliminar Estudiantes");
        Console.WriteLine("6- Submenu Reportes");
        Console.WriteLine("7- Salir");
    }

    static int LeerOpcion()
    {
        int opcion;
        Console.Write("Seleccione una opción: ");
        while (!int.TryParse(Console.ReadLine(), out opcion) || opcion < 1 || opcion > 7)
        {
            Console.WriteLine("Opción inválida. Intente de nuevo.");
            Console.Write("Seleccione una opción: ");
        }
        return opcion;
    }


    //menu a base del switch
    static void ProcesarOpcion(int opcion)
    {
        switch (opcion)
        {
            case 1:
                InicializarVectores();
                break;
            case 2:
                IncluirEstudiantes();
                break;
            case 3:
                ConsultarEstudiantes();
                break;
            case 4:
                ModificarEstudiante();
                break;
            case 5:
                EliminarEstudiantes();
                break;
            case 6:
                MostrarSubMenuReportes();
                break;
            case 7:
                Console.WriteLine("Saliendo del sistema. ¡Hasta luego!");
                break;
        }
    }

    static void InicializarVectores()
    {
        estudiantes.Clear();
        Console.WriteLine("Vectores inicializados correctamente.");
    }

    static void IncluirEstudiantes()
    {
        Console.WriteLine("Ingrese la cantidad de estudiantes a incluir:");
        int cantidad = int.Parse(Console.ReadLine());
        for (int i = 0; i < cantidad; i++)
        {
            Console.WriteLine($"Ingrese los datos del estudiante {i + 1}:");
            int cedula;
            string nombre;
            float promedio;

            // se utiliza try catch para la entrada incorrecta de la cédula ya sea por que es un string o si es la misma
            try
            {
                Console.Write("Cédula: ");
                cedula = int.Parse(Console.ReadLine());
                // Verificamos si la cédula ya existe en la lista de estudiantes
                if (estudiantes.Exists(estudiante => estudiante.Cedula == cedula))
                {
                    Console.WriteLine("Error: La cédula ingresada ya ha sido registrada anteriormente. Por favor, ingrese una nueva cédula.");
                    i--; // Decrementamos el contador para que el bucle repita esta iteración
                    continue; // Saltamos al próximo ciclo del bucle
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: La cédula debe ser un número entero.");
                i--; // Decrementamos el contador para que el bucle repita esta iteración
                continue; // Saltamos al próximo ciclo del bucle
            }

            Console.Write("Nombre: ");
            nombre = Console.ReadLine();

            // Capturamos la excepción para manejar la entrada incorrecta del promedio
            try
            {
                Console.Write("Promedio: ");
                promedio = float.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: El promedio debe ser un número decimal (puede incluir decimales separados por punto o coma).");
                i--; // Decrementamos el contador para que el bucle repita esta iteración
                continue; // Saltamos al próximo ciclo del bucle
            }

            estudiantes.Add(new Estudiante(cedula, nombre, promedio));
        }
        Console.WriteLine("Estudiantes incluidos correctamente.");
    }

    static void ConsultarEstudiantes()
    {
        Console.WriteLine("Lista de estudiantes:");
        estudiantes.ForEach(Console.WriteLine);
    }

    static void ModificarEstudiante()
    {
        Console.Write("Ingrese la cédula del estudiante a modificar: ");
        int cedula = int.Parse(Console.ReadLine());
        var estudiante = estudiantes.Find(e => e.Cedula == cedula);
        if (estudiante != null)
        {
            Console.WriteLine("Seleccione qué desea modificar:");
            Console.WriteLine("1- Cédula");
            Console.WriteLine("2- Nombre");
            Console.WriteLine("3- Promedio");
            Console.Write("Seleccione una opción: ");

            int opcionModificacion;
            while (!int.TryParse(Console.ReadLine(), out opcionModificacion) || opcionModificacion < 1 || opcionModificacion > 3)
            {
                Console.WriteLine("Opción inválida. Intente de nuevo.");
                Console.Write("Seleccione una opción: ");
            }

            switch (opcionModificacion)
            {
                case 1:
                    ModificarCedula(estudiante);
                    break;
                case 2:
                    Console.Write("Nuevo nombre: ");
                    estudiante.Nombre = Console.ReadLine();
                    Console.WriteLine("Estudiante modificado correctamente.");
                    break;
                case 3:
                    Console.Write("Nuevo promedio: ");
                    estudiante.Promedio = float.Parse(Console.ReadLine());
                    Console.WriteLine("Estudiante modificado correctamente.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("No se encontró ningún estudiante con esa cédula.");
        }
    }

    static void ModificarCedula(Estudiante estudiante)
    {
        Console.Write("Ingrese la nueva cédula: ");
        int nuevaCedula = int.Parse(Console.ReadLine());

        if (nuevaCedula == estudiante.Cedula)
        {
            Console.WriteLine("Error: La nueva cédula es igual a la cédula actual del estudiante.");
            Console.WriteLine("Por favor, ingrese una cédula diferente.");
        }
        else
        {
            estudiante.Cedula = nuevaCedula;
            Console.WriteLine("Estudiante modificado correctamente.");
        }
    }

    static void EliminarEstudiantes()
    {
        Console.Write("Ingrese la cédula del estudiante a eliminar: ");
        int cedula = int.Parse(Console.ReadLine());
        var estudiante = estudiantes.Find(e => e.Cedula == cedula);
        if (estudiante != null)
        {
            estudiantes.Remove(estudiante);
            Console.WriteLine("Estudiante eliminado correctamente.");
        }
        else
        {
            Console.WriteLine("No se encontró ningún estudiante con esa cédula.");
        }
    }

    static void MostrarSubMenuReportes()
    {
        int opcionReporte;
        do
        {
            Console.WriteLine("Submenú de Reportes");
            Console.WriteLine("1- Estudiantes por Condición");
            Console.WriteLine("2- Todos los Datos");
            Console.WriteLine("3- Regresar al Menú Principal");
            Console.Write("Seleccione una opción: ");

            while (!int.TryParse(Console.ReadLine(), out opcionReporte) || opcionReporte < 1 || opcionReporte > 3)
            {
                Console.WriteLine("Opción inválida. Intente de nuevo.");
                Console.Write("Seleccione una opción: ");
            }

            switch (opcionReporte)
            {
                case 1:
                    MostrarEstudiantesPorCondicion();
                    break;
                case 2:
                    MostrarTodosLosDatos();
                    break;
            }
        } while (opcionReporte != 3);
    }

    static void MostrarEstudiantesPorCondicion()
    {
        Console.WriteLine("Seleccione la condición de los estudiantes a mostrar:");
        Console.WriteLine("1 - Aprobados");
        Console.WriteLine("2 - Reprobados");
        int opcionCondicion = int.Parse(Console.ReadLine());

        string condicionBusqueda = opcionCondicion == 1 ? "APROBADO" : "REPROBADO";

        var estudiantesFiltrados = estudiantes.FindAll(e => e.Condicion.ToUpper() == condicionBusqueda);
        if (estudiantesFiltrados.Count == 0)
        {
            Console.WriteLine("No hay estudiantes con esa condición.");
        }
        else
        {
            Console.WriteLine("Lista de estudiantes:");
            estudiantesFiltrados.ForEach(Console.WriteLine);
        }
    }

    static void MostrarTodosLosDatos()
    {
        Console.WriteLine("Lista de todos los estudiantes:");
        estudiantes.ForEach(Console.WriteLine);
    }
}
//se declaran los get y set
class Estudiante
{
    public int Cedula { get; set; }
    public string Nombre { get; set; }
    public float Promedio { get; set; }
    public string Condicion => Promedio >= 70 ? "APROBADO" : "REPROBADO";

    public Estudiante(int cedula, string nombre, float promedio)
    {
        Cedula = cedula;
        Nombre = nombre;
        Promedio = promedio;
    }

    public override string ToString()
    {
        return $"Cédula: {Cedula}, Nombre: {Nombre}, Promedio: {Promedio}, Condición: {Condicion}";
    }
}
