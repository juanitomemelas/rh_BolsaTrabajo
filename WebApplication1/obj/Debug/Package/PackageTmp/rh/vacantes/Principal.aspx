<%@ Page Language="C#" Title="Lista de vacantes" AutoEventWireup="true"  MasterPageFile="~/rh/headerVacio.Master" CodeBehind="Principal.aspx.cs" Inherits="RHVacantes.vacantes.Principal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentVacio" runat="server">
    <link href="../../Scripts/UI/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/buscaTabla/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentVacio" runat="server">
       <div class="Contactos">
            <asp:GridView ID="GridView1" runat="server" 
                DataKeyNames="ID" DataSourceID="SqlDataSource1" 
                 PageSize="25" AutoGenerateColumns="False" Width="95%" cssclass="search-table tablesorter"
                onselectedindexchanged="GridView1_SelectedIndexChanged">
                <Columns >      
                                    <asp:TemplateField HeaderText="Postularse" SortExpression="ID">
                        <ItemTemplate>
                           <img alt="imagen Postulacion" 
                                title="Clic para postularse" 
                                class="estiloMano" 
                                style="height:24px; width:24px;" 
                                src="../Imagenes/iconoPostularse.png" 
                                onclick="javascript:abreVacante('<%# String.Format("{0:00000}", Eval("ID")) %>','<%# Eval("PUESTO") %>')" />
                        </ItemTemplate>
                    </asp:TemplateField>               
                    <asp:BoundField DataField="PUESTO" HeaderText="Puesto"  />
                   <asp:TemplateField HeaderText="Descripción" SortExpression="DESCRIPCION">
                        <ItemTemplate>
                            <div id="POS_<%# String.Format("{0:00000}", Eval("ID")) %>"><%# Eval("DESCRIPCION")%></div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="COMPETENCIAS" HeaderText="Competencias"  />
                    <asp:BoundField DataField="UBICACION" HeaderText="Ubicación" />
                    <asp:BoundField DataField="RANGO_EDAD" HeaderText="Rango de Edad" />

                </Columns>
                <HeaderStyle CssClass="encabezado" />
            </asp:GridView>


            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConflictDetection="CompareAllValues" 
                ConnectionString="<%$ ConnectionStrings:ConexionOracleSanchez %>" 
                DeleteCommand="DELETE FROM &quot;RH_VACANTES&quot; WHERE &quot;ID&quot; = ? AND ((&quot;PUESTO&quot; = ?) OR (&quot;PUESTO&quot; IS NULL AND ? IS NULL)) AND ((&quot;DESCRIPCION&quot; = ?) OR (&quot;DESCRIPCION&quot; IS NULL AND ? IS NULL)) AND ((&quot;UBICACION&quot; = ?) OR (&quot;UBICACION&quot; IS NULL AND ? IS NULL)) AND ((&quot;TIPO_CONTRATO&quot; = ?) OR (&quot;TIPO_CONTRATO&quot; IS NULL AND ? IS NULL)) AND ((&quot;RANGO_EDAD&quot; = ?) OR (&quot;RANGO_EDAD&quot; IS NULL AND ? IS NULL)) AND ((&quot;IDIOMA&quot; = ?) OR (&quot;IDIOMA&quot; IS NULL AND ? IS NULL)) AND ((&quot;SEXO&quot; = ?) OR (&quot;SEXO&quot; IS NULL AND ? IS NULL)) AND &quot;STATUS&quot; = ?" 
                InsertCommand="INSERT INTO &quot;RH_VACANTES&quot; (&quot;ID&quot;, &quot;PUESTO&quot;, &quot;DESCRIPCION&quot;, &quot;UBICACION&quot;, &quot;TIPO_CONTRATO&quot;, &quot;RANGO_EDAD&quot;, &quot;IDIOMA&quot;, &quot;SEXO&quot;, &quot;STATUS&quot;) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)" 
                OldValuesParameterFormatString="original_{0}" 
                ProviderName="<%$ ConnectionStrings:ConexionOracleSanchez.ProviderName %>" 
                SelectCommand="
 SELECT RH_VACANTES.ID, 
        RH_VACANTES.PUESTO, 
        RH_VACANTES.DESCRIPCION,
        RH_VACANTES.COMPETENCIAS, 
        RH_LOCACIONES.LOCACION as UBICACION,
        RH_TIPOCONTRATO.CONTRATO as TIPO_CONTRATO, 
        RH_HORARIO.HORARIO as HORARIO,
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
WHERE (RH_VACANTES.STATUS = 'V') order by RH_VACANTES.ID" 
>
                
                
            </asp:SqlDataSource>
</div>
    <script src="../../Scripts/jquery-1.12.3.min.js" type="text/javascript"></script>
        <script src="../../Scripts/UI/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/buscaTabla/html-table-search.js" type="text/javascript"></script>
    <script src="../../Scripts/vacantes.js" type="text/javascript"></script>
    <script src="../../Scripts/buscaTabla/jquery.tablesorter.min.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                $('table.search-table').tableSearch({
                    searchText: 'Búsqueda',
                    searchPlaceHolder: 'ingrese un valor',
                    caseSensitive: false
                });
                $("#MainContentVacio_GridView1").tablesorter({
                    headers: {
                        0: { sorter: false }
                    }
                });
            });
           
            $("#elIDFrame").load(function () {
            $(this).height($(this).contents().find("html").height());
        });

        </script>
    <div id="dialog" title="Basic dialog" style="display:none; width:550px;">
  <iframe id="elIDFrame"  class="iframeSinBordes" src=""> Su navegador no soporta iFrames, por favor, habilitelos  </iframe>
  </div>
  <br />
  <br />
  <br />
  <br />
</asp:Content>