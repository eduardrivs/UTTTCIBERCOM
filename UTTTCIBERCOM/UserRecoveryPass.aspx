<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRecoveryPass.aspx.cs" Inherits="UTTTCIBERCOM.UserRecoveryPass" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Que menso, perdiste tu contraseña</title>
    <link rel="icon" type="image/vnd.microsoft.icon" href="~/content/images/favicon.png" />
    <link href="~/content/css/ErrorPageServer.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous" />
</head>
<body>
    <form id="renta" runat="server">

        <!-- Barra de Navegacion -->
        <nav class="navbar navbar-expand-lg navbar-dark bg-theme sticky-top">
            <div class="container-fluid">
                <img src="/content/images/LogoBannerNeg.png" alt="INTERCOM" class="imagenBanner" />
                <button class="navbar-toggler light" type="button" data-bs-toggle="offcanvas" data-bs-target="#offcanvasNavbar" aria-controls="offcanvasNavbar">
                    <i class="navbar-toggler-icon light"></i>
                </button>
                <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasNavbar" aria-labelledby="offcanvasNavbarLabel">
                    <div class="offcanvas-header text-light bg-theme">
                        <h5 class="offcanvas-title" id="offcanvasNavbarLabel">Acciones Disponibles</h5>
                        <button type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
                    </div>
                    <div class="offcanvas-body ms-5">
                    </div>
                </div>
            </div>
        </nav>

        <!-- Cuerpo -->
        <div class="mt-4">
            <div class="container-fluid px-5 py-2">
                <h2 class="mt-2">Recuperar contraseña</h2>
                <h6>Ingrese el nombre de usuario</h6>
                <div class="mt-3 d-flex">
                    <asp:TextBox ID="txtNombre" class="form-control me-3" runat="server" Width="50%" AutoPostBack="true"></asp:TextBox>
                    <asp:Button ID="btnBuscar" class="btn btn-outline-success" runat="server" Text="Buscar" ViewStateMode="Disabled" />
                </div>
                <div id="cuerpoUsuario" runat="server" Visible="false">
                    <div id="usuarioLista" class="mt-2">
                        <asp:GridView ID="dgvPersonas" runat="server"
                            AllowPaging="True" AutoGenerateColumns="False" DataSourceID="DataSourceUser"
                            Width="100%" CellPadding="3" GridLines="Horizontal"
                            OnRowCommand="dgvUser_RowCommand" BackColor="White"
                            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px"
                            ViewStateMode="Disabled" class="table table-sm mt-2" Visible="false">
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
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>

                        <div class="container-fluid row mt-5">
                            <div class="col-12 col-lg-6">
                                <div class="d-flex justify-content-between">
                                    <label for="txtPass1">Nueva contraseña:</label>
                                    <asp:RequiredFieldValidator ID="rvftxtPass1" runat="server" class="text-danger me-5" ControlToValidate="txtPass1" ErrorMessage="&quot;La contraseña es obligatoria&quot;" ValidationGroup="gvSave"></asp:RequiredFieldValidator>
                                </div>
                                <asp:TextBox ID="txtPass1" class="form-control me-3" runat="server" Width="100%" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="col-12 col-lg-6">
                                <div class="d-flex justify-content-between">
                                    <label for="txtPass2">Repita su contraseña:</label>
                                    <asp:RequiredFieldValidator ID="rvftxtPass2" runat="server" class="text-danger me-5" ControlToValidate="txtPass2" ErrorMessage="&quot;La contraseña es obligatoria&quot;" ValidationGroup="gvSave"></asp:RequiredFieldValidator>
                                </div>
                                <asp:TextBox ID="txtPass2" class="form-control me-3" runat="server" Width="100%" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>
                        <div class="my-3 mb-5">
                            <asp:Button ID="btnCambiar" class="btn btn-outline-success" runat="server" Text="Cambiar" ViewStateMode="Disabled" OnClick="btnCambiarContra_Click" ValidationGroup="gvSave" />
                        </div>
                    </div>
                </div>
                <div class="mt-3">
                    <asp:Button Text="Regresar" runat="server" class="btn btn-outline-secondary" OnClick="btnVolver_Click" />
                </div>
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
