using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace WebApplication1
{
    public class Global : System.Web.HttpApplication
    {
        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

            // Get the exception object.
            Exception exc = Server.GetLastError();

            // Handle HTTP errors
            if (exc.GetType() == typeof(HttpException))
            {
                // The Complete Error Handling Example generates
                // some errors using URLs with "NoCatch" in them;
                // ignore these here to simulate what would happen
                // if a global.asax handler were not implemented.
                if (exc.Message.Contains("NoCatch") || exc.Message.Contains("maxUrlLength"))
                    return;

                //Redirect HTTP errors to HttpError page
                Server.Transfer("~/rh/Error/error?handler=Application_Error%20-%20Global.asax", true);
            }

            // For other kinds of errors give the user some information
            // but stay on the default page
            Response.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">");
            Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\" xml:lang=\"en\">");
            Response.Write("<head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title>");
            Response.Write("Error no especificado");
            Response.Write("</title><link href=\"../../Styles/GrupoSanchez.css\" rel=\"stylesheet\" type=\"text/css\" />");
            Response.Write("<script>");
            Response.Write("(function (i, s, o, g, r, a, m) {");
            Response.Write("i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {");
            Response.Write("(i[r].q = i[r].q || []).push(arguments)");
            Response.Write("}, i[r].l = 1 * new Date(); a = s.createElement(o),");
            Response.Write("m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)");
            Response.Write("})(window, document, 'script', 'https://www.google-analytics.com/analytics.js', 'ga');");
            Response.Write("");
            Response.Write("ga('create', 'UA-81212952-1', 'auto');");
            Response.Write("ga('require', 'linkid');");
            Response.Write("ga('send', 'pageview');");
            Response.Write("");
            Response.Write("</script>");
            Response.Write("</head>");
            Response.Write("<body>");
            Response.Write("<form>");

            Response.Write("<div id=\"MainContentVacio_ErrorMsgTextBox\">");
            Response.Write("Ocurrio un error no especificado, por favor contacto al equipo de TI");
            Response.Write("</div>");

            Response.Write("</form>");
            Response.Write("</body>");
            Response.Write("</html>");



            // Clear the error from the server
            Server.ClearError();
        }
        
        void Application_Start(object sender, EventArgs e)
        {
            // Código que se ejecuta al iniciarse la aplicación

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Código que se ejecuta cuando se cierra la aplicación

        }



        void Session_Start(object sender, EventArgs e)
        {
            // Código que se ejecuta cuando se inicia una nueva sesión

        }

        void Session_End(object sender, EventArgs e)
        {
            // Código que se ejecuta cuando finaliza una sesión.
            // Nota: el evento Session_End se desencadena sólo cuando el modo sessionstate
            // se establece como InProc en el archivo Web.config. Si el modo de sesión se establece como StateServer 
            // o SQLServer, el evento no se genera.

        }

    }
}
