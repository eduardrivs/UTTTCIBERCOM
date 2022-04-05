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

namespace UTTTCIBERCOM.app
{
    public partial class PCPrincipal : System.Web.UI.Page
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
            //Verificacion de sesion
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

            //Llenado de etiquetas
            if (!this.IsPostBack)
            {
                ListItem i = new ListItem("Todos", "0");
                this.ddlDisp.Items.Add(i);
                ListItem i1 = new ListItem("Libre", "1");
                this.ddlDisp.Items.Add(i1);
                ListItem i2 = new ListItem("Renta", "2");
                this.ddlDisp.Items.Add(i2);

                this.ddlDisp.SelectedIndex = 0;
                this.ddlDisp.DataBind();
            }
        }

        #region Eventos del Menu
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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                this.DataSourcePC.RaiseViewChanged();
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al buscar");
            }
        }

        protected void DataSourcePC_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            try
            {
                DataContext dcConsulta = new DcGeneralDataContext();
                bool nombreBool = false;
                bool dispBool = false;
                if (!this.txtBuscar.Text.Equals(String.Empty))
                {
                    nombreBool = true;
                }
                if (this.ddlDisp.SelectedItem.Value != "0")
                {
                    dispBool = true;
                }

                Expression<Func<COMPUTADORA, bool>>
                    predicate =
                    (c =>
                    ((dispBool) ? ((this.ddlDisp.SelectedItem.Value == "1") ? c.tempInicioRenta == null : c.tempInicioRenta != null) : true) &&
                    ((nombreBool) ? (((nombreBool) ? c.strNombre.Contains(this.txtBuscar.Text.Trim()) : false)) : true)
                    );

                predicate.Compile();

                List<COMPUTADORA> listaPC =
                    dcConsulta.GetTable<COMPUTADORA>().Where(predicate).ToList();
                e.Result = listaPC;
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        protected void dgvPC_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int idPC = int.Parse(e.CommandArgument.ToString());
                switch (e.CommandName)
                {
                    case "Editar":
                        this.editar(idPC);
                        break;
                    case "Eliminar":
                        this.eliminar(idPC);
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

        private void editar(int idPC)
        {
            try
            {
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idPC", idPC);
                this.session.Parametros = parametrosRagion;
                this.session.Pantalla = "/PCManager.aspx";

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
                COMPUTADORA renta = dcDelete.GetTable<COMPUTADORA>().First(c => c.Id == idPC);
                dcDelete.GetTable<COMPUTADORA>().DeleteOnSubmit(renta);
                dcDelete.SubmitChanges();
                this.showMessage("El registro se elimino correctamente.");
                this.DataSourcePC.RaiseViewChanged();
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        #endregion

    }
}