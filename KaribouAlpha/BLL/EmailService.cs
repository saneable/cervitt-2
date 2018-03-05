using KaribouAlpha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KaribouAlpha.BLL
{
    public class EmailService
    {
        public static bool CheckAnyEmailIsNotValid(string[] emails)
        {
            bool hasInvalidEmailAddressInList = false;

            for (int i = 0; i <= emails.Length - 1; i++)
            {
                if (string.IsNullOrEmpty(emails[i]))
                {
                    continue;
                }
                string emailToInvite = emails[i].Trim();
                try
                {
                    bool isValid = (new EmailAddressAttribute().IsValid(emailToInvite));
                    if (!isValid)
                    {
                        hasInvalidEmailAddressInList = true;
                        break;
                    }
                }
                catch
                {
                    continue;
                }
            }
            return hasInvalidEmailAddressInList;
        }

        public bool SendEmail(SmtpSetting smtpSetting, string mailSubject, string mailBody, string emailTo, string emailCc, string emailBcc, out string errorMessage)
        {
            try
            {

                errorMessage = "";
                bool enable_ssl = smtpSetting.SmtpSSLEnable;
                string host = smtpSetting.SmtpHost;
                int port = 25;
                port = smtpSetting.SmtpPort;
                string userName = smtpSetting.SmtpUserName;
                string password = smtpSetting.SmtpPassword;
                string displayName = smtpSetting.DisplayName;

                if (string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password))
                {
                    //default email client
                    MailMessage message = new MailMessage(smtpSetting.SmtpEmailFrom, emailTo, mailSubject, mailBody);
                    message.IsBodyHtml = true;
                    SmtpClient emailClient = new SmtpClient(host);
                    emailClient.Port = port;
                    emailClient.Send(message);
                }
                else
                {
                    //custom email client
                    string from = smtpSetting.SmtpEmailFrom;
                    MailMessage msg = new MailMessage();
                    
                    msg.Sender = new MailAddress(from, displayName);
                    msg.From = new MailAddress(from, displayName);
                    msg.To.Add(emailTo);

                    if (!string.IsNullOrEmpty(emailCc))
                    {
                        msg.CC.Add(new MailAddress(emailCc));
                    }
                    if (!string.IsNullOrEmpty(emailBcc))
                    {
                        msg.Bcc.Add(new MailAddress(emailBcc));
                    }
                    msg.Subject = mailSubject;
                    msg.Body = mailBody;
                    msg.IsBodyHtml = true;
                    msg.Priority = MailPriority.Normal;
                    SmtpClient smtp = new SmtpClient(host, port);
                    
                    smtp.EnableSsl = enable_ssl;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(userName, password);
                    smtp.Send(msg);
                }
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.ToString();
                return false;
            }
        }
    }
}