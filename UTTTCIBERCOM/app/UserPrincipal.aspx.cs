﻿using Data.Linq.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTTCIBERCOM.Control;

namespace UTTTCIBERCOM.app
{
    public partial class UserPrincipal : System.Web.UI.Page
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


        protected void btnUserPrincipal_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConfigurationManager.AppSettings["session"] == "0")
                {
                    this.Response.Redirect("~/app/Login.aspx", true);
                }
                if (ConfigurationManager.AppSettings["session"] == "1")
                {
                    this.session = (SessionManager)Session["SessionManager"];
                    if (!session.IsLoged)
                        this.Response.Redirect("~/app/Login.aspx", true);

                    this.session.Pantalla = "~/app/UserPrincipal.aspx";
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
                    this.Response.Redirect("~/app/Login.aspx", true);
                }
                if (ConfigurationManager.AppSettings["session"] == "1")
                {
                    this.session = (SessionManager)Session["SessionManager"];
                    if (!session.IsLoged)
                        this.Response.Redirect("~/app/Login.aspx", true);

                    this.session.Pantalla = "~/app/PCPrincipal.aspx";
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
                    this.Response.Redirect("~/app/Login.aspx", true);
                }
                if (ConfigurationManager.AppSettings["session"] == "1")
                {
                    this.session = (SessionManager)Session["SessionManager"];
                    if (!session.IsLoged)
                        this.Response.Redirect("~/app/Login.aspx", true);

                    this.session.Pantalla = "~/app/RentPrincipal.aspx";
                    Session["SessionManager"] = this.session;
                    this.Response.Redirect(this.session.Pantalla, false);
                }

            }
            catch (Exception error)
            {
                throw error;
            }
        }

        protected void DataSourceEmpleado_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            try
            {
                DataContext dcConsulta = new DcGeneralDataContext();
                //bool nombreBool = false;
                //bool sexoBool = false;
                //if (!this.txtNombre.Text.Equals(String.Empty))
                //{
                //    nombreBool = true;
                //}
                //if (this.ddlSexo.Text != "-1")
                //{
                //    sexoBool = true;
                //}

                //Expression<Func<UTTT.Ejemplo.Linq.Data.Entity.Persona, bool>>
                //    predicate =
                //    (c =>
                //    ((sexoBool) ? c.idCatSexo == int.Parse(this.ddlSexo.Text) : true) &&
                //    ((nombreBool) ? (((nombreBool) ? c.strNombre.Contains(this.txtNombre.Text.Trim()) : false)) : true)
                //    );

                //predicate.Compile();

                List<EMPLEADO> listaPersona =
                    dcConsulta.GetTable<EMPLEADO>().ToList();
                e.Result = listaPersona;
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        protected void dgvEmpleados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int idPersona = int.Parse(e.CommandArgument.ToString());
                switch (e.CommandName)
                {
                    case "Editar":
                        this.editar(idPersona);
                        break;
                    case "Eliminar":
                        this.eliminar(idPersona);
                        break;
                    case "Usuario":
                        this.usuario(idPersona);
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

        private void editar(int _idPersona)
        {
            Console.WriteLine("Wnas");
        }

        private void eliminar(int _idPersona)
        {
            Console.WriteLine("Pa tu casa");
        }

        private void usuario(int _idPersona)
        {
            Console.WriteLine("wenas");
        }

        #endregion


    }
}