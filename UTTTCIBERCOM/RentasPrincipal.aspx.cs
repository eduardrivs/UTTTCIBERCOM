using Data.Linq.Entity;
using System;
using System.Collections;
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
    public partial class RentasPrincipal : System.Web.UI.Page
    {
        #region Variables
        private SessionManager session;
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
        #endregion

        protected void DataSourceRentas_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            try
            {
                DataContext dcConsulta = new DcGeneralDataContext();

                List<RENTA> listaRentas =
                    dcConsulta.GetTable<RENTA>().ToList();

                e.Result = listaRentas;
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        protected void dgvRentas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int idRenta = int.Parse(e.CommandArgument.ToString());
                switch (e.CommandName)
                {
                    case "Editar":
                        this.editar(idRenta);
                        break;
                    case "Eliminar":
                        this.eliminar(idRenta);
                        break;
                }
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        #endregion


        #region Metodos

        private void editar(int idRenta)
        {
            try
            {
                DataContext dcConsulta = new DcGeneralDataContext();
                RENTA renta = dcConsulta.GetTable<RENTA>().FirstOrDefault(c=>c.Id == idRenta);

                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idPC", renta.idEquipo);
                parametrosRagion.Add("idRenta", idRenta);
                this.session.Parametros = parametrosRagion;
                this.session.Pantalla = "/RentManager.aspx";

                Session["SessionManager"] = this.session;
                this.Response.Redirect(this.session.Pantalla, false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void eliminar(int idPC)
        {
            try
            {
                DataContext dcDelete = new DcGeneralDataContext();
                RENTA renta = dcDelete.GetTable<RENTA>().First(c => c.Id == idPC);
                dcDelete.GetTable<RENTA>().DeleteOnSubmit(renta);
                dcDelete.SubmitChanges();
                this.showMessage("El registro se elimino correctamente.");
                this.DataSourceRentas.RaiseViewChanged();
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        #endregion
    }
}