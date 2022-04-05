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
    public partial class UserManager : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session;
        EMPLEADO emp;
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
                    this.lblAction.Text = "Editar empleado";
                    emp = dcConsulta.GetTable<EMPLEADO>().FirstOrDefault(c => c.Id == int.Parse(this.session.Parametros["idEmp"].ToString()));

                    if (emp != null && !this.IsPostBack)
                    {
                        this.txtNombre.Text = emp.strNombre.ToString();
                        this.txtAPaterno.Text = emp.strAPaterno.ToString();
                        this.txtAMaterno.Text = emp.strAMaterno.ToString();
                        this.txtFechaNacimiento.Text = emp.dteFechaNacimiento.ToString();
                        this.txtEdad.Text = emp.intEdad.ToString();
                        this.txtCURP.Text = emp.strCURP.ToString();
                        this.txtRFC.Text = emp.strRFC.ToString();
                        this.txtFechaIngreso.Text = emp.dteFechaIngreso.ToString();
                        this.txtRol.Text = emp.idRol.ToString();
                        this.txtArea.Text = emp.idArea.ToString();
                        this.chbxActivo.Checked = emp.boolActivo;
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
            try
            {
                if(String.IsNullOrEmpty(this.txtNombre.Text + this.txtAPaterno.Text + this.txtAMaterno.Text + this.txtFechaNacimiento.Text
                    + this.txtEdad.Text + this.txtCURP.Text + this.txtRFC.Text + txtFechaIngreso.Text + this.txtRol.Text + this.txtArea.Text))
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
                        this.Response.Redirect(this.session.Pantalla, true);
                    }
                }

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

        #endregion

        #region Metodos

        private bool updateDatos()
        {
            try
            {
                int res = 0;
                DataContext dcConsulta = new DcGeneralDataContext();
                EMPLEADO updateEmp = dcConsulta.GetTable<EMPLEADO>().FirstOrDefault(c => c.Id == emp.Id);

                res = DateTime.TryParse(this.txtFechaNacimiento.Text, CultureInfo.CreateSpecificCulture("es-MX"), DateTimeStyles.None, out DateTime newFechaNacimiento) ? res + 1 : 105;
                res = int.TryParse(this.txtEdad.Text, out int newEdad) ? res + 1 : 115;
                res = DateTime.TryParse(this.txtFechaIngreso.Text, CultureInfo.CreateSpecificCulture("es-MX"), DateTimeStyles.None, out DateTime newFechaIngreso) ? res + 1 : 125;
                res = int.TryParse(this.txtRol.Text, out int newIdRol) ? res + 1 : 135;
                CatRol catRol = dcConsulta.GetTable<CatRol>().FirstOrDefault(c => c.Id == newIdRol);
                if (catRol == null)
                    res = 145;
                res = int.TryParse(this.txtArea.Text, out int newIdArea) ? res + 1 : 155;
                CatArea catArea = dcConsulta.GetTable<CatArea>().FirstOrDefault(c => c.Id == newIdArea);
                if (catArea == null)
                    res = 165;

                if (res == 5)
                {
                    updateEmp.strNombre = this.txtNombre.Text;
                    updateEmp.strAPaterno = this.txtAPaterno.Text;
                    updateEmp.strAMaterno = this.txtAMaterno.Text;
                    updateEmp.dteFechaNacimiento = newFechaNacimiento;
                    updateEmp.intEdad = newEdad;
                    updateEmp.strCURP = this.txtCURP.Text;
                    updateEmp.strRFC = this.txtRFC.Text;
                    updateEmp.dteFechaIngreso = newFechaIngreso;
                    updateEmp.idRol = newIdRol;
                    updateEmp.idArea = newIdArea;
                    updateEmp.boolActivo = this.chbxActivo.Checked;

                    dcConsulta.SubmitChanges();

                    return true;
                }
                else
                {
                    if (res > 100 && res < 110)
                        this.lblMensaje.Text = "La fecha de nacimiento no es correcta";
                    else if (res > 110 && res < 120)
                        this.lblMensaje.Text = "La edad no es correcta";
                    else if (res > 120 && res < 130)
                        this.lblMensaje.Text = "La fecha de ingreso no es correcta";
                    else if (res > 130 && res < 140)
                        this.lblMensaje.Text = "El Rol no es correcto";
                    else if (res > 140 && res < 150)
                        this.lblMensaje.Text = "El Rol no existe";
                    else if (res > 150 && res < 160)
                        this.lblMensaje.Text = "El Area no es correcta";
                    else if (res > 160 && res < 170)
                        this.lblMensaje.Text = "El Are no existe";
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
                EMPLEADO newEmp = new EMPLEADO();

                res = DateTime.TryParse(this.txtFechaNacimiento.Text, CultureInfo.CreateSpecificCulture("es-MX"), DateTimeStyles.None, out DateTime newFechaNacimiento) ? res + 1 : 105;
                res = int.TryParse(this.txtEdad.Text, out int newEdad) ? res + 1 : 115;
                res = DateTime.TryParse(this.txtFechaIngreso.Text, CultureInfo.CreateSpecificCulture("es-MX"), DateTimeStyles.None, out DateTime newFechaIngreso) ? res + 1 : 125;
                res = int.TryParse(this.txtRol.Text, out int newIdRol) ? res + 1 : 135;
                CatRol catRol = dcConsulta.GetTable<CatRol>().FirstOrDefault(c => c.Id == newIdRol);
                if (catRol == null)
                    res = 145;
                res = int.TryParse(this.txtArea.Text, out int newIdArea) ? res + 1 : 155;
                CatArea catArea = dcConsulta.GetTable<CatArea>().FirstOrDefault(c => c.Id == newIdArea);
                if (catArea == null)
                    res = 165;

                if (res == 5)
                {
                    newEmp.strNombre = this.txtNombre.Text;
                    newEmp.strAPaterno = this.txtAPaterno.Text;
                    newEmp.strAMaterno = this.txtAMaterno.Text;
                    newEmp.dteFechaNacimiento = newFechaNacimiento;
                    newEmp.intEdad = newEdad;
                    newEmp.strCURP = this.txtCURP.Text;
                    newEmp.strRFC = this.txtRFC.Text;
                    newEmp.dteFechaIngreso = newFechaIngreso;
                    newEmp.idRol = newIdRol;
                    newEmp.idArea = newIdArea;
                    newEmp.boolActivo = this.chbxActivo.Checked;

                    dcConsulta.GetTable<EMPLEADO>().InsertOnSubmit(newEmp);
                    dcConsulta.SubmitChanges();

                    return true;
                }
                else
                {
                    if (res > 100 && res < 110)
                        this.lblMensaje.Text = "La fecha de nacimiento no es correcta";
                    else if (res > 110 && res < 120)
                        this.lblMensaje.Text = "La edad no es correcta";
                    else if (res > 120 && res < 130)
                        this.lblMensaje.Text = "La fecha de ingreso no es correcta";
                    else if (res > 130 && res < 140)
                        this.lblMensaje.Text = "El Rol no es correcto";
                    else if (res > 140 && res < 150)
                        this.lblMensaje.Text = "El Rol no existe";
                    else if (res > 150 && res < 160)
                        this.lblMensaje.Text = "El Area no es correcta";
                    else if (res > 160 && res < 170)
                        this.lblMensaje.Text = "El Are no existe";
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