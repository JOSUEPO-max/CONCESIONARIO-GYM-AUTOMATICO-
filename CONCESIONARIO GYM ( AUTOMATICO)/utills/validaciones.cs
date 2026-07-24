using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CONCESIONARIO_GYM___AUTOMATICO_.utills
{
    public class Validaciones
    {
        // Valida que el texto ingresado sea un número entero positivo
        public static int LeerEnteroPositivo(string mensaje)
        {
            int valor;
            while (true)
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine();
                if (int.TryParse(entrada, out valor) && valor > 0)
                {
                    return valor;
                }
                Console.WriteLine("[ERROR]: Debe ingresar un número entero positivo válido.");
            }
        }

        // Valida que la cédula tenga exactamente 10 dígitos numéricos
        public static string LeerCedulaValida(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje);
                string cedula = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(cedula) && Regex.IsMatch(cedula, @"^\d{10}$"))
                {
                    return cedula;
                }
                Console.WriteLine("[ERROR]: La cédula debe contener exactamente 10 dígitos numéricos.");
            }
        }

        // Valida que la cadena ingresada no esté vacía ni contenga solo espacios
        public static string LeerTextoNoVacio(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje);
                string texto = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(texto))
                {
                    return texto;
                }
                Console.WriteLine("[ERROR]: Este campo no puede quedar vacío.");
            }
        }
    }
}
