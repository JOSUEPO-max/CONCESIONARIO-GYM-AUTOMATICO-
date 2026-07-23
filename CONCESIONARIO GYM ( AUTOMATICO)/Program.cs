using CONCESIONARIO_GYM___AUTOMATICO_.models;
using CONCESIONARIO_GYM___AUTOMATICO_.servicios;
using CONCESIONARIO_GYM___AUTOMATICO_.utills;
using System;
using System.Collections.Generic;
using System.Text;

namespace CONCESIONARIO_GYM___AUTOMATICO_
{
    class Program
    {
        static void Main(string[] args)
        {
            // Inicialización de entidades y servicios base
            Gimnasio miGimnasio = new Gimnasio("Smart Fit Centro", "Av. Principal 123", "María López", 100);
            GestionSocios servicioSocios = new GestionSocios(miGimnasio);
            ControlAcceso servicioAcceso = new ControlAcceso(miGimnasio);
            GestionClases servicioClases = new GestionClases();

            // Carga de datos de prueba iniciales
            miGimnasio.AgregarSocio(new Socio(1, "Carlos Andrade", "0912345678", 22, true, "estandar"));
            miGimnasio.AgregarSocio(new Socio(2, "Ana Gómez", "0987654321", 25, false, "vip"));

            servicioClases.AgregarClase(new ClaseGrupal(101, "Crossfit", "08:00 AM", 15));
            servicioClases.AgregarClase(new ClaseGrupal(102, "Spinning", "05:00 PM", 10));

            bool ejecutando = true;

            while (ejecutando)
            {
                MenuConsola.MostrarBanner();
                MenuConsola.MostrarMenuPrincipal();
                string opcion = Console.ReadLine();

                try
                {
                    switch (opcion)
                    {
                        case "1":
                            Console.Write("\nIngrese ID del socio: ");
                            int id = int.Parse(Console.ReadLine());
                            Console.Write("Ingrese Nombre: ");
                            string nombre = Console.ReadLine();
                            Console.Write("Ingrese Cédula: ");
                            string cedula = Console.ReadLine();
                            Console.Write("Ingrese Edad: ");
                            int edad = int.Parse(Console.ReadLine());
                            Console.Write("Ingrese Tipo de Membresía (estandar/vip): ");
                            string tipo = Console.ReadLine();

                            Socio nuevoSocio = new Socio(id, nombre, cedula, edad, true, tipo);
                            servicioSocios.RegistrarNuevoSocio(nuevoSocio);
                            break;

                        case "2":
                            Console.Write("\nIngrese Cédula del socio para renovar: ");
                            string cedulaRenovar = Console.ReadLine();
                            servicioSocios.RenovarMembresia(cedulaRenovar);
                            break;

                        case "3":
                            Console.Write("\n[CHECK-IN] Ingrese Cédula del socio: ");
                            string cedulaAcceso = Console.ReadLine();
                            servicioAcceso.ValidarIngreso(cedulaAcceso);
                            break;


                        case "4":
                            servicioClases.MostrarClasesDisponibles();
                            Console.Write("\nIngrese el ID de la clase que desea reservar (o 0 para volver): ");
                            int idClase = int.Parse(Console.ReadLine());
                            if (idClase != 0)
                            {
                                servicioClases.AgendarCupoEnClase(idClase);
                            }
                            Console.WriteLine("\nPresione cualquier tecla para continuar...");
                            Console.ReadKey();
                            break;

                        case "5":
                            Console.Write("\nIngrese Cédula a buscar: ");
                            string cedulaBuscar = Console.ReadLine();
                            servicioSocios.ConsultarSocioPorCedula(cedulaBuscar);
                            break;

                        case "6":
                            servicioAcceso.MostrarResumenAcceso();
                            Console.WriteLine("\nPresione cualquier tecla para continuar...");
                            Console.ReadKey();
                            break;

                        case "7":
                            ejecutando = false;
                            Console.WriteLine("\nGracias por usar GymSmart OS. ¡Hasta pronto!");
                            break;

                        default:
                            Console.WriteLine("\nOpción no válida. Intente de nuevo.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[ERROR]: {ex.Message}");
                }

                if (ejecutando && opcion != "4" && opcion != "6")
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }
    }
}