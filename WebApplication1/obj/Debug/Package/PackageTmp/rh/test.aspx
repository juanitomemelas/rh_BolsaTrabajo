<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, System.EventArgs e) { 
        if(!Page.IsPostBack){
            ListBox1.DataSource = Roles.GetAllRoles();
            ListBox1.DataBind();
        }
    }

    protected void ListBox1_SelectedIndexChanged(object sender, System.EventArgs e) {
        ListBox2.DataSource = Roles.GetUsersInRole(ListBox1.SelectedItem.Text.ToString());
        ListBox2.DataBind();
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GetUsersInRole method example: how to get all the users in a role programmatically</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>GetUsersInRole method example</h2>
        <b>All Roles</b>
        <br />
        <asp:ListBox ID="ListBox1" runat="server" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
        <br /><br />
        <b style="color:Red">All users in selected role</b>
        <br />
        <asp:ListBox ID="ListBox2" runat="server"></asp:ListBox>
    </div>
    </form>
</body>
</html>
