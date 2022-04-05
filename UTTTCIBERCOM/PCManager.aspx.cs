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
                        this.txtFechaAlta.Text = pc.dteFechaAlta.ToString();
                        this.txtArea.Text = pc.idArea.ToString();
                        this.txtTarifa.Text = pc.monTarifa.ToString();
                        this.txtTeclado.Text = pc.strTeclado.ToString();
                        this.txtMonitor.Text = pc.strMonitor.ToString();
                        this.txtMouse.Text = pc.strMouse.ToString();
                        this.txtAudifonos.Text = pc.strAudifonos.ToString();
                        this.txtCPU.Text = pc.strCPU.ToString();
                        this.txtRAM.Text = pc.strRAM.ToString();
                        this.txtGPU.Text = pc.strGPU.ToString();
                        this.txtTempRenta.Text = pc.tempInicioRenta.ToString();
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

                try
                {
                    DataContext dcConsulta = new DcGeneralDataContext();
                    if (this.session.Parametros["idPC"] != null)
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

                                this.session.Pantalla = "/PCPrincipal.aspx";
                                this.session.Parametros["idPC"] = null;
                                this.session.Parametros["idRenta"] = null;
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

                                this.session.Pantalla = "/PCPrincipal.aspx";
                                this.session.Parametros["idPC"] = null;
                                this.session.Parametros["idRenta"] = null;
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
                    res = DateTime.TryParse(this.txtTempRenta.Text, out newTempRenta) ? res + 1 : 145;

                if (res == 4)
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

        private bool validar()
        {

            return false;
        }

        #endregion
    }
}