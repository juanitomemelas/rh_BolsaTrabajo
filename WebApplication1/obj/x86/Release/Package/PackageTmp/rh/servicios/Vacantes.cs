using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RHVacantes.rh.servicios
{
    public class Registro
    {
        public int Id { get; set; }
        public string Puesto{ get; set; }
        public string Descripcion { get; set; }
        public string Ubicacion { get; set; }
        public string Tipo_Contrato { get; set; }
        public string Rango_Edad{ get; set; }
        public string Competencias{ get; set; }
        public string Sexo { get; set; }
        public string Status { get; set; }
    }
    public class Locacion
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }
       
    }
    }