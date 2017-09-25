using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Mvc;
using EnviarCorreos.Models;
using MailKit.Net;
using MimeKit;
using MailKit.Net.Smtp;

namespace EnviarCorreos.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SendEmail(SendEmail parameters)
        {
            if (ModelState.IsValid)
            {
                if (IsValidEmail(parameters.ClientEmail))
                {
                    try
                    {
                        MimeMessage mail = new MimeMessage();
                        mail.From.Add(new MailboxAddress(ConfigurationManager.AppSettings["FromEmail"].ToString()));
                        mail.Date = new DateTime();
                        mail.Priority = MessagePriority.Normal;
                        mail.To.Add(new MailboxAddress(parameters.ClientEmail.ToString().TrimEnd().TrimStart()));
                        mail.Subject = Constants.ConstantsSendEmail.Subject;
                        mail.Body = new TextPart("html")
                        {
                            Text = Constants.ConstantsSendEmail.MessageEmail + "<br/>" + "<br/>" + "<br/>" + Constants.ConstantsSendEmail.NoticePrivacy

                        };

                        using (var client = new SmtpClient())
                        {
                            client.Connect(ConfigurationManager.AppSettings["FromEmailServer"].ToString(), Convert.ToInt32(ConfigurationManager.AppSettings["FromEmailPort"].ToString()));
                            client.Authenticate(ConfigurationManager.AppSettings["FromEmail"].ToString(), ConfigurationManager.AppSettings["FromEmailPsw"].ToString());
                            client.Send(mail);
                            client.Disconnect(false);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                        return View("Error");
                    }
                }
                else {
                    return View("Error");
                }
            }
            return View("Index");
        }

        public static bool IsValidEmail(String eMail)
        {
            bool response = false;
            try
            {
                var valorEMail = new System.Net.Mail.MailAddress(eMail);
                response = true;
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}