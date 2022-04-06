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
                                    //
                                    this.btnInfo1.Visible = true;
                                    this.btnInfo2.Visible = true;
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
                            this.lblInstruccion.Text = "";
                            this.lblMensaje2.Text = "Si la contraseña se deja vacia, no se modificara";
                            this.lblMensaje2.Visible = true;
                            
                            this.txtCorreo.Text = user.email.ToString();
                            this.txtUsername.Text = user.username.ToString();
                            this.txtPassword.Text = user.password.ToString();
                            this.chbxActivo.Checked = user.isValid;
                            
                            //this.txtIdEmp.AutoPostBack = false;
                            this.btnDelete.Enabled = true;
                        }

                        //this.txtIdEmp.Text = user.idEmpleado.ToString();
                        //this.txtIdEmp.Enabled = false;

                        this.ddlEmp.Items.Add(new ListItem(emp.strNombre + " " + emp.strAPaterno, emp.Id.ToString()));
                        this.ddlEmp.SelectedValue = emp.Id.ToString();
                        this.txtNombre.Text = emp.strNombre;
                        this.txtAPaterno.Text = emp.strAPaterno;
                        this.txtAMaterno.Text = emp.strAMaterno;
                    }
                    else if (user == null && emp != null)
                    {
                        this.lblAction.Text = "Asingar Usuario a empleado";
                        this.lblInstruccion.Text = "El empleado no cuenta con un usuario asignado, agrege los datos necesarios para crear un nuevo usuario.";
                        this.lblMensaje2.Text = "";
                        this.lblMensaje2.Visible = false;
                        this.txtNombre.Text = emp.strNombre;
                        this.txtAPaterno.Text = emp.strAPaterno;
                        this.txtAMaterno.Text = emp.strAMaterno;
                        //this.txtIdEmp.Text = emp.Id.ToString();
                        //this.txtIdEmp.Enabled = false;

                        this.ddlEmp.Items.Add(new ListItem(emp.strNombre + " " + emp.strAPaterno, emp.Id.ToString()));
                        this.ddlEmp.SelectedValue = emp.Id.ToString();
                        this.rvftxtPassword.Enabled = true;
                        this.rvftxtPassword2.Enabled = true;
                        this.btnDelete.Enabled = false;
                    }
                    else if (emp != null)
                    {
                        if (!this.IsPostBack)
                        {
                            this.txtNombre.Text = emp.strNombre;
                            this.txtAPaterno.Text = emp.strAPaterno;
                            this.txtAMaterno.Text = emp.strAMaterno;
                            //this.txtIdEmp.Text = emp.Id.ToString();
                            //this.txtIdEmp.Enabled = false;
                            this.ddlEmp.Items.Add(new ListItem(emp.strNombre + " " + emp.strAPaterno, emp.Id.ToString()));
                            this.ddlEmp.SelectedValue = emp.Id.ToString();
                        }
                        else
                        {
                            this.btnFin.ValidationGroup = "gvSave";
                            Page.Validate("gvSave");

                            this.txtNombre.Text = emp.strNombre;
                            this.txtAPaterno.Text = emp.strAPaterno;
                            this.txtAMaterno.Text = emp.strAMaterno;
                            //this.txtIdEmp.Text = emp.Id.ToString();
                            //this.txtIdEmp.Enabled = false;

                            this.ddlEmp.Items.Add(new ListItem(emp.strNombre + " " + emp.strAPaterno, emp.Id.ToString()));
                            this.ddlEmp.SelectedValue = emp.Id.ToString();
                        }
                    }
                    else
                    {
                        this.lblMensaje.Text = "Error al procesar la informacion";
                        this.lblMensaje.Visible = true;
                        this.lblMensaje.ForeColor = System.Drawing.Color.Red;

                        this.txtCorreo.Enabled = false;
                        this.txtUsername.Enabled = false;
                        this.txtPassword.Enabled = false;
                        this.txtPassword2.Enabled = false;

                        valid = false;
                    }
                }
                else
                {
                    this.lblAction.Text = "Nuevo Usuario";
                    this.lblInstruccion.Text = "Seleccione el empleado al cual agrear el nuevo usuario";

                    if (this.IsPostBack)
                    {
                        if (int.TryParse(this.ddlEmp.SelectedValue, out int newIdEmp))
                        {
                            EMPLEADO newEmpleado = dcConsulta.GetTable<EMPLEADO>().FirstOrDefault(c => c.Id == newIdEmp);
                            if (newEmpleado != null)
                            {
                                if (dcConsulta.GetTable<USUARIO>().FirstOrDefault(c => c.idEmpleado == newIdEmp) != null)
                                {
                                    this.lblMensaje.Text = "El empleado ya tiene un usuario";
                                    this.lblMensaje.Visible = true;
                                    this.lblMensaje.ForeColor = System.Drawing.Color.Red;

                                    this.txtCorreo.Enabled = false;
                                    this.txtUsername.Enabled = false;
                                    this.txtPassword.Enabled = false;
                                    this.txtPassword2.Enabled = false;

                                    valid = false;
                                }
                                else
                                {
                                    this.txtNombre.Text = newEmpleado.strNombre;
                                    this.txtAPaterno.Text = newEmpleado.strAPaterno;
                                    this.txtAMaterno.Text = newEmpleado.strAMaterno;

                                    this.lblMensaje.Visible = false;
                                    this.rvftxtPassword.Enabled = true;
                                    this.rvftxtPassword2.Enabled = true;
                                    valid = true;
                                }
                            }
                            else
                            {
                                this.lblMensaje.Text = "El empleado no existe";
                                this.lblMensaje.Visible = true;
                                this.lblMensaje.ForeColor = System.Drawing.Color.Red;

                                this.txtCorreo.Enabled = false;
                                this.txtUsername.Enabled = false;
                                this.txtPassword.Enabled = false;
                                this.txtPassword2.Enabled = false;

                                valid = false;
                            }
                        }
                        else
                        {
                            this.lblMensaje.Text = "Ingrese un empleado valido";
                            this.lblMensaje.Visible = true;
                            this.lblMensaje.ForeColor = System.Drawing.Color.Red;
                            valid = false;
                            this.txtCorreo.Enabled = false;
                            this.txtUsername.Enabled = false;
                            this.txtPassword.Enabled = false;
                            this.txtPassword2.Enabled = false;

                            //if (String.IsNullOrEmpty(this.ddlEmp.SelectedValue)) {
                            //AQUI WE
                            ///

                            if (this.ddlEmp.SelectedValue == "0") {
                                valid = true;
                                this.rvftxtCorreo.Enabled = false;
                                this.rvftxtUsername.Enabled = false;
                            }
                        }
                    }
                    else
                    {
                        this.txtCorreo.Enabled = false;
                        this.txtUsername.Enabled = false;
                        this.txtPassword.Enabled = false;
                        this.txtPassword2.Enabled = false;

                        ListItem i;
                        List<EMPLEADO> listEmp = dcConsulta.GetTable<EMPLEADO>().ToList();
                        ddlEmp.Items.Add(new ListItem("Seleccione el empleado disponible", "0"));
                        foreach (var r in listEmp)
                        {
                            i = new ListItem(r.strNombre + " " + r.strAPaterno, r.Id.ToString());
                            ddlEmp.Items.Add(i);
                        }
                        ddlEmp.SelectedValue = "0";
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
                && ddlEmp.SelectedValue == "0" && valid)
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
                                this.session.Parametros["idPC"] = null;
                                this.session.Parametros["idRenta"] = null;
                                this.session.Parametros["idEmp"] = null;
                                Session["SessionManager"] = this.session;
                                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "clientscript", "alert('Usuario actualizado correctamente'); parent.location.href='/UserPrincipal.aspx'", true);
                            }
                        }
                        else if (emp != null)
                        {
                            if (llenarDatos())
                            {
                                this.session.Parametros["idPC"] = null;
                                this.session.Parametros["idRenta"] = null;
                                this.session.Parametros["idEmp"] = null;
                                Session["SessionManager"] = this.session;
                                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "clientscript", "alert('Usuario creado correctamente'); parent.location.href='/UserPrincipal.aspx'", true);
                            }
                        }
                    }
                    else
                    {
                        if (llenarDatos())
                        {
                            this.session.Parametros["idPC"] = null;
                            this.session.Parametros["idRenta"] = null;
                            this.session.Parametros["idEmp"] = null;
                            Session["SessionManager"] = this.session;
                            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "clientscript", "alert('Usuario creado correctamente'); parent.location.href='/UserPrincipal.aspx'", true);
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

                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "clientscript", "alert('Usuario eliminado correctamente'); parent.location.href='/UserPrincipal.aspx'", true);
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

                if(this.txtPassword.Text.Equals(this.txtPassword2.Text))
                {
                    updateUser.email = this.txtCorreo.Text;
                    updateUser.username = this.txtUsername.Text;

                    if (!String.IsNullOrEmpty(this.txtPassword.Text))
                        updateUser.password = Seguridad.Encriptar(this.txtPassword.Text);
                    updateUser.isValid = this.chbxActivo.Checked;
                    dcConsulta.SubmitChanges();

                    return true;
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
                    if (this.ddlEmp.SelectedValue != "0")
                        res = int.TryParse(this.ddlEmp.SelectedValue, out newIdEmp) ? res + 1 : 105;
                    else
                        res = int.TryParse(this.session.Parametros["idEmp"].ToString(), out newIdEmp) ? res + 1 : 105;
                    EMPLEADO catEmp = dcConsulta.GetTable<EMPLEADO>().First(c => c.Id == newIdEmp);
                    if (catEmp == null)
                        res = 115;
                    if (!this.txtPassword.Text.Equals(this.txtPassword2.Text))
                        res = 125;
                    USUARIO extraUser = dcConsulta.GetTable<USUARIO>().FirstOrDefault(c => c.username == txtUsername.Text);
                    if (extraUser != null)
                        res = 135;

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
                        else if(res > 130 && res < 140)
                            this.lblMensaje.Text = "El nombre de usuario no esta disponible";
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