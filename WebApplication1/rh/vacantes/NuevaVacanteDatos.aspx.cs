using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using Oracle.DataAccess.Client;
using RH_VacantesWeb.Clases;
namespace RHVacantes.Vacantes
{
    public partial class NuevaVacanteDatos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RellenaASP();
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
                    TableRow renglon = new TableRow();
                    TableCell celda1 = new TableCell();
                    TableCell celda2 = new TableCell();
                    TableCell celda3 = new TableCell();
                    TableCell celda4 = new TableCell();
                    TableCell celda5 = new TableCell();
                    TableCell celda6 = new TableCell();
                    TableCell celda7 = new TableCell();
                    TableCell celda8 = new TableCell();
                    TableCell celda9 = new TableCell();
                    TableCell celda10 = new TableCell();

                    //Ingresamos el encabezado de los distribuidores, independientemente de que si se traigan valores o no.
                    celda1.Text = "Vacante01";
                    celda1.CssClass = "encabezado";
                    celda2.Text = "Puesto Ofrecido";
                    celda2.CssClass = "encabezado";
                    celda3.Text = "Descripción";
                    celda3.CssClass = "encabezado";
                    celda4.Text = "Ubicación";
                    celda4.CssClass = "encabezado";
                    celda5.Text = "Tipo Contrato";
                    celda5.CssClass = "encabezado";
                    celda6.Text = "Escolaridad";
                    celda6.CssClass = "encabezado";
                    celda7.Text = "Edad";
                    celda7.CssClass = "encabezado";
                    celda8.Text = "Horario";
                    celda8.CssClass = "encabezado";
                    celda9.Text = "Idioma";
                    celda9.CssClass = "encabezado";
                    celda10.Text = " ";
                    celda10.CssClass = "encabezado";

                    renglon.Cells.Add(celda1);
                    renglon.Cells.Add(celda2);
                    renglon.Cells.Add(celda3);
                    renglon.Cells.Add(celda4);
                    renglon.Cells.Add(celda5);
                    renglon.Cells.Add(celda6);
                    renglon.Cells.Add(celda7);
                    renglon.Cells.Add(celda8);
                    renglon.Cells.Add(celda9);
                    renglon.Cells.Add(celda10);

                    tablaNuevaVacante.Rows.Add(renglon);
                    try
                    {
                        cnn.Open();

                        OracleCommand cmd = new OracleCommand("SELECT ID, PUESTO, DESCRIPCION, UBICACION, TIPO_CONTRATO, RANGO_EDAD, IDIOMA, SEXO, STATUS FROM ADMINISTRADOR.RH_VACANTES");
                        /*
                         *
                          OracleCommand oraCommand = new OracleCommand("SELECT fullname FROM sup_sys.user_profile
                           WHERE domain_user_name = :userName", db);
                           oraCommand.Parameters.Add(new OracleParameter("userName", domainUser));
                         * 
                         * 
                         * Esta mas papa con el datagrid:
                          
                        https://msdn.microsoft.com/en-us/library/system.web.ui.webcontrols.datagrid(v=vs.110).aspx
                         * */

                        cmd.Connection = cnn;
                        cmd.CommandType = CommandType.Text;
                        OracleDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                       {
                            while (rdr.Read())
                            {

                                 renglon = new TableRow();
                                celda1 = new TableCell();
                                celda2 = new TableCell();
                                celda3 = new TableCell();
                                celda4 = new TableCell();
                                celda5 = new TableCell();
                                celda6 = new TableCell();
                                celda7 = new TableCell();
                                celda8 = new TableCell();
                                celda9 = new TableCell();
                                celda10 = new TableCell();

                                //Ingresamos el encabezado de los distribuidores, independientemente de que si se traigan valores o no.
                                celda1.Text = rdr["ID"].ToString();
                                celda1.CssClass = "";
                                celda2.Text = rdr["PUESTO"].ToString();
                                celda2.CssClass = "";
                                celda3.Text = rdr["DESCRIPCION"].ToString();
                                celda3.CssClass = "";
                                celda4.Text = rdr["UBICACION"].ToString();
                                celda4.CssClass = "";
                                celda5.Text = rdr["TIPO_CONTRATO"].ToString();
                                celda5.CssClass = "";
                                celda6.Text = "Falta escolaridad";
                                celda6.CssClass = "";
                                celda7.Text = rdr["RANGO_EDAD"].ToString();
                                celda7.CssClass = "";
                                celda8.Text = "Falta horario";
                                celda8.CssClass = "";
                                celda9.Text = rdr["IDIOMA"].ToString();
                                celda9.CssClass = "";
                                celda10.Text = "liga";
                                celda10.CssClass = "";

                                renglon.Cells.Add(celda1);
                                renglon.Cells.Add(celda2);
                                renglon.Cells.Add(celda3);
                                renglon.Cells.Add(celda4);
                                renglon.Cells.Add(celda5);
                                renglon.Cells.Add(celda6);
                                renglon.Cells.Add(celda7);
                                renglon.Cells.Add(celda8);
                                renglon.Cells.Add(celda9);
                                renglon.Cells.Add(celda10);

                                tablaNuevaVacante.Rows.Add(renglon);
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
            catch (Exception ExCargaInfo)
            {
                elError.InnerText = ExCargaInfo.Message;
                Console.WriteLine(ExCargaInfo.Message);
            }
        }
      
    }
}