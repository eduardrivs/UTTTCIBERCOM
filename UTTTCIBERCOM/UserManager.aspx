<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserManager.aspx.cs" Inherits="UTTTCIBERCOM.UserManager" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Empleado | Administración</title>
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
                    <asp:Label Text="Se han bloqueado ciertas acciones debido a los permisos de tu cuenta" runat="server" CssClass="txtInfo" />
                </div>
            </div>

            <!-- Cuerpo -->
            <div class="bodySize ps-5 ms-3 ms-lg-0 row d-flex">
                <asp:Label ID="lblAction" Text="Agregar empleado" runat="server" Font-Size="200%" />
                <div class="col-12 col-lg-6">
                    <div class="my-3">
                        <div class="d-flex justify-content-between">
                            <label for="txtNombre">Nombre:</label>
                            <asp:RequiredFieldValidator ID="rvfNombre" runat="server" class="text-danger me-5" ControlToValidate="txtNombre" ErrorMessage="&quot;El nombre es obligatorio&quot;" ValidationGroup="gvSave"></asp:RequiredFieldValidator>
                        </div>
                        <asp:TextBox ID="txtNombre" runat="server" Width="90%" ViewStateMode="Disabled" onkeypress="return validaLetras(event);"></asp:TextBox>
                    </div>
                    <div class="my-3">
                        <div class="d-flex justify-content-between">
                            <label for="txtAPaterno">A.Paterno:</label>
                            <asp:RequiredFieldValidator ID="fvrtxtAPaterno" runat="server" class="text-danger me-5" ControlToValidate="txtAPaterno" ErrorMessage="&quot;El Apellido Paterno es obligatorio&quot;" ValidationGroup="gvSave"></asp:RequiredFieldValidator>
                        </div>
                        <asp:TextBox ID="txtAPaterno" runat="server" Width="90%" ViewStateMode="Disabled" onkeypress="return validaLetras(event);"></asp:TextBox>
                    </div>
                    <div class="my-3">
                        <div class="d-flex justify-content-between">
                            <label for="txtAMaterno">A.Materno:</label>
                            <asp:RequiredFieldValidator ID="rvftxtAMaterno" runat="server" class="text-danger me-5" ControlToValidate="txtAMaterno" ErrorMessage="&quot;El Apellido Materno es obligatorio&quot;" ValidationGroup="gvSave"></asp:RequiredFieldValidator>
                        </div>
                        <asp:TextBox ID="txtAMaterno" runat="server" Width="90%" ViewStateMode="Disabled" onkeypress="return validaLetras(event);"></asp:TextBox>
                    </div>
                    <div class="my-3">
                        <div class="d-flex justify-content-between">
                            <label for="txtFechaNacimiento">Fecha de nacimiento:</label>
                            <asp:RequiredFieldValidator ID="rvftxtFechaNacimiento" runat="server" class="text-danger me-5" ControlToValidate="txtFechaNacimiento" ErrorMessage="&quot;La fecha es obligatoria&quot;" ValidationGroup="gvSave"></asp:RequiredFieldValidator>
                        </div>
                        <div class="d-flex">
                            <asp:TextBox ID="txtFechaNacimiento" runat="server" Width="80%" CssClass="me-2" ViewStateMode="Disabled" onkeypress="return validaFecha(event);"></asp:TextBox>
                            <asp:ImageButton ID="imgPopup" ImageUrl="~/content/images/calendar.png" ImageAlign="Bottom" Height="35px" runat="server" CausesValidation="false" />
                        </div>
                        <ajaxToolkit:CalendarExtender ID="calendar1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtFechaNacimiento" Format="dd-MM-yyyy HH:mm:ss" />
                    </div>
                    <div class="my-3">
                        <div class="d-flex justify-content-between">
                            <label for="txtEdad">Edad:</label>
                            <asp:RequiredFieldValidator ID="rvftxtEdad" runat="server" class="text-danger me-5" ControlToValidate="txtEdad" ErrorMessage="&quot;La edad es obligatoria&quot;" ValidationGroup="gvSave"></asp:RequiredFieldValidator>
                        </div>
                        <asp:TextBox ID="txtEdad" runat="server" Width="90%" ViewStateMode="Disabled" onkeypress="return validaNumeros(event);"></asp:TextBox>
                    </div>
                </div>
                <div class="col-12 col-lg-6">
                    <div class="my-3">
                        <div class="d-flex justify-content-between">
                            <label for="txtCURP">CURP:</label>
                            <asp:RequiredFieldValidator ID="rvftxtCURP" runat="server" class="text-danger me-5" ControlToValidate="txtCURP" ErrorMessage="&quot;La CURP es obligatoria&quot;" ValidationGroup="gvSave"></asp:RequiredFieldValidator>
                        </div>
                        <asp:TextBox ID="txtCURP" runat="server" Width="90%" ViewStateMode="Disabled" onkeypress="return validaAlfanumericosGrande(event);"></asp:TextBox>
                    </div>
                    <div class="my-3">
                        <div class="d-flex justify-content-between">
                            <label for="txtRFC">RFC:</label>
                            <asp:RequiredFieldValidator ID="rvftxtRFC" runat="server" class="text-danger me-5" ControlToValidate="txtRFC" ErrorMessage="&quot;El RFC es obligatorios&quot;" ValidationGroup="gvSave"></asp:RequiredFieldValidator>
                        </div>
                        <asp:TextBox ID="txtRFC" runat="server" Width="90%" ViewStateMode="Disabled" onkeypress="return validaAlfanumericosGrande(event);"></asp:TextBox>
                    </div>
                    <div class="my-3">
                        <div class="d-flex justify-content-between">
                            <label for="txtFechaIngreso">Fecha de ingreso:</label>
                            <asp:RequiredFieldValidator ID="rvftxtFechaIngreso" runat="server" class="text-danger me-5" ControlToValidate="txtFechaIngreso" ErrorMessage="&quot;La fecha de ingreso es obligatoria&quot;" ValidationGroup="gvSave"></asp:RequiredFieldValidator>
                        </div>
                        <div class="d-flex">
                            <asp:TextBox ID="txtFechaIngreso" runat="server" Width="80%" CssClass="me-2" ViewStateMode="Disabled" onkeypress="return validaFecha(event);"></asp:TextBox>
                            <asp:ImageButton ID="imgPopup2" ImageUrl="~/content/images/calendar.png" ImageAlign="Bottom" Height="35px" runat="server" CausesValidation="false" />
                        </div>
                        <ajaxToolkit:CalendarExtender ID="calendar2" PopupButtonID="imgPopup2" runat="server" TargetControlID="txtFechaIngreso" Format="dd-MM-yyyy HH:mm:ss" />
                    </div>
                    <div class="my-3">
                        <div class="d-flex justify-content-between">
                            <label for="txtRol">ID Rol:</label>
                            <%--<asp:RequiredFieldValidator ID="rvftxtRol" runat="server" class="text-danger me-5" ControlToValidate="ddlRol" ErrorMessage="&quot;El Rol es obligatorios&quot;" ValidationGroup="gvSave"></asp:RequiredFieldValidator>--%>
                        </div>
                        <%--<asp:TextBox ID="txtRol" runat="server" Width="90%" ViewStateMode="Disabled" onkeypress="return validaNumeros(event);"></asp:TextBox>--%>
                        <asp:DropDownList ID="ddlRol" class="btn btn-secondary dropdown-toggle me-2" runat="server"></asp:DropDownList>
                    </div>
                    <div class="my-3">
                        <div class="d-flex justify-content-between">
                            <label for="txtArea">ID Area:</label>
                            <asp:RequiredFieldValidator ID="rvftxtArea" runat="server" class="text-danger me-5" ControlToValidate="txtArea" ErrorMessage="&quot;El Area es obligatorios&quot;" ValidationGroup="gvSave"></asp:RequiredFieldValidator>
                        </div>
                        <asp:TextBox ID="txtArea" runat="server" Width="90%" ViewStateMode="Disabled" onkeypress="return validaNumeros(event);"></asp:TextBox>
                    </div>
                </div>
                <div class="mt-2 pe-5 text-end row">
                    <div class="col-12 col-lg-2 my-2">
                        <asp:Button Text="Editar Usuario" runat="server" class="btnForm mx-1" OnClick="btnUserLogManagerList_Click" />
                    </div>
                    <div class="col-12 col-lg-10 my-2">
                        <%--<label for="txtActivo">Activo:</label>--%>
                        <asp:Label ID="lblMensaje" Text="hola :3" runat="server" Visible="false" CssClass="mx-2" />
                        <asp:CheckBox ID="chbxActivo" runat="server" Text=" Activo" />
                        <%--<ajaxToolkit:ToggleButtonExtender ID="tbeActivo" runat="server" />--%>
                        <%--<asp:TextBox ID="txtActivo" runat="server" Width="90%" ViewStateMode="Disabled" onkeypress="return validaNumeros(event);"></asp:TextBox>--%>
                        <asp:Button Text="Regresar" runat="server" class="btnForm mx-1" OnClick="btnUserPrincipal_Click" />
                        <asp:Button ID="btnFin" Text="Finalizar" runat="server" class="btnFormAct mx-1" OnClick="btnSave_Click" />
                    </div>
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
