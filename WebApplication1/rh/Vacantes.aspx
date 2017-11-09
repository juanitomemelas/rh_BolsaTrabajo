<%@ Page Title="Página principal" Language="C#" MasterPageFile="~/rh/Site.master"
    AutoEventWireup="true" CodeBehind="Vacantes.aspx.cs" Inherits="WebApplication1._Default" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

    <link href="../Scripts/jArbol/style.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/UI/jquery-ui.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="contenedorPrincipal" id="contenedorPrincipal">
        
        <div id="contenedorArbol" class="contenedorArbol">
            <div id="jArbol">
                <ul>
                 <asp:LoginView id="loginAdministracion" runat="server">

                    <LoggedInTemplate>
                    <li>Administración
                        <ul>
                            <li id="10001">Crear Vacante</li>
                            <li id="10002">Editar Vacante</li>
                            <li id="10003">Revisar Postulados</li>
                            <li id="10005">Catálogos</li>
                        </ul>
                    </li>
                    </LoggedInTemplate>
                    </asp:LoginView>        
                                                          
                    <li id="10004">Vacantes</li>
                                       
                </ul>
            </div>
        </div>
        <iframe id="elframePrincipal" class="iframeSinBordes contenedorDerecho" name="elframePrincipal" src="../rh/grupoSanchez.htm" frameborder="0" scrolling="yes"   >
                Su navegador no soporta iFrames, por favor, habilitelos </iframe>
        <!--<div id="contenedorDerecho" class="contenedorDerecho"></div>-->
    </div>
    <script src="../Scripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../Scripts/jArbol/jstree.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () { $("#jArbol").jstree(), $("#jArbol").on("changed.jstree", function (e, t) { manipulaDivcontenedorDerecho(t.selected) }), $("button").on("click", function () { $("#jArbol").jstree(!0).select_node("CreaVacante"), $("#jArbol").jstree("select_node", "CreaVacante"), $.jstree.reference("#jArbol").select_node("CreaVacante") }) }), $("#elframePrincipal").load(function () { $(this).contents().find("html").height() > 550 ? $(this).height($(this).contents().find("html").height()) : $(this).height("550px") });
    </script>
    <script src="../Scripts/vacantes.js" type="text/javascript"></script>
</asp:Content>
