using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace WorkerCauCapa.Model.Clases
{
    public class EnviodeCorreo
    {
        public void ErrorApiTransbank(ResponseModel Texto)
        {
            var response = new ResponseModel();
            try
            {


                var asunto = "WorkerTraspaso_fp_registro_trxs";
                String textoEmail = "";
                SmtpClient client = new SmtpClient("smtp.office365.com", 587);


                client.EnableSsl = true;
                client.Timeout = 1000000000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;

                // nos autenticamos con nuestra cuenta de gmail

                //client.Credentials = new NetworkCredential("bienestar@fashionspark.com", "rrhh.2021");
                client.Credentials = new NetworkCredential("sisges.cuadratura@fashionspark.com", "Cat03082");//cambio contraseña 
                MailMessage mail = new MailMessage();
                mail.To.Add(new MailAddress("eugenio.barra@fashionspark.com"));
                mail.To.Add(new MailAddress("patricio.meneses@fashionspark.com"));
                mail.To.Add(new MailAddress("ricardo.sanchez@fashionspark.com"));
                mail.To.Add(new MailAddress("remigio.saez@fashionspark.com"));
                mail.To.Add(new MailAddress("marcelo.roa@fashionspark.com"));

                mail.From = new MailAddress("sisges.cuadratura@fashionspark.com");
                //mail.From = new MailAddress("pedro.astete@fashionspark.com");
                mail.Subject = asunto;
                mail.IsBodyHtml = false;

                //textoEmail = "<br><b>Día y fecha de solicitud</b>"
                //   + "<br>" + DateTime.Now.ToString() + "."
                //   + "<br><br>Estimado " + comensal.Nombre.ToUpper() + " " + comensal.ApellidoPaterno.ToUpper() + " " + (comensal.ApellidoMaterno == null ? " " : comensal.ApellidoMaterno.ToUpper()) + "<br><br>"
                //   + "Adjunto a este correo se encuentra codigo QR con el cual podra recibir su almuerzo diario en el casino de casa matriz<br>"
                //   + "este codigo debera ser scaneado en el dispositivo disponible en la fila del casino <br>"
                //   + "este codigo es unico e instranferible, no debe compartir su codigo para no presentar problemas al momento de solicitar su almuerzo<br><br>"
                //   + "<br><br>";




                //textoEmail = "Estimado <strong>" + comensal.Nombre.ToUpper() + " " + comensal.ApellidoPaterno.ToUpper() + " " + (comensal.ApellidoMaterno == null ? " " : comensal.ApellidoMaterno.ToUpper()) + "</strong><br><br>" +
                //             "<div style= text-align: justify;>Adjunto a este correo se encuentra Código QR con el cual podrá recibir su almuerzo diario en el casino ubicado en Casa Matriz. <br>" +
                //             "Este código deberá se escaneado en el dispositivo disponible en la fila del casino, el cual es personal e intransferible y solo está habilitado para hacer uso del servicio de casino una vez al día.<br><br>" +
                //             "Para consultas o dudas escribir a bienestar@fashionspark.com </div>";

                //textoEmail = " <strong>Alerta de Error en WokerdeApiTransbank</strong><br><br>";
                //textoEmail = Texto.Message ;

                if (Texto.error)
                {
                    textoEmail = "  <strong> Estimados el Worker a Presentado un error a las: " + DateTime.Now + " </strong><br>" +
                    "<div style= text-align: justify;>ExcepcionMensaje :" + Texto.respuesta + "<br>" +
                    "ExcepInnerException: " + "</div>";
                }
                else
                {
                    textoEmail = "  <strong> Estimados el worker ha presentado a las : " + DateTime.Now + " </strong><br>" +
                   "<div style= text-align: justify;>ExcepcionMensaje :" + Texto.respuesta + "<br>" +
                   "ExcepInnerException: " + "</div>";
                }


                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(textoEmail, null, "text/html");

                mail.BodyEncoding = UTF8Encoding.UTF8;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                //htmlView.LinkedResources.Add(
                //    new LinkedResource(path + filename)
                //    {
                //        ContentId = "QrComensal",
                //        TransferEncoding = TransferEncoding.Base64,
                //        ContentType = new ContentType("image/jpg")
                //    });

                mail.AlternateViews.Add(htmlView);
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                //mail.Attachments.Add(data);
                client.Send(mail);

                response.error = false;
                response.respuesta = "CORREO ENVIADO CORRECTAMENTE";

                // SE ACTUALIZA FECHA ENVIO QR
                //using (var dbcontext = db.Database.BeginTransaction())
                //{
                //    //var dato = db.Comensals.Where(x => x.IdComensal == guid).FirstOrDefault();
                //    //dato.FechaEnvioQr = DateTime.Now;
                //    //db.SaveChanges();
                //    //dbcontext.Commit();
                //}


            }
            catch (Exception ex)
            {
                response.error = true;
                response.respuesta = "ERROR: " + ex.Message;
            }




        }
    }
}
