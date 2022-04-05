using Data.Linq.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTTCIBERCOM.Control;

namespace UTTTCIBERCOM
{
    public partial class UserRecoveryPass : System.Web.UI.Page
    {
        DataContext dcConsulta = new DcGeneralDataContext();
        USUARIO user;
        int idPersona;
        protected void Page_Load(object sender, EventArgs e)
        {

            user = dcConsulta.GetTable<USUARIO>().FirstOrDefault(c=>c.username == this.txtNombre.Text);
            if(user != null)
                this.cuerpoUsuario.Visible = true;
            else
                this.cuerpoUsuario.Visible = false;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            ConfigurationManager.AppSettings["session"] = "0";
            ConfigurationManager.AppSettings["trylog"] = null;
            Session["SessionManager"] = null;
            this.Response.Redirect("Login.aspx", false);
        }

        protected void DataSourceUser_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            try
            {
                bool nombreBool = false;
                if (!this.txtNombre.Text.Equals(String.Empty))
                {
                    nombreBool = true;
                }

                Expression<Func<USUARIO, bool>>
                    predicate =
                    (c =>
                    ((nombreBool) ? (((nombreBool) ? c.username.Equals(this.txtNombre.Text) : false)) : true)
                    );

                predicate.Compile();

                List<USUARIO> listaPersona =
                    dcConsulta.GetTable<USUARIO>().Where(predicate).ToList();
                e.Result = listaPersona;
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        protected void dgvUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            idPersona = int.Parse(e.CommandArgument.ToString());
            this.txtPass1.Visible = true;
            this.txtPass2.Visible = true;
            this.btnCambiar.Visible = true;
            btnCambiar.CommandName = idPersona.ToString();
            this.Session["Session"] = idPersona;
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alerta1", "<script>alert('" + idPersona + "')</script>");

        }

        protected void btnCambiarContra_Click(object sender, EventArgs e)
        {
            string pass1 = txtPass1.Text;
            string pass2 = txtPass2.Text;

            if(pass1 == pass2)
            {
                DataContext dcConsulta = new DcGeneralDataContext();
                USUARIO user = dcConsulta.GetTable<USUARIO>().FirstOrDefault(c => c.username == txtNombre.Text);
                if(user != null)
                {
                    user.password = Seguridad.Encriptar(pass1);
                    dcConsulta.SubmitChanges();
                    this.Session["Session"] = 100;
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "alerta1", "<script>alert('Las contraseñas fueron cambiadas con exito')</script>");
                    ClientScript.RegisterStartupScript(this.GetType(),"alerta", "alert('Las contraseñas fueron cambiadas con exito'); window.location='Login.aspx';",true);

                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alerta1", "<script>alert('No se ha encontrado al usuario solicitado')</script>");
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alerta1", "<script>alert('Las contraseñas deben coincidir')</script>");
            }
        }
    }
}