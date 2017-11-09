<%@ Page Title="" Language="C#" CodeBehind="NuevaVacanteDatos.aspx.cs" Inherits="RHVacantes.Vacantes.NuevaVacanteDatos" %>
<div>
<asp:table id="tablaNuevaVacante" CssClass="directorio" runat="server"></asp:table>

<div id="elError" runat="server"></div>
    <form id="formaNuevo" runat="server">
    <div>
    <asp:linkbutton onclientclick="manipulaDivcontenedorDerecho('20001');return false"  id="botonNuevo" runat="server">arre</asp:linkbutton>
    </div>
    </form>
</div>