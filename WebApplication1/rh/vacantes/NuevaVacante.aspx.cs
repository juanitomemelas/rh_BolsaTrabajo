using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using RH_VacantesWeb.Clases;
using System.Globalization;
using RHVacantes.Clases;

namespace RHVacantes.Vacantes
{
    public partial class NuevaVacante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UtilesOracle utilesOracle = new UtilesOracle();
                DataTable localidades = utilesOracle.locaciones();
                int x = 0;
                foreach (DataRow row in localidades.Rows)
                {
                    TextBoxUbicacion.Items.Add(new ListItem("Text", "Value"));
                    TextBoxUbicacion.Items[x].Text = row["LOCACION"].ToString();
                    TextBoxUbicacion.Items[x].Value = row["ID"].ToString();
                    x++;
                }
                DataTable contrato =utilesOracle.TipoContratos();
                x = 0;
                foreach (DataRow row in contrato.Rows)
                {
                    TextBoxTipoContrato.Items.Add(new ListItem("Text", "Value"));
                    TextBoxTipoContrato.Items[x].Text = row["CONTRATO"].ToString();
                    TextBoxTipoContrato.Items[x].Value = row["ID"].ToString();
                    x++;
                }

                DataTable horario = utilesOracle.ObtieneHorarios();
                x = 0;
                foreach (DataRow row in horario.Rows)
                {
                    TextBoxHorario.Items.Add(new ListItem("Text", "Value"));
                    TextBoxHorario.Items[x].Text = row["HORARIO"].ToString();
                    TextBoxHorario.Items[x].Value = row["ID"].ToString();
                    x++;
                }

                //                TextBoxHorario
                // RellenaASP();
            }
        }
        public void Cancelar(object sender, EventArgs e)
        {
            Response.Redirect("~/rh/grupoSanchez.htm");
        }
        public void GuardarDatos(object sender, EventArgs e)
        {
            BDC objCnn = new BDC();

            using (OracleConnection cnn = objCnn.getConection())
            {
                try
                {
                    cnn.Open();
                    int consecutivo = 1;
                    //Obtenemos el número de consecutivo para crear el registro
                    OracleCommand cmd = new OracleCommand("select max(ID) as siguiente from RH_VACANTES");
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
                    //Agregamos los nuevos valores  la tabla
                     cmd = new OracleCommand("INSERT INTO RH_VACANTES (ID,PUESTO,DESCRIPCION,UBICACION,TIPO_CONTRATO,HORARIO,ESCOLARIDAD,RANGO_EDAD,COMPETENCIAS,SEXO,STATUS,FECHA_VIGENCIA)" +
                        "VALUES (:pID, :pPuesto,:pDescripcion,:pUbicacion,:pTipo_Contrato,:pHORARIO, :pESCOLARIDAD, :pRango_Edad,:pCompetencias,:pSexo,'V',:pFechaVigencia)");
                    //consecutivo
                     cmd.Parameters.Add("pID", OracleDbType.Int32);
                     cmd.Parameters["pID"].Value = consecutivo;
                    cmd.Parameters.Add("pPuesto", OracleDbType.Varchar2);
                    cmd.Parameters["pPuesto"].Value = TextBoxPuesto.Text;
                    cmd.Parameters.Add("pDescripcion", OracleDbType.Clob);
                    cmd.Parameters["pDescripcion"].Value = TextBoxDescripcion.Text;
                    cmd.Parameters.Add("pUbicacion", OracleDbType.Varchar2);
                    cmd.Parameters["pUbicacion"].Value = TextBoxUbicacion.SelectedItem.Value;
                    cmd.Parameters.Add("pTipo_Contrato", OracleDbType.Varchar2);
                    cmd.Parameters["pTipo_Contrato"].Value = TextBoxTipoContrato.SelectedItem.Value;
                    cmd.Parameters.Add("pHORARIO", OracleDbType.Varchar2);
                    cmd.Parameters["pHORARIO"].Value = TextBoxHorario.SelectedItem.Value;
                    cmd.Parameters.Add("pESCOLARIDAD", OracleDbType.Varchar2);
                    cmd.Parameters["pESCOLARIDAD"].Value = TextBoxEscolaridad.SelectedItem.Value;
                    cmd.Parameters.Add("pRango_Edad", OracleDbType.Varchar2);
                    cmd.Parameters["pRango_Edad"].Value = TextBoxEdad.Text;
                    cmd.Parameters.Add("pCompetencias", OracleDbType.Clob);
                    cmd.Parameters["pCompetencias"].Value = TextBoxCompetencia.Text;
                    cmd.Parameters.Add("pSexo", OracleDbType.Char);
                    cmd.Parameters["pSexo"].Value = TextBoxSexo.SelectedItem.Value;
                    cmd.Parameters.Add("pFECHA_VIGENCIA",OracleDbType.Date);

                    //2016-07-31
                    //String[] fechaSeparada = fechaVig.Text.Split('-');
                    // DateTime.ParseExact(txtEvtDt.Text, "dd/MMM/yyyy", CultureInfo.InvariantCulture)
                 //   cmd.Parameters["pFECHA_VIGENCIA"].Value =DateTime.ParseExact(fechaVig.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.Text;
                     rdr = cmd.ExecuteReader();
                     MensajeEstatus.InnerText = "La vacante: " + consecutivo.ToString() + " - " + TextBoxPuesto.Text + " se creó exitosamente";
                     contenedorFormulario.Visible = false;
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
}