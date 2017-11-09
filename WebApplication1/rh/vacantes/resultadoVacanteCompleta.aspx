<%@ Page Title="" Language="C#" MasterPageFile="~/rh/headerVacio.Master" AutoEventWireup="true"
    CodeBehind="resultadoVacanteCompleta.aspx.cs" Inherits="RHVacantes.rh.vacantes.resultadoVacanteCompleta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentVacio" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentVacio" runat="server">
    <div id="elError" runat="server">
    </div>
        <div id="MensajeEstatus" runat="server">
    </div>
    <div class="Contactos">
        <asp:Table ID="tablaNuevaVacante" CssClass="directorio" runat="server">
        </asp:Table>
    </div>

   
    <input id="candidatoIDPadre" type="hidden" runat="server" value=""/>

        <script src="../../Scripts/jquery-1.12.3.min.js" type="text/javascript"></script>
</asp:Content>
