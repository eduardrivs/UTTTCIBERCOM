<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLogManager.aspx.cs" Inherits="UTTTCIBERCOM.UserLogManager" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Usuario | Administración</title>
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
                                    <asp:Button Text="Nuevo Empleado" runat="server" class="menuLinkBtnActivo" OnClick="btnPCManager_Click" />
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
            <div class="bodySize ps-5 ms-3 ms-lg-0 row d-flex">
                <asp:Label ID="lblAction" Text="Agregar usuario nuevo" runat="server" Font-Size="200%" />
                <div class="col-12 col-lg-6">
                    <div class="my-3">
                        <div>
                            <label for="txtNombre">Nombre:</label>
                        </div>
                        <asp:TextBox ID="txtNombre" runat="server" Width="90%" ViewStateMode="Disabled" onkeypress="return validaLetras(event);" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="my-3">
                        <div>
                            <label for="txtAPaterno">A.Paterno:</label>
                        </div>
                        <asp:TextBox ID="txtAPaterno" runat="server" Width="90%" ViewStateMode="Disabled" onkeypress="return validaLetras(event);" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="my-3">
                        <div>
                            <label for="txtAMaterno">A.Materno:</label>
                        </div>
                        <asp:TextBox ID="txtAMaterno" runat="server" Width="90%" ViewStateMode="Disabled" onkeypress="return validaLetras(event);" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="my-3">
                        <div class="d-flex justify-content-between">
                            <label for="txtIdEmp">ID Empleado:</label>
                            <asp:RequiredFieldValidator ID="rvftxtIdEmp" runat="server" class="text-danger me-5" ControlToValidate="txtIdEmp" ErrorMessage="&quot;El Id empleado es obligatorio&quot;" ValidationGroup="gvSave"></asp:RequiredFieldValidator>
                        </div>
                        <asp:TextBox ID="txtIdEmp" runat="server" Width="90%" ViewStateMode="Disabled" onkeypress="return validaNumeros(event);" AutoPostBack="true"></asp:TextBox>
                    </div>
                    <div class="my-3">
                        <asp:CheckBox ID="chbxActivo" runat="server" Text=" Activo" />
                    </div>
                </div>
                <div class="col-12 col-lg-6">
                    <div class="my-3">
                        <div class="d-flex justify-content-between">
                            <label for="txtCorreo">Email:</label>
                            <asp:RequiredFieldValidator ID="rvftxtCorreo" runat="server" class="text-danger me-5" ControlToValidate="txtCorreo" ErrorMessage="&quot;El correo electronico es obligatorio&quot;" ValidationGroup="gvSave"></asp:RequiredFieldValidator>
                        </div>
                        <asp:TextBox ID="txtCorreo" runat="server" Width="90%" ViewStateMode="Disabled" onkeypress="return validaCorreo(event);" TextMode="Email"></asp:TextBox>
                    </div>
                    <div class="my-3">
                        <div class="d-flex justify-content-between">
                            <label for="txtUsername">Username:</label>
                            <asp:RequiredFieldValidator ID="rvftxtUsername" runat="server" class="text-danger me-5" ControlToValidate="txtUsername" ErrorMessage="&quot;El nombre de usuario es obligatorio&quot;" ValidationGroup="gvSave"></asp:RequiredFieldValidator>
                        </div>
                        <asp:TextBox ID="txtUsername" runat="server" Width="90%" ViewStateMode="Disabled" onkeypress="return validaAlfanumericos(event);"></asp:TextBox>
                    </div>
                    <div class="my-3">
                        <div class="d-flex justify-content-between">
                            <label for="txtPassword">Password:</label>
                            <asp:RequiredFieldValidator ID="rvftxtPassword" runat="server" class="text-danger me-5" ControlToValidate="txtPassword" ErrorMessage="&quot;La contraseña es obligatorio&quot;" ValidationGroup="gvSave"></asp:RequiredFieldValidator>
                        </div>
                        <asp:TextBox ID="txtPassword" runat="server" Width="90%" ViewStateMode="Disabled" onkeypress="return validaAlfanumericosGrande(event);" TextMode="Password"></asp:TextBox>
                    </div>
                    <div class="my-3">
                        <div class="d-flex justify-content-between">
                            <label for="txtPassword2">Password:</label>
                            <asp:RequiredFieldValidator ID="rvftxtPassword2" runat="server" class="text-danger me-5" ControlToValidate="txtPassword2" ErrorMessage="&quot;La contraseña es obligatorio&quot;" ValidationGroup="gvSave"></asp:RequiredFieldValidator>
                        </div>
                        <asp:TextBox ID="txtPassword2" runat="server" Width="90%" ViewStateMode="Disabled" onkeypress="return validaAlfanumericosGrande(event);" TextMode="Password"></asp:TextBox>
                    </div>
                    <div class="my-3">
                        <div>
                            <asp:Label ID="lblMensaje2" Text="text" runat="server" Visible="false" CssClass="mx-2" />
                        </div>
                    </div>
                </div>
                <div class="mt-2 pe-5 text-end">
                    <asp:Label ID="lblMensaje" Text="text" runat="server" Visible="false" CssClass="mx-2" />
                    <asp:Button ID="btnDelete" Text="Eliminar" runat="server" class="btnForm mx-1" OnClick="btnDelete_Click" Enabled="false" />
                    <asp:Button Text="Regresar" runat="server" class="btnForm mx-1" OnClick="btnUserPrincipal_Click" />
                    <asp:Button ID="btnFin" Text="Finalizar" runat="server" class="btnFormAct mx-1" OnClick="btnSave_Click" />
                </div>
            </div>
        </div>

    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
    <%--<script src="../content/bootstrap.js"></script>
    <script src="../content/bootstrap.bundle.js"></script>--%>
    <script type="text/javascript" language="JavaScript">
        document.onkeydown = function (evt) {
            return (evt ? evt.which : event.keyCode) != 13;
        }
    </script>

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

        function validaFecha(e) {
            var reges = new RegExp("^([0-2][0-9]|3[0-1])(\/|-)(0[1-9]|1[0-2])\2(\d{4})(\s)([0-1][0-9]|2[0-3])(:)([0-5][0-9])(:)([0-5][0-9])$");
            var key = String.fromCharCode(!e.charCode ? e.which : e.charCode);
            if (!regex.test(key)) {
                e.preventDefault();
                return false;
            }
        }

        function validaCorreo(e) {
            var reges = new RegExp("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$");
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
