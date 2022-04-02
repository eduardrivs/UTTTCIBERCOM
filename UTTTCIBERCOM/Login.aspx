<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UTTTCIBERCOM.app.Login" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Bienvenido</title>
    <link rel="icon" type="image/vnd.microsoft.icon" href="~/content/images/favicon.png">
    <link href="~/content/css/LoginStyle.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous">
    <link href="../content/bootstrap.css" rel="stylesheet" />
</head>
<body onload="error">
    <div class="container-fluid">
        <form id="login" runat="server">
            <!-- Barra de navegación -->
            <nav class="navbar fixed-top banner">
                <div class="container-fluid row">
                    <div class="navCol col-12 col-md-4 p-4 pt-5 align-self-center d-none d-md-block">
                        <div class="text-light mb-2">Intercom ofrece los servicios de:</div>
                        <div class="text-light light">Renta, Venta y Reparación de artículos de computación y oficina.</div>
                    </div>
                    <div class="navCol col-12 col-md-4 text-center">
                        <img src="/content/images/LogoBannerNeg.png" alt="INTERCOM" class="imagenBanner" />
                        <div class="text-light imgText light">Sistema de administración interna</div>
                    </div>
                    <div class="navCol col-12 col-md-4 p-4 pt-5 text-center d-none d-md-block">
                        <div class="text-light mb-2">Redes Sociales</div>
                        <a class="text-light p-2" target="_blank" href="https://www.facebook.com/IntercomJilotepec1"><i class="bi bi-facebook iconoNavbar"></i></a>
                        <a class="text-light p-2" target="_blank" href="https://www.instagram.com/explore/locations/493594414044903/intercomjilo/"><i class="bi bi-instagram iconoNavbar"></i></a>
                        <a class="text-light p-2" target="_blank" href="https://twitter.com/hashtag/intercom?src=hashtag_click"><i class="bi bi-twitter iconoNavbar"></i></a>
                    </div>
                </div>
            </nav>

            <!-- Formulario -->
            <div class="justify-content-center d-flex flex-column bodyForm">
                <div class=" justify-content-center text-center row my-4 text-danger">
                    <asp:Label ID="txtAlerta" Text="el usuario o contraseña no coinciden" runat="server" Visible="false"/>
                </div>
                <div class=" justify-content-center text-center row">
                    <div class="col-md-3 col-12">
                        <h4>Usuario</h4>
                        <div class="col">
                            <asp:TextBox ID="txtUser" runat="server" onkeypress="return validaUser(event);" CssClass="form-control text-center" placeholder="Nombre de usuario o correo"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class=" justify-content-center text-center row mt-4">
                    <div class="col-md-3 col-12">
                        <h4>Contraseña</h4>
                        <div class="col">
                            <asp:TextBox ID="txtPass" runat="server" CssClass="form-control text-center" placeholder="contraseña" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class=" justify-content-center text-center row mt-4">
                    <div class="col-md-3 col-12">
                        <div class="col">
                            <asp:Button Text="Iniciar sesión" runat="server" class="btn-login" OnClick="btnLogin_Click"/>
                        </div>
                    </div>
                </div>
                <div class=" justify-content-center text-center row mt-4">
                    <div class="col-md-3 col-12">
                        <div class="col justify-content-center d-flex">
                            <asp:Button Text="¿Olvidaste tu contraseña?" runat="server" class="btn nav-link text-center" OnClick="btnRecoveryPass_Click"/>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Barra de footer -->
            <nav class="navbar fixed-bottom footer">
                <div class="container-fluid row justify-content-around">
                    <div class="col-md-8 d-none d-md-inline text-light light">Intercom Av. Lázaro Cárdenas 124, Col. Centro, Jilotepec, Estado de México. Tel 761 734 4229</div>
                    <div class="col-md-4 col-12 text-light light text-center text-md-end">
                        <label class="d-none d-md-inline">José Eduardo Rivas Cuevas</label>
                        <a class="text-light p-2" target="_blank" href="https://www.facebook.com/ed.rivas17/"><i class="bi bi-facebook"></i></a>
                        <a class="text-light p-2" target="_blank" href="https://www.instagram.com/eduardrivs/"><i class="bi bi-instagram"></i></a>
                        <a class="text-light p-2" target="_blank" href="https://twitter.com/EduardoRvas"><i class="bi bi-twitter"></i></a>
                        <a class="text-light p-2" target="_blank" href="mailto:eduardo.rivas.cuevas@outlook.com"><i class="bi bi-envelope"></i></a>
                    </div>
                </div>
            </nav>
        </form>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11.4.7/dist/sweetalert2.all.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js" integrity="sha256-/xUj+3OJU5yExlq6GSYGSHk7tPXikynS7ogEvDej/m4=" crossorigin="anonymous"></script>
    <script src="../content/js/validaciones.js"></script>
    <script src="../content/bootstrap.js"></script>
    <script src="../content/bootstrap.bundle.js"></script>
</body>
</html>
