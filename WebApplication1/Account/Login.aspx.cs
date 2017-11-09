using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
//using System.Web.ApplicationServices;
using Oracle.DataAccess.Client;
using RH_VacantesWeb.Clases;
/*
 * Para conectarse al AD
 * 
 * https://msdn.microsoft.com/en-us/library/ff650308.aspx
 * 
 * Para ver la conexión a Oracle
 * https://social.msdn.microsoft.com/Forums/es-ES/ed195285-ad5b-4eb8-b2cc-d3e20444320d/ayuda-con-conexion-a-oracle-desde-aspnet?forum=vbes
 * */

namespace WebApplication1.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // System.Web.Security.ActiveDirectoryMembershipProvider tes = new ActiveDirectoryMembershipProvider();
           // RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        }
      
    }
}
