using CONCESIONARIO_GYM___AUTOMATICO_.models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CONCESIONARIO_GYM___AUTOMATICO_.servicios
{
    public class ServicioEmail
    {
        // Configuración SMTP (Ejemplo usando Gmail)
        private readonly string _servidorSmtp = "smtp.gmail.com";
        private readonly int _puerto = 587;
        private readonly string _correoEmisor = "josuemarcillo68@gmail.com"; // Correo del gimnasio
        private readonly string _passwordEmisor = "rkpu lpfa evcc uvqd"; // Contraseña de aplicación de Gmail

        /// <summary>
        /// Envía un correo electrónico de forma asíncrona/síncrona.
        /// </summary>
        public bool EnviarCorreo(string correoDestino, string asunto, string mensajeBody)
        {
            try
            {
                // Validación básica de parámetros
                if (string.IsNullOrWhiteSpace(correoDestino))
                {
                    Console.WriteLine("[EMAIL] Error: El correo de destino está vacío.");
                    return false;
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(_correoEmisor, "Smart Fit Centro");
                    mail.To.Add(correoDestino);
                    mail.Subject = asunto;
                    mail.Body = mensajeBody;
                    mail.IsBodyHtml = true; // Permite formato HTML en el mensaje

                    using (SmtpClient smtp = new SmtpClient(_servidorSmtp, _puerto))
                    {
                        smtp.Credentials = new NetworkCredential(_correoEmisor, _passwordEmisor);
                        smtp.EnableSsl = true; // Requerido para servidores seguros como Gmail/Outlook

                        smtp.Send(mail);
                        Console.WriteLine($"[EMAIL EXITOSO] Correo enviado a {correoDestino}");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EMAIL ERROR] No se pudo enviar el correo: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Plantilla lista para enviar la bienvenida a un nuevo socio.
        /// </summary>
        public bool EnviarBienvenidaSocio(Socio socio)
        {
            string asunto = "¡Bienvenido a Smart Fit Centro!";
            string mensaje = $@"
        <h2>¡Hola, {socio.Nombre}!</h2>
        <p>Nos alegra darle la bienvenida a nuestro gimnasio.</p>
        <p><b>Detalles de tu registro:</b></p>
        <ul>
            <li><b>Cédula:</b> {socio.Cedula}</li>
            <li><b>Tipo de Membresía:</b> {socio.TipoMembresia.ToUpper()}</li>
            <li><b>Estado:</b> {(socio.EstadoMembresia ? "Activa" : "Inactiva")}</li>
        </ul>
        <p>¡Te esperamos en tus entrenamientos!</p>";

            return EnviarCorreo(socio.Correo, asunto, mensaje);
        }






        /// <summary>
        /// Envía el comprobante de pago de suscripción mensual ($20) con fechas exactas.
        /// </summary>
        public bool EnviarComprobantePago(Socio socio, decimal monto, DateTime fechaPago)
        {
            DateTime fechaVencimiento = fechaPago.AddMonths(1); // Suma un mes a la fecha de pago

            string asunto = "🏋️ Comprobante de Pago - Smart Fit Centro";
            string mensaje = $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; padding: 20px; border: 1px solid #e0e0e0; border-radius: 8px;'>
                    <h2 style='color: #2c3e50; text-align: center;'>¡Gracias por tu pago, {socio.Nombre}!</h2>
                    <p>Se ha procesado exitosamente el registro de tu membresía en <b>Smart Fit Centro</b>.</p>
                    
                    <hr style='border: 0; border-top: 1px solid #ccc;' />
                    
                    <h3>Detalles del Recibo:</h3>
                    <ul>
                        <li><b>Socio:</b> {socio.Nombre} (Cédula: {socio.Cedula})</li>
                        <li><b>Monto Pagado:</b> ${monto:F2} USD</li>
                        <li><b>Fecha y Hora de Pago:</b> {fechaPago:dd/MM/yyyy HH:mm:ss}</li>
                        <li><b>Tipo de Membresía:</b> {socio.TipoMembresia.ToUpper()}</li>
                        <li><b>Estado:</b> ACTIVA</li>
                    </ul>

                    <div style='background-color: #f8f9fa; padding: 15px; border-left: 4px solid #28a745; margin: 15px 0;'>
                        <p style='margin: 0; font-weight: bold;'>📅 Vencimiento de Membresía:</p>
                        <p style='margin: 5px 0 0 0; font-size: 1.1em; color: #28a745;'>Su suscripción expira el <b>{fechaVencimiento:dd/MM/yyyy}</b>.</p>
                    </div>

                    <p style='text-align: center; color: #7f8c8d; font-size: 0.9em; margin-top: 20px;'>
                        ¡Disfruta tus entrenamientos! Si tienes dudas, contáctanos en recepción.
                    </p>
                </div>";

            return EnviarCorreo(socio.Correo, asunto, mensaje);
        }
    }
}
