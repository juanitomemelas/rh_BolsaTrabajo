using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using RHVacantes.rh.servicios;
using RHVacantes.Clases;
using System.Collections.Specialized;
using System.Data;

[ScriptService]
public class MyServiceClass
{
    // EnableSession = true
    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string CambiosVacantes(String id, String puesto, String descripcion, String ubicacion, String contrato, String rango, String competencias, String sexo, String status)
    {
        // EnableSession = true
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = "application/json";
        Registro vacante = new Registro();
        UtilesOracle actualizar = new UtilesOracle();
        Vacante vacanteActualizar = new Vacante();
        vacanteActualizar.Id = id;
        vacanteActualizar.Puesto = puesto;
        vacanteActualizar.Descripcion = descripcion;
        vacanteActualizar.Ubicacion = ubicacion;
        vacanteActualizar.Contrato = contrato;
        vacanteActualizar.Rango = rango;
        vacanteActualizar.Competencias = competencias;
        vacanteActualizar.Sexo = sexo;
        vacanteActualizar.Status = status;
        if (actualizar.actualizaRegistroVacante(vacanteActualizar))
        {
            DataTable tablaDatos = actualizar.obtieneVacantes(id);
            foreach (DataRow row in tablaDatos.Rows)
            {
                vacante.Id = Convert.ToInt32(row["ID"]);
                vacante.Puesto = validaValor(row,"PUESTO");
                vacante.Descripcion = validaValor(row, "DESCRIPCION");
                vacante.Ubicacion = validaValor(row,"UBICACION");
                vacante.Tipo_Contrato = validaValor(row, "TIPO_CONTRATO");
                vacante.Rango_Edad = validaValor(row,"RANGO_EDAD");
                vacante.Competencias = validaValor(row,"COMPETENCIAS");
                vacante.Sexo = regresaGenero(validaValor(row, "SEXO"));
                vacante.Status = "V".Equals(validaValor(row,"STATUS"))?"Vigente":"Cerrado";
            }
        }       
        return new JavaScriptSerializer().Serialize(vacante);
    }
    public String regresaGenero(String sexo)
    {
        String genero = "";
        switch (sexo)
        {
            case "I":
                genero = "Indistinto";
                break;
            case "M":
                genero = "Masculino";
                break;
            case "F":
                genero = "Femenino";
                break;
        }
        return genero;
    }
    public String validaValor(DataRow registro, String valorDeseado)
    {
        String cadena = "";
        if (! DBNull.Value.Equals(registro[valorDeseado])) {
            cadena = (string)registro[valorDeseado];
        }
        return cadena;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public String obtieneLocaciones()
    {
       // List<Subject> subjects = new List<Subject>();
        //subjects.add(new Subject{....});
        DataTable tablaDatos = new DataTable();
        List<Locacion> locaciones = new List <Locacion>();
        UtilesOracle lasLocaciones = new UtilesOracle();
        tablaDatos = lasLocaciones.locaciones();
        foreach (DataRow row in tablaDatos.Rows)
        {
            locaciones.Add(new Locacion { Id = validaValor(row, "ID"), Descripcion = validaValor(row, "LOCACION") });
        }

        return new JavaScriptSerializer().Serialize(locaciones);
    }
}