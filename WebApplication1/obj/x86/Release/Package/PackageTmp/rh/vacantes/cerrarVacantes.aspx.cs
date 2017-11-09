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

namespace RHVacantes.rh.vacantes
{
    public partial class cerrarVacantes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //BindData();

                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        protected void Gridview1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "PathUpdate")
            {
                string path = e.CommandArgument.ToString();
                // do you what you need to do
            }
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void sqlDataSource1_Updating(object sender, SqlDataSourceCommandEventArgs e)
        {
                        
            //e.Command.Parameters["ID"].Value = 1;




            BDC objCnn = new BDC();

            using (OracleConnection cnn = objCnn.getConection())
            {
                try
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand("UPDATE EDUARDO.RH_VACANTES SET STATUS = :pSTATUS WHERE ID = :pID");
                    //consecutivo
                    cmd.Parameters.Add("pID", OracleDbType.Int32);
                    cmd.Parameters["pID"].Value = e.Command.Parameters["ID"].Value;
                    cmd.Parameters.Add("pSTATUS", OracleDbType.Varchar2);
                    cmd.Parameters["pSTATUS"].Value = e.Command.Parameters["STATUS"].Value;
/*                    cmd.Parameters.Add("pDescripcion", OracleDbType.Clob);
                    cmd.Parameters["pDescripcion"].Value = TextBoxDescripcion.Text;
                    cmd.Parameters.Add("pUbicacion", OracleDbType.Varchar2);
                    cmd.Parameters["pUbicacion"].Value = TextBoxUbicacion.SelectedItem.Value;
                    cmd.Parameters.Add("pTipo_Contrato", OracleDbType.Varchar2);
                    cmd.Parameters["pTipo_Contrato"].Value = TextBoxTipoContrato.SelectedItem.Value;
                    cmd.Parameters.Add("pRango_Edad", OracleDbType.Varchar2);
                    cmd.Parameters["pRango_Edad"].Value = TextBoxEdad.Text;
                    cmd.Parameters.Add("pIdioma", OracleDbType.Varchar2);
                    cmd.Parameters["pIdioma"].Value = TextBoxIdioma.SelectedItem.Value;
                    cmd.Parameters.Add("pSexo", OracleDbType.Char);
                    cmd.Parameters["pSexo"].Value = TextBoxSexo.SelectedItem.Value;
                    */
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.Text;
                    SqlDataSource1.UpdateCommand = "UPDATE EDUARDO.RH_VACANTES SET DESCRIPCION ='" + e.Command.Parameters["STATUS"].Value + "' WHERE ID = " + e.Command.Parameters["ID"].Value;
                    //OracleDataReader rdr.InsertCommand = cmd;
                    OracleDataReader rdr = cmd.ExecuteReader();
                    rdr = cmd.ExecuteReader();


                }
                catch (Exception ExCargaInfoInterno)
                {
                    //elError.InnerText = ExCargaInfoInterno.Message;
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