<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RentasPrincipal.aspx.cs" Inherits="UTTTCIBERCOM.RentasPrincipal" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Rentas | Administración</title>
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
                        <div class="menu menuActivo d-inline">
                            <asp:Button Text="Rentas" runat="server" class="menuLinkBtnActivo text-center" OnClick="btnRentPrincipal_Click" />
                            <%--<a href="#" class="menuLinkActivo">Rentas</a>--%>
                            <div class="d-lg-none d-inline">
                                <div class="my-4" runat="server">
                                    <i class="me-2 bi bi-calendar-plus"></i>
                                    <asp:Button Text="Iniciar Renta" runat="server" class="menuLinkBtnActivo" OnClick="btnRentPrincipal_Click" />
                                </div>
                                <div class="my-4" runat="server">
                                    <i class="me-2 bi bi-calendar2-x"></i>
                                    <asp:Button Text="Terminar Renta" runat="server" class="menuLinkBtnActivo" OnClick="btnRentManager_Click" />
                                </div>
                                <div class="my-4" runat="server">
                                    <i class="me-2 bi bi-cash-stack"></i>
                                    <asp:Button Text="Ver rentas" runat="server" class="menuLinkBtnActivo linkActivo" OnClick="btnRentas_Click" />
                                </div>
                                <div class="my-4" runat="server">
                                    <i class="me-2 bi bi-pc-display"></i>
                                    <asp:Button Text="Ver maquinas" runat="server" class="menuLinkBtnActivo" OnClick="btnPCPrincipal_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="menu">
                            <asp:Button Text="Computadoras" runat="server" class="menuLinkBtn text-center" OnClick="btnPCPrincipal_Click" />
                            <%--<a href="#" class="menuLink">Computadoras</a>--%>
                        </div>
                        <div class="menu">
                            <asp:Button Text="Empleados" runat="server" class="menuLinkBtn text-center" OnClick="btnUserPrincipal_Click" />
                            <%--<a href="#" class="menuLink">Empleados</a>--%>
                        </div>
                        <div class="menu">
                            <asp:Button Text="Cerrar sesión" runat="server" class="menuLinkBtn text-center" OnClick="btnLogout_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </nav>

        <!-- Cuerpo -->
        <div class="mt-4 row container-fluid">
            <div class="text-start ps-4 d-lg-block d-none" style="border-right: 1px solid #555; width: 20%;">
                <div class="my-4" runat="server">
                    <i class="me-2 bi bi-calendar-plus"></i>
                    <asp:Button Text="Iniciar Renta" runat="server" class="menuLinkBtnActivo" OnClick="btnRentPrincipal_Click" />
                </div>
                <div class="my-4" runat="server">
                    <i class="me-2 bi bi-calendar2-x"></i>
                    <asp:Button Text="Terminar Renta" runat="server" class="menuLinkBtnActivo" OnClick="btnRentManager_Click" />
                </div>
                <div class="my-4" runat="server">
                    <i class="me-2 bi bi-cash-stack"></i>
                    <asp:Button Text="Ver rentas" runat="server" class="menuLinkBtnActivo linkActivo" OnClick="btnRentas_Click" />
                </div>
                <div class="my-4" runat="server">
                    <i class="me-2 bi bi-pc-display"></i>
                    <asp:Button Text="Ver maquinas" runat="server" class="menuLinkBtnActivo" OnClick="btnPCPrincipal_Click" />
                </div>
            </div>

            <!-- Dispositivo Grande -->
            <div class="bodySize ps-5">
                <div class="px-4 d-none">
                    <nav class="navbar navbar-expand-lg navbar-light">
                        <div class="container-fluid">
                            <div class="collapse navbar-collapse" id="navbarSupportedContent">
                                <ul class="navbar-nav me-auto mb-2 mb-lg-0"></ul>
                                <div class="d-flex">
                                    <input class="form-control me-2" type="search" placeholder="Search" aria-label="Search">
                                    <asp:DropDownList ID="ddlSexo" class="btn btn-secondary dropdown-toggle me-2" runat="server" />
                                    <button class="btn btn-outline-success" type="submit">Search</button>
                                </div>
                            </div>
                        </div>
                    </nav>
                </div>
                <div class="mt-2 ms-4"><h3>Rentas realizadas</h3></div>
                <!-- Tabla de contenido -->
                <div class="table-responsive p-4" style="width: 100%">
                    <asp:GridView ID="dgvRentas" runat="server"
                        AllowPaging="false" AutoGenerateColumns="False" DataSourceID="DataSourceRentas"
                        Width="100%" CellPadding="3" GridLines="Horizontal"
                        OnRowCommand="dgvRentas_RowCommand" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px"
                        ViewStateMode="Disabled" class="table table-sm ">
                        <AlternatingRowStyle BackColor="#F7F7F7" />
                        <Columns>
                            <asp:BoundField DataField="dteFechaInicio" HeaderText="Fecha Inicio" ReadOnly="True"
                                SortExpression="dteFechaInicio" />
                            <asp:BoundField DataField="dteFechaFinal" HeaderText="Fecha Final" ReadOnly="True"
                                SortExpression="dteFechaFinal" />
                            <asp:BoundField DataField="idEmpleado" HeaderText="Empleado" ReadOnly="True"
                                SortExpression="idEmpleado" />
                            <asp:BoundField DataField="idEquipo" HeaderText="Equipo" ReadOnly="True"
                                SortExpression="idEquipo" />
                            <asp:BoundField DataField="monTotal" HeaderText="Total" ReadOnly="True"
                                SortExpression="monTotal" />
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
                        </Columns>

                    </asp:GridView>
                </div>
            </div>
        </div>

    </form>


    <asp:LinqDataSource ID="DataSourceRentas" runat="server"
        ContextTypeName="Data.Linq.Entity.DcGeneralDataContext"
        OnSelecting="DataSourceRentas_Selecting"
        Select="new (dteFechaInicio, dteFechaFinal, idEmpleado, idEquipo, monTotal, Id)"
        TableName="RENTA" EntityTypeName="">
    </asp:LinqDataSource>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
    <%--<script src="../content/bootstrap.js"></script>
    <script src="../content/bootstrap.bundle.js"></script>--%>
</body>
</html>
