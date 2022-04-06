using Data.Linq.Entity;
using System;
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
    public partial class PCManager : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session;
        RENTA nuevaRenta = new RENTA();
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
                                    this.btnNewPC1.Visible = false;
                                    this.btnNewPC2.Visible = false;
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
                if (this.session.Parametros["idPC"] != null)
                {
                    COMPUTADORA pc = dcConsulta.GetTable<COMPUTADORA>().FirstOrDefault(c => c.Id == int.Parse(this.session.Parametros["idPC"].ToString()));

                    if (pc != null && !this.IsPostBack)
                    {
                        this.txtNombre.Text = pc.strNombre.ToString();
                        this.txtDescripcion.Text = pc.strDescripcion.ToString();
                        if(DateTime.TryParse(pc.dteFechaAlta.ToString(), out DateTime fechaAlta))
                            this.txtFechaAlta.Text = fechaAlta.ToString("dd-MM-yyyy HH:mm:ss");
                        this.txtArea.Text = pc.idArea.ToString();
                        this.txtTarifa.Text = pc.monTarifa.ToString();
                        this.txtTeclado.Text = pc.strTeclado.ToString();
                        this.txtMonitor.Text = pc.strMonitor.ToString();
                        this.txtMouse.Text = pc.strMouse.ToString();
                        this.txtAudifonos.Text = pc.strAudifonos.ToString();
                        this.txtCPU.Text = pc.strCPU.ToString();
                        this.txtRAM.Text = pc.strRAM.ToString();
                        this.txtGPU.Text = pc.strGPU.ToString();
                        if (DateTime.TryParse(pc.tempInicioRenta.ToString(), out DateTime fechaTemp))
                            this.txtTempRenta.Text = fechaTemp.ToString("dd-MM-yyyy HH:mm:ss");
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
                    {
                        this.session.Pantalla = "/RentPrincipal.aspx";
                        this.session.Parametros["idPC"] = null;
                    }
                    else
                    {
                        this.session.Pantalla = "/RentasPrincipal.aspx";
                        this.session.Parametros["idPC"] = null;
                        this.session.Parametros["idRenta"] = null;
                    }
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
            if (String.IsNullOrEmpty((txtNombre.Text + txtDescripcion.Text + txtFechaAlta.Text + txtArea.Text + txtTarifa.Text + txtTempRenta.Text 
                + txtTeclado.Text + txtMonitor.Text + txtMouse.Text + txtAudifonos.Text + txtCPU.Text + txtRAM.Text + txtGPU.Text).Trim()))
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
            else
            {
                this.btnFin.ValidationGroup = "gvSave";
                Page.Validate("gvSave");

                if (!Page.IsValid)
                {
                    return;
                }

                String mensaje = "";
                if (validar(ref mensaje)) {
                    try
                    {
                        DataContext dcConsulta = new DcGeneralDataContext();
                        if (this.session.Parametros["idPC"] != null)
                        {
                            if (updateDatos())
                            {
                                this.session.Pantalla = "/PCPrincipal.aspx";
                                this.session.Parametros["idPC"] = null;
                                this.session.Parametros["idRenta"] = null;
                                Session["SessionManager"] = this.session;

                                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "clientscript", "alert('Computadora actualizada correctamente'); parent.location.href='/PCPrincipal.aspx'", true);
                            }
                        }
                        else
                        {
                            if (llenarDatos())
                            {
                                this.session.Pantalla = "/PCPrincipal.aspx";
                                this.session.Parametros["idPC"] = null;
                                this.session.Parametros["idRenta"] = null;
                                Session["SessionManager"] = this.session;
                                
                                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "clientscript", "alert('Computadora agregada correctamente'); parent.location.href='/PCPrincipal.aspx'", true);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw;
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

        #endregion

        #region Metodos

        private bool updateDatos()
        {
            try
            {
                int res = 0;
                DataContext dcConsulta = new DcGeneralDataContext();
                COMPUTADORA updatePC = dcConsulta.GetTable<COMPUTADORA>().FirstOrDefault(c => c.Id == int.Parse(this.session.Parametros["idPC"].ToString()));

                res = DateTime.TryParse(this.txtFechaAlta.Text, CultureInfo.CreateSpecificCulture("es-MX"), DateTimeStyles.None, out DateTime newDteAlta) ? res + 1 : 105;
                res = int.TryParse(this.txtArea.Text, out int newIdArea) ? res + 1 : 115;
                CatArea catArea = dcConsulta.GetTable<CatArea>().First(c => c.Id == newIdArea);
                if (catArea == null)
                    res = 125;
                res = decimal.TryParse(this.txtTarifa.Text, out decimal newTarifa) ? res + 1 : 135;

                DateTime newTempRenta = DateTime.Now;
                if (String.IsNullOrEmpty(txtTempRenta.Text))
                    res++;
                else
                    res = DateTime.TryParse(this.txtTempRenta.Text, CultureInfo.CreateSpecificCulture("es-MX"), DateTimeStyles.None, out newTempRenta) ? res + 1 : 145;

                res = (newDteAlta <= DateTime.Now)? res+1 : 155;
                res = (newTempRenta < DateTime.Now)? res+1 : 165;

                if (res == 6)
                {
                    updatePC.strNombre = this.txtNombre.Text;
                    updatePC.strDescripcion = this.txtDescripcion.Text;
                    updatePC.dteFechaAlta = newDteAlta;
                    updatePC.idArea = newIdArea;
                    updatePC.monTarifa = newTarifa;
                    updatePC.strTeclado = this.txtTeclado.Text;
                    updatePC.strMonitor = this.txtMonitor.Text;
                    updatePC.strMouse = this.txtMouse.Text;
                    updatePC.strAudifonos = this.txtAudifonos.Text;
                    updatePC.strCPU = this.txtCPU.Text;
                    updatePC.strRAM = this.txtRAM.Text;
                    updatePC.strGPU = this.txtGPU.Text;
                    if (String.IsNullOrEmpty(this.txtTempRenta.Text))
                        updatePC.tempInicioRenta = null;
                    else
                        updatePC.tempInicioRenta = newTempRenta;

                    dcConsulta.SubmitChanges();

                    return true;
                }
                else
                {
                    if (res > 100 && res < 110)
                        this.lblMensaje.Text = "La fecha de alta no es correcta";
                    else if (res > 110 && res < 120)
                        this.lblMensaje.Text = "El ID del Area no es correcto";
                    else if (res > 120 && res < 130)
                        this.lblMensaje.Text = "El ID del Area no existe";
                    else if (res > 130 && res < 140)
                        this.lblMensaje.Text = "La tarifa no es correcta";
                    else if (res > 140 && res < 150)
                        this.lblMensaje.Text = "La renta actual no es correcta";
                    else if (res > 150 && res < 160)
                        this.lblMensaje.Text = "La fecha de alta no puede ser mayor al dia de hoy";
                    else if (res > 160 && res < 170)
                        this.lblMensaje.Text = "La renta actual no puede ser mayor a la fecha y hora actuales";
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
                COMPUTADORA newPC = new COMPUTADORA();

                res = DateTime.TryParse(this.txtFechaAlta.Text, CultureInfo.CreateSpecificCulture("es-MX"), DateTimeStyles.None, out DateTime newDteAlta) ? res + 1 : 105;
                res = int.TryParse(this.txtArea.Text, out int newIdArea) ? res + 1 : 115;
                CatArea catArea = dcConsulta.GetTable<CatArea>().First(c => c.Id == newIdArea);
                if (catArea == null)
                    res = 125;
                res = decimal.TryParse(this.txtTarifa.Text, out decimal newTarifa) ? res + 1 : 135;

                DateTime newTempRenta = DateTime.Now;
                if (String.IsNullOrEmpty(txtTempRenta.Text))
                    res++;
                else
                    res = DateTime.TryParse(this.txtTempRenta.Text, out newTempRenta) ? res + 1 : 145;

                if (res == 4)
                {
                    newPC.strNombre = this.txtNombre.Text;
                    newPC.strDescripcion = this.txtDescripcion.Text;
                    newPC.dteFechaAlta = newDteAlta;
                    newPC.idArea = newIdArea;
                    newPC.monTarifa = newTarifa;
                    newPC.strTeclado = this.txtTeclado.Text;
                    newPC.strMonitor = this.txtMonitor.Text;
                    newPC.strMouse = this.txtMouse.Text;
                    newPC.strAudifonos = this.txtAudifonos.Text;
                    newPC.strCPU = this.txtCPU.Text;
                    newPC.strRAM = this.txtRAM.Text;
                    newPC.strGPU = this.txtGPU.Text;
                    if (String.IsNullOrEmpty(this.txtTempRenta.Text))
                        newPC.tempInicioRenta = null;
                    else
                        newPC.tempInicioRenta = newTempRenta;

                    dcConsulta.GetTable<COMPUTADORA>().InsertOnSubmit(newPC);
                    dcConsulta.SubmitChanges();

                    return true;
                }
                else
                {
                    if(res>100 && res<110)
                        this.lblMensaje.Text = "La fecha de alta no es correcta";
                    else if(res>110 && res<120)
                        this.lblMensaje.Text = "El ID del Area no es correcto";
                    else if(res>120 && res<130)
                        this.lblMensaje.Text = "El ID del Area no existe";
                    else if (res > 130 && res < 140)
                        this.lblMensaje.Text = "La tarifa no es correcta";
                    else if (res > 140 && res < 150)
                        this.lblMensaje.Text = "La renta actual no es correcta";
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

            if (txtNombre.Text.Length < 2)
            {
                _mensaje = "El nombre debe tener 2 o más caracteres";
                return false;
            }

            if (txtNombre.Text.Length > 50)
            {
                txtNombre.Text = Regex.Replace(txtNombre.Text, @"\s{2,}", " ");
                if (txtNombre.Text.Length > 50)
                {
                    _mensaje = "Los caracteres permitidos para Nombre rebasan lo permitido (50 caracteres)";
                    return false;
                }
            }

            if (!Regex.IsMatch(txtNombre.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙüïÏÜ1234567890\-_. ]+$"))
            {
                _mensaje = "Los caracteres insertados para 'Nombre' no son permitidos";
                return false;
            }
            #endregion
            #region Descripcion
            if (txtDescripcion.Text.Equals(String.Empty))
            {
                _mensaje = "La descripcion esta vacia";
                return false;
            }

            if (txtDescripcion.Text.Length > 50)
            {
                txtDescripcion.Text = Regex.Replace(txtDescripcion.Text, @"\s{2,}", " ");
                if (txtDescripcion.Text.Length > 50)
                {
                    _mensaje = "Los caracteres permitidos para descripcion rebasan lo permitido (50 caracteres)";
                    return false;
                }
            }

            if (!Regex.IsMatch(txtDescripcion.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙüïÏÜ1234567890\-_., ]+$"))
            {
                _mensaje = "Los caracteres insertados para 'Descripcion' no son permitidos";
                return false;
            }
            #endregion
            #region Fecha Alta
            if (txtFechaAlta.Text.Equals(String.Empty))
            {
                _mensaje = "La fecha de alta esta vacia";
                return false;
            }

            if (!Regex.IsMatch(txtFechaAlta.Text, @"^([0-2][0-9]|3[0-1])(\/|-)(0[1-9]|1[0-2])\2(\d{4})(\s)([0-1][0-9]|2[0-3])(:)([0-5][0-9])(:)([0-5][0-9])$"))
            {
                if (!Regex.IsMatch(txtFechaAlta.Text, @"^([0-2][0-9]|3[0-1])(\/|-)(0[1-9]|1[0-2])\2(\d{4})$"))
                {
                    _mensaje = "La fecha de alta ingresada no corresponde con el formato solicitado";
                    return false;
                }
            }
            #endregion
            #region Id Area
            if (txtArea.Text.Equals(String.Empty))
            {
                _mensaje = "La clave del Area esta vacia";
                return false;
            }
            if (int.TryParse(txtArea.Text, out int i) == false)
            {
                _mensaje = "La clave del Area no es un número";
                return false;
            }
            #endregion
            #region Tarifa
            if (txtTarifa.Text.Equals(String.Empty))
            {
                _mensaje = "La tarifa esta vacia";
                return false;
            }
            if (decimal.TryParse(txtTarifa.Text, out decimal tarifa) == false)
            {
                _mensaje = "La tarifa no es una cantidad valida";
                return false;
            }
            #endregion
            #region Fecha de Renta
            if (!Regex.IsMatch(txtTempRenta.Text, @"^([0-2][0-9]|3[0-1])(\/|-)(0[1-9]|1[0-2])\2(\d{4})(\s)([0-1][0-9]|2[0-3])(:)([0-5][0-9])(:)([0-5][0-9])$"))
            {
                if (!String.IsNullOrEmpty(txtTempRenta.Text))
                {
                    _mensaje = "La fecha u hora de la Renta Actual no corresponde con el formato solicitado";
                    return false;
                }
            }
            #endregion
            #region Teclado
            if (txtTeclado.Text.Equals(String.Empty))
            {
                _mensaje = "El Teclado esta vacio";
                return false;
            }

            if (txtTeclado.Text.Length < 2)
            {
                _mensaje = "El Teclado debe tener 2 o más caracteres";
                return false;
            }

            if (txtTeclado.Text.Length > 50)
            {
                txtTeclado.Text = Regex.Replace(txtTeclado.Text, @"\s{2,}", " ");
                if (txtTeclado.Text.Length > 50)
                {
                    _mensaje = "Los caracteres permitidos para Teclado rebasan lo permitido (50 caracteres)";
                    return false;
                }
            }

            if (!Regex.IsMatch(txtTeclado.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙüïÏÜ1234567890\-_.,@()[\] ]+$"))
            {
                _mensaje = "Los caracteres insertados para 'Teclado' no son permitidos";
                return false;
            }
            #endregion
            #region Monitor
            if (txtMonitor.Text.Equals(String.Empty))
            {
                _mensaje = "El Monitor esta vacio";
                return false;
            }

            if (txtMonitor.Text.Length < 2)
            {
                _mensaje = "El Monitor debe tener 2 o más caracteres";
                return false;
            }

            if (txtMonitor.Text.Length > 50)
            {
                txtMonitor.Text = Regex.Replace(txtMonitor.Text, @"\s{2,}", " ");
                if (txtMonitor.Text.Length > 50)
                {
                    _mensaje = "Los caracteres permitidos para Monitor rebasan lo permitido (50 caracteres)";
                    return false;
                }
            }

            if (!Regex.IsMatch(txtMonitor.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙüïÏÜ1234567890\-_.,@()[\] ]+$"))
            {
                _mensaje = "Los caracteres insertados para 'Monitor' no son permitidos";
                return false;
            }
            #endregion
            #region Mouse
            if (txtMouse.Text.Equals(String.Empty))
            {
                _mensaje = "El Mouse esta vacio";
                return false;
            }

            if (txtMouse.Text.Length < 2)
            {
                _mensaje = "El Mouse debe tener 2 o más caracteres";
                return false;
            }

            if (txtMouse.Text.Length > 50)
            {
                txtMouse.Text = Regex.Replace(txtMouse.Text, @"\s{2,}", " ");
                if (txtMouse.Text.Length > 50)
                {
                    _mensaje = "Los caracteres permitidos para Mouse rebasan lo permitido (50 caracteres)";
                    return false;
                }
            }

            if (!Regex.IsMatch(txtMouse.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙüïÏÜ1234567890\-_.,@()[\] ]+$"))
            {
                _mensaje = "Los caracteres insertados para 'Mouse' no son permitidos";
                return false;
            }
            #endregion
            #region Audifonos
            if (txtAudifonos.Text.Equals(String.Empty))
            {
                _mensaje = "Audifonos esta vacio";
                return false;
            }

            if (txtAudifonos.Text.Length < 2)
            {
                _mensaje = "Audifonos debe tener 2 o más caracteres";
                return false;
            }

            if (txtAudifonos.Text.Length > 50)
            {
                txtAudifonos.Text = Regex.Replace(txtAudifonos.Text, @"\s{2,}", " ");
                if (txtAudifonos.Text.Length > 50)
                {
                    _mensaje = "Los caracteres permitidos para Audifonos rebasan lo permitido (50 caracteres)";
                    return false;
                }
            }

            if (!Regex.IsMatch(txtAudifonos.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙüïÏÜ1234567890\-_.,@()[\] ]+$"))
            {
                _mensaje = "Los caracteres insertados para 'Audifonos' no son permitidos";
                return false;
            }
            #endregion
            #region CPU
            if (txtCPU.Text.Equals(String.Empty))
            {
                _mensaje = "CPU esta vacio";
                return false;
            }

            if (txtCPU.Text.Length < 2)
            {
                _mensaje = "CPU debe tener 2 o más caracteres";
                return false;
            }

            if (txtCPU.Text.Length > 50)
            {
                txtCPU.Text = Regex.Replace(txtCPU.Text, @"\s{2,}", " ");
                if (txtCPU.Text.Length > 50)
                {
                    _mensaje = "Los caracteres permitidos para CPU rebasan lo permitido (50 caracteres)";
                    return false;
                }
            }

            if (!Regex.IsMatch(txtCPU.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙüïÏÜ1234567890\-_.,@()[\] ]+$"))
            {
                _mensaje = "Los caracteres insertados para 'CPU' no son permitidos";
                return false;
            }
            #endregion
            #region RAM
            if (txtRAM.Text.Equals(String.Empty))
            {
                _mensaje = "RAM esta vacio";
                return false;
            }

            if (txtRAM.Text.Length < 2)
            {
                _mensaje = "RAM debe tener 2 o más caracteres";
                return false;
            }

            if (txtRAM.Text.Length > 50)
            {
                txtRAM.Text = Regex.Replace(txtRAM.Text, @"\s{2,}", " ");
                if (txtRAM.Text.Length > 50)
                {
                    _mensaje = "Los caracteres permitidos para RAM rebasan lo permitido (50 caracteres)";
                    return false;
                }
            }

            if (!Regex.IsMatch(txtRAM.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙüïÏÜ1234567890\-_.,@()[\] ]+$"))
            {
                _mensaje = "Los caracteres insertados para 'RAM' no son permitidos";
                return false;
            }
            #endregion
            #region GPU
            if (txtGPU.Text.Equals(String.Empty))
            {
                _mensaje = "GPU esta vacio";
                return false;
            }

            if (txtGPU.Text.Length < 2)
            {
                _mensaje = "GPU debe tener 2 o más caracteres";
                return false;
            }

            if (txtGPU.Text.Length > 50)
            {
                txtGPU.Text = Regex.Replace(txtGPU.Text, @"\s{2,}", " ");
                if (txtGPU.Text.Length > 50)
                {
                    _mensaje = "Los caracteres permitidos para GPU rebasan lo permitido (50 caracteres)";
                    return false;
                }
            }

            if (!Regex.IsMatch(txtGPU.Text, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙüïÏÜ1234567890\-_.,@()[\] ]+$"))
            {
                _mensaje = "Los caracteres insertados para 'GPU' no son permitidos";
                return false;
            }
            #endregion

            return true;
        }

        #endregion
    }
}