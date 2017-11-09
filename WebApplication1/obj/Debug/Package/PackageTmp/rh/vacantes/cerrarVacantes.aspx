<%@ Page Title="Cerrar vacantes" Language="C#" MasterPageFile="~/rh/headerVacio.Master"
    AutoEventWireup="true" CodeBehind="cerrarVacantes.aspx.cs" Inherits="RHVacantes.rh.vacantes.cerrarVacantes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentVacio" runat="server">
    <link href="../../Scripts/UI/jquery-ui.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentVacio" runat="server">
    <div>
        <div class="Contactos">
            <asp:GridView ID="GridView1" runat="server" AllowPaging="false"  DataKeyNames="STATUS" DataSourceID="SqlDataSource1"
                PageSize="25" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                 cssclass="search-table tablesorter" >
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="Id" ReadOnly="false" />
                    <asp:BoundField DataField="PUESTO" HeaderText="Puesto" />
                    <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción" />
                    <asp:BoundField DataField="COMPETENCIAS" HeaderText="Competencias" />
                    <asp:BoundField DataField="UBICACION" HeaderText="Ubicación" />
                    <asp:BoundField DataField="TIPO_CONTRATO" HeaderText="Tipo Contrato" />
                    <asp:BoundField DataField="HORARIO" HeaderText="Horario" />
                    <asp:BoundField DataField="ESCOLARIDAD" HeaderText="Escolaridad" />
                    <asp:BoundField DataField="RANGO_EDAD" HeaderText="Edad" />
                    <asp:TemplateField HeaderText="Sexo" SortExpression="SEXO">
                        <ItemTemplate>
                        <%# ("I".Equals(Convert.ToString(Eval("SEXO")))?"Indistinto":"F".Equals(Convert.ToString(Eval("SEXO")))? "Femenino":"Masculino")%>      
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Estatus" SortExpression="STATUS">
                        <ItemTemplate>
                            <%# (Convert.ToString(Eval("STATUS"))=="V"?"Vigente":"Cerrado")%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Modificar" SortExpression="ID">
                        <ItemTemplate>
                            <img alt="imagen Modificar Registro" 
                            title="Clic para modificar el registro" 
                            class="estiloMano"
                            style="height: 24px; width: 24px;" 
                            src="../Imagenes/IconoModificar.png" 
                            onclick="javascript:agregaFilaEditable('<%# Eval("ID") %>',$(this))" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="encabezado" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues"
                ConnectionString="<%$ ConnectionStrings:ConexionOracleSanchez %>" ProviderName="<%$ ConnectionStrings:ConexionOracleSanchez.ProviderName %>"
                SelectCommand="
 SELECT RH_VACANTES.ID, 
        RH_VACANTES.PUESTO, 
        RH_VACANTES.DESCRIPCION,
        RH_VACANTES.COMPETENCIAS, 
        RH_LOCACIONES.LOCACION as UBICACION,
        RH_TIPOCONTRATO.CONTRATO as TIPO_CONTRATO, 
        RH_HORARIO.HORARIO as HORARIO,
        RH_ESCOLARIDAD.DESCRIPCION as ESCOLARIDAD,
        RH_VACANTES.RANGO_EDAD, 
        RH_VACANTES.SEXO, 
        RH_VACANTES.STATUS 
 FROM RH_VACANTES

 LEFT JOIN RH_LOCACIONES
on RH_VACANTES.UBICACION = RH_LOCACIONES.ID 
left join RH_TIPOCONTRATO
ON RH_VACANTES.TIPO_CONTRATO = RH_TIPOCONTRATO.ID
LEFT JOIN RH_HORARIO
ON RH_VACANTES.HORARIO = RH_HORARIO.ID
LEFT JOIN RH_ESCOLARIDAD
ON RH_VACANTES.ESCOLARIDAD = RH_ESCOLARIDAD.ID
order by RH_VACANTES.ID" >
                <UpdateParameters>
                    <asp:Parameter Name="Status" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
    </div>
    <div>
    </div>
    <script src="../../Scripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../../Scripts/buscaTabla/html-table-search.js" type="text/javascript"></script>
    <script src="../../Scripts/buscaTabla/jquery.tablesorter.min.js" type="text/javascript"></script>
    <script src="../../Scripts/vacantes.js" type="text/javascript"></script>
    <script type="text/javascript">
            $(document).ready(function () {
                $('table.search-table').tableSearch({
                    searchText: 'Búsqueda',
                    searchPlaceHolder: 'ingrese un valor',
                    caseSensitive: false
                });
                $("#MainContentVacio_GridView1").tablesorter({
                    headers: {
                        7: { sorter: false }
                    }
                });
            });
            </script>
</asp:Content>
