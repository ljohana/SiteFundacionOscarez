using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibNotificaciones;

namespace SitioFundacionOscarez.Seguridad
{
    public class Notificaciones
    {

        #region Atributos
        private clsEmail ObjMail;
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
        private string RutaLOG;
        #endregion

        #region Metodos Publicos

        public void EnviarNotificacion()
        {
            try
            {
                var ServidorSMTP = System.Configuration.ConfigurationManager.AppSettings["ServidorSMTP"].ToString();
                var Puerto = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Puerto"].ToString());
                var BuzonCorreo = System.Configuration.ConfigurationManager.AppSettings["BuzonCorreo"].ToString();
                var PwdBuzon = System.Configuration.ConfigurationManager.AppSettings["PwdBuzon"].ToString();

                ObjMail = new clsEmail(ServidorSMTP, Puerto, BuzonCorreo, PwdBuzon);
                ObjMail.From = BuzonCorreo;
                ObjMail.To = BuzonCorreo;
                ObjMail.Subject = "Notificación Contactenos Fundación Oscarez";
                string strBody = "<p>Sr(a) Usuario </p>";
                strBody = strBody + "<p> Se acaba de generar una notificación electronica desde el contactenos de la Fundación Oscarez con la siguiente descripción: <p>";                
                strBody = strBody + "<p>" + Asunto + "</p>";
                strBody = strBody + "<p>" + Mensaje + "</p>";
                strBody = strBody + "<p> Para obtener mas detalle del mensaje ingrese a la opción Contactenos/Index de la pagina de la Fundación Oscarez</p> <br/>";
                strBody = strBody + "<p> Saludos</p>";
                ObjMail.HTMLBody = strBody;
                ObjMail.EnviarMail();
                ObjMail = null;
            }
            catch (Exception ex)
            {
                RutaLOG = System.Configuration.ConfigurationManager.AppSettings["RutaLOG"].ToString();
                System.IO.File.AppendAllText(RutaLOG + "LogError" + DateTime.Today.ToString("ddMMyyyy") + ".txt", ex.Message);
            }
        }

        #endregion

    }
}