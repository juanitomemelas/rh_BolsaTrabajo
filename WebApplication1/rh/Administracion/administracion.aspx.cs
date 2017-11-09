using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RHVacantes.Clases;
namespace RHVacantes.rh.Administracion
{
    public partial class administracion : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }
        //This event is used for paging. As you can see from the code below, we simply set a new page index and rebind the data.

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindData();
        }

        //This event shows how to delete a row on delete LinkButton click.

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label lblstid = (Label)GridView1.Rows[e.RowIndex].FindControl("lblstId");
            UtilesOracle utiles = new UtilesOracle();
            if (utiles.BorraLocaciones(lblstid.Text))
            {
                GridView1.EditIndex = -1;
                BindData();
            }
        }

        //This event is used to show a row in editable mode.

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindData();
        }

        //This event will update information in database.

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblstid = (Label)GridView1.Rows[e.RowIndex].FindControl("lblstId");
            TextBox txtLocacion = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtLocacion");
            UtilesOracle utiles = new UtilesOracle();
            if (utiles.actualizaLocaciones(lblstid.Text, txtLocacion.Text))
            {
                GridView1.EditIndex = -1;
                BindData();
            }
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindData();
        }
        public void BindData()
        {
            UtilesOracle utiles = new UtilesOracle();
            GridView1.DataSource = utiles.locaciones();
            GridView1.DataBind();
            gridViewContratos.DataSource = utiles.TipoContratos();
            gridViewContratos.DataBind();
            gridViewHorario.DataSource = utiles.ObtieneHorarios();
            gridViewHorario.DataBind();

        }
        public void Submit_Click1(object sender, EventArgs e)
        {
            if (!"".Equals(txtNuevaLocacion.Text))
            {
                mensajeError.Text = "";
                UtilesOracle utiles = new UtilesOracle();
                if (utiles.CreaLocaciones(txtNuevaLocacion.Text))
                {
                    txtNuevaLocacion.Text = "";
                    BindData();
                }
            }
            else
            {
                mensajeError.Text = "Se debe de agregar un valor al campo de locaciones";
            }
        }
 

        /*******************************************
         * 
         * 
         * Contratos
         * 
         * 
         */
        protected void GridView1_PageIndexChangingContratos(object sender, GridViewPageEventArgs e)
        {
            gridViewContratos.PageIndex = e.NewPageIndex;
            BindData();
        }

        //This event shows how to delete a row on delete LinkButton click.

        protected void GridView1_RowDeletingContratos(object sender, GridViewDeleteEventArgs e)
        {
            Label lblstidContratos = (Label)gridViewContratos.Rows[e.RowIndex].FindControl("lblstIdContratos");
            UtilesOracle utiles = new UtilesOracle();
            if (utiles.BorraContratos(lblstidContratos.Text))
            {
                gridViewContratos.EditIndex = -1;
                BindData();
            }
        }

        //This event is used to show a row in editable mode.

        protected void GridView1_RowEditingContratos(object sender, GridViewEditEventArgs e)
        {
            gridViewContratos.EditIndex = e.NewEditIndex;
            BindData();
        }

        //This event will update information in database.

        protected void GridView1_RowUpdatingContratos(object sender, GridViewUpdateEventArgs e)
        {
            Label lblstidContratos = (Label)gridViewContratos.Rows[e.RowIndex].FindControl("lblstIdContratos");
            TextBox txtContratos = (TextBox)gridViewContratos.Rows[e.RowIndex].FindControl("txtContratos");
            UtilesOracle utiles = new UtilesOracle();
            if (utiles.ActualizaContratos(lblstidContratos.Text, txtContratos.Text))
            {
                gridViewContratos.EditIndex = -1;
                BindData();
            }
        }
        protected void GridView1_RowCancelingEditContratos(object sender, GridViewCancelEditEventArgs e)
        {
            gridViewContratos.EditIndex = -1;
            BindData();
        }
        public void Submit_Click1Contratos(object sender, EventArgs e)
        {
            if (!"".Equals(txtContrato.Text))
            {
                mensajeError.Text = "";
                UtilesOracle utiles = new UtilesOracle();
                if (utiles.CreaContratos(txtContrato.Text))
                {
                    txtContrato.Text = "";
                    BindData();
                }
            }
            else
            {
                mensajeError.Text = "Se debe de agregar un valor al campo de Contratos";
            }
        }
        /*******************************************
         * 
         * 
         * Horarios
         * 
         * 
         */
        protected void GridView1_PageIndexChangingHorario(object sender, GridViewPageEventArgs e)
        {
            gridViewHorario.PageIndex = e.NewPageIndex;
            BindData();
        }

        //This event shows how to delete a row on delete LinkButton click.

        protected void GridView1_RowDeletingHorario(object sender, GridViewDeleteEventArgs e)
        {
            Label lblstidHorario = (Label)gridViewHorario.Rows[e.RowIndex].FindControl("lblstIdHorario");
            UtilesOracle utiles = new UtilesOracle();
            if (utiles.BorraHorario(lblstidHorario.Text))
            {
                gridViewHorario.EditIndex = -1;
                BindData();
            }
        }

        //This event is used to show a row in editable mode.

        protected void GridView1_RowEditingHorario(object sender, GridViewEditEventArgs e)
        {
            gridViewHorario.EditIndex = e.NewEditIndex;
            BindData();
        }

        //This event will update information in database.

        protected void GridView1_RowUpdatingHorario(object sender, GridViewUpdateEventArgs e)
        {
            Label lblstidHorario = (Label)gridViewHorario.Rows[e.RowIndex].FindControl("lblstIdHorario");
            TextBox txtHorario = (TextBox)gridViewHorario.Rows[e.RowIndex].FindControl("txtHorario");
            UtilesOracle utiles = new UtilesOracle();
            if (utiles.ActualizaHorario(lblstidHorario.Text, txtHorario.Text))
            {
                gridViewHorario.EditIndex = -1;
                BindData();
            }
        }
        protected void GridView1_RowCancelingEditHorario(object sender, GridViewCancelEditEventArgs e)
        {
            gridViewHorario.EditIndex = -1;
            BindData();
        }
        public void Submit_Click1Horario(object sender, EventArgs e)
        {
            if (!"".Equals(txtNuevoHorario.Text))
            {
                mensajeError.Text = "";
                UtilesOracle utiles = new UtilesOracle();
                if (utiles.CreaHorario(txtNuevoHorario.Text))
                {
                    txtNuevoHorario.Text = "";
                    BindData();
                }
            }
            else
            {
                mensajeError.Text = "Se debe de agregar un valor al campo de Horario";
            }
        }



    }
}