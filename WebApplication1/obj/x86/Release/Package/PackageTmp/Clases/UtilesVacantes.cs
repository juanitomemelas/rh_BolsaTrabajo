using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RHVacantes.Clases
{
    public struct Vacante
    {
        public string Id;
        public string Puesto; 
        public string Descripcion; 
        public string Ubicacion; 
        public string Contrato; 
        public string Rango; 
        public string Competencias;
        public string Sexo;
        public string Status;
        public Vacante(string id,string puesto, string descripcion, string ubicacion, string contrato, string rango, string competencias, string sexo, string status)
        {
            Id = id;
            Puesto = puesto;
            Descripcion = descripcion;
            Ubicacion = ubicacion;
            Contrato = contrato;
            Rango = rango;
            Competencias = competencias;
            Sexo = sexo;
            Status = status;

        }
    }
    public struct Locaciones
    {
        public string Id;
        public string Descripcion; 
     
        public Locaciones(string id,string descripcion)
        {
            Id = id;           
            Descripcion = descripcion;
        }
    }
    public class UtilesVacantes
    {
       
    }
}