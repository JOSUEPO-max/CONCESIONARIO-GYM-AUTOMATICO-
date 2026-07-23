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
            Console.WriteLine("==================================================");
            Console.WriteLine("       GYMSMART OS - CONTROL DE GIMNASIO          ");
            Console.WriteLine("==================================================");
        }

        public static void MostrarMenuPrincipal()
        {
            Console.WriteLine("\n--- MENÚ PRINCIPAL ---");
            Console.WriteLine("1. Registrar Nuevo Socio");
            Console.WriteLine("2. Renovar Membresía");
            Console.WriteLine("3. Validar Ingreso Diario (Check-in)");
            Console.WriteLine("4. Ver Clases Grupales y Reservar Cupo");
            Console.WriteLine("5. Consultar Ficha de Socio");
            Console.WriteLine("6. Ver Reporte de Accesos del Día");
            Console.WriteLine("7. Salir del Sistema");
            Console.WriteLine("==================================================");
            Console.Write("Seleccione una opción (1-7): ");
        }
    }
}
