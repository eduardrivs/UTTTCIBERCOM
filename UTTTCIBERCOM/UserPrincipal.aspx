<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPrincipal.aspx.cs" Inherits="UTTTCIBERCOM.app.UserPrincipal" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Usuarios | Administración</title>
    <link rel="icon" type="image/vnd.microsoft.icon" href="~/content/images/favicon.png">
    <link href="~/content/css/RentPrincipal.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous">
    <%--<link href="../content/bootstrap.css" rel="stylesheet" />--%>
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
                        <div class="menu">
                            <asp:Button Text="Rentas" runat="server" class="menuLinkBtn text-center" OnClick="btnRentPrincipal_Click" />
                            <%--<a href="#" class="menuLinkActivo">Rentas</a>--%>
                        </div>
                        <div class="menu">
                            <asp:Button Text="Computadoras" runat="server" class="menuLinkBtn text-center" OnClick="btnPCPrincipal_Click" />
                            <%--<a href="#" class="menuLink">Computadoras</a>--%>
                        </div>
                        <div class="menu menuActivo d-inline-block">
                            <asp:Button Text="Empleados" runat="server" class="menuLinkBtnActivo text-center" OnClick="btnUserPrincipal_Click" />
                            <%--<a href="#" class="menuLink">Empleados</a>--%>
                            <div class="d-lg-none d-inline">
                                <div id="btnNewEmp1" class="my-4" runat="server">
                                    <i class="me-2 bi bi-person-plus-fill"></i>
                                    <asp:Button Text="Nuevo Empleado" runat="server" class="menuLinkBtnActivo" OnClick="btnUserManager_Click" />
                                </div>
                                <div id="btnNewUser1" class="my-4" runat="server">
                                    <i class="me-2 bi bi-person-badge"></i>
                                    <asp:Button Text="Nuevo Usuario" runat="server" class="menuLinkBtnActivo" OnClick="btnUserLogManager_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="menu">
                            <asp:Button Text="Cerrar sesión" runat="server" class="menuLinkBtn text-center" OnClick="btnLogout_Click" />
                        </div>
                        <div id="btnInfo1" class="menu d-block d-lg-none" runat="server" visible="false">
                            <asp:Label Text="Se han bloqueado ciertas acciones debido a los permisos de tu cuenta" runat="server" CssClass="txtInfo" />
                        </div>
                    </div>
                </div>
            </div>
        </nav>

        <!-- Contenido -->
        <div class="mt-4 row container-fluid mb-5 mb-lg-0">
            <div class="text-start ps-4 d-lg-block d-none" style="border-right: 1px solid #555; width: 20%;">
                <div id="btnNewEmp2" class="my-4" runat="server">
                    <i class="me-2 bi bi-person-plus-fill"></i>
                    <asp:Button Text="Nuevo Empleado" runat="server" class="menuLinkBtnActivo" OnClick="btnUserManager_Click" />
                </div>
                <div id="btnNewUser2" class="my-4" runat="server">
                    <i class="me-2 bi-person-badge"></i>
                    <asp:Button Text="Nuevo Usuario" runat="server" class="menuLinkBtnActivo" OnClick="btnUserLogManager_Click" />
                </div>
                <div id="btnInfo2" class="my-4 pt-2" runat="server" visible="false">
                    <asp:Label Text="Se han bloqueado ciertas acciones debido a los permisos de tu cuenta" runat="server" CssClass="txtInfo"/>
                </div>
            </div>

            <!-- Cuerpo -->
            <div class="ps-lg-5 ps-2 bodySize">
                <!-- Barra Busqueda -->
                <div class="px-lg-4">
                    <nav class="navbar navbar-expand navbar-light">
                        <div class="container-fluid">
                            <div class="collapse navbar-collapse" id="navbarSupportedContent">
                                <ul class="navbar-nav me-auto mb-2 mb-lg-0"></ul>
                                <div class="d-flex">
                                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control me-2 d-flex" AutoPostBack="true" />
                                    <asp:DropDownList ID="ddlDisp" class="btn btn-secondary dropdown-toggle me-2" runat="server" AutoPostBack="true" />
                                    <asp:Button Text="Buscar" runat="server" CssClass="btn btn-outline-success" OnClick="btnBuscar_Click" />
                                </div>
                            </div>
                        </div>
                    </nav>
                </div>
                <!-- Tabla de contenido -->
                <div class="table-responsive p-4" style="width: 100%">
                    <asp:GridView ID="dgvEmpleados" runat="server"
                        AllowPaging="true" ShowFooter="true" AutoGenerateColumns="False" DataSourceID="DataSourceEmpleados"
                        Width="100%" CellPadding="3" GridLines="Horizontal"
                        OnRowCommand="dgvEmpleados_RowCommand" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px"
                        ViewStateMode="Disabled" class="table table-sm mt-2">
                        <AlternatingRowStyle BackColor="#F7F7F7" />
                        <Columns>
                            <asp:BoundField DataField="strNombre" HeaderText="Nombre" ReadOnly="True"
                                SortExpression="strNombre" />
                            <asp:BoundField DataField="strAPaterno" HeaderText="A.Paterno" ReadOnly="True"
                                SortExpression="strAPaterno" />
                            <asp:BoundField DataField="strAMaterno" HeaderText="A.Materno" ReadOnly="True"
                                SortExpression="strAMaterno" />
                            <asp:BoundField DataField="intEdad" HeaderText="Edad"
                                SortExpression="intEdad" />
                            <asp:BoundField DataField="dteFechaIngreso" HeaderText="Fecha Ingreso"
                                SortExpression="dteFechaIngreso" />
                            <%--<asp:BoundField DataField="boolActivo" HeaderText="Estatus"
                                SortExpression="boolActivo" />--%>

                            <asp:TemplateField HeaderText="Estatus">
                                <ItemTemplate>
                                    <%# (Boolean.Parse(Eval("boolActivo").ToString())) ? "Activo" : "Inactivo" %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Editar">

                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="imgEditar" CommandName="Editar" CommandArgument='<%#Bind("id") %>' ImageUrl="~/content/images/editrecord_16x16.png" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Eliminar" Visible="True">

                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="imgEliminar" CommandName="Eliminar" CommandArgument='<%#Bind("id") %>' ImageUrl="~/content/images/delrecord_16x16.png" OnClientClick="javascript:return confirm('¿Está seguro de querer eliminar el registro seleccionado?', 'Mensaje de sistema')" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Usuario">

                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="imgUsuario" CommandName="Usuario" CommandArgument='<%#Bind("id") %>' ImageUrl="~/content/images/editrecord_16x16.png" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />

                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>


    <asp:LinqDataSource ID="DataSourceEmpleados" runat="server"
        ContextTypeName="Data.Linq.Entity.DcGeneralDataContext"
        OnSelecting="DataSourceEmpleado_Selecting"
        Select="new (strNombre, strAPaterno, strAMaterno, intEdad, dteFechaIngreso,boolActivo,Id)"
        TableName="EMPLEADO" EntityTypeName="">
    </asp:LinqDataSource>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
    <%--<script src="../content/bootstrap.js"></script>
    <script src="../content/bootstrap.bundle.js"></script>--%>
</body>
</html>
