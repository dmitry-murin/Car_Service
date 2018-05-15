using Car_Service.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Car_Service.Helpers
{
    static class SendEmail
    {
        public static bool ConfirmReservation(Reservation reservation, ConfirmReservation confirmReservation)
        {
            //Intialise Parameters  
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            // setup Smtp authentication
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential("dimaxik97@gmail.com", "12345Qaz");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;
            //can be obtained from your model
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("car_service@gmail.com");
            msg.To.Add(new MailAddress(reservation.ApplicationUser.Email));

            msg.Subject = "Confirm reservation";
            msg.IsBodyHtml = true;
            msg.Body = string.Format("<html><head></head><body>Зы забранировали место на время {1} - {2} у мастера {3}. Для подтверждения бронирования перейдите по ссылке <a href='http://localhost:8080/confirm/{0}'>Подтвердить<a/></body>", confirmReservation.Guid, reservation.DateStart.ToLocalTime().ToString("MM/dd/yyyy H:mm:ss"), reservation.DateEnd.ToLocalTime().ToString("MM/dd/yyyy H:mm:ss"), reservation.Worker.FirstName+" "+reservation.Worker.SurName);
            try
            {
                client.Send(msg);
                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }
    }
}