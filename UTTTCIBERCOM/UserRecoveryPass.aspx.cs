using Data.Linq.Entity;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UTTTCIBERCOM
{
    public partial class UserRecoveryPass : System.Web.UI.Page
    {

        int idPersona;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DataSourceUser_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            try
            {
                DataContext dcConsulta = new DcGeneralDataContext();
                bool nombreBool = false;
                if (!this.txtNombre.Text.Equals(String.Empty))
                {
                    nombreBool = true;
                }

                Expression<Func<USUARIO, bool>>
                    predicate =
                    (c =>
                    ((nombreBool) ? (((nombreBool) ? c.username.Contains(this.txtNombre.Text.Trim()) : false)) : true)
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
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alerta1", "<script>alert('" + idPersona + "')</script>");

        }

        protected void btnCambiarContra_Click(object sender, EventArgs e)
        {
            string pass1 = txtPass1.Text;
            string pass2 = txtPass2.Text;

            if(pass1 == pass2)
            {
                DataContext dcConsulta = new DcGeneralDataContext();
                USUARIO user = dcConsulta.GetTable<USUARIO>().FirstOrDefault(c => c.Id == int.Parse(this.Session["Session"].ToString()));
                if(user != null)
                {
                    user.password = pass1;
                    dcConsulta.SubmitChanges();
                    this.Session["Session"] = 0;
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alerta1", "<script>alert('Las contraseñas deben coincidir')</script>");
            }
        }
    }
}