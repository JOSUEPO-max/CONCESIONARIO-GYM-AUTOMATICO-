using System;
using System.Collections.Generic;
using System.Text;

namespace CONCESIONARIO_GYM___AUTOMATICO_.IA
{
    public class RespuestaIA
    {
        public string Texto { get; set; }
        public DateTime Fecha { get; set; }

        public RespuestaIA(string texto)
        {
            Texto = texto;
            Fecha = DateTime.Now;
        }
    }
}
