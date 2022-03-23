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
</head>
<body>
    <form id="login" runat="server">
        <!-- Barra de navegación -->
        <nav class="navbar fixed-top banner">
            <div class="container-fluid row">
                <div class="navCol col-12 col-md-4 p-4 pt-5 align-self-center d-none d-md-block">
                    <div class="text-light mb-2">Intercom ofrece los servicios de:</div>
                    <div class="text-light light">Renta, Venta y Reparación de artículos de computación y oficina.</div>
                </div>
                <div class="navCol col-12 col-md-4 text-center">
                    <img src="/content/images/LogoBannerNeg.png" alt="INTERCOM" class="imagenBanner"/>
                    <div class="text-light imgText light">Sistema de administración interna</div>
                </div>
                <div class="navCol col-12 col-md-4 p-4 pt-5 text-center d-none d-md-block">
                    <div class="text-light mb-2">Redes Sociales</div>
                    <a class="text-light p-2" target="_blank" href="https://www.facebook.com/IntercomJilotepec1"><i class="bi bi-facebook"></i></a>
                    <a class="text-light p-2" target="_blank" href="https://www.instagram.com/explore/locations/493594414044903/intercomjilo/"><i class="bi bi-instagram"></i></a>
                    <a class="text-light p-2" target="_blank" href="https://twitter.com/hashtag/intercom?src=hashtag_click"><i class="bi bi-twitter"></i></a>
                </div>
            </div>
        </nav>

        <!-- Formulario -->

        <!-- Barra de footer -->
        <nav class="navbar fixed-bottom footer">
            Buenas
        </nav>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
</body>
</html>
