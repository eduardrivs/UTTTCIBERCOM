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
                            <asp:Button Text="Rentas" runat="server" class="menuLinkBtnActivo text-center" />
                            <%--<a href="#" class="menuLinkActivo">Rentas</a>--%>
                        </div>
                        <div class="menu">
                            <asp:Button Text="Computadoras" runat="server" class="menuLinkBtn text-center" />
                            <%--<a href="#" class="menuLink">Computadoras</a>--%>
                        </div>
                        <div class="menu">
                            <asp:Button Text="Empleados" runat="server" class="menuLinkBtn text-center" />
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
                <div class="table-responsive-lg">
                    <asp:GridView ID="GridView1" runat="server"
                        AllowPaging="true" AutoGenerateColumns="False" DataSourceID="DataSourceComputadora"
                        Width="100%" CellPadding="3" GridLines="Horizontal"
                        OnRowCommand="dgvComputadora_RowCommand" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px"
                        ViewStateMode="Disabled" class="table table-sm mt-2">
                        <AlternatingRowStyle BackColor="#F7F7F7" />
                        <Columns>
                            <asp:BoundField DataField="strNombre" HeaderText="Nombre" ReadOnly="True"
                                SortExpression="strNombre" />
                            <asp:BoundField DataField="tempInicioRenta" HeaderText="Renta Actual" ReadOnly="True"
                                SortExpression="tmpInicioRenta" />
                            <asp:TemplateField HeaderText="Editar">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="imgEditar" CommandName="Editar" CommandArgument='<%#Bind("Id") %>' ImageUrl="~/content/images/editrecord_16x16.png" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Eliminar" Visible="True">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="imgEliminar" CommandName="Eliminar" CommandArgument='<%#Bind("Id") %>' ImageUrl="~/content/images/delrecord_16x16.png" OnClientClick="javascript:return confirm('¿Está seguro de querer eliminar el registro seleccionado?', 'Mensaje de sistema')" />
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

    <!-- DATA SOURCE -->
    <asp:LinqDataSource ID="DataSourceComputadora" runat="server"
        ContextTypeName="DcGeneralDataContext"
        OnSelecting="DataSourceComputadora_Selecting"
        Select="new (strNombre,tempInicioRenta,Id)"
        TableName="COMPUTADORA" EntityTypeName="">
    </asp:LinqDataSource>


    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
    <%--<script src="../content/bootstrap.js"></script>
    <script src="../content/bootstrap.bundle.js"></script>--%>
</body>
</html>
