using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using RHVacantes.Clases;
using System.IO;
using Oracle.DataAccess.Types;



namespace RHVacantes.rh.vacantes
{
    public partial class Candidatos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void obtienePDF(object sender, EventArgs e)
        {

            var dataTable = new DataTable();
            UtilesOracle cv = new UtilesOracle();
            Button lnk = sender as Button;


            Button btn = (Button)sender;
            GridViewRow rowUno = (GridViewRow)btn.NamingContainer;
           // Response.Write(GridView1.DataKeys[rowUno.RowIndex].Value.ToString());
            String valorID = GridView1.DataKeys[rowUno.RowIndex].Value.ToString();//lnk.Attributes["CommandArgument"];

            //ID_PADRE,NOMBRE_ADJUNTO,CONTENTTYPE,THEDATA
            DataTable curriculum = cv.obtieneCV(valorID);
            
            foreach (DataRow row in curriculum.Rows)
            {

                OracleBinary obj = (byte[])row["THEDATA"];
                byte[] bytes= obj.Value;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + row["NOMBRE_ADJUNTO"].ToString());
                Response.BinaryWrite(bytes);
                // myMemoryStream.WriteTo(Response.OutputStream); //works too
                Response.Flush();
                Response.Close();

            }
          

        }
    }
}