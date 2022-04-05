using Data.Linq.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTTCIBERCOM.Control;

namespace UTTTCIBERCOM.app
{
    public partial class Login : System.Web.UI.Page
    {

        #region Variables

        private SessionManager session;
        DataContext dataContext;
        USUARIO baseEntity = null;

        #endregion


        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                AppDomain.CurrentDomain.FirstChanceException += (senderr, ee) => {
                    System.Text.StringBuilder msg = new System.Text.StringBuilder();
                    msg.AppendLine(ee.Exception.GetType().FullName);
                    msg.AppendLine(ee.Exception.Message);
                    System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
                    msg.AppendLine(st.ToString());
                    msg.AppendLine();
                    SessionManager._lastError = msg;
                };
            }
            catch (Exception error)
            {
                throw error;
            }

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = this.txtUser.Text;
            string password = this.txtPass.Text;
            
            using(dataContext = new DcGeneralDataContext())
            {
                baseEntity = dataContext.GetTable<USUARIO>().FirstOrDefault(c => (c.username.Equals(username) || c.email.Equals(username)) && (c.password.Equals(password)));
            }

            if(baseEntity != null)
            {
                if (baseEntity.isValid)
                {
                    session = new SessionManager(baseEntity.idEmpleado);
                    session.Pantalla = "/RentPrincipal.aspx";
                    Session["SessionManager"] = this.session;
                    Session["idUser"] = baseEntity.Id;
                    this.Response.Redirect(this.session.Pantalla, false);
                }
                else
                {
                    ConfigurationManager.AppSettings["trylog"] = "1";
                    this.txtAlerta.Visible = true;
                }
            }
            else
            {
                ConfigurationManager.AppSettings["trylog"] = "1";
                this.txtAlerta.Visible = true;
            }
            
        }

        protected void btnRecoveryPass_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("/UserRecoveryPass.aspx", false);
        }

        #endregion
    }
}