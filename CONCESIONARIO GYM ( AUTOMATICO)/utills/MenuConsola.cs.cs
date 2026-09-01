using System;
using System.Collections.Generic;
using System.Text;

namespace CONCESIONARIO_GYM___AUTOMATICO_.utills
{
    public class MenuConsola
    {
        public static void MostrarBanner()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
  ██████╗ ██╗   ██╗███╗   ███╗███████╗███╗   ███╗ █████╗ ██████╗ ████████╗    ██████╗ ███████╗
  ██╔════╝ ╚██╗ ██╔╝████╗ ████║██╔════╝████╗ ████║██╔══██╗██╔══██╗╚══██╔══╝   ██╔═══██╗██╔════╝
  ██║  ███╗ ╚████╔╝ ██╔████╔██║███████╗██╔████╔██║███████║██████╔╝   ██║      ██║   ██║███████╗
  ██║   ██║  ╚██╔╝  ██║╚██╔╝██║╚════██║██║╚██╔╝██║██╔══██║██╔══██╗   ██║      ██║   ██║╚════██║
  ╚██████╔╝   ██║   ██║ ╚═╝ ██║███████║██║ ╚═╝ ██║██║  ██║██║  ██║   ██║      ╚██████╔╝███████║
   ╚═════╝    ╚═╝   ╚═╝     ╚═╝╚══════╝╚═╝     ╚═╝╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝       ╚═════╝ ╚══════╝
        ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  ==========================================================================================");
            Console.ResetColor();
        }

        public static void MostrarMenuPrincipal()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n +------------------------- PANEL DE CONTROL PRINCIPAL -------------------------+");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" |                                                                               |");
            Console.WriteLine(" |   [1] Registrar Nuevo Socio                                                   |");
            Console.WriteLine(" |   [2] Renovar Membresia                                                       |");
            Console.WriteLine(" |   [3] Validar Ingreso Diario (Check-in)                                       |");
            Console.WriteLine(" |   [4] Ver Clases Grupales y Reservar Cupo                                     |");
            Console.WriteLine(" |   [5] Consultar Ficha de Socio                                                |");
            Console.WriteLine(" |   [6] Ver Reporte de Accesos del Dia                                          |");
            Console.WriteLine(" |   [7] Consultar Asistente IA (OpenAI)                                         |");
            Console.WriteLine(" |   [8] Salir del Sistema                                                       |");
            Console.WriteLine(" |                                                                               |");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" +-------------------------------------------------------------------------------+");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\n >> Seleccione una opcion (1-8): ");
            Console.ResetColor();
        }

        public static void MostrarMensajeExito(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  ✔ [ÉXITO]: {mensaje}");
            Console.ResetColor();
        }

        public static void MostrarMensajeError(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n  ✖ [ERROR]: {mensaje}");
            Console.ResetColor();
        }
    }
}
