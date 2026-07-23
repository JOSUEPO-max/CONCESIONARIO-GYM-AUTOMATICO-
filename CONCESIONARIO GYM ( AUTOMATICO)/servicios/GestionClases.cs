using CONCESIONARIO_GYM___AUTOMATICO_.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CONCESIONARIO_GYM___AUTOMATICO_.servicios
{
    public class GestionClases
    {
        private List<ClaseGrupal> listaClases;

        public List<ClaseGrupal> ListaClases { get => listaClases; set => listaClases = value; }

        public GestionClases()
        {
            this.ListaClases = new List<ClaseGrupal>();
        }

        public void AgregarClase(ClaseGrupal objClase)
        {
            if (objClase == null)
            {
                throw new Exception("No se puede agregar una clase nula.");
            }
            this.ListaClases.Add(objClase);
            Console.WriteLine($"Clase {objClase.NombreDisciplina} registrada exitosamente.");
        }

        public void MostrarClasesDisponibles()
        {
            Console.WriteLine("--- CLASES GRUPALES DISPONIBLES ---");
            foreach (ClaseGrupal objClase in this.ListaClases)
            {
                objClase.MostrarDetalle();
            }
        }

        public void AgendarCupoEnClase(int idClase)
        {
            foreach (ClaseGrupal objClase in this.ListaClases)
            {
                if (objClase.Id == idClase)
                {
                    objClase.ReservarCupo();
                    return;
                }
            }
            Console.WriteLine("Error: El código de clase ingresado no existe.");
        }
    }
}
