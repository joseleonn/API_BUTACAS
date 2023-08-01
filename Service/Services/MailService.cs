using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Model.DTO;
using Service.Helper;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace Service.Services
{
    public class MailService : IMailService
    {

     
      public bool EnviarMailmethod(EnviarMailDTO mail)
        {
            EmailDTO correoDTO = null;


            ///MANDAMOS MAIL DE CONFIRMACION AL REGISTRARSE.
            string contenido = $@"
<!DOCTYPE html>
<html>
<head>
    <title>Confirmación de compra</title>
    <style>
        /* Estilos generales */
        body {{
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
        }}
        
        #logoContainer {{
            text-align: center;
            display: flex;
            align-items: center;
            justify-content: center;
            flex-wrap: wrap;
            gap: 10px;
            margin-bottom: 20px;
        }}

        #logoContainer img {{
            max-width: 100px;
            max-height: 100px;
        }}

        #logoContainer h1 {{
            font-size: 40px;
            font-weight: bold;
            color: #333333; /* Color negro */
            margin-left: 10px;
        }}

        #logoContainer .nombre-logo {{
            margin: 0;
        }}
        
        /* Estilos del contenido */
        .container {{
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            color: #333333; /* Color negro */

        }}
        
        /* Estilos del mensaje */
        .message {{
            font-size: 16px;
            line-height: 1.6;
            color: #333333; /* Color negro */

        }}
        
    
        
        h1, p {{
            color: #000; /* Color del texto (negro) */
        }}
    </style>
</head>
<body>
    <div id=""logoContainer"">
        <img loading=""lazy"" class=""logo"" src=""https://bafybeifsz4oit6o7qcltj77ytm5te4damv4o3j6zejlcj2kdmcjkursi4m.ipfs.w3s.link/vividanzalogo.png"" alt=""Logo"" />
        <h1 class=""font-bold text-primary m-0"">Estudio de Danzas Viviana Musso</h1>
    </div>

    <div class='container'>
        <div class='message'>
            <h1>Confirmación de inscripción</h1>
            <p>Gracias por inscribirte a {mail.disciplina}.</p>
        </div>
    </div>
</body>
</html>";



            correoDTO = new EmailDTO()
            {
                Para = mail.mail,
                Asunto = "Gracias por inscribirte!",
                Contenido = contenido,


            };
            MailHelper.EnviarMail(correoDTO);

            return true;
        }

    }
}
