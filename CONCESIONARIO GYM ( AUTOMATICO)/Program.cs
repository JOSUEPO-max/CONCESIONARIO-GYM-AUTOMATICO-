using CONCESIONARIO_GYM___AUTOMATICO_.Data;
using CONCESIONARIO_GYM___AUTOMATICO_.IA;
using CONCESIONARIO_GYM___AUTOMATICO_.models;
using CONCESIONARIO_GYM___AUTOMATICO_.servicios;
using CONCESIONARIO_GYM___AUTOMATICO_.utills;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            servicioClases.ListaClases = Database.Clases;



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
                            Console.WriteLine("\n--- REGISTRAR NUEVO SOCIO ---");

                            // Uso de tus métodos de validación
                            string cedula = Validaciones.LeerCedulaValida("Ingrese Cédula: ");
                            string nombre = Validaciones.LeerTextoNoVacio("Ingrese Nombre: ");
                            int edad = Validaciones.LeerEnteroPositivo("Ingrese Edad: ");
                            string tipo = Validaciones.LeerTextoNoVacio("Ingrese Tipo de Membresía (estandar/vip): ");
                            string correo = Validaciones.LeerTextoNoVacio("Ingrese Correo: ");
                            string telefono = Validaciones.LeerTextoNoVacio("Ingrese Teléfono: ");

                            int nuevoId = Database.Socios.Count + 1;
                            DateTime fechaHoy = DateTime.Now;

                            // 1. Instancia del socio con membresía activa
                            Socio nuevoSocio = new Socio(nuevoId, nombre, cedula, edad, true, tipo, correo, telefono);

                            // 2. Guardar e impulsar la persistencia en JSON
                            Database.Socios.Add(nuevoSocio);
                            Database.GuardarSocios();

                            Console.WriteLine("\n[ÉXITO] Socio registrado correctamente.");

                            // 3. Enviar correo de recibo por $20
                            Console.WriteLine("Enviando comprobante de pago por correo...");
                            ServicioEmail emailService = new ServicioEmail();
                            emailService.EnviarComprobantePago(nuevoSocio, 20.00m, fechaHoy);

                            // 4. Enviar SMS de confirmación
                            Console.WriteLine("Enviando SMS de bienvenida...");
                            ServicioSMS smsService = new ServicioSMS();
                            smsService.EnviarSmsConfirmacion(nuevoSocio.Telefono, nuevoSocio.Nombre);

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
                            Console.WriteLine("\n--- VALIDAR INGRESO DIARIO (CHECK-IN) ---");
                            string cedulaIngreso = Validaciones.LeerTextoNoVacio("Ingrese Cédula del socio: ");

                            // 1. Usar el método real de tu clase: ValidarIngreso
                            bool permitido = servicioAcceso.ValidarIngreso(cedulaIngreso);

                            // 2. Guardar accesos (sin pasar parámetros)
                            Database.GuardarAccesos();
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

                            // Pedimos la cédula del socio para asociarlo a la reserva
                            string cedulaReserva = Validaciones.LeerTextoNoVacio("Ingrese la Cédula del socio que reserva: ");

                            // Buscamos si el socio existe en la lista del gimnasio
                            Socio socioQueReserva = miGimnasio.Socios.FirstOrDefault(s => s.Cedula == cedulaReserva);

                            if (socioQueReserva != null)
                            {
                                bool reservado = servicioClases.AgendarCupoEnClase(idClase, socioQueReserva);
                                if (reservado)
                                {
                                    Database.GuardarClases(); // Guarda la reserva en el JSON
                                    MenuConsola.MostrarMensajeExito($"Cupo agendado con éxito para {socioQueReserva.Nombre}.");
                                }
                            }
                            else
                            {
                                MenuConsola.MostrarMensajeError("No se encontró ningún socio con esa cédula. Debe registrarlo primero.");
                            }
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

                            //  Le pasamos 'Database.Clases' para que muestre las clases reservadas en su ficha
                            servicioSocios.ConsultarSocioPorCedula(cedulaBuscar, Database.Clases);
                            break;

                        case "6":
                            servicioAcceso.MostrarResumenAcceso();
                            break;

                        case "7":
                            Console.WriteLine("\n--- ASISTENTE VIRTUAL IA ---");
                            string pregunta = Validaciones.LeerTextoNoVacio("Ingrese su consulta: ");

                            Console.WriteLine("\n[PROCESANDO] Consultando a OpenAI...");

                            var respuesta = ServicioIA.PreguntarAsync(pregunta).GetAwaiter().GetResult();

                            Console.WriteLine("\n==================================================");
                            Console.WriteLine($"[RESPUESTA IA - {respuesta.Fecha:HH:mm:ss}]:\n{respuesta.Texto}");
                            Console.WriteLine("==================================================");
                            break;

                        case "8":
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


            


