using Data.Linq.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTTCIBERCOM.Control;
using UTTTCIBERCOM.Control.Filters;

namespace UTTTCIBERCOM.app
{
    public partial class RentPrincipal : System.Web.UI.Page
    {

        #region Variables

        int index = 0;
        private SessionManager session;
        DataContext dataContext;
        List<COMPUTADORA> listaComputadoras = null;
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

                List<COMPUTADORA> listaPCOcupadas =
                    dcConsulta.GetTable<COMPUTADORA>().Where(C => C.tempInicioRenta.ToString().Length > 0).ToList();
                this.txtPCUsando.Text = listaPCOcupadas.Count().ToString();

                List<COMPUTADORA> listaPCLibres =
                    dcConsulta.GetTable<COMPUTADORA>().Where(C => C.tempInicioRenta.ToString() == null).ToList();
                this.txtPCLibres.Text = listaPCLibres.Count().ToString();
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
                    Session["SessionManager"] = this.session;
                    this.Response.Redirect(this.session.Pantalla, false);
                }

            }
            catch (Exception error)
            {
                throw error;
            }
        }

        protected void DataSourceComputadora_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            try
            {
                DataContext dcConsulta = new DcGeneralDataContext();

                List<COMPUTADORA> listaPC =
                    dcConsulta.GetTable<COMPUTADORA>().ToList();
                listaComputadoras = listaPC;
                e.Result = listaPC;
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        protected void dgvComputadora_RowCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                int idPC = int.Parse(e.CommandArgument.ToString());
                //ClientScript.RegisterClientScriptBlock(this.GetType(),"alerta1","<script>alert('"+idPC+"')</script>");

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

                        DataContext dcConsulta = new DcGeneralDataContext();
                        COMPUTADORA pc = dcConsulta.GetTable<COMPUTADORA>().FirstOrDefault(c=>c.Id == idPC);
                        if (String.IsNullOrEmpty(pc.tempInicioRenta.ToString()))
                        {
                            pc.tempInicioRenta = DateTime.Now;
                            dcConsulta.SubmitChanges();
                            this.DataBind();
                        }
                        else
                        {
                            Hashtable parametrosRagion = new Hashtable();
                            parametrosRagion.Add("idPC", idPC);
                            this.session.Parametros = parametrosRagion;
                            this.session.Pantalla = "/RentManager.aspx";

                            Session["SessionManager"] = this.session;
                            this.Response.Redirect(this.session.Pantalla, false);
                        }
                        
                    }

                }
                catch (Exception error)
                {
                    throw error;
                }
            }
            catch (Exception _e)
            {
                Console.WriteLine("MAL");
            }
        }

        #endregion

        #region Metodos

        #endregion

    }
}