using Data.Linq.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
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
        EMPLEADO newEmp = new EMPLEADO();
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

                if (!this.IsPostBack)
                {
                    ListItem i;
                    List<CatRol> listRoles = dcConsulta.GetTable<CatRol>().ToList();
                    foreach (var r in listRoles)
                    {
                        i = new ListItem(r.strRol.ToString(), r.Id.ToString());
                        ddlRol.Items.Add(i);
                    }
                }

                if (this.session.Parametros["idEmp"] != null)
                {
                    this.lblAction.Text = "Editar empleado";
                    emp = dcConsulta.GetTable<EMPLEADO>().FirstOrDefault(c => c.Id == int.Parse(this.session.Parametros["idEmp"].ToString()));
                    if (emp.Id == 1)
                    {
                        ddlRol.Items.Clear();
                        ddlRol.Items.Add(new ListItem("No se puede cambiar el rol de este usuario", "1"));
                    }

                    if (emp != null && !this.IsPostBack)
                    {
                        this.txtNombre.Text = emp.strNombre.ToString();
                        this.txtAPaterno.Text = emp.strAPaterno.ToString();
                        this.txtAMaterno.Text = emp.strAMaterno.ToString();
                        if (DateTime.TryParse(emp.dteFechaNacimiento.ToString(), CultureInfo.CreateSpecificCulture("es-MX"), DateTimeStyles.None, out DateTime fechaNacimiento))
                            this.txtFechaNacimiento.Text = fechaNacimiento.ToString("dd-MM-yyyy HH:mm:ss");
                        this.txtEdad.Text = emp.intEdad.ToString();
                        this.txtCURP.Text = emp.strCURP.ToString();
                        this.txtRFC.Text = emp.strRFC.ToString();
                        if (DateTime.TryParse(emp.dteFechaIngreso.ToString(), CultureInfo.CreateSpecificCulture("es-MX"), DateTimeStyles.None, out DateTime fechaIngreso))
                            this.txtFechaIngreso.Text = fechaIngreso.ToString("dd-MM-yyyy HH:mm:ss");
                        this.ddlRol.SelectedValue = emp.idRol.ToString();
                        //this.txtRol.Text = emp.idRol.ToString();
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
                    + this.txtEdad.Text + this.txtCURP.Text + this.txtRFC.Text + txtFechaIngreso.Text + this.txtArea.Text))
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
                else
                {
                    this.btnFin.ValidationGroup = "gvSave";
                    Page.Validate("gvSave");

                    if (!Page.IsValid)
                        return;

                    String mensaje = "";
                    if (validar(ref mensaje)){

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

                                    //this.session.Pantalla = "/UserPrincipal.aspx";
                                    //this.session.Parametros["idPC"] = null;
                                    //this.session.Parametros["idRenta"] = null;
                                    //this.session.Parametros["idEmp"] = null;
                                    //Session["SessionManager"] = this.session;

                                    Hashtable parametrosRagion = new Hashtable();
                                    parametrosRagion.Add("idEmp", newEmp.Id);
                                    this.session.Parametros = parametrosRagion;
                                    this.session.Pantalla = "/UserLogManager.aspx";

                                    Session["SessionManager"] = this.session;
                                    this.Response.Redirect(this.session.Pantalla, false);
                                }
                            }
                        }
                    }
                    else
                    {
                        this.lblMensaje.Text = mensaje;
                        this.lblMensaje.Visible = true;
                        this.lblMensaje.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        protected void btnUserLogManagerList_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.session.Parametros["idEmp"] != null)
                {
                    Hashtable parametrosRagion = new Hashtable();
                    parametrosRagion.Add("idEmp", int.Parse(this.session.Parametros["idEmp"].ToString()));
                    this.session.Parametros = parametrosRagion;
                    this.session.Pantalla = "/UserLogManager.aspx";

                    Session["SessionManager"] = this.session;
                    this.Response.Redirect(this.session.Pantalla, false);
                }
                else
                {
                    this.lblMensaje.Text = "Primero registre al empleado en proceso";
                    this.lblMensaje.Visible = true;
                    this.lblMensaje.ForeColor = System.Drawing.Color.Red;
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
                res = int.TryParse(this.ddlRol.SelectedValue, out int newIdRol) ? res + 1 : 135;
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
                    updateEmp.dteFechaNacimiento = DateTime.Parse(newFechaNacimiento.ToString("MM-dd-yyyy HH:mm:ss"));
                    updateEmp.intEdad = newEdad;
                    updateEmp.strCURP = this.txtCURP.Text;
                    updateEmp.strRFC = this.txtRFC.Text;
                    updateEmp.dteFechaIngreso = DateTime.Parse(newFechaIngreso.ToString("MM-dd-yyyy HH:mm:ss"));
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

                res = DateTime.TryParse(this.txtFechaNacimiento.Text, CultureInfo.CreateSpecificCulture("es-MX"), DateTimeStyles.None, out DateTime newFechaNacimiento) ? res + 1 : 105;
                res = int.TryParse(this.txtEdad.Text, out int newEdad) ? res + 1 : 115;
                res = DateTime.TryParse(this.txtFechaIngreso.Text, CultureInfo.CreateSpecificCulture("es-MX"), DateTimeStyles.None, out DateTime newFechaIngreso) ? res + 1 : 125;
                res = int.TryParse(this.ddlRol.SelectedValue, out int newIdRol) ? res + 1 : 135;
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
                    newEmp.dteFechaNacimiento = DateTime.Parse(newFechaNacimiento.ToString("MM-dd-yyyy HH:mm:ss")); ;
                    newEmp.intEdad = newEdad;
                    newEmp.strCURP = this.txtCURP.Text;
                    newEmp.strRFC = this.txtRFC.Text;
                    newEmp.dteFechaIngreso = DateTime.Parse(newFechaIngreso.ToString("MM-dd-yyyy HH:mm:ss"));
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
                        this.lblMensaje.Text = "El Area no existe";
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

        private bool validar(ref String _mensaje)
        {
            #region Nombre
            if (txtNombre.Text.Equals(String.Empty))
            {
                _mensaje = "El nombre esta vacio";
                return false;
            }

            if (txtNombre.Text.Length < 3)
            {
                _mensaje = "El nombre debe tener 3 o más caracteres";
                return false;
            }

            if (txtNombre.Text.Length > 50)
            {
                txtNombre.Text = Regex.Replace(txtNombre.Text, @"\s{2,}", " ");
                if (txtNombre.Text.Length > 50)
                {
                    _mensaje = "Los caracteres para Nombre rebasan lo permitido (50 caracteres)";
                    return false;
                }
            }

            if (!Regex.IsMatch(txtNombre.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙüïÏÜ. ]+$"))
            {
                _mensaje = "Los caracteres insertados para 'Nombre' no son permitidos";
                return false;
            }

            if (Regex.IsMatch(txtNombre.Text, @"(.)\1{2,}"))
            {
                _mensaje = "El nombre tiene caracteres repetidos que no son correctos";
                return false;
            }
            #endregion
            #region A Paterno
            if (txtAPaterno.Text.Equals(String.Empty))
            {
                _mensaje = "El Apellido Paterno esta vacio";
                return false;
            }

            if (txtAPaterno.Text.Length < 3)
            {
                _mensaje = "El Apellido Paterno debe tener 3 o más caracteres";
                return false;
            }

            if (txtAPaterno.Text.Length > 50)
            {
                txtAPaterno.Text = Regex.Replace(txtAPaterno.Text, @"\s{2,}", " ");
                if (txtAPaterno.Text.Length > 50)
                {
                    _mensaje = "Los caracteres para Apellido Paterno rebasan lo permitido (50 caracteres)";
                    return false;
                }
            }

            if (!Regex.IsMatch(txtAPaterno.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙüïÏÜ. ]+$"))
            {
                _mensaje = "Los caracteres insertados para 'Apellido Paterno' no son permitidos";
                return false;
            }
            #endregion
            #region A Materno
            if (txtAMaterno.Text.Equals(String.Empty))
            {
                _mensaje = "El Apellido Materno esta vacio";
                return false;
            }

            if (txtAMaterno.Text.Length < 3)
            {
                _mensaje = "El Apellido Materno debe tener 3 o más caracteres";
                return false;
            }

            if (txtAMaterno.Text.Length > 50)
            {
                txtAMaterno.Text = Regex.Replace(txtAMaterno.Text, @"\s{2,}", " ");
                if (txtAMaterno.Text.Length > 50)
                {
                    _mensaje = "Los caracteres permitidos para Apellido Materno rebasan lo permitido (50 caracteres)";
                    return false;
                }
            }

            if (!Regex.IsMatch(txtAMaterno.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙüïÏÜ. ]+$"))
            {
                _mensaje = "Los caracteres insertados para 'Apellido Materno' no son permitidos";
                return false;
            }
            #endregion
            #region Fecha Nacimiento
            if (txtFechaNacimiento.Text.Equals(String.Empty))
            {
                _mensaje = "La fecha de nacimiento esta vacia";
                return false;
            }

            if (!Regex.IsMatch(txtFechaNacimiento.Text, @"^([0-2][0-9]|3[0-1])(\/|-)(0[1-9]|1[0-2])\2(\d{4})(\s)([0-1][0-9]|2[0-3])(:)([0-5][0-9])(:)([0-5][0-9])$"))
            {
                if (!Regex.IsMatch(txtFechaNacimiento.Text, @"^([0-2][0-9]|3[0-1])(\/|-)(0[1-9]|1[0-2])\2(\d{4})$"))
                {
                    _mensaje = "La fecha de nacimiento no corresponde con el formato solicitado";
                    return false;
                }
            }
            #endregion
            #region Edad
            if (txtEdad.Text.Equals(String.Empty))
            {
                _mensaje = "La Edad esta vacia";
                return false;
            }
            if (int.TryParse(txtEdad.Text, out int i) == false)
            {
                _mensaje = "La Edad no es un número";
                return false;
            }
            else
            {
                if(i<15 || i > 90)
                {
                    _mensaje = "La Edad no esta en un rango permitido";
                    return false;
                }
            }
            #endregion
            #region CURP
            if (!Regex.IsMatch(txtCURP.Text, @"^[A-Z]{1}[AEIOU]{1}[A-Z]{2}[0-9]{2}(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])[HM]{1}(AS|BC|BS|CC|CS|CH|CL|CM|DF|DG|GT|GR|HG|JC|MC|MN|MS|NT|NL|OC|PL|QT|QR|SP|SL|SR|TC|TS|TL|VZ|YN|ZS|NE)[B-DF-HJ-NP-TV-Z]{3}[0-9A-Z]{1}[0-9]{1}$"))
            {
                _mensaje = "La CURP no corresponde con el formato solicitado";
                return false;
            }
            #endregion
            #region RFC
            if (!Regex.IsMatch(txtRFC.Text, @"^([A-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))([A-Z\d]{3})?$"))
            {
                _mensaje = "El RFC no corresponde con el formato solicitado";
                return false;
            }
            #endregion
            #region Fecha Ingreso
            if (txtFechaIngreso.Text.Equals(String.Empty))
            {
                _mensaje = "La fecha de ingreso esta vacia";
                return false;
            }

            if (!Regex.IsMatch(txtFechaIngreso.Text, @"^([0-2][0-9]|3[0-1])(\/|-)(0[1-9]|1[0-2])\2(\d{4})(\s)([0-1][0-9]|2[0-3])(:)([0-5][0-9])(:)([0-5][0-9])$"))
            {
                if (!Regex.IsMatch(txtFechaIngreso.Text, @"^([0-2][0-9]|3[0-1])(\/|-)(0[1-9]|1[0-2])\2(\d{4})$"))
                {
                    _mensaje = "La fecha de ingreso no corresponde con el formato solicitado";
                    return false;
                }
            }
            #endregion
            //#region Rol
            //if (txtRol.Text.Equals(String.Empty))
            //{
            //    _mensaje = "El Rol esta vacia";
            //    return false;
            //}
            //if (int.TryParse(txtRol.Text, out int rol) == false)
            //{
            //    _mensaje = "El Rol no es un número";
            //    return false;
            //}
            //#endregion
            #region Area
            if (txtArea.Text.Equals(String.Empty))
            {
                _mensaje = "El Rol esta vacia";
                return false;
            }
            if (int.TryParse(txtArea.Text, out int area) == false)
            {
                _mensaje = "El Area no es un número";
                return false;
            }
            #endregion
            return true;
        }

        #endregion
    }
}