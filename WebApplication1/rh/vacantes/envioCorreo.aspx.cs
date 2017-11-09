using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Text;
using System.Data;
using RHVacantes.Clases;
using Oracle.DataAccess.Types;


namespace RHVacantes.rh.vacantes
{
    public partial class envioCorreo : System.Web.UI.Page
    {
        public void enviaCorreo(object sender, EventArgs e)
        {

            // Command line argument must the the SMTP host.
            MailMessage correo = new MailMessage();
            correo.From = new MailAddress("no-reply@sanchez.com");
            correo.To.Add(txtCorreo.Text);
            correo.Subject = txtAsunto.Text;
            correo.Body = txtMensaje.Text;

            //por si se requiere html
            //correo.IsBodyHtml = true;
            SmtpClient cliente = new SmtpClient("smtp.gmail.com");
            cliente.Port = 587;
            cliente.Credentials = new System.Net.NetworkCredential("webmaster.grupo.sanchez@gmail.com", "webmaster001");
            cliente.EnableSsl = true;
            //Seagrega esta linea para que no truene
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
            cliente.Send(correo);
            MensajeEstatus.InnerText = "El mensaje se envió exitosamente a: " + txtCorreo.Text;
            Table1.Visible = false;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Page.Title = "Información de la vacante " + Request.QueryString["descripcion"];
                txtCorreo.Text = Request.QueryString["correo"]; ;
                txtAsunto.Text = "Información de la vacante " + Request.QueryString["descripcion"];
                txtMensaje.Text = "";
            }
        }
    }
}