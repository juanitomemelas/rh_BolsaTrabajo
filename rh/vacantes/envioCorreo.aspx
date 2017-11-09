<%@ Page Title="" Language="C#" MasterPageFile="~/rh/headerVacio.Master" AutoEventWireup="true"
    CodeBehind="envioCorreo.aspx.cs" Inherits="RHVacantes.rh.vacantes.envioCorreo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentVacio" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentVacio" runat="server">
    <div id="elError" runat="server">
    </div>
    <div id="MensajeEstatus" runat="server">
    </div>
    <asp:Table ID="Table1" runat="server">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell ColumnSpan="2">Envío de correo</asp:TableHeaderCell></asp:TableHeaderRow>
        <asp:TableRow>
            <asp:TableCell>Asunto</asp:TableCell><asp:TableCell>
                <asp:TextBox ID="txtAsunto" runat="server" /></asp:TableCell></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>Destinatario</asp:TableCell><asp:TableCell>
                <asp:TextBox ID="txtCorreo" runat="server" /></asp:TableCell></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2">Mensaje</asp:TableCell></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2">
                <asp:TextBox ID="txtMensaje" CssClass="textboxSanchez" TextMode="multiline" Columns="50"
                    Rows="2" runat="server"></asp:TextBox></asp:TableCell></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Button ID="Button2" OnClick="enviaCorreo" runat="server" Text="Enviar" /></asp:TableCell><asp:TableCell></asp:TableCell></asp:TableRow>
    </asp:Table>
</asp:Content>
