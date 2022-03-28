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
using UTTTCIBERCOM.Control.Filters;

namespace UTTTCIBERCOM.app
{
    public partial class RentPrincipal : System.Web.UI.Page
    {

        #region Variables

        private SessionManager session;
        DataContext dataContext;
        List<COMPUTADORA> listaComputadoras = null;
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ConfigurationManager.AppSettings["session"] == "0")
                {
                    this.Response.Redirect("~/app/Login.aspx", true);
                }
                if (ConfigurationManager.AppSettings["trylog"] == "1")
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "error()", true);
                }
                if (ConfigurationManager.AppSettings["session"] == "1")
                {
                    this.session = (SessionManager)Session["SessionManager"];
                    if (!session.IsLoged)
                        this.Response.Redirect("~/app/Login.aspx", true);
                }

            }
            catch (Exception error)
            {
                throw error;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            ConfigurationManager.AppSettings["session"] = "0";
            session.Pantalla = "~/app/Login.aspx";
            Session["SessionManager"] = null;
            this.Response.Redirect(this.session.Pantalla, false);
        }







        protected void DataSourceComputadora_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            try
            {
                DataContext dcConsulta = new DcGeneralDataContext();

                List<COMPUTADORA> listaPC =
                    dcConsulta.GetTable<COMPUTADORA>().ToList();
                e.Result = listaPC;
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        protected void dgvComputadora_RowCommand(object sender, GridViewCommandEventArgs e)
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
                Console.WriteLine("MAL");
            }
        }

        #endregion

        #region Metodos

        private void editar(int _idPersona)
        {
            Console.WriteLine("Wnas");
        }

        private void eliminar(int _idPersona)
        {
            Console.WriteLine("Pa tu casa");
        }

        #endregion
    }
}