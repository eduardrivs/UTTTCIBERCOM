<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RentPrincipal.aspx.cs" Inherits="UTTTCIBERCOM.app.RentPrincipal" %>

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
                        <div class="menu menuActivo">
                            <asp:Button Text="Rentas" runat="server" class="menuLinkBtnActivo text-center" OnClick="btnRentPrincipal_Click" />
                            <%--<a href="#" class="menuLinkActivo">Rentas</a>--%>
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
                <div class="my-4" runat="server" onclick="btnLogout_Click"><a href="#" class="menuLinkActivo"><i class="me-2 bi bi-calendar-plus"></i>Nueva renta</a></div>
                <div class="my-4"><a href="#" class="menuLinkActivo"><i class="me-2 bi bi-calendar2-x"></i>Terminar renta</a></div>
                <div class="my-4"><a href="#" class="menuLinkActivo"><i class="me-2 bi bi-pc-display"></i>Ver maquinas</a></div>
            </div>
            <div class="ps-5" style="width: 80%;">
                <div class="table-responsive-lg p-4" style="width:100%">
                    <asp:ListView ID="lstViewComputadoras" runat="server" DataSourceID="DataSourceComputadora" GroupItemCount="5">
                        <ItemTemplate>
                            <td runat="server">
                                <asp:ImageButton runat="server" ID="imgEliminar" Width="50%" CommandName="Eliminar" CommandArgument='<%# Bind("id") %>' ImageUrl="~/content/images/PCOff.png" OnClientClick="javascript:return confirm('¿Está seguro de querer eliminar el registro seleccionado?', 'Mensaje de sistema')" />
                                <br />
                                <asp:Label ID="tempInicioRentaLabel" runat="server" Text='<%# Eval("tempInicioRenta") %>' />
                                <%--<% if (String.IsNullOrEmpty(Eval("tempInicioRenta").ToString()))
                                    {%>
                                    <asp:ImageButton runat="server" ID="imgEliminar" Width="50%" CommandName="Eliminar" CommandArgument='<%#Bind("id") %>' ImageUrl="~/content/images/PCO.png" OnClientClick="javascript:return confirm('¿Está seguro de querer eliminar el registro seleccionado?', 'Mensaje de sistema')" />
                                <% }
                                    else
                                    { %>
                                    <asp:ImageButton runat="server" ID="ImageButton1" Width="50%" CommandName="Eliminar" CommandArgument='<%#Bind("id") %>' ImageUrl="~/content/images/PCOn.png" OnClientClick="javascript:return confirm('¿Está seguro de querer eliminar el registro seleccionado?', 'Mensaje de sistema')" />
                                <%} %>--%>
                                <%--<asp:ImageButton runat="server" ID="imgEliminar" Width="50%" CommandName="Eliminar" CommandArgument='<%# Bind("id") %>' ImageUrl='<%# (String.IsNullOrEmpty("Wenas".ToString())) ? "~/ content / images / PCOff.png" : "~/content/images/ProtectedConfigurationProvider.png" %>' OnClientClick="javascript:return confirm('¿Está seguro de querer eliminar el registro seleccionado?', 'Mensaje de sistema')" />--%>
                            </td>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table runat="server" style="">
                                <tr>
                                    <td>No se han devuelto datos.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <EmptyItemTemplate>
                            <td runat="server" />
                        </EmptyItemTemplate>
                        <GroupTemplate>
                            <tr id="itemPlaceholderContainer" runat="server">
                                <td id="itemPlaceholder" runat="server"></td>
                            </tr>
                        </GroupTemplate>
                        <LayoutTemplate>
                            <table runat="server">
                                <tr runat="server">
                                    <td runat="server">
                                        <table id="groupPlaceholderContainer" runat="server" border="0" style="">
                                            <tr id="groupPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server" style=""></td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <SelectedItemTemplate>
                            <td runat="server" style="">Id:
                                <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
                                <br />
                                strNombre:
                                <asp:Label ID="strNombreLabel" runat="server" Text='<%# Eval("strNombre") %>' />
                                <br />
                                tempInicioRenta:
                                <asp:Label ID="tempInicioRentaLabel" runat="server" Text='<%# Eval("tempInicioRenta") %>' />
                                <br />
                            </td>
                        </SelectedItemTemplate>
                    </asp:ListView>
                </div>
            </div>

        </div>

    </form>

    <!-- DATA SOURCE -->
    <asp:LinqDataSource ID="DataSourceComputadora" runat="server"
        ContextTypeName="Data.Linq.Entity.DcGeneralDataContext"
        OnSelecting="DataSourceComputadora_Selecting"
        Select="new (Id, strNombre, tempInicioRenta)"
        TableName="COMPUTADORA" EntityTypeName="">
    </asp:LinqDataSource>


    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
    <%--<script src="../content/bootstrap.js"></script>
    <script src="../content/bootstrap.bundle.js"></script>--%>
</body>
</html>
