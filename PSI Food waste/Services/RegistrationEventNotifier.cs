// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PSI_Food_waste.Models;
using PSI_Food_waste;
using PSI_Food_waste.Pages.Forms;
using System.Net.Mail;
using System.Net;

namespace PSI_Food_waste.Services
{
    public class RegistrationEventNotifier : IRegistrationEventNotifier
    {
        public event EventHandler<EmailNotificationArgs> SuccessfulRegistrationEvent;
        public void RaiseEvent(object sender, EmailNotificationArgs e)
        {
            var Notifier = new RegistrationEventNotifier();
            SuccessfulRegistrationEvent += Notifier.OnSucessfullRegistrationEvent;
            SuccessfulRegistrationEvent?.Invoke(sender, e);
        }
        public async void OnSucessfullRegistrationEvent(object sender, EmailNotificationArgs e)
        {
            string To;
            if (e._email == "admin")
            {
                To = "foodwasteinc1@gmail.com";
            }
            else
            {
                To = e._email;
            }
            string Subject = "Welcome to Food Waste ap  p!";
            string Body = e._msg;
            using (MailMessage message = new MailMessage())
            { 
                message.To.Add(To);
                message.Subject = Subject;
                message.Body = Body;
                message.IsBodyHtml = false;
                message.From = new MailAddress("foodwasteinc1@gmail.com");
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("foodwasteinc1@gmail.com", "PSI2021ABC");
                    smtp.EnableSsl = true;
                    try
                    {
                        await smtp.SendMailAsync(message);
                    }
                    catch
                    {

                    }
                }
            }
        }
    }
}
