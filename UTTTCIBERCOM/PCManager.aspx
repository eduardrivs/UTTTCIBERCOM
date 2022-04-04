<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PCManager.aspx.cs" Inherits="UTTTCIBERCOM.PCManager" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Computadoras | Administración</title>
    <link rel="icon" type="image/vnd.microsoft.icon" href="~/content/images/favicon.png">
    <link href="~/content/css/RentPrincipal.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous">
    <%--<link href="../content/bootstrap.css" rel="stylesheet" />--%>
</head>
<body>
    <form id="renta" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        
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
                        <div class="menu menuActivo d-inline">
                            <asp:Button Text="Computadoras" runat="server" class="menuLinkBtnActivo text-center" OnClick="btnPCPrincipal_Click" />
                            <%--<a href="#" class="menuLink">Computadoras</a>--%>
                            <div class="d-lg-none d-inline">
                                <div class="my-4" runat="server">
                                    <i class="bi bi-plus-square-fill"></i>
                                    <asp:Button Text="Nuevo Equipo" runat="server" class="menuLinkBtnActivo" OnClick="btnPCManager_Click" />
                                </div>
                                <div class="my-4" runat="server">
                                    <i class="me-2 bi bi-cash-stack"></i>
                                    <asp:Button Text="Nueva Renta" runat="server" class="menuLinkBtnActivo" OnClick="btnRentPrincipal_Click" />
                                </div>
                                <div class="my-4" runat="server">
                                    <i class="me-2 bi bi-calendar2-x"></i>
                                    <asp:Button Text="Terminar Renta" runat="server" class="menuLinkBtnActivo" OnClick="btnRentManager_Click" />
                                </div>
                            </div>
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
        <div class="mt-2 row container-fluid">
            <div class="text-start ps-4 d-lg-block d-none" style="border-right: 1px solid #555; width: 20%;">
                <div class="my-4" runat="server">
                    <i class="bi bi-plus-square-fill"></i>
                    <asp:Button Text="Nuevo Equipo" runat="server" class="menuLinkBtnActivo" OnClick="btnPCManager_Click" />
                </div>
                <div class="my-4" runat="server">
                    <i class="me-2 bi bi-cash-stack"></i>
                    <asp:Button Text="Nueva Renta" runat="server" class="menuLinkBtnActivo" OnClick="btnRentPrincipal_Click" />
                </div>
                <div class="my-4" runat="server">
                    <i class="me-2 bi bi-calendar2-x"></i>
                    <asp:Button Text="Terminar Renta" runat="server" class="menuLinkBtnActivo" OnClick="btnRentManager_Click" />
                </div>
            </div>
            <!-- Cuerpo -->
            <div class="bodySize ps-5 ms-3 ms-lg-0 row d-flex">
                <div class="col-12 col-lg-6">
                    <div class="my-3">
                        <div><label for="txtNombre">Nombre:</label></div>
                        <asp:TextBox ID="txtNombre" runat="server" Width="90%" ViewStateMode="Disabled" onkeypress="return validaAlfanumericos(event);"></asp:TextBox>
                    </div>
                    <div class="my-3">
                        <div><label for="txtDescripcion">Descripcion:</label></div>
                        <asp:TextBox ID="txtDescripcion" runat="server" Width="90%" Height="100px" ViewStateMode="Disabled" onkeypress="return validaAlfanumericosGrande(event);"></asp:TextBox>
                    </div>
                    <div class="my-3">
                        <div><label for="txtFechaAlta">Fecha de alta:</label></div>
                        <asp:TextBox ID="txtFechaAlta" runat="server" Width="90%" ViewStateMode="Disabled"></asp:TextBox>
                    </div>
                    <div class="my-3">
                        <div><label for="txtArea">Id Area:</label></div>
                        <asp:TextBox ID="txtArea" runat="server" Width="90%" ViewStateMode="Disabled" onkeypress="return validaNumeros(event);"></asp:TextBox>
                    </div>
                    <div class="my-3">
                        <div><label for="txtTarifa">Tarifa por hora:</label></div>
                        <asp:TextBox ID="txtTarifa" runat="server" Width="90%" ViewStateMode="Disabled" onkeypress="return validaDinero(event);"></asp:TextBox>                        
                    </div>
                    <div class="my-3">
                        <div><label for="txtTempRenta">Renta actual:</label></div>
                        <asp:TextBox ID="txtTempRenta" runat="server" Width="90%" ViewStateMode="Disabled"></asp:TextBox>
                    </div>
                </div>
                <div class="col-12 col-lg-6">
                    <div class="my-3">
                        <div><label for="txtTeclado">Teclado:</label></div>
                        <asp:TextBox ID="txtTeclado" runat="server" Width="90%" ViewStateMode="Disabled" onkeypress="return validaAlfanumericosGrande(event);"></asp:TextBox>
                    </div>
                    <div class="my-3">
                        <div><label for="txtMonitor">Monitor:</label></div>
                        <asp:TextBox ID="txtMonitor" runat="server" Width="90%" ViewStateMode="Disabled" onkeypress="return validaAlfanumericosGrande(event);"></asp:TextBox>
                    </div>
                    <div class="my-3">
                        <div><label for="txtMouse">Mouse:</label></div>
                        <asp:TextBox ID="txtMouse" runat="server" Width="90%" ViewStateMode="Disabled" onkeypress="return validaAlfanumericosGrande(event);"></asp:TextBox>
                    </div>
                    <div class="my-3">
                        <div><label for="txtAudifonos">Audifonos:</label></div>
                        <asp:TextBox ID="txtAudifonos" runat="server" Width="90%" ViewStateMode="Disabled" onkeypress="return validaAlfanumericosGrande(event);"></asp:TextBox>
                    </div>
                    <div class="my-3">
                        <div><label for="txtCPU">CPU:</label></div>
                        <asp:TextBox ID="txtCPU" runat="server" Width="90%" ViewStateMode="Disabled" onkeypress="return validaAlfanumericosGrande(event);"></asp:TextBox>
                    </div>
                    <div class="my-3">
                        <div><label for="txtRAM">RAM:</label></div>
                        <asp:TextBox ID="txtRAM" runat="server" Width="90%" ViewStateMode="Disabled" onkeypress="return validaAlfanumericosGrande(event);"></asp:TextBox>
                    </div>
                    <div class="my-3">
                        <div><label for="txtGPU">GPU:</label></div>
                        <asp:TextBox ID="txtGPU" runat="server" Width="90%" ViewStateMode="Disabled" onkeypress="return validaAlfanumericosGrande(event);"></asp:TextBox>
                    </div>
                </div>
                <div class="mt-2 pe-5 text-end">
                    <asp:Label ID="lblMensaje" Text="text" runat="server" Visible="false" CssClass="mx-2"/>
                    <asp:Button Text="Regresar" runat="server" class="btnForm mx-1" OnClick="btnRentPrincipal_Click" />
                    <asp:Button ID="btnFin" Text="Finalizar" runat="server" class="btnFormAct mx-1" OnClick="btnSave_Click" Enabled="false"/>
                </div>
            </div>
        </div>

    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
    <%--<script src="../content/bootstrap.js"></script>
    <script src="../content/bootstrap.bundle.js"></script>--%>
    <script type="text/javascript">
        function validaNumeros(evt) {
            //valida que solo se ingresan números en la caja de texto
            var code = (evt.which) ? evt.which : evt.keyCode;
            if (code == 8) {
                return true;
            } else if (code >= 48 && code <= 57) {
                return true;
            } else {
                return false;
            }
        }

        function validaLetras(e) {
            //valida quer solo ingreses letras y algunos caracteres especiales
            key = e.keyCode || e.which;
            tecla = String.fromCharCode(key).toLowerCase();
            letras = "áéíóúàèìòùüïabcdefghijklmnñopqrstuvwxyz ";
            especiales = "8-37-39-46";
            tecla_especial = false;
            for (var i in especiales) {
                if (key == especiales[i]) {
                    tecla_especial = true;
                    break;
                }
            }
            if (letras.indexOf(tecla) == -1 && !tecla_especial) {
                return false;
            }
        }

        function validaAlfanumericosGrande(e) {
            //valida quer solo ingreses letras y algunos caracteres especiales
            key = e.keyCode || e.which;
            tecla = String.fromCharCode(key).toLowerCase();
            letras = "áéíóúàèìòùüïabcdefghijklmnñopqrstuvwxyz0123456789 ";
            especiales = "8-37-39-46";
            tecla_especial = false;
            for (var i in especiales) {
                if (key == especiales[i]) {
                    tecla_especial = true;
                    break;
                }
            }
            if (letras.indexOf(tecla) == -1 && !tecla_especial) {
                return false;
            }
        }

        function validaAlfanumericos(e) {
            var regex = new RegExp("^[a-zA-Z0-9]+$");
            var key = String.fromCharCode(!e.charCode ? e.which : e.charCode);
            if (!regex.test(key)) {
                e.preventDefault();
                return false;
            }
        }

        function validaDinero(evt) {
            //valida que solo se ingresan números en la caja de texto
            var code = (evt.which) ? evt.which : evt.keyCode;
            if (code == 8)
                return true;
            else if (code >= 48 && code <= 57)
                return true;
            else if (code == 46)
                return true;
            else
                return false;
        }
    </script>
</body>
</html>
