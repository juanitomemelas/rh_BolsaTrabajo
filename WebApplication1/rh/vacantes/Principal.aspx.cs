using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Oracle.DataAccess.Client;
using RH_VacantesWeb.Clases;


namespace RHVacantes.vacantes
{
    public partial class Principal : System.Web.UI.Page
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
            
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

       }
}