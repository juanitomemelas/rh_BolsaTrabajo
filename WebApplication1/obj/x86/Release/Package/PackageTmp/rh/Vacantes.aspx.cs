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

namespace WebApplication1
{

    public partial class _Default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        { 
            //Validamos los roles del usuario
            loginAdministracion.Visible= false;
            bool autenticado = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (autenticado)
            {
                String nombre = System.Web.HttpContext.Current.User.Identity.Name;
                var dataTable = new DataTable();
                UtilesOracle utiles = new UtilesOracle();
                DataTable roles = utiles.rolesUsuarioFirmado(nombre);
                loginAdministracion.Visible = roles.Rows.Count > 0;
               
            }
        }

    }
}
