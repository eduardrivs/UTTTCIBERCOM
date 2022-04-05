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
        USUARIO user = null;
        EMPLEADO emp = null;
        bool valid = false;
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
                    if (int.TryParse(Session["idUser"].ToString(), out int idUserSession))
                    {
                        DataContext dcSession = new DcGeneralDataContext();
                        USUARIO user = dcSession.GetTable<USUARIO>().FirstOrDefault(c => c.Id == idUserSession);
                        if (user != null)
                        {
                            EMPLEADO emp = dcSession.GetTable<EMPLEADO>().FirstOrDefault(c => c.Id == user.idEmpleado);
                            if (emp != null)
                            {
                                if (emp.idRol != 1)
                                {
                                    this.btnNewUser1.Visible = false;
                                    this.btnNewUser2.Visible = false;
                                    //
                                    this.btnNewEmp1.Visible = false;
                                    this.btnNewEmp2.Visible = false;
                                }
                            }
                        }
                    }
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
                    emp = dcConsulta.GetTable<EMPLEADO>().FirstOrDefault(c => c.Id == int.Parse(this.session.Parametros["idEmp"].ToString()));
                    user = dcConsulta.GetTable<USUARIO>().FirstOrDefault(c => c.idEmpleado == int.Parse(this.session.Parametros["idEmp"].ToString()));

                    if (user != null && emp != null)
                    {
                        if (!this.IsPostBack)
                        {
                            this.lblAction.Text = "Editar Usuario";
                            this.lblMensaje2.Text = "Si la contraseña se deja vacia, no se modificara";
                            this.lblMensaje2.Visible = true;
                            this.txtNombre.Text = emp.strNombre;
                            this.txtAPaterno.Text = emp.strAPaterno;
                            this.txtAMaterno.Text = emp.strAMaterno;
                            this.txtCorreo.Text = user.email.ToString();
                            this.txtUsername.Text = user.username.ToString();
                            this.txtPassword.Text = user.password.ToString();
                            this.chbxActivo.Checked = user.isValid;
                            this.txtIdEmp.Text = user.idEmpleado.ToString();
                            this.txtIdEmp.Enabled = false;
                            this.btnDelete.Enabled = true;
                        }
                    }
                    else if (emp != null)
                    {
                        if (!this.IsPostBack)
                        {
                            this.txtNombre.Text = emp.strNombre;
                            this.txtAPaterno.Text = emp.strAPaterno;
                            this.txtAMaterno.Text = emp.strAMaterno;
                            this.txtIdEmp.Text = emp.Id.ToString();
                            this.txtIdEmp.Enabled = false;
                        }
                        else
                        {
                            this.btnFin.ValidationGroup = "gvSave";
                            Page.Validate("gvSave");

                            this.txtNombre.Text = emp.strNombre;
                            this.txtAPaterno.Text = emp.strAPaterno;
                            this.txtAMaterno.Text = emp.strAMaterno;
                            this.txtIdEmp.Text = emp.Id.ToString();

                            this.txtIdEmp.Enabled = false;
                        }
                    }
                    else
                    {
                        this.lblMensaje.Text = "Error al procesar la informacion";
                        this.lblMensaje.Visible = true;
                        this.lblMensaje.ForeColor = System.Drawing.Color.Red;

                        valid = false;
                    }
                }
                else
                {
                    this.lblAction.Text = "Nuevo Usuario";

                    if (this.IsPostBack)
                    {
                        if (int.TryParse(this.txtIdEmp.Text, out int newIdEmp))
                        {
                            EMPLEADO newEmpleado = dcConsulta.GetTable<EMPLEADO>().FirstOrDefault(c => c.Id == newIdEmp);
                            if (newEmpleado != null)
                            {
                                if (dcConsulta.GetTable<USUARIO>().FirstOrDefault(c => c.idEmpleado == newIdEmp) != null)
                                {
                                    this.lblMensaje.Text = "El empleado ya tiene un usuario";
                                    this.lblMensaje.Visible = true;
                                    this.lblMensaje.ForeColor = System.Drawing.Color.Red;

                                    valid = false;
                                }
                                else
                                {
                                    this.txtNombre.Text = newEmpleado.strNombre;
                                    this.txtAPaterno.Text = newEmpleado.strAPaterno;
                                    this.txtAMaterno.Text = newEmpleado.strAMaterno;

                                    this.lblMensaje.Visible = false;
                                    valid = true;
                                }
                            }
                            else
                            {
                                this.lblMensaje.Text = "El empleado no existe";
                                this.lblMensaje.Visible = true;
                                this.lblMensaje.ForeColor = System.Drawing.Color.Red;
                                
                                valid = false;
                            }
                        }
                        else
                        {
                            this.lblMensaje.Text = "El id del empleado es erroneo";
                            this.lblMensaje.Visible = true;
                            this.lblMensaje.ForeColor = System.Drawing.Color.Red;
                            valid = false;
                        }
                    }
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
                this.btnFin.ValidationGroup = "gvSave";
                Page.Validate("gvSave");

                if (!Page.IsValid)
                    return;
                else
                    valid = true;

                try
                {
                    DataContext dcConsulta = new DcGeneralDataContext();
                    if (this.session.Parametros["idEmp"] != null)
                    {
                        if (user != null && emp != null)
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
                        else if (emp != null)
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataContext dcDelete = new DcGeneralDataContext();
                USUARIO userDelete = dcDelete.GetTable<USUARIO>().First(c => c.Id == user.Id);
                dcDelete.GetTable<USUARIO>().DeleteOnSubmit(userDelete);
                dcDelete.SubmitChanges();

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
            catch (Exception _e)
            {
                throw _e;
            }
        }

        #endregion

        #region Metodos

        private bool updateDatos()
        {
            try
            {
                DataContext dcConsulta = new DcGeneralDataContext();
                USUARIO updateUser = dcConsulta.GetTable<USUARIO>().FirstOrDefault(c => c.Id == user.Id);
                USUARIO extraUser = dcConsulta.GetTable<USUARIO>().FirstOrDefault(c => c.username == updateUser.username);

                if(this.txtPassword.Text.Equals(this.txtPassword2.Text))
                {
                    updateUser.email = this.txtCorreo.Text;
                    updateUser.username = this.txtUsername.Text;
                    if (extraUser == null)
                    {
                        if (!String.IsNullOrEmpty(this.txtPassword.Text))
                            updateUser.password = Seguridad.Encriptar(this.txtPassword.Text);
                        updateUser.isValid = this.chbxActivo.Checked;

                        dcConsulta.SubmitChanges();

                        return true;
                    }
                    else
                    {
                        this.lblMensaje.Visible = true;
                        this.lblMensaje.Text = "El nombre de usuario ya existe";
                        this.lblMensaje.ForeColor = System.Drawing.Color.Red;
                        return false;
                    }
                }
                else
                {
                    this.lblMensaje.Visible = true;
                    this.lblMensaje.Text = "Las contraseñas no coinciden";
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
                if (valid)
                {
                    int res = 0;
                    DataContext dcConsulta = new DcGeneralDataContext();
                    USUARIO newUser = new USUARIO();
                    int newIdEmp = 0;
                    if (this.txtIdEmp.Enabled)
                        res = int.TryParse(this.txtIdEmp.Text, out newIdEmp) ? res + 1 : 105;
                    else
                        res = int.TryParse(this.session.Parametros["idEmp"].ToString(), out newIdEmp) ? res + 1 : 105;
                    EMPLEADO catEmp = dcConsulta.GetTable<EMPLEADO>().First(c => c.Id == newIdEmp);
                    if (catEmp == null)
                        res = 115;
                    if (!this.txtPassword.Text.Equals(this.txtPassword2.Text))
                        res = 125;

                    if (res == 1)
                    {
                        newUser.email = this.txtCorreo.Text;
                        newUser.username = this.txtUsername.Text;
                        newUser.password = Seguridad.Encriptar(this.txtPassword.Text);
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
                        else if (res > 120 && res < 130)
                            this.lblMensaje.Text = "Las contraseñas no coinciden";
                        else
                            this.lblMensaje.Text = "Error al procesar los datos llenados";

                        this.lblMensaje.Visible = true;
                        this.lblMensaje.ForeColor = System.Drawing.Color.Red;

                        return false;
                    }
                }
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}