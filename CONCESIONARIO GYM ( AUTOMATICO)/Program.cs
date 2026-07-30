using CONCESIONARIO_GYM___AUTOMATICO_.Data;
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
            // 1. Cargar datos almacenados previamente en JSON
            Database.CargarDatos();

            // 2. Inicialización de entidades y servicios base
            Gimnasio miGimnasio = new Gimnasio("Smart Fit Centro", "Av. Principal 123", "María López", 100);

            // Asignar la lista cargada desde JSON al gimnasio
            miGimnasio.Socios = Database.Socios;

            GestionSocios servicioSocios = new GestionSocios(miGimnasio);
            ControlAcceso servicioAcceso = new ControlAcceso(miGimnasio);
            GestionClases servicioClases = new GestionClases();

            // Asignar la lista de clases cargada desde JSON
            servicioClases.ListaClases = Database.Clases;

            // 3. Carga de datos de prueba iniciales 
            if (Database.Socios.Count == 0)
            {
                miGimnasio.AgregarSocio(new Socio(1, "Carlos Andrade", "0912345678", 22, true, "estandar"));
                miGimnasio.AgregarSocio(new Socio(2, "Ana Gómez", "0987654321", 25, false, "vip"));
                Database.GuardarSocios();
            }

            if (Database.Clases.Count == 0)
            {
                servicioClases.AgregarClase(new ClaseGrupal(101, "Crossfit", "08:00 AM", 15));
                servicioClases.AgregarClase(new ClaseGrupal(102, "Spinning", "05:00 PM", 10));
                Database.GuardarClases();
            }

            if (Database.Clases.Count == 0)
            {
                servicioClases.AgregarClase(new ClaseGrupal(101, "Crossfit", "08:00 AM", 15));
                servicioClases.AgregarClase(new ClaseGrupal(102, "Spinning", "05:00 PM", 10));
                Database.GuardarClases();
            }

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
                            Console.WriteLine("\n--- REGISTRO DE NUEVO SOCIO ---");
                            int id = Validaciones.LeerEnteroPositivo("Ingrese ID del socio: ");
                            if (id == 0)
                            {
                                MenuConsola.MostrarMensajeError("Operación cancelada por el usuario.");
                                break; // Rompe el case y regresa al menú
                            }
                            string nombre = Validaciones.LeerTextoNoVacio("Ingrese Nombre completo: ");
                            string cedula = Validaciones.LeerCedulaValida("Ingrese Cédula (10 dígitos): ");
                            int edad = Validaciones.LeerEnteroPositivo("Ingrese Edad: ");
                            string tipo = Validaciones.LeerTextoNoVacio("Ingrese Tipo de Membresía (estandar/vip): ");

                            Socio nuevoSocio = new Socio(id, nombre, cedula, edad, true, tipo);
                            servicioSocios.RegistrarNuevoSocio(nuevoSocio);
                            Database.GuardarSocios();
                            MenuConsola.MostrarMensajeExito("Socio registrado y guardado correctamente.");
                            break;

                        case "2":
                            Console.WriteLine("\n--- RENOVACIÓN DE MEMBRESÍA ---");
                            string cedulaRenovar = Validaciones.LeerTextoNoVacio("Ingrese Cédula del socio (o '0' para regresar): ");

                            //  Cancelar si es '0'
                            if (cedulaRenovar == "0")
                            {
                                MenuConsola.MostrarMensajeError("Operación cancelada.");
                                break;
                            }

                            servicioSocios.RenovarMembresia(cedulaRenovar);
                            break;

                        case "3":
                            Console.WriteLine("\n--- CONTROL DE ACCESO (CHECK-IN) ---");
                            string cedulaAcceso = Validaciones.LeerTextoNoVacio("Ingrese Cédula para ingresar (o '0' para regresar): ");

                            // Cancelar si es '0'
                            if (cedulaAcceso == "0")
                            {
                                MenuConsola.MostrarMensajeError("Operación cancelada.");
                                break;
                            }

                            servicioAcceso.ValidarIngreso(cedulaAcceso);
                            break;

                        case "4":
                            Console.WriteLine("\n--- CLASES GRUPALES DISPONIBLES ---");
                            servicioClases.MostrarClasesDisponibles();

                            int idClase = Validaciones.LeerEnteroPositivo("Ingrese el ID de la clase que desea reservar (o 0 para volver): ");

                            //  Cancelar si es 0
                            if (idClase == 0)
                            {
                                MenuConsola.MostrarMensajeError("Operación cancelada.");
                                break;
                            }

                            servicioClases.AgendarCupoEnClase(idClase);
                            Database.GuardarClases(); // Guarda la reserva
                            break;

                        case "5":
                            Console.Write("\nIngrese Cédula a buscar: ");
                            string cedulaBuscar = Validaciones.LeerTextoNoVacio("Ingrese Cédula a buscar (o '0' para regresar): ");

                            //  Cancelar si es '0'
                            if (cedulaBuscar == "0")
                            {
                                MenuConsola.MostrarMensajeError("Operación cancelada.");
                                break;
                            }

                            servicioSocios.ConsultarSocioPorCedula(cedulaBuscar);
                            break;

                        case "6":
                            servicioAcceso.MostrarResumenAcceso();
                            break;

                        case "7":
                            ejecutando = false;
                            MenuConsola.MostrarMensajeExito("Gracias por usar GymSmart OS. ¡Hasta pronto!");
                            break;

                        default:
                            MenuConsola.MostrarMensajeError("Opción no válida. Intente de nuevo.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MenuConsola.MostrarMensajeError(ex.Message);
                }

                if (ejecutando)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ResetColor();
                    Console.ReadKey();
                }
            }
        }

         }
    }


            


