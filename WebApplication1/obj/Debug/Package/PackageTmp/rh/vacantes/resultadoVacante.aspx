<%@ Page Title="" Language="C#" MasterPageFile="~/rh/headerVacio.Master" AutoEventWireup="true"
    CodeBehind="resultadoVacante.aspx.cs" Inherits="RHVacantes.rh.vacantes.resultadoVacante" %>

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
        <div id="postularme" runat="server">
            <div id="postularmeTexto" class="postularme estiloMano">
                Postularme</div>
            <div id="postularmeTabla" style="display: none">
            
             <!------------------------------------------------------------>
                  <asp:LoginView ID="HeadLoginView" runat="server" EnableViewState="false">
        <LoggedInTemplate>
            
        </LoggedInTemplate>
        <AnonymousTemplate>
        Para poder postularte, es necesario iniciar sesión
    <asp:Login id="Login1" runat="server" 
        PasswordLabelText="Contraseña"
        UserNameLabelText="Usuario de intranet"
        DisplayRememberMe="false" />
          </AnonymousTemplate>
    </asp:LoginView>


                <!-------------------------------------------------------------->
                <asp:Table ID="candidato" CssClass="directorio" runat="server">
                    <asp:TableHeaderRow ID="candidatoEncabezado" runat="server">
                        <asp:TableHeaderCell CssClass="InfoCatTitHdrVacante" ColumnSpan="3">Datos del candidato</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                    <asp:TableRow ID="candidatoNombre" runat="server">
                        <asp:TableCell CssClass="encabezado">Nombre</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox required CssClass="textboxSanchez" runat="server" ID="txtCandidatoNombre"></asp:TextBox></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="candidatoNumeroEmp" runat="server">
                        <asp:TableCell CssClass="encabezado">Número de empleado</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox required CssClass="textboxSanchez" runat="server" ID="txtcandidatoNumeroEmp"></asp:TextBox></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="candidatoLugarTrabajo" runat="server">
                        <asp:TableCell CssClass="encabezado">Lugar de trabajo</asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList CssClass="textboxSanchez" ID="txtcandidatoLugarTrabajo" runat="server">
                            </asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="candidatoCorreo" runat="server">
                        <asp:TableCell CssClass="encabezado">Correo</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox required runat="server" CssClass="textboxSanchez" ID="txtcandidatoCorreo"></asp:TextBox></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="candidatoTelefono" runat="server">
                        <asp:TableCell CssClass="encabezado">Teléfono</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox required runat="server" CssClass="textboxSanchez" ID="txtcandidatoTelefono"></asp:TextBox></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="candidatoCelular" runat="server">
                        <asp:TableCell CssClass="encabezado">Celular</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox required runat="server" CssClass="textboxSanchez" ID="txtcandidatoCelular"></asp:TextBox></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="candidatoSueldo" runat="server">
                        <asp:TableCell CssClass="encabezado">Sueldo deseado</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox required CssClass="textboxSanchez" runat="server" ID="txtcandidatoSueldo"></asp:TextBox></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="candidatoCV" runat="server">
                        <asp:TableCell CssClass="encabezado" >CV Actualizado</asp:TableCell>
                        <asp:TableCell>
                            <asp:FileUpload required CssClass="textboxSanchez" ID="filecandidatoCV" runat="server"></asp:FileUpload>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow1" runat="server">
                        <asp:TableCell>
                            <asp:Button ID="botonEnviar" OnClick="GuardarDatos" runat="server" Text="Aceptar">
                            </asp:Button></asp:TableCell>
                        <asp:TableCell><input id="Reset1" type="reset" value="Limpiar" /></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableFooterRow ID="TableFooterRow1" runat="server">
                        <asp:TableCell ColumnSpan="3" HorizontalAlign="Right" Font-Italic="true">
                    Grupo Sánchez
                        </asp:TableCell>
                    </asp:TableFooterRow>
                </asp:Table>
            </div>
        </div>
    </div>
    <input id="candidatoIDPadre" type="hidden" runat="server" value="" />
    <script src="../../Scripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#postularmeTexto").click(function () {

                // $("#postularmeTexto").hide(900);
                $("#postularmeTexto").slideUp("slow");
                // $("#postularmeTabla").show(900);
                $("#postularmeTabla").slideDown("slow");
                parent.acomoda();
            });
        });
    </script>
</asp:Content>
