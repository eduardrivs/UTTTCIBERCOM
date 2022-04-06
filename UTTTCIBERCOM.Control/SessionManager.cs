using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTTTCIBERCOM.Control
{
    public class SessionManager
    {
        public static StringBuilder _lastError;

        private int idEmpleado;
        private DateTime horaLogeo;
        private String pantalla;
        private Hashtable parametros;
        private bool isLoged;
        private StringBuilder ultimoError;

        public SessionManager(int id)
        {
            this.idEmpleado = id;
            this.horaLogeo = DateTime.Now;
            this.isLoged = true;
            ConfigurationManager.AppSettings["session"] = "1";
        }

        public bool IsLoged
        {
            get {
                if(!(isLoged && DateTime.Now < horaLogeo.AddMinutes(60)))
                {
                    ConfigurationManager.AppSettings["session"] = "0";
                    isLoged = false;
                }
                else
                {
                    horaLogeo = DateTime.Now;
                }
                return isLoged;
            }
        }

        public String Pantalla
        {
            get { return pantalla; }
            set { pantalla = value; }
        }

        public Hashtable Parametros
        {
            get
            {
                if (parametros == null)
                {
                    parametros = new Hashtable();
                }
                return parametros;
            }
            set { parametros = value; }
        }

        public StringBuilder UltimoError
        {
            get { return ultimoError; }
            set { ultimoError = value;  }
        }

        public int getEmpleado
        {
            get { return this.idEmpleado; }
        }

    }
}
