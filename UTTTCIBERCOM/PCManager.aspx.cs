using Data.Linq.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
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
            try
            {
                DataContext dcConsulta = new DcGeneralDataContext();
                if (this.session.Parametros["idPC"] != null)
                {
                    if (this.session.Parametros["idRenta"] == null)
                    {
                        COMPUTADORA pc = dcConsulta.GetTable<COMPUTADORA>().FirstOrDefault(c => c.Id == int.Parse(this.session.Parametros["idPC"].ToString()));
                        dcConsulta.GetTable<RENTA>().InsertOnSubmit(nuevaRenta);
                        pc.tempInicioRenta = null;
                        dcConsulta.SubmitChanges();

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
                            Session["SessionManager"] = this.session;
                            this.Response.Redirect(this.session.Pantalla, false);
                        }
                    }
                    else
                    {
                        if (!llenarDatos())
                        {
                            this.lblMensaje.Visible = true;
                            this.lblMensaje.ForeColor = System.Drawing.Color.Red;
                            this.lblMensaje.Text = "Error al procesar los datos llenados";
                        }
                        else
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
                                Session["SessionManager"] = this.session;
                                this.Response.Redirect(this.session.Pantalla, false);
                            }
                        }
                    }
                }
                else
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
                        Session["SessionManager"] = this.session;
                        this.Response.Redirect(this.session.Pantalla, false);
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

        private bool llenarDatos()
        {
            try
            {
                int res = 0;
                DataContext dcConsulta = new DcGeneralDataContext();
                COMPUTADORA updatePC = dcConsulta.GetTable<COMPUTADORA>().FirstOrDefault(c => c.Id == int.Parse(this.session.Parametros["idPC"].ToString()));

                res = DateTime.TryParse(this.txtFechaAlta.Text, out DateTime newDteAlta) ? res + 1 : res - 1;
                res = int.TryParse(this.txtArea.Text, out int newIdArea) ? res + 1 : res - 1;
                CatArea catArea = dcConsulta.GetTable<CatArea>().First(c => c.Id == newIdArea);
                if (catArea == null)
                    res = 0;
                res = decimal.TryParse(this.txtTarifa.Text, out decimal newTarifa)? res + 1 : res - 1;
                res = DateTime.TryParse(this.txtTempRenta.Text, out DateTime newTempRenta) ? res + 1 : res - 1;

                if (res >= 4 && updatePC != null)
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
                    updatePC.tempInicioRenta = newTempRenta;

                    dcConsulta.SubmitChanges();

                    return true;
                }
                else
                {
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