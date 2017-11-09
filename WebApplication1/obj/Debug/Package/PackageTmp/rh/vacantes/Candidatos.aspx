<%@ Page Title="Lista de Candidatos" Language="C#" MasterPageFile="~/rh/headerVacio.Master" AutoEventWireup="true" CodeBehind="Candidatos.aspx.cs" Inherits="RHVacantes.rh.vacantes.Candidatos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentVacio" runat="server">
    <link href="../../Scripts/UI/jquery-ui.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentVacio" runat="server">
       <div class="Contactos">
<asp:GridView ID="GridView1" runat="server" DataKeyNames="ID_VACANTE" DataSourceID="SqlDataSource1"
                PageSize="10" AutoGenerateColumns="False" >
                <Columns>
                    <asp:BoundField DataField="ID_VACANTE" HeaderText="Número de vacante" ReadOnly="false" />
                    <asp:TemplateField HeaderText="Vacante" SortExpression="PUESTO">
                        <ItemTemplate>
                        <a href="javascript:abreVacanteCompleta('<%# Eval("ID") %>')" title="Clic para ver la vacante"  onclick=""><%# Eval("PUESTO")%></a>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FECHA_POSTULACION" HeaderText="Fecha de postulación" DataFormatString="{0:dd/MMM/yyyy}" />
                    <asp:BoundField DataField="NUMEMPLEADO" HeaderText="Número de empleado" />
                    <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" />
                    <asp:BoundField DataField="LUGAR_TRABAJO" HeaderText="Lugar de trabajo" />
                    <asp:TemplateField HeaderText="Correo" SortExpression="CORREO">
                        <ItemTemplate>
                        <a href="javascript:abreFormaCorreo('<%# Eval("ID") %>','<%# Eval("PUESTO") %>','<%# Eval("NUMEMPLEADO") %>','<%# Eval("NOMBRE") %>','<%# Eval("CORREO") %>')" title="Clic para enviar correo al candidato"  onclick=""><%# Eval("CORREO") %></a>
                            
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="TELEFONO" NullDisplayText="N/A" HeaderText="Teléfono" />
                    <asp:BoundField DataField="CELULAR" NullDisplayText="N/A" HeaderText="Celular" />
                    <asp:TemplateField HeaderText="Sueldo Deseado" SortExpression="SUELDO_DESEADO">
                        <ItemTemplate>
                        <%# String.Format("{0:C0}", Eval("SUELDO_DESEADO"))%>            
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CV" SortExpression="ID_VACANTE">
                        <ItemTemplate>
                            <asp:Button id="Button1"
                                Text=""
                                OnCommand="obtienePDF"    
                                CssClass="botonVacantesPDF"                             
                                runat="server"
                                />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="encabezado" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues"
                ConnectionString="<%$ ConnectionStrings:ConexionOracleSanchez %>" ProviderName="<%$ ConnectionStrings:ConexionOracleSanchez.ProviderName %>"
                SelectCommand="SELECT RH_CANDIDATOS.ID_VACANTE, 
        RH_CANDIDATOS.ID, 
        RH_VACANTES.PUESTO,
        RH_CANDIDATOS.FECHA_POSTULACION, 
        RH_CANDIDATOS.NOMBRE, 
        RH_CANDIDATOS.NUMEMPLEADO, 
        RH_LOCACIONES.LOCACION as LUGAR_TRABAJO, 
        RH_CANDIDATOS.CORREO, 
        RH_CANDIDATOS.TELEFONO, 
        RH_CANDIDATOS.CELULAR, 
        NVL(RH_CANDIDATOS.SUELDO_DESEADO,0) as SUELDO_DESEADO,
        RH_CANDIDATOS.TELEFONO,
        RH_CANDIDATOS.CELULAR,
        RH_CANDIDATOS.SUELDO_DESEADO,
        RH_CANDIDATOS.CV 
        FROM RH_CANDIDATOS       
        LEFT OUTER JOIN
    EDUARDO.RH_VACANTES
ON
    ( RH_CANDIDATOS.ID = RH_VACANTES.ID)     
     LEFT JOIN RH_LOCACIONES
on RH_CANDIDATOS.LUGAR_TRABAJO = RH_LOCACIONES.ID     
        order by RH_CANDIDATOS.ID_VACANTE"
                UpdateCommand="UPDATE RH_VACANTES SET STATUS =:STATUS WHERE ID = 1">
                <UpdateParameters>
                    <asp:Parameter Name="Status" />
                </UpdateParameters>
            </asp:SqlDataSource>
            </div>
    <script src="../../Scripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../../Scripts/UI/jquery-ui.min.js" type="text/javascript"></script>
     <script src="../../Scripts/vacantes.js" type="text/javascript"></script>
        <script type="text/javascript">
            $("#elIDFrame").load(function () {
                $(this).height($(this).contents().find("html").height());
            });
            function acomoda() {
                $("#elIDFrame").height("99%");
            }
        </script>
    <div id="dialog" title="Basic dialog" style="display:none; width:550px;">
  <iframe id="elIDFrame" class="iframeSinBordes" src=""> Su navegador no soporta iFrames, por favor, habilitelos  </iframe>
  </div>
</asp:Content>

