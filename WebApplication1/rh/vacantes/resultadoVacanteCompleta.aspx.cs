using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using RH_VacantesWeb.Clases;

using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing; 

namespace RHVacantes.rh.vacantes
{
    public partial class resultadoVacanteCompleta : System.Web.UI.Page
    {      
        
        protected void Page_Load(object sender, EventArgs e)
        {
            RellenaASP();
        }

   
        public void RellenaASP()
        {
            string strHTMLInfoContacto = string.Empty;
            BDC objCnn = new BDC();

            try
            {
                using (OracleConnection cnn = objCnn.getConection())
                {
                    
                    String consecutivo = Request.QueryString["vacante"];
                    if(!"".Equals(consecutivo)){
                    try
                    {
                        candidatoIDPadre.Value= consecutivo;
                        cnn.Open();

                        OracleCommand cmd = new OracleCommand("SELECT ID, PUESTO, DESCRIPCION, UBICACION, TIPO_CONTRATO, RANGO_EDAD, COMPETENCIAS,ESCOLARIDAD,HORARIO, SEXO, STATUS" +
                            " FROM RH_VACANTES"+
                            " WHERE ID =:pID");

                        cmd.Parameters.Add("pID", OracleDbType.Int32);
                        cmd.Parameters["pID"].Value = consecutivo;
                        cmd.Connection = cnn;
                        cmd.CommandType = CommandType.Text;
                        OracleDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                tablaNuevaVacante.Rows.Add(elRenglon("Puesto", "encabezado", rdr["PUESTO"].ToString(), ""));
                                tablaNuevaVacante.Rows.Add(elRenglon("Descripción", "encabezado", rdr["DESCRIPCION"].ToString(),""));
                                tablaNuevaVacante.Rows.Add(elRenglon("Ubicación", "encabezado", rdr["UBICACION"].ToString(), ""));
                                tablaNuevaVacante.Rows.Add(elRenglon("Tipo de Contrato", "encabezado", rdr["TIPO_CONTRATO"].ToString(), ""));
                                tablaNuevaVacante.Rows.Add(elRenglon("Escolaridad", "encabezado", rdr["ESCOLARIDAD"].ToString(), ""));
                                tablaNuevaVacante.Rows.Add(elRenglon("Edad", "encabezado", rdr["RANGO_EDAD"].ToString(), ""));
                                tablaNuevaVacante.Rows.Add(elRenglon("Horario", "encabezado", rdr["HORARIO"].ToString(), ""));
                                tablaNuevaVacante.Rows.Add(elRenglon("Competencias", "encabezado", rdr["COMPETENCIAS"].ToString(), ""));
                                tablaNuevaVacante.Rows.Add(elRenglon("Sexo", "encabezado", rdr["SEXO"].ToString(), ""));
                            }

                        }
                    }
                    catch (Exception ExCargaInfoInterno)
                    {
                        elError.InnerText = ExCargaInfoInterno.Message;
                        Console.WriteLine(ExCargaInfoInterno.Message);
                    }
                    finally
                    {
                        if (cnn.State == ConnectionState.Open)
                            cnn.Close();
                    }
                }
            }
            }
            catch (Exception ExCargaInfo)
            {
                elError.InnerText = ExCargaInfo.Message;
                Console.WriteLine(ExCargaInfo.Message);
            }

        }
        /**
         * función encargada de rellenar las filas de la tabla con sus encabezados y su texto
         */
        private TableRow elRenglon(String texto1, String estilo1, String texto2, String estilo2)
        {
            TableRow fila = new TableRow();
            TableRow renglon = new TableRow();
            TableCell celda = new TableCell();
            celda.Text = texto1;
            celda.CssClass = estilo1;
            renglon.Cells.Add(celda);
            celda = new TableCell();
            celda.Text = texto2;
            celda.CssClass = estilo2;
            renglon.Cells.Add(celda);
            return renglon;
        }

    }
}