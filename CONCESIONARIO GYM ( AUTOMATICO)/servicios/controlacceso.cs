using CONCESIONARIO_GYM___AUTOMATICO_.Data;
using CONCESIONARIO_GYM___AUTOMATICO_.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CONCESIONARIO_GYM___AUTOMATICO_.servicios
{
    public class ControlAcceso
    {
        private Gimnasio gimnasio;

        public Gimnasio Gimnasio { get => gimnasio; set => gimnasio = value; }

        // Propiedades calculadas en tiempo real desde los datos persistentes guardados en la BD/JSON
        public int AccesosExitosos
        {
            get => Database.Accesos.Count(a => a.Permitido && a.FechaHora.Date == DateTime.Today);
        }

        public int AccesosDenegados
        {
            get => Database.Accesos.Count(a => !a.Permitido && a.FechaHora.Date == DateTime.Today);
        }

        public ControlAcceso(Gimnasio gimnasio)
        {
            if (gimnasio == null)
            {
                throw new Exception("El control de acceso requiere un gimnasio válido.");
            }
            this.Gimnasio = gimnasio;
        }

        public bool ValidarIngreso(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula))
            {
                throw new Exception("Debe ingresar una cédula válida para verificar el acceso.");
            }

            bool permitido = false;
            string motivo = "";
            Socio socioEncontrado = null;

            foreach (Socio objSocio in this.Gimnasio.Socios)
            {
                if (objSocio.Cedula == cedula)
                {
                    socioEncontrado = objSocio;
                    break;
                }
            }

            if (socioEncontrado != null)
            {
                if (socioEncontrado.EstadoMembresia)
                {
                    Console.WriteLine($"[ACCESO PERMITIDO] Bienvenido/a {socioEncontrado.Nombre}. Disfruta tu entrenamiento.");
                    permitido = true;
                    motivo = "Acceso Concedido";
                }
                else
                {
                    Console.WriteLine($"[ACCESO DENEGADO] El socio {socioEncontrado.Nombre} tiene la membresía VENCIDA.");
                    permitido = false;
                    motivo = "Membresía Vencida";
                }
            }
            else
            {
                Console.WriteLine("[ERROR] No se encontró ningún socio registrado con esa cédula.");
                permitido = false;
                motivo = "Socio No Encontrado";
            }

            // 🔴 GUARDAR ACCESO DE FORMA PERMANENTE EN EL JSON
            RegistroAcceso nuevoRegistro = new RegistroAcceso
            {
                CedulaSocio = cedula,
                NombreSocio = socioEncontrado != null ? socioEncontrado.Nombre : "Desconocido",
                FechaHora = DateTime.Now,
                Permitido = permitido,
                Motivo = motivo
            };

            Database.Accesos.Add(nuevoRegistro);
            Database.GuardarAccesos();

            return permitido;
        }

        public void MostrarResumenAcceso()
        {
            Console.WriteLine($"--- RESUMEN DE ACCESOS DEL DÍA ---");
            Console.WriteLine($"Ingresos permitidos: {this.AccesosExitosos}");
            Console.WriteLine($"Ingresos denegados: {this.AccesosDenegados}");
        }
    }
}