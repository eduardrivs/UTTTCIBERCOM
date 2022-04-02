<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRecoveryPass.aspx.cs" Inherits="UTTTCIBERCOM.UserRecoveryPass" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Rentas | Administración</title>
    <link rel="icon" type="image/vnd.microsoft.icon" href="~/content/images/favicon.png" />
    <link href="~/content/css/RentPrincipal.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous" />
    <%--<link href="../content/bootstrap.css" rel="stylesheet" />--%>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2 class="mt-2">Recupera tu contraseña we</h2>
            <div class="mt-3">
                <asp:TextBox ID="txtNombre" class="form-control me-3" runat="server" Width="50%" AutoPostBack="true"></asp:TextBox>
                <asp:Button ID="btnBuscar" class="btn btn-outline-success" runat="server" Text="Buscar" ViewStateMode="Disabled" />
            </div>
            <div class="mt-2">
                <asp:GridView ID="dgvPersonas" runat="server"
                    AllowPaging="True" AutoGenerateColumns="False" DataSourceID="DataSourceUser"
                    Width="100%" CellPadding="3" GridLines="Horizontal"
                    OnRowCommand="dgvUser_RowCommand" BackColor="White"
                    BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px"
                    ViewStateMode="Disabled" class="table table-sm mt-2">
                    <AlternatingRowStyle BackColor="#F7F7F7" />
                    <Columns>
                        <asp:BoundField DataField="email" HeaderText="email" ReadOnly="True" SortExpression="email" />
                        <asp:BoundField DataField="username" HeaderText="username" ReadOnly="True" SortExpression="username" />
                        <asp:BoundField DataField="password" HeaderText="contraseña" ReadOnly="True" SortExpression="password" />
                        
                        <asp:TemplateField HeaderText="Accion">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="imgCambiar" CommandName="Password" CommandArgument='<%#Bind("Id") %>' ImageUrl="~/content/images/editrecord_16x16.png" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="50px"/>
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>

                <asp:TextBox ID="txtPass1" Visible="false" class="form-control me-3" runat="server" Width="50%"></asp:TextBox>
                <asp:TextBox ID="txtPass2" Visible="false" class="form-control me-3" runat="server" Width="50%"></asp:TextBox>
                <asp:Button ID="btnCambiar" Visible="false" class="btn btn-outline-success" runat="server" Text="Cambiar" ViewStateMode="Disabled" OnClick="btnCambiarContra_Click"/>
            </div>
        </div>
    </form>

    <asp:LinqDataSource ID="DataSourceUser" runat="server"
        ContextTypeName="Data.Linq.Entity.DcGeneralDataContext"
        OnSelecting="DataSourceUser_Selecting"
        Select="new (email, username, password, id)"
        TableName="Persona" EntityTypeName="">
    </asp:LinqDataSource>
</body>
</html>
