using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace PodCastPipocaAgilApi.SendEmail
{
    public class EmailService
    {
        public static bool ValidaEnderecoEmail(string enderecoEmail)
        {
            try
            {
                string texto_Validar = enderecoEmail;
                Regex expressaoRegex = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");

                // testa o email com a expressão
                if (expressaoRegex.IsMatch(texto_Validar))
                {
                    // o email é valido
                    return true;
                }
                else
                {
                    // o email é inválido
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void EnviarEmail(string ToAddress, string Subject, string Body)
        {
            try
            {
                var fromAddress = new MailAddress("victorsergioantunes23@gmail.com");
                var fromPassword = "ejnabvcdwdsvqmir";
                var toAddress = new MailAddress(ToAddress);

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (
                    var message = new MailMessage(fromAddress, toAddress)
                    {
                        IsBodyHtml = true,
                        Priority = MailPriority.High,
                        SubjectEncoding = Encoding.GetEncoding("ISO-8859-1"),
                        BodyEncoding = Encoding.GetEncoding("ISO-8859-1"),
                        Subject = Subject,
                        Body = Body
                    }
                )

                    smtp.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
                Console.WriteLine($"Detalhes: {ex.StackTrace}");
            }
        }
    }
}
