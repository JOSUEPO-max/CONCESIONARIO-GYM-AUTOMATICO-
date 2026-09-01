using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
public class ServicioSMS
{
    // Reemplaza estos valores con tus credenciales de la consola de Twilio
    private const string AccountSid = "TU_ACCOUNT_SID";
    private const string AuthToken = "TU_AUTH_TOKEN";
    private const string NumeroTwilio = "+1XXXXXXXXXX"; // Tu número de SMS asignado por Twilio

    public ServicioSMS()
    {
        TwilioClient.Init(AccountSid, AuthToken);
    }

    public bool EnviarSmsConfirmacion(string numeroDestino, string nombreSocio)
    {
        try
        {
            var message = MessageResource.Create(
                body: $"Hola {nombreSocio}, ¡bienvenido a Smart Fit! Tu membresía ha sido activada exitosamente.",
                from: new PhoneNumber(NumeroTwilio),
                to: new PhoneNumber(numeroDestino)
            );

            Console.WriteLine($"[SMS ENVIADO] SID del mensaje: {message.Sid}");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR SMS] No se pudo enviar el SMS: {ex.Message}");
            return false;
        }
    }
}
