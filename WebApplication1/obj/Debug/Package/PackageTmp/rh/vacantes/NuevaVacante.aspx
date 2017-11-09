
<%@ Page Title="Nueva vacante" Language="C#" MasterPageFile="~/rh/headerVacio.Master"
    CodeBehind="NuevaVacante.aspx.cs" Inherits="RHVacantes.Vacantes.NuevaVacante" %>
  
 
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentVacio" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentVacio" runat="server">
    <div class="Contactos">
        <asp:Table ID="tablaNuevaVacante" CssClass="directorio" runat="server">
        </asp:Table>
        <div id="elError" runat="server">
        </div>
        <div id="MensajeEstatus" runat="server">
        </div>
        <div id="contenedorFormulario" runat="server">
        <asp:Table ID="Table1" CssClass="directorio" runat="server">
            <asp:TableRow>
                <asp:TableCell CssClass="encabezado">Puesto Ofrecido</asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox CssClass="textboxSanchez" ID="TextBoxPuesto" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell CssClass="encabezado">Descripción</asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TextBoxDescripcion" CssClass="textboxSanchez" TextMode="multiline"
                        Columns="50" Rows="5" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell CssClass="encabezado">Ubicación</asp:TableCell>
                <asp:TableCell>
                <asp:DropDownList CssClass="textboxSanchez" ID="TextBoxUbicacion" runat="server">
                    </asp:DropDownList>
                    </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell CssClass="encabezado">Tipo Contrato</asp:TableCell>
                <asp:TableCell>
                   <asp:DropDownList CssClass="textboxSanchez" ID="TextBoxTipoContrato" runat="server">
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell CssClass="encabezado">Escolaridad</asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList CssClass="textboxSanchez" ID="TextBoxEscolaridad" runat="server">
                        <asp:ListItem Value="001">Primaria</asp:ListItem>
                        <asp:ListItem Value="002">Secundaria</asp:ListItem>
                        <asp:ListItem Selected="True" Value="003">Preparatoria</asp:ListItem>
                        <asp:ListItem Value="004">Universidad</asp:ListItem>
                        <asp:ListItem Value="005">Maestria</asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell CssClass="encabezado">Rango de Edad</asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox CssClass="textboxSanchez" ID="TextBoxEdad" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell CssClass="encabezado">Horario</asp:TableCell>
                <asp:TableCell>
                        <asp:DropDownList CssClass="textboxSanchez" ID="TextBoxHorario" runat="server">
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell CssClass="encabezado">Competencias</asp:TableCell>
                <asp:TableCell>
                    
                    <asp:TextBox ID="TextBoxCompetencia" name="TextBoxCompetencia" CssClass="textboxSanchez" TextMode="multiline"
                        Columns="50" Rows="5" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell CssClass="encabezado">Sexo</asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList CssClass="textboxSanchez" ID="TextBoxSexo" runat="server">
                        <asp:ListItem Selected="True" Value="I">Indistinto</asp:ListItem>
                        <asp:ListItem Value="M">Masculino</asp:ListItem>
                        <asp:ListItem Value="F">Femenino</asp:ListItem>
                    </asp:DropDownList>
                    </asp:TableCell>
            </asp:TableRow>
                        <asp:TableRow>
                <asp:TableCell CssClass="encabezado">Fecha de vigencia</asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox CssClass="textboxSanchez" type="date" name="fechaVig" ID="fechaVig" runat="server"></asp:TextBox>

                    </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Button ID="Button1" OnClick="GuardarDatos" runat="server" Text="Aceptar" /></asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="Button2" OnClick="Cancelar" runat="server" Text="Cancelar" /></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        </div>
    </div>
</asp:Content>
