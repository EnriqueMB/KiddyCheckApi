using MailKit.Security;
using MimeKit.Utils;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyCheckApi.Core.Helpers
{
    public static class MailHelper
    {
        //public static void SendMailSocios(string host, string port, string user, string password,
        //    SociosVM socio, AsambleaVM asamblea, string usuario, string contraseña, string qr, string nombrePlantilla)
        //{
        //    //Obtener plantilla de html de la carpeta de recursos
        //    string htmlFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Recursos", nombrePlantilla);

        //    string htmlContent = string.Empty;

        //    using (StreamReader reader = new StreamReader(htmlFilePath))
        //    {
        //        htmlContent = reader.ReadToEnd();
        //    }

        //    //Reemplazar los valores de la plantilla
        //    htmlContent = htmlContent.Replace("{{ $nombre }}", socio.Nombre + socio.APaterno + socio.AMaterno);
        //    htmlContent = htmlContent.Replace("{{ $evento }}", asamblea.Descripcion);
        //    htmlContent = htmlContent.Replace("{{ $fecha }}", asamblea.FechaString);
        //    htmlContent = htmlContent.Replace("{{ $hora }}", asamblea.Fecha.Value.ToString("hh:mm"));
        //    htmlContent = htmlContent.Replace("{{ $lugar }}", asamblea.Lugar);
        //    htmlContent = htmlContent.Replace("{{ $usuario }}", usuario);
        //    htmlContent = htmlContent.Replace("{{ $contrasena }}", contraseña);

        //    //htmlContent = htmlContent.Replace("{$QRCODE}", "cid:qrId");

        //    var builder = new BodyBuilder();

        //    byte[] imageBytes = Convert.FromBase64String(qr);

        //    var image = new MimePart("image", "png")
        //    {
        //        Content = new MimeContent(new MemoryStream(imageBytes), ContentEncoding.Default),
        //        ContentId = MimeUtils.GenerateMessageId()
        //    };

        //    // Add the image to the linked resources
        //    builder.LinkedResources.Add(image);

        //    builder.HtmlBody = htmlContent;

        //    // Replace the 'cid' in the HTML body with the correct 'Content-Id' of the image attachment
        //    builder.HtmlBody = builder.HtmlBody.Replace("{$QRCODE}", "cid:" + image.ContentId);

        //    //Enviar correo
        //    var email = new MimeMessage();

        //    email.From.Add(MailboxAddress.Parse(user));
        //    email.To.Add(MailboxAddress.Parse(socio.Correo));
        //    email.Subject = "Credenciales de acceso a la asamblea";

        //    email.Body = builder.ToMessageBody();

        //    using var smtp = new MailKit.Net.Smtp.SmtpClient();

        //    smtp.Connect(host, int.Parse(port), SecureSocketOptions.StartTls);

        //    smtp.Authenticate(user, password);

        //    smtp.Send(email);
        //    smtp.Disconnect(true);
        //}
    }
}
