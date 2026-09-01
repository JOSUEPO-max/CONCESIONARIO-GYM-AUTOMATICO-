using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CONCESIONARIO_GYM___AUTOMATICO_.models;
using CONCESIONARIO_GYM___AUTOMATICO_.servicios;
namespace CONCESIONARIO_GYM___AUTOMATICO_.Data
{
    public static class Database
    {
        private static readonly string rutaCarpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Datos");
        private static readonly string rutaArchivoSocios = Path.Combine(rutaCarpeta, "socios.json");
        private static readonly string rutaArchivoClases = Path.Combine(rutaCarpeta, "clases.json");
        private static readonly string rutaArchivoEntrenadores = Path.Combine(rutaCarpeta, "entrenadores.json");
        // 🔴 1. NUEVA RUTA PARA ACCESOS
        private static readonly string rutaArchivoAccesos = Path.Combine(rutaCarpeta, "accesos.json");

        public static List<Socio> Socios = new List<Socio>();
        public static List<ClaseGrupal> Clases = new List<ClaseGrupal>();
        public static List<Entrenador> Entrenadores = new List<Entrenador>();
        // 🔴 2. NUEVA LISTA GLOBAL DE ACCESOS
        public static List<RegistroAcceso> Accesos = new List<RegistroAcceso>();

        public static void CargarDatos()
        {
            if (!Directory.Exists(rutaCarpeta))
            {
                Directory.CreateDirectory(rutaCarpeta);
            }
            Socios = ArchivoJson.Cargar<Socio>(rutaArchivoSocios);
            Clases = ArchivoJson.Cargar<ClaseGrupal>(rutaArchivoClases);
            Entrenadores = ArchivoJson.Cargar<Entrenador>(rutaArchivoEntrenadores);
            // 🔴 3. CARGAR ACCESOS
            Accesos = ArchivoJson.Cargar<RegistroAcceso>(rutaArchivoAccesos);
        }

        public static void GuardarDatos()
        {
            ArchivoJson.Guardar(rutaArchivoSocios, Socios);
            ArchivoJson.Guardar(rutaArchivoClases, Clases);
            ArchivoJson.Guardar(rutaArchivoEntrenadores, Entrenadores);
            // 🔴 4. GUARDAR ACCESOS EN GUARDARDATOS
            ArchivoJson.Guardar(rutaArchivoAccesos, Accesos);
        }

        public static void GuardarSocios()
        {
            ArchivoJson.Guardar(rutaArchivoSocios, Socios);
        }

        public static void GuardarClases()
        {
            ArchivoJson.Guardar(rutaArchivoClases, Clases);
        }

        public static void GuardarEntrenadores()
        {
            ArchivoJson.Guardar(rutaArchivoEntrenadores, Entrenadores);
        }

        // 🔴 5. MÉTODO EXCLUSIVO PARA GUARDAR ACCESOS
        public static void GuardarAccesos()
        {
            ArchivoJson.Guardar(rutaArchivoAccesos, Accesos);
        }
    }
}

