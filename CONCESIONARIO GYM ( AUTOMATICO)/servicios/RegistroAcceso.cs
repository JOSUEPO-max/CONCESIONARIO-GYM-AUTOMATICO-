using System;
using System.Collections.Generic;
using System.Text;

namespace CONCESIONARIO_GYM___AUTOMATICO_.servicios
{
    public class RegistroAcceso
    {
        public string CedulaSocio { get; set; } = string.Empty;
        public string NombreSocio { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; } = DateTime.Now;
        public bool Permitido { get; set; }
        public string Motivo { get; set; } = string.Empty;

        // Constructor vacío necesario para la deserialización JSON
        public RegistroAcceso() { }

        // Constructor para inicialización rápida
        public RegistroAcceso(string cedula, string nombre, bool permitido, string motivo)
        {
            CedulaSocio = cedula;
            NombreSocio = nombre;
            FechaHora = DateTime.Now;
            Permitido = permitido;
            Motivo = motivo;
        }
    }
}
