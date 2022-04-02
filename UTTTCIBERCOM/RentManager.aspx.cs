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
                        this.txtFechaInicio.Text = pc.tempInicioRenta.ToString();
                        nuevaRenta.dteFechaInicio = pc.tempInicioRenta;
                        this.txtFechaFinal.Text = DateTime.Now.ToString();
                        nuevaRenta.dteFechaFinal = DateTime.Now;
                        this.txtTiempoTotal.Text = (DateTime.Now - pc.tempInicioRenta.GetValueOrDefault()).TotalHours.ToString();
                        if (double.TryParse(this.txtTiempoTotal.Text, out double tTotal))
                            nuevaRenta.dteTiempoTotal = tTotal;
                        this.txtIdEmplado.Text = this.Session["idUser"].ToString();
                        if (int.TryParse(this.Session["idUser"].ToString(), out int idEmp))
                            nuevaRenta.idEmpleado = idEmp;
                        ListItem i = new ListItem(pc.strNombre, pc.Id.ToString());
                        this.ddlEquipo.Items.Add(i);
                        nuevaRenta.idEquipo = pc.Id;
                        //this.txtSubtotal.Text = ((double.Parse((DateTime.Now - pc.tempInicioRenta.GetValueOrDefault()).TotalHours.ToString()))*double.Parse(pc.monTarifa.ToString())).ToString();
                        this.txtSubtotal.Text = ((double.Parse(txtTiempoTotal.Text)) * double.Parse(pc.monTarifa.ToString())).ToString();
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
                        }
                    }
                }
                else
                {
                    COMPUTADORA pc = null;
                    if (int.TryParse(this.ddlEquipo.Text, out int idPCW))
                         pc = dcConsulta.GetTable<COMPUTADORA>().FirstOrDefault(c => c.Id == idPCW && c.tempInicioRenta != null);
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
                        List<COMPUTADORA> PCLista = dcConsulta.GetTable<COMPUTADORA>().ToList();
                        foreach (var r in PCLista)
                        {
                            i = new ListItem(r.strNombre.ToString(), r.Id.ToString());
                            ddlEquipo.Items.Add(i);
                        }
                    }
                }
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

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

                    this.session.Pantalla = "/RentPrincipal.aspx";
                    this.session.Parametros["idPC"] = null;
                    Session["SessionManager"] = this.session;
                    this.Response.Redirect(this.session.Pantalla, false);
                }

            }
            catch (Exception error)
            {
                throw error;
            }
        }

        protected void btnFinalRenta_Click(object sender, EventArgs e)
        {
            DataContext dcConsulta = new DcGeneralDataContext();
            if (this.session.Parametros["idPC"] != null)
            {
                COMPUTADORA pc = dcConsulta.GetTable<COMPUTADORA>().FirstOrDefault(c => c.Id == int.Parse(this.session.Parametros["idPC"].ToString()));
                dcConsulta.GetTable<RENTA>().InsertOnSubmit(nuevaRenta);
                pc.tempInicioRenta = null;
                dcConsulta.SubmitChanges();

                this.session.Parametros["idPC"] = null;
            }

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

                    this.session.Pantalla = "/RentPrincipal.aspx";
                    this.session.Parametros["idPC"] = null;
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



    }
}