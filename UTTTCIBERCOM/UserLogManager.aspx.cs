using Data.Linq.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTTCIBERCOM.Control;

namespace UTTTCIBERCOM
{
    public partial class UserLogManager : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session;
        USUARIO user;
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Session
            try
            {
                if (ConfigurationManager.AppSettings["session"] == "0")
                {
                    this.Response.Redirect("/Login.aspx", true);
                }
                if (ConfigurationManager.AppSettings["trylog"] == "1")
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "error()", true);
                }
                if (ConfigurationManager.AppSettings["session"] == "1")
                {
                    this.session = (SessionManager)Session["SessionManager"];
                    if (!session.IsLoged)
                        this.Response.Redirect("/Login.aspx", true);
                }

            }
            catch (Exception error)
            {
                throw error;
            }

            //Llenar etiquetas
            try
            {
                DataContext dcConsulta = new DcGeneralDataContext();
                if (this.session.Parametros["idEmp"] != null)
                {
                    this.lblAction.Text = "Editar Usuario";
                    user = dcConsulta.GetTable<USUARIO>().FirstOrDefault(c => c.idEmpleado == int.Parse(this.session.Parametros["idEmp"].ToString()));
                    EMPLEADO emp = dcConsulta.GetTable<EMPLEADO>().FirstOrDefault(c => c.Id == user.idEmpleado);

                    if (user != null && emp != null && !this.IsPostBack)
                    {
                        this.txtNombre.Text = emp.strNombre;
                        this.txtAPaterno.Text = emp.strAPaterno;
                        this.txtAMaterno.Text = emp.strAMaterno;
                        this.txtCorreo.Text = user.email.ToString();
                        this.txtUsername.Text = user.username.ToString();
                        this.txtPassword.Text = user.password.ToString();
                        this.chbxActivo.Checked = user.isValid;
                        this.txtIdEmp.Text = user.idEmpleado.ToString();
                    }
                }
                else
                {
                    this.txtNombre.Enabled = true;
                    this.txtAPaterno.Enabled = true;
                    this.txtAMaterno.Enabled = true;
                    this.txtIdEmp.Enabled = true;
                }
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        #region Eventos del menu
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            ConfigurationManager.AppSettings["session"] = "0";
            session.Pantalla = "/Login.aspx";
            Session["SessionManager"] = null;
            this.Response.Redirect(this.session.Pantalla, false);
        }

        protected void btnUserPrincipal_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConfigurationManager.AppSettings["session"] == "0")
                {
                    this.Response.Redirect("/Login.aspx", true);
                }
                if (ConfigurationManager.AppSettings["session"] == "1")
                {
                    this.session = (SessionManager)Session["SessionManager"];
                    if (!session.IsLoged)
                        this.Response.Redirect("/Login.aspx", true);

                    this.session.Pantalla = "/UserPrincipal.aspx";
                    this.session.Parametros["idPC"] = null;
                    this.session.Parametros["idRenta"] = null;
                    this.session.Parametros["idEmp"] = null;
                    Session["SessionManager"] = this.session;
                    this.Response.Redirect(this.session.Pantalla, false);
                }

            }
            catch (Exception error)
            {
                throw error;
            }
        }

        protected void btnPCPrincipal_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConfigurationManager.AppSettings["session"] == "0")
                {
                    this.Response.Redirect("/Login.aspx", true);
                }
                if (ConfigurationManager.AppSettings["session"] == "1")
                {
                    this.session = (SessionManager)Session["SessionManager"];
                    if (!session.IsLoged)
                        this.Response.Redirect("/Login.aspx", true);

                    this.session.Pantalla = "/PCPrincipal.aspx";
                    this.session.Parametros["idPC"] = null;
                    this.session.Parametros["idRenta"] = null;
                    this.session.Parametros["idEmp"] = null;
                    Session["SessionManager"] = this.session;
                    this.Response.Redirect(this.session.Pantalla, false);
                }

            }
            catch (Exception error)
            {
                throw error;
            }
        }

        protected void btnRentPrincipal_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConfigurationManager.AppSettings["session"] == "0")
                {
                    this.Response.Redirect("/Login.aspx", true);
                }
                if (ConfigurationManager.AppSettings["session"] == "1")
                {
                    this.session = (SessionManager)Session["SessionManager"];
                    if (!session.IsLoged)
                        this.Response.Redirect("/Login.aspx", true);

                    if (this.session.Parametros["idRenta"] == null)
                        this.session.Pantalla = "/RentPrincipal.aspx";
                    else
                        this.session.Pantalla = "/RentasPrincipal.aspx";
                    this.session.Parametros["idPC"] = null;
                    this.session.Parametros["idRenta"] = null;
                    this.session.Parametros["idEmp"] = null;
                    Session["SessionManager"] = this.session;
                    this.Response.Redirect(this.session.Pantalla, false);
                }

            }
            catch (Exception error)
            {
                throw error;
            }
        }

        protected void btnRentManager_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConfigurationManager.AppSettings["session"] == "0")
                {
                    this.Response.Redirect("/Login.aspx", true);
                }
                if (ConfigurationManager.AppSettings["session"] == "1")
                {
                    this.session = (SessionManager)Session["SessionManager"];
                    if (!session.IsLoged)
                        this.Response.Redirect("/Login.aspx", true);

                    this.session.Pantalla = "/RentManager.aspx";
                    this.session.Parametros["idPC"] = null;
                    this.session.Parametros["idRenta"] = null;
                    this.session.Parametros["idEmp"] = null;
                    Session["SessionManager"] = this.session;
                    this.Response.Redirect(this.session.Pantalla, false);
                }

            }
            catch (Exception error)
            {
                throw error;
            }

        }

        protected void btnRentas_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConfigurationManager.AppSettings["session"] == "0")
                {
                    this.Response.Redirect("/Login.aspx", true);
                }
                if (ConfigurationManager.AppSettings["session"] == "1")
                {
                    this.session = (SessionManager)Session["SessionManager"];
                    if (!session.IsLoged)
                        this.Response.Redirect("/Login.aspx", true);

                    this.session.Pantalla = "/RentasPrincipal.aspx";
                    this.session.Parametros["idRenta"] = null;
                    this.session.Parametros["idEmp"] = null;
                    Session["SessionManager"] = this.session;
                    this.Response.Redirect(this.session.Pantalla, false);
                }

            }
            catch (Exception error)
            {
                throw error;
            }
        }

        protected void btnPCManager_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConfigurationManager.AppSettings["session"] == "0")
                {
                    this.Response.Redirect("/Login.aspx", true);
                }
                if (ConfigurationManager.AppSettings["session"] == "1")
                {
                    this.session = (SessionManager)Session["SessionManager"];
                    if (!session.IsLoged)
                        this.Response.Redirect("/Login.aspx", true);

                    this.session.Pantalla = "/PCManager.aspx";
                    this.session.Parametros["idEmp"] = null;
                    Session["SessionManager"] = this.session;
                    this.Response.Redirect(this.session.Pantalla, false);
                }

            }
            catch (Exception error)
            {
                throw error;
            }
        }

        protected void btnUserManager_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConfigurationManager.AppSettings["session"] == "0")
                {
                    this.Response.Redirect("/Login.aspx", true);
                }
                if (ConfigurationManager.AppSettings["session"] == "1")
                {
                    this.session = (SessionManager)Session["SessionManager"];
                    if (!session.IsLoged)
                        this.Response.Redirect("/Login.aspx", true);

                    this.session.Pantalla = "/UserManager.aspx";
                    Session["SessionManager"] = this.session;
                    this.Response.Redirect(this.session.Pantalla, false);
                }

            }
            catch (Exception error)
            {
                throw error;
            }
        }

        protected void btnUserLogManager_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConfigurationManager.AppSettings["session"] == "0")
                {
                    this.Response.Redirect("/Login.aspx", true);
                }
                if (ConfigurationManager.AppSettings["session"] == "1")
                {
                    this.session = (SessionManager)Session["SessionManager"];
                    if (!session.IsLoged)
                        this.Response.Redirect("/Login.aspx", true);

                    this.session.Pantalla = "/UserLogManager.aspx";

                    Session["SessionManager"] = this.session;
                    this.Response.Redirect(this.session.Pantalla, false);
                }

            }
            catch (Exception error)
            {
                throw error;
            }
        }
        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.txtNombre.Text) && String.IsNullOrEmpty(this.txtAPaterno.Text) && String.IsNullOrEmpty(this.txtAMaterno.Text)
                && String.IsNullOrEmpty(this.txtCorreo.Text) && String.IsNullOrEmpty(this.txtUsername.Text) && String.IsNullOrEmpty(this.txtPassword.Text)
                && String.IsNullOrEmpty(this.txtIdEmp.Text))
            {
                if (ConfigurationManager.AppSettings["session"] == "0")
                {
                    this.Response.Redirect("/Login.aspx", true);
                }
                if (ConfigurationManager.AppSettings["session"] == "1")
                {
                    this.session = (SessionManager)Session["SessionManager"];
                    if (!session.IsLoged)
                        this.Response.Redirect("/Login.aspx", true);

                    this.session.Pantalla = "/UserPrincipal.aspx";
                    this.session.Parametros["idPC"] = null;
                    this.session.Parametros["idRenta"] = null;
                    this.session.Parametros["idEmp"] = null;
                    Session["SessionManager"] = this.session;
                    this.Response.Redirect(this.session.Pantalla, false);
                }
            }
            else
            {
                try
                {
                    DataContext dcConsulta = new DcGeneralDataContext();
                    if (this.session.Parametros["idEmp"] != null)
                    {
                        if (updateDatos())
                        {
                            if (ConfigurationManager.AppSettings["session"] == "0")
                            {
                                this.Response.Redirect("/Login.aspx", true);
                            }
                            if (ConfigurationManager.AppSettings["session"] == "1")
                            {
                                this.session = (SessionManager)Session["SessionManager"];
                                if (!session.IsLoged)
                                    this.Response.Redirect("/Login.aspx", true);

                                this.session.Pantalla = "/UserPrincipal.aspx";
                                this.session.Parametros["idPC"] = null;
                                this.session.Parametros["idRenta"] = null;
                                this.session.Parametros["idEmp"] = null;
                                Session["SessionManager"] = this.session;
                                this.Response.Redirect(this.session.Pantalla, false);
                            }
                        }
                    }
                    else
                    {
                        if (llenarDatos())
                        {
                            if (ConfigurationManager.AppSettings["session"] == "0")
                            {
                                this.Response.Redirect("/Login.aspx", true);
                            }
                            if (ConfigurationManager.AppSettings["session"] == "1")
                            {
                                this.session = (SessionManager)Session["SessionManager"];
                                if (!session.IsLoged)
                                    this.Response.Redirect("/Login.aspx", true);

                                this.session.Pantalla = "/UserPrincipal.aspx";
                                this.session.Parametros["idPC"] = null;
                                this.session.Parametros["idRenta"] = null;
                                this.session.Parametros["idEmp"] = null;
                                Session["SessionManager"] = this.session;
                                this.Response.Redirect(this.session.Pantalla, false);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        #endregion

        #region Metodos

        private bool updateDatos()
        {
            try
            {
                int res = 0;
                DataContext dcConsulta = new DcGeneralDataContext();
                USUARIO updateUser = dcConsulta.GetTable<USUARIO>().FirstOrDefault(c => c.Id == user.Id);

                res = int.TryParse(this.txtIdEmp.Text, out int newIdEmp) ? res + 1 : 105;
                EMPLEADO catEmp = dcConsulta.GetTable<EMPLEADO>().First(c => c.Id == newIdEmp);
                if (catEmp == null)
                    res = 115;

                if (res == 1)
                {
                    updateUser.email = this.txtCorreo.Text;
                    updateUser.username = this.txtUsername.Text;
                    updateUser.password = this.txtPassword.Text;
                    updateUser.isValid = this.chbxActivo.Checked;
                    updateUser.idEmpleado = newIdEmp;
                    
                    dcConsulta.SubmitChanges();

                    return true;
                }
                else
                {
                    if (res > 100 && res < 110)
                        this.lblMensaje.Text = "El ID del empleado no es correcto";
                    else if (res > 110 && res < 120)
                        this.lblMensaje.Text = "El ID del empleado no existe";
                    else
                        this.lblMensaje.Text = "Error al procesar los datos llenados";

                    this.lblMensaje.Visible = true;
                    this.lblMensaje.ForeColor = System.Drawing.Color.Red;

                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool llenarDatos()
        {
            try
            {
                int res = 0;
                DataContext dcConsulta = new DcGeneralDataContext();
                USUARIO newUser = new USUARIO();

                res = int.TryParse(this.txtIdEmp.Text, out int newIdEmp) ? res + 1 : 105;
                EMPLEADO catEmp = dcConsulta.GetTable<EMPLEADO>().First(c => c.Id == newIdEmp);
                if (catEmp == null)
                    res = 115;

                if (res == 1)
                {
                    newUser.email = this.txtCorreo.Text;
                    newUser.username = this.txtUsername.Text;
                    newUser.password = this.txtPassword.Text;
                    newUser.isValid = this.chbxActivo.Checked;
                    newUser.idEmpleado = newIdEmp;

                    dcConsulta.GetTable<USUARIO>().InsertOnSubmit(newUser);
                    dcConsulta.SubmitChanges();

                    return true;
                }
                else
                {
                    if (res > 100 && res < 110)
                        this.lblMensaje.Text = "El ID del empleado no es correcto";
                    else if (res > 110 && res < 120)
                        this.lblMensaje.Text = "El ID del empleado no existe";
                    else
                        this.lblMensaje.Text = "Error al procesar los datos llenados";

                    this.lblMensaje.Visible = true;
                    this.lblMensaje.ForeColor = System.Drawing.Color.Red;

                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}