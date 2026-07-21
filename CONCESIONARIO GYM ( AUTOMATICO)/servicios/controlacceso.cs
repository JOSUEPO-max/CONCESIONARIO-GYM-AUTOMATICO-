using CONCESIONARIO_GYM___AUTOMATICO_.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CONCESIONARIO_GYM___AUTOMATICO_.servicios
{
    public class ControlAcceso
    {
        private Gimnasio gimnasio;
        private int accesosExitosos;
        private int accesosDenegados;

        public Gimnasio Gimnasio { get => gimnasio; set => gimnasio = value; }
        public int AccesosExitosos { get => accesosExitosos; set => accesosExitosos = value; }
        public int AccesosDenegados { get => accesosDenegados; set => accesosDenegados = value; }

        public ControlAcceso(Gimnasio gimnasio)
        {
            if (gimnasio == null)
            {
                throw new Exception("El control de acceso requiere un gimnasio válido.");
            }
            this.Gimnasio = gimnasio;
            this.AccesosExitosos = 0;
            this.AccesosDenegados = 0;
        }

        public bool ValidarIngreso(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula))
            {
                throw new Exception("Debe ingresar una cédula válida para verificar el acceso.");
            }

            foreach (Socio objSocio in this.Gimnasio.Socios)
            {
                if (objSocio.Cedula == cedula)
                {
                    if (objSocio.EstadoMembresia)
                    {
                        Console.WriteLine($"[ACCESO PERMITIDO] Bienvenido/a {objSocio.Nombre}. Disfruta tu entrenamiento.");
                        this.AccesosExitosos++;
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"[ACCESO DENEGADO] El socio {objSocio.Nombre} tiene la membresía VENCIDA.");
                        this.AccesosDenegados++;
                        return false;
                    }
                }
            }

            Console.WriteLine("[ERROR] No se encontró ningún socio registrado con esa cédula.");
            this.AccesosDenegados++;
            return false;
        }

        public void MostrarResumenAcceso()
        {
            Console.WriteLine($"--- RESUMEN DE ACCESOS DEL DÍA ---");
            Console.WriteLine($"Ingresos permitidos: {this.AccesosExitosos}");
            Console.WriteLine($"Ingresos denegados: {this.AccesosDenegados}");
        }
    }
}