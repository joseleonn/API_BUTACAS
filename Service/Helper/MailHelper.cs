using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
namespace Service.Helper
{
    public static class MailHelper
    {
        private static string _Host = "smtp.gmail.com";
        private static int _Puerto = 587;

        private static string _NombreEnvia = "Estudio De Danzas Viviana Musso";

        private static string _Correo = "estudiodedanzasvivianamusso0@gmail.com";
        private static string _Clave = "keaeenyiznbksaui";
        public static bool EnviarMail(EmailDTO correodto)
        {
          

            try
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(_NombreEnvia, _Correo));
                email.To.Add(MailboxAddress.Parse(correodto.Para));
                email.Subject = correodto.Asunto;
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = correodto.Contenido
                };

                var smtp = new SmtpClient();
                smtp.Connect(_Host, _Puerto, SecureSocketOptions.StartTls);
                smtp.Authenticate(_Correo, _Clave);
                smtp.Send(email);
                smtp.Disconnect(true);


                return true;
            }
            catch
            {
                return false;
            }


        }
    }
}
