<%@ Page Title="" Language="C#" MasterPageFile="~/rh/headerVacio.Master" AutoEventWireup="true" CodeBehind="administracion.aspx.cs" Inherits="RHVacantes.rh.Administracion.administracion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentVacio" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentVacio" runat="server">
    <div class="Contactos">
    <div>
      <asp:Label ID="mensajeError" runat="server" ></asp:Label>
        <table cellspacing="0" border="1" style="width:80%;border-collapse:collapse;">
            <tr>
                <td colspan="2" class="encabezado">
                    Nueva Ubicación
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblName" runat="server" Text="Ubicación"></asp:Label>
                    <asp:TextBox ID="txtNuevaLocacion" runat="server"></asp:TextBox>
                </td>
                <td>
                
                   <asp:Button ID="Button2" runat="server" Text="Agregar" OnClick="Submit_Click1" /> 
                </td>
            </tr>
        </table>
        </div>
         <br /> 
        <div>
        <asp:GridView ID="GridView1" 
                      runat="server" 
                      PageSize="50" 
                      AutoGenerateColumns="false"
                      AllowPaging="true" 
                      width="80%"
                      OnRowEditing="GridView1_RowEditing" 
                      OnRowUpdating="GridView1_RowUpdating"
                      OnPageIndexChanging="GridView1_PageIndexChanging" 
                      OnRowCancelingEdit="GridView1_RowCancelingEdit"
                      OnRowDeleting="GridView1_RowDeleting">
                      
            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
            <HeaderStyle cssClass="encabezado" />
            <Columns>
                <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>
                        <asp:Label ID="lblstid" runat="server" Text='<%#Eval ("ID")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ubicación">
                <EditItemTemplate>
                <asp:TextBox ID="txtLocacion" runat="server" Text='<%#Eval("LOCACION")%>'> </asp:TextBox>
                </EditItemTemplate>
                    <ItemTemplate>                        
                          <asp:Label ID="lblLocacion" runat="server" Text='<%#Eval ("LOCACION")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField ShowHeader="false">
                    <EditItemTemplate>
                        <asp:LinkButton ID="lnkbtnUpdate" runat="server" CausesValidation="true" Text="Actualizar"
                            CommandName="Update"></asp:LinkButton>
                        <asp:LinkButton ID="lnkbtnCancel" runat="server" CausesValidation="false" Text="Cancelar"
                            CommandName="Cancel"></asp:LinkButton>
                            <asp:LinkButton ID="lnkbtnDelete" runat="server" CausesValidation="false" Text="Eliminar"
                            CommandName="Delete"  ></asp:LinkButton>
                            
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CommandName="Edit"
                            Text="Editar"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
        </asp:GridView>
       
        </div>
        <!-------------------------------------------------Contratos---------------------------------------------------------------------------------->
            <div>
            <br />
            <br />
    
        <table cellspacing="0" border="1" style="width:80%;border-collapse:collapse;">
            <tr>
                <td colspan="2" class="encabezado">
                    Nuevo Tipo de Contrato
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Contrato"></asp:Label>
                    <asp:TextBox ID="txtContrato" runat="server"></asp:TextBox>
                </td>
                <td>
                
                   <asp:Button ID="Button1" runat="server" Text="Agregar" OnClick="Submit_Click1Contratos" /> 
                </td>
            </tr>
        </table>
        </div>
         <br /> 
        <div>
        <asp:GridView ID="gridViewContratos" 
                      runat="server" 
                      PageSize="50" 
                      AutoGenerateColumns="false"
                      AllowPaging="true" 
                      width="80%"
                      OnRowEditing="GridView1_RowEditingContratos" 
                      OnRowUpdating="GridView1_RowUpdatingContratos"
                      OnPageIndexChanging="GridView1_PageIndexChangingContratos" 
                      OnRowCancelingEdit="GridView1_RowCancelingEditContratos"
                      OnRowDeleting="GridView1_RowDeletingContratos">
                      
            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
            <HeaderStyle cssClass="encabezado" />
            <Columns>
                <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>
                        <asp:Label ID="lblstidContratos" runat="server" Text='<%#Eval ("ID")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tipo de Contrato">
                <EditItemTemplate>
                <asp:TextBox ID="txtContratos" runat="server" Text='<%#Eval("CONTRATO")%>'> </asp:TextBox>
                </EditItemTemplate>
                    <ItemTemplate>                        
                          <asp:Label ID="lblContratos" runat="server" Text='<%#Eval ("CONTRATO")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField ShowHeader="false">
                    <EditItemTemplate>
                        <asp:LinkButton ID="lnkbtnUpdateContratos" runat="server" CausesValidation="true" Text="Actualizar"
                            CommandName="Update"></asp:LinkButton>
                        <asp:LinkButton ID="lnkbtnCancelContratos" runat="server" CausesValidation="false" Text="Cancelar"
                            CommandName="Cancel"></asp:LinkButton>
                            <asp:LinkButton ID="lnkbtnDeleteContratos" runat="server" CausesValidation="false" Text="Eliminar"
                            CommandName="Delete"  ></asp:LinkButton>
                            
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEditContratos" runat="server" CausesValidation="false" CommandName="Edit"
                            Text="Editar"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
        </asp:GridView>
       
        </div>
        <!---------------------------------------------------------------------Horarios-------------------------------------------------------------->
                    <div>
            <br />
            <br />
    
        <table cellspacing="0" border="1" style="width:80%;border-collapse:collapse;">
            <tr>
                <td colspan="2" class="encabezado">
                    Nuevo Horario
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelHorario" runat="server" Text="Horario"></asp:Label>
                    <asp:TextBox ID="txtNuevoHorario" runat="server"></asp:TextBox>
                </td>
                <td>
                
                   <asp:Button ID="Button3" runat="server" Text="Agregar" OnClick="Submit_Click1Horario" /> 
                </td>
            </tr>
        </table>
        </div>
         <br /> 
        <div>
        <asp:GridView ID="gridViewHorario" 
                      runat="server" 
                      PageSize="50" 
                      AutoGenerateColumns="false"
                      AllowPaging="true" 
                      width="80%"
                      OnRowEditing="GridView1_RowEditingHorario" 
                      OnRowUpdating="GridView1_RowUpdatingHorario"
                      OnPageIndexChanging="GridView1_PageIndexChangingHorario" 
                      OnRowCancelingEdit="GridView1_RowCancelingEditHorario"
                      OnRowDeleting="GridView1_RowDeletingHorario">
                      
            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
            <HeaderStyle cssClass="encabezado" />
            <Columns>
                <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>
                        <asp:Label ID="lblstidHorario" runat="server" Text='<%#Eval ("ID")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Horario">
                <EditItemTemplate>
                <asp:TextBox ID="txtHorario" runat="server" Text='<%#Eval("HORARIO")%>'> </asp:TextBox>
                </EditItemTemplate>
                    <ItemTemplate>                        
                          <asp:Label ID="lblHorario" runat="server" Text='<%#Eval ("HORARIO")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField ShowHeader="false">
                    <EditItemTemplate>
                        <asp:LinkButton ID="lnkbtnUpdateHorario" runat="server" CausesValidation="true" Text="Actualizar"
                            CommandName="Update"></asp:LinkButton>
                        <asp:LinkButton ID="lnkbtnCancelHorario" runat="server" CausesValidation="false" Text="Cancelar"
                            CommandName="Cancel"></asp:LinkButton>
                            <asp:LinkButton ID="lnkbtnDeleteHorario" runat="server" CausesValidation="false" Text="Eliminar"
                            CommandName="Delete"  ></asp:LinkButton>
                            
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEditHorario" runat="server" CausesValidation="false" CommandName="Edit"
                            Text="Editar"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
        </asp:GridView>
       
        </div>
        <!------------------------------------------------------------------------------------------------------------------------------>
    </div>
    <script src="../../Scripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../../Scripts/UI/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/vacantes.js" type="text/javascript"></script>
</asp:Content>
