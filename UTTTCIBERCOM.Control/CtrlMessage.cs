using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTTTCIBERCOM.Control
{
    public static class CtrlMessage
    {
        public static void showMessage(this System.Web.UI.Page _page, String _message, bool type)
        {
            _page.ClientScript.RegisterStartupScript(_page.GetType(),"ClientScript", "<script src='https://cdn.jsdelivr.net/npm/sweetalert2@11.4.7/dist/sweetalert2.all.min.js'>const Toast = Swal.mixin({ toast: true, position: 'top-end', showConfirmButton: false, timer: 3000, timerProgressBar: true, didOpen: (toast) => { toast.addEventListener('mouseenter', Swal.stopTimer); toast.addEventListener('mouseleave', Swal.resumeTimer)}})Toast.fire({ icon: 'error',title: 'El usuario o contraseña no coinciden'})</script>"
                + "<script>alert('wenas')</script>");
        }
        

        public static void showMessageException(this System.Web.UI.Page _page, String _message)
        {
            String mensaje = "Error de tipo " + _message + ". Ponerse en contacto con su administrador de sistema";
            _page.ClientScript.RegisterStartupScript(_page.GetType(),
                   "ClientScript",
                   "<SCRIPT>alert( '" + mensaje + "');</SCRIPT>");

        }
    }
}
