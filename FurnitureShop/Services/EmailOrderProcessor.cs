using System.Net.Mail;
using System.Text;
using System.Net;
using FurnitureShop.Interface;
using FurnitureShop.Models;

namespace FurnitureShop.Services
{
    public class EmailSettings
    {
        public string MailToAddress = "orders@example.com";
        public string MailFromAddress = "sportsstore@example.com";
        public bool UseSsl = true;
        public string Username = "MySmtpUsername";
        public string Password = "MySmtpPassword";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"c:\eshop_emails";
    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private readonly EmailSettings _emailSettings;
        public EmailOrderProcessor(EmailSettings settings)
        {
            _emailSettings = settings;
        }
        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = _emailSettings.UseSsl;
                smtpClient.Host = _emailSettings.ServerName;
                smtpClient.Port = _emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                = new NetworkCredential(_emailSettings.Username,
                _emailSettings.Password);
                if (_emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod
                    = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = _emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }
                StringBuilder body = new StringBuilder()
                .AppendLine("A new order has been submitted")
                .AppendLine("---")
                .AppendLine("Items:");
                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Product.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (subtotal: {2:c}", line.Quantity,
                    line.Product.Name,
                    subtotal);
                }
                body.AppendFormat("Total order value: {0:c}", cart.ComputeTotalValue())
                .AppendLine("---")
                .AppendLine("Ship to:")
                .AppendLine(shippingInfo.FullName)
                .AppendLine(shippingInfo.AddressLine1)
                .AppendLine(shippingInfo.AddressLine2 ?? "")
                .AppendLine(shippingInfo.AddressLine3 ?? "")
                .AppendLine(shippingInfo.City)
                .AppendLine(shippingInfo.Country)
                .AppendLine(shippingInfo.Postal)
                .AppendLine("---");
                //.AppendFormat("Gift wrap: {0}",
                //shippingInfo.GiftWrap ? "Yes" : "No");
                MailMessage mailMessage = new MailMessage(
                _emailSettings.MailFromAddress, // From
                _emailSettings.MailToAddress, // To
                "New order submitted!", // Subject
                body.ToString()); // Body
                if (_emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }
                smtpClient.Send(mailMessage);
            }
        }

    }
}