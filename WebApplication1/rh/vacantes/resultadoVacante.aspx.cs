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
using RHVacantes.Clases;
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
    public partial class resultadoVacante : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Resultado vacante: " + Request.QueryString["vacante"] + " - " + Request.QueryString["titulo"];
            RellenaASP();
            bool autenticado = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (autenticado)
            {
                String nombre = System.Web.HttpContext.Current.User.Identity.Name;
                obtieneNombreActiveDirectory(nombre);

            }else {
                candidato.Visible = false;
            }
        }

        public void obtieneNombreActiveDirectory(String nombre)
        {
            DataTable usuario = new DataTable();
            UtilesOracle utiles = new UtilesOracle();
            usuario = utiles.obtieneNombreActiveDirectory(nombre);

            foreach (DataRow row in usuario.Rows)
            {
                if (row["USER_NAME"] != DBNull.Value)
                {
                    txtCandidatoNombre.Text = row["USER_NAME"].ToString();
                    txtCandidatoNombre.Enabled = false;
                }
                if (row["EMPLEADO"] != DBNull.Value)
                {
                    txtcandidatoNumeroEmp.Text = row["EMPLEADO"].ToString();
                    txtcandidatoNumeroEmp.Enabled = false;
                }
                if (row["EMAIL"] != DBNull.Value)
                {
                    txtcandidatoCorreo.Text = row["EMAIL"].ToString();
                   // txtcandidatoCorreo.Enabled = false;
                }
            }
            DataTable localidades = utiles.locaciones();
            int x = 0;
            foreach (DataRow row in localidades.Rows)
            {
                txtcandidatoLugarTrabajo.Items.Add(new ListItem("Text", "Value"));
                txtcandidatoLugarTrabajo.Items[x].Text = row["LOCACION"].ToString();
                txtcandidatoLugarTrabajo.Items[x].Value = row["ID"].ToString();
                x++;
            }

            
        }



        public void GuardarDatos(object sender, EventArgs e)
        {
            BDC objCnn = new BDC();

            using (OracleConnection cnn = objCnn.getConection())
            {
                int consecutivo = 1;
                try
                {
                    cnn.Open();

                    OracleCommand cmd = new OracleCommand("select max(ID_VACANTE) as siguiente from RH_CANDIDATOS");
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            if (rdr["siguiente"] != DBNull.Value)
                            {
                                consecutivo = Int32.Parse(rdr["siguiente"].ToString()) + 1;
                            }
                        }
                    }

                    //validamos que solo ingresen números en el campo de sueldo
                    String salarioDeseado;
                    int j;
                    if (Int32.TryParse(txtcandidatoSueldo.Text, out j))
                        salarioDeseado = j.ToString();
                    else
                        salarioDeseado = "0";                    
                    //Agregamos los nuevos valores  la tabla
                    cmd = new OracleCommand("INSERT INTO RH_CANDIDATOS (ID,ID_VACANTE, FECHA_POSTULACION, NOMBRE, NUMEMPLEADO, LUGAR_TRABAJO, CORREO, TELEFONO, CELULAR, SUELDO_DESEADO)" +
                       "VALUES (:pID, :pID_VACANTE,:pFECHA_POSTULACION,:pNOMBRE,:pNUMEMPLEADO,:pLUGAR_TRABAJO,:pCORREO,:pTELEFONO, :pCELULAR,:pSUELDO_DESEADO)");
                    //consecutivo
                    cmd.Parameters.Add("pID", OracleDbType.Int32);
                    cmd.Parameters["pID"].Value = candidatoIDPadre.Value;
                    cmd.Parameters.Add("pID_VACANTE", OracleDbType.Int32);
                    cmd.Parameters["pID_VACANTE"].Value = consecutivo;
                    cmd.Parameters.Add("pFECHA_POSTULACION", OracleDbType.Date);
                    cmd.Parameters["pFECHA_POSTULACION"].Value = DateTime.Now;
                    cmd.Parameters.Add("pNOMBRE", OracleDbType.Varchar2);
                    cmd.Parameters["pNOMBRE"].Value = txtCandidatoNombre.Text;
                    cmd.Parameters.Add("pNUMEMPLEADO", OracleDbType.Varchar2);
                    cmd.Parameters["pNUMEMPLEADO"].Value = txtcandidatoNumeroEmp.Text;
                    cmd.Parameters.Add("pLUGAR_TRABAJO", OracleDbType.Varchar2);
                    cmd.Parameters["pLUGAR_TRABAJO"].Value = txtcandidatoLugarTrabajo.SelectedItem.Value;
                    cmd.Parameters.Add("pCORREO", OracleDbType.Varchar2);
                    cmd.Parameters["pCORREO"].Value = txtcandidatoCorreo.Text;
                    cmd.Parameters.Add("pTELEFONO", OracleDbType.Varchar2);
                    cmd.Parameters["pTELEFONO"].Value = txtcandidatoTelefono.Text;
                    cmd.Parameters.Add("pCELULAR", OracleDbType.Varchar2);
                    cmd.Parameters["pCELULAR"].Value = txtcandidatoCelular.Text;
                    cmd.Parameters.Add("pSUELDO_DESEADO", OracleDbType.Varchar2);
                    cmd.Parameters["pSUELDO_DESEADO"].Value = salarioDeseado;

                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.Text;
                    //OracleDataReader rdr.InsertCommand = cmd;
                    rdr = cmd.ExecuteReader();


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
                int contentLength = filecandidatoCV.PostedFile.ContentLength;
                string contentType = filecandidatoCV.PostedFile.ContentType;
                string fileName = filecandidatoCV.PostedFile.FileName;

                filecandidatoCV.PostedFile.SaveAs(@"c:\Temp\" + fileName);
                InsertaPDF(@"c:\Temp\", fileName, consecutivo);
                MensajeEstatus.InnerText = "Felicidades, tu número de postulación es el siguiente: " + consecutivo.ToString();
                postularme.Visible = false;
                Page.Title ="Felicidades, tu número de postulación es el siguiente: " + consecutivo.ToString();
            }

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
                    if (!"".Equals(consecutivo))
                    {                       
                            var lasVacantes = new DataTable();
                            UtilesOracle vacantes = new UtilesOracle();
                            candidatoIDPadre.Value = consecutivo;
                            lasVacantes = vacantes.obtieneVacantes(consecutivo);
                            foreach (DataRow row in lasVacantes.Rows)
                            {
                                tablaNuevaVacante.Rows.Add(elRenglon("Descripción", "encabezado", row["DESCRIPCION"].ToString(), ""));
                                tablaNuevaVacante.Rows.Add(elRenglon("Competencias", "encabezado", row["COMPETENCIAS"].ToString(), ""));
                                tablaNuevaVacante.Rows.Add(elRenglon("Ubicación", "encabezado", row["UBICACION"].ToString(), ""));
                                tablaNuevaVacante.Rows.Add(elRenglon("Tipo de Contrato", "encabezado", row["TIPO_CONTRATO"].ToString(), ""));
                                tablaNuevaVacante.Rows.Add(elRenglon("Escolaridad", "encabezado", row["ESCOLARIDAD"].ToString(), ""));
                                tablaNuevaVacante.Rows.Add(elRenglon("Edad", "encabezado", row["RANGO_EDAD"].ToString(), ""));
                                tablaNuevaVacante.Rows.Add(elRenglon("Horario", "encabezado", row["HORARIO"].ToString(), ""));
                                tablaNuevaVacante.Rows.Add(elRenglon("Sexo", "encabezado", regresaGenero(row["SEXO"].ToString()), ""));
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
        private String regresaGenero(String elGenero)
        {
            String elSexo="";
            switch (elGenero)
            {
                case "F":
                    elSexo = "Femenino";
                    break;
                case "M":
                    elSexo = "Masculino";
                    break;
                default:
                    elSexo = "Indistinto";
                    break;
            }
            return elSexo;
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
        protected void InsertaPDF(String ruta, String nombreAdjunto, int elID)
        {
            // Read the file and convert it to Byte Array
            //string filePath = Server.MapPath(ruta);
            string filename = ruta + nombreAdjunto;// Path.GetFileName(filePath);

            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            Byte[] bytes = br.ReadBytes((Int32)fs.Length);
            br.Close();
            fs.Close();

            BDC objCnn = new BDC();

            using (OracleConnection cnn = objCnn.getConection())
            {
                try
                {
                    cnn.Open();
                    //insert the file into database
                    string strQuery = "insert into RH_ADJUNTOS(ID_PADRE,NOMBRE_ADJUNTO, CONTENTTYPE, THEData) values (:pID, :pName, :pContentType, :pData)";
                    OracleCommand cmd = new OracleCommand(strQuery);
                    cmd.Parameters.Add("pID", OracleDbType.Int32);
                    cmd.Parameters["pID"].Value = elID;
                    cmd.Parameters.Add("pName", OracleDbType.Varchar2);
                    cmd.Parameters["pName"].Value = nombreAdjunto;
                    cmd.Parameters.Add("pContentType", OracleDbType.Varchar2);
                    cmd.Parameters["pContentType"].Value = "application/pdf";
                    cmd.Parameters.Add("pData", OracleDbType.Blob);
                    cmd.Parameters["pData"].Value = bytes;

                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader rdr = cmd.ExecuteReader();
                    //YA que acabamos la chamba, borramos el archivo
                    File.Delete(filename);
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

            //InsertUpdateData(cmd);
        }

    }

}
