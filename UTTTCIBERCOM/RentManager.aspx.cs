using Data.Linq.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTTCIBERCOM.Control;

namespace UTTTCIBERCOM
{
    public partial class RentManager : System.Web.UI.Page
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
                if(this.session.Parametros["idPC"] != null)
                {
                    COMPUTADORA pc = dcConsulta.GetTable<COMPUTADORA>().FirstOrDefault(c=>c.Id == int.Parse(this.session.Parametros["idPC"].ToString()));

                    if (pc != null)
                    {
                        if (this.session.Parametros["idRenta"] == null)
                        {
                            if (DateTime.TryParse(pc.tempInicioRenta.ToString(), out DateTime fechInicio))
                                this.txtFechaInicio.Text = fechInicio.ToString("dd-MM-yyyy HH:mm:ss");
                            nuevaRenta.dteFechaInicio = pc.tempInicioRenta;
                            this.txtFechaFinal.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                            nuevaRenta.dteFechaFinal = DateTime.Now;
                            this.txtTiempoTotal.Text = (DateTime.Now - pc.tempInicioRenta.GetValueOrDefault()).TotalHours.ToString();
                            if (double.TryParse(this.txtTiempoTotal.Text, out double tTotal))
                                nuevaRenta.dteTiempoTotal = tTotal;
                            if (int.TryParse(this.Session["idUser"].ToString(), out int idEmp))
                            {
                                ListItem iemp;
                                List<EMPLEADO> EmpLista = dcConsulta.GetTable<EMPLEADO>().Where(c => c.Id == idEmp).ToList();
                                if (EmpLista.Count > 0)
                                {
                                    foreach (var r in EmpLista)
                                    {
                                        iemp = new ListItem(r.strNombre.ToString() + " " + r.strAPaterno, r.Id.ToString());
                                        ddlEmpleado.Items.Add(iemp);
                                    }
                                    ddlEmpleado.SelectedValue = idEmp.ToString();
                                    nuevaRenta.idEmpleado = idEmp;
                                }
                            }
                            
                            ListItem i = new ListItem(pc.strNombre, pc.Id.ToString());
                            this.ddlEquipo.Items.Add(i);
                            nuevaRenta.idEquipo = pc.Id;
                            //this.txtSubtotal.Text = ((double.Parse((DateTime.Now - pc.tempInicioRenta.GetValueOrDefault()).TotalHours.ToString()))*double.Parse(pc.monTarifa.ToString())).ToString();
                            if (double.Parse(txtTiempoTotal.Text) > 1.0)
                                this.txtSubtotal.Text = ((double.Parse(txtTiempoTotal.Text)) * double.Parse(pc.monTarifa.ToString())).ToString();
                            else
                                this.txtSubtotal.Text = (double.Parse(pc.monTarifa.ToString())).ToString();
                            if (decimal.TryParse(txtSubtotal.Text, out decimal subTotal))
                                nuevaRenta.monSubtotal = subTotal;
                            this.txtIVA.Text = (double.Parse(txtSubtotal.Text) * 0.16).ToString();
                            if (decimal.TryParse(txtIVA.Text, out decimal iva))
                                nuevaRenta.monIVA = iva;
                            this.txtTotal.Text = ((double.Parse(txtSubtotal.Text)) + (double.Parse(txtIVA.Text))).ToString();
                            if (decimal.TryParse(txtTotal.Text, out decimal total))
                                nuevaRenta.monTotal = total;
                            if (double.TryParse(this.txtPago.Text, out double pagoTotal))
                            {
                                if (decimal.TryParse(txtPago.Text, out decimal pago))
                                    nuevaRenta.monPago = pago;
                                this.txtCambio.Text = (pagoTotal - double.Parse(txtTotal.Text)).ToString();
                                if (decimal.TryParse(txtCambio.Text, out decimal cambio))
                                    nuevaRenta.monCambio = cambio;
                                if (pagoTotal - double.Parse(txtTotal.Text) >= 0)
                                    this.btnFin.Enabled = true;
                                else
                                    this.btnFin.Enabled = false;
                            }
                            else
                            {
                                this.btnFin.Enabled = false;
                            }
                        }
                        else
                        {
                            if (!this.IsPostBack)
                            {
                                nuevaRenta = dcConsulta.GetTable<RENTA>().FirstOrDefault(c => c.Id == int.Parse(this.session.Parametros["idRenta"].ToString()));

                                this.txtFechaInicio.Text = nuevaRenta.dteFechaInicio.ToString();
                                this.txtFechaFinal.Text = nuevaRenta.dteFechaFinal.ToString();
                                this.txtTiempoTotal.Text = nuevaRenta.dteTiempoTotal.ToString();
                                if (int.TryParse(nuevaRenta.idEmpleado.ToString(), out int idEmp))
                                {
                                    ListItem iemp;
                                    List<EMPLEADO> EmpLista = dcConsulta.GetTable<EMPLEADO>().ToList();
                                    if (EmpLista.Count > 0)
                                    {
                                        foreach (var r in EmpLista)
                                        {
                                            iemp = new ListItem(r.strNombre.ToString() + " " + r.strAPaterno, r.Id.ToString());
                                            ddlEmpleado.Items.Add(iemp);
                                        }
                                        ddlEmpleado.SelectedValue = idEmp.ToString();
                                    }
                                }

                                ListItem i;
                                List<COMPUTADORA> PCLista = dcConsulta.GetTable<COMPUTADORA>().ToList();
                                if (PCLista.Count > 0)
                                {
                                    foreach (var r in PCLista)
                                    {
                                        i = new ListItem(r.strNombre.ToString(), r.Id.ToString());
                                        ddlEquipo.Items.Add(i);
                                    }
                                    ddlEquipo.SelectedIndex = nuevaRenta.idEquipo - 1;
                                }
                                else
                                {
                                    this.lblMensaje.Visible = true;
                                    this.lblMensaje.ForeColor = System.Drawing.Color.Red;
                                    this.lblMensaje.Text = "Actualemnte no existen rentas activas";
                                }

                                this.txtSubtotal.Text = nuevaRenta.monSubtotal.ToString();
                                this.txtIVA.Text = nuevaRenta.monIVA.ToString();
                                this.txtTotal.Text = nuevaRenta.monTotal.ToString();
                                this.txtPago.Text = nuevaRenta.monPago.ToString();
                                this.txtCambio.Text = nuevaRenta.monCambio.ToString();

                            }

                            this.txtFechaInicio.Enabled = true;
                            this.txtFechaFinal.Enabled = true;
                            this.txtTiempoTotal.Enabled = true;
                            //this.txtIdEmplado.Enabled = true;
                            this.txtSubtotal.Enabled = true;
                            this.txtIVA.Enabled = true;
                            this.txtTotal.Enabled = true;
                            this.txtPago.Enabled = true;
                            this.txtPago.AutoPostBack = false;
                            this.txtCambio.Enabled = true;

                            if(double.TryParse(txtPago.Text, out double pago))
                                if(double.TryParse(txtTotal.Text, out double total))
                                    if(pago - total >= 0)
                                        this.btnFin.Enabled = true;
                        }
                    }
                }
                else
                {
                    COMPUTADORA pc = null;
                    if (int.TryParse(this.ddlEquipo.Text, out int idPCW))
                         pc = dcConsulta.GetTable<COMPUTADORA>().FirstOrDefault(c => c.Id == idPCW);
                    if (pc != null)
                    {
                        this.session.Parametros["idPC"] = pc.Id;
                        this.ddlEquipo.Enabled = false;
                        this.txtPago.Enabled = true;
                        this.Page_Load(sender, e);
                    }
                    else
                    {
                        this.txtPago.Enabled = false;
                        this.ddlEquipo.Enabled = true;
                        this.ddlEquipo.AutoPostBack = true;

                        ListItem i;
                        List<COMPUTADORA> PCLista = dcConsulta.GetTable<COMPUTADORA>().Where(c => c.tempInicioRenta != null).ToList();
                        if (PCLista.Count > 0)
                        {
                            ddlEquipo.Items.Add(new ListItem("Seleccione el equipo rentado","0"));
                            foreach (var r in PCLista)
                            {
                                i = new ListItem(r.strNombre.ToString(), r.Id.ToString());
                                ddlEquipo.Items.Add(i);
                            }
                            ddlEquipo.DataBind();
                        }
                        else
                        {
                            this.lblMensaje.Visible = true;
                            this.lblMensaje.ForeColor = System.Drawing.Color.Red;
                            this.lblMensaje.Text = "Actualmente no existen rentas activas";
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
        #endregion

        protected void btnFinalRenta_Click(object sender, EventArgs e)
        {
            try
            {
                DataContext dcConsulta = new DcGeneralDataContext();
                if (this.session.Parametros["idPC"] != null)
                {
                    if (this.session.Parametros["idRenta"] == null)
                    {
                        if (decimal.TryParse(txtPago.Text, out decimal pago) == false)
                        {
                            this.lblMensaje.Visible = true;
                            this.lblMensaje.ForeColor = System.Drawing.Color.Red;
                            this.lblMensaje.Text = "Error al procesar los datos llenados";
                        }
                        else
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
                RENTA updateRenta = dcConsulta.GetTable<RENTA>().FirstOrDefault(c => c.Id == int.Parse(this.session.Parametros["idRenta"].ToString()));

                res = DateTime.TryParse(this.txtFechaInicio.Text, out DateTime newFechaInicio) ? res+1 : res-1;
                res = DateTime.TryParse(this.txtFechaFinal.Text, out DateTime newFechaFinal) ? res+1 : res-1;
                res = double.TryParse(this.txtTiempoTotal.Text, out double newTiempoTotal) ? res+1 : res-1;
                res = int.TryParse(this.ddlEmpleado.SelectedValue, out int newIdEmpleado) ? res+1 : res-1;
                res = int.TryParse(this.ddlEquipo.Text, out int newIdEquipo) ? res+1 : res-1;
                res = decimal.TryParse(this.txtSubtotal.Text, out decimal newMonSubtotal) ? res+1 : res-1;
                res = decimal.TryParse(this.txtIVA.Text, out decimal newMonIVA) ? res+1 : res-1;
                res = decimal.TryParse(this.txtTotal.Text, out decimal newMonTotal) ? res+1 : res-1;
                res = decimal.TryParse(this.txtPago.Text, out decimal newMonPago) ? res+1 : res-1;
                res = decimal.TryParse(this.txtCambio.Text, out decimal newMonCambio) ? res+1 : res - 1;

                if (res >= 10 && updateRenta != null)
                {
                    updateRenta.dteFechaInicio = newFechaInicio;
                    updateRenta.dteFechaFinal = newFechaFinal;
                    updateRenta.dteTiempoTotal = newTiempoTotal;
                    updateRenta.idEmpleado = newIdEmpleado;
                    updateRenta.idEquipo = newIdEquipo;
                    updateRenta.monSubtotal = newMonSubtotal;
                    updateRenta.monIVA = newMonIVA;
                    updateRenta.monTotal = newMonTotal;
                    updateRenta.monPago = newMonPago;
                    updateRenta.monCambio = newMonCambio;

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