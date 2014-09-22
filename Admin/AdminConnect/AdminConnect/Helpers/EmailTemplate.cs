using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CampeonatosNParty.Helpers
{
    public class EmailTemplate
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool BodyIsHtml { get; set; }

        public System.Net.Mail.MailAddress From { get; set; }
        public System.Net.Mail.MailAddress Sender { get; set; }
        public System.Net.Mail.MailAddress ReplyTo { get; set; }
        public System.Net.Mail.SmtpClient Smtp { get; set; }

        public EmailTemplate()
        {
            this.Smtp = new System.Net.Mail.SmtpClient();
            this.BodyIsHtml = true;
        }

        public string SmtpHost { get { return this.Smtp.Host; } set { this.Smtp.Host = value; } }
        public int SmtpPort { get { return this.Smtp.Port; } set { this.Smtp.Port = value; } }
        public System.Net.ICredentialsByHost SmtpCredentials { get { return this.Smtp.Credentials; } set { this.Smtp.Credentials = value; } }
        public bool SmtpSSL { get { return this.Smtp.EnableSsl; } set { this.Smtp.EnableSsl = value; } }
        public System.Net.Mail.SmtpDeliveryMethod SmtpDeliveryMethod { get { return this.Smtp.DeliveryMethod; } set { this.Smtp.DeliveryMethod = value; } }


        public System.Net.Mail.MailMessage BuildMessage(IDictionary<string, string> replaceTerms)
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            message.From = this.From;
            message.Sender = this.Sender;
            message.ReplyTo = this.ReplyTo;

            string subject = this.Subject;
            if (replaceTerms != null && replaceTerms.Count > 0)
                foreach (KeyValuePair<string, string> kvp in replaceTerms)
                    subject = subject.Replace(kvp.Key, kvp.Value);

            message.Subject = subject;
            message.SubjectEncoding = Encoding.UTF8;

            string body = this.Body;
            if (replaceTerms != null && replaceTerms.Count > 0)
                foreach (KeyValuePair<string, string> kvp in replaceTerms)
                    body = body.Replace(kvp.Key, kvp.Value);

            message.Body = body;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = this.BodyIsHtml;

            return message;
        }

        public System.Net.Mail.MailMessage BuildMessage(IDictionary<string, string> replaceTerms, IEnumerable<System.Net.Mail.MailAddress> to)
        {
            System.Net.Mail.MailMessage messge = BuildMessage(replaceTerms);
            if (to != null)
                foreach (System.Net.Mail.MailAddress ma in to)
                    messge.To.Add(ma);

            return messge;
        }

        public void Send(IDictionary<string, string> replaceTerms, IEnumerable<System.Net.Mail.MailAddress> to)
        {
            using (System.Net.Mail.MailMessage message = BuildMessage(replaceTerms, to))
            {
                this.Smtp.Send(message);
            }
        }

        public void Send(IDictionary<string, string> replaceTerms, string toName, string toEmail)
        {
            using (System.Net.Mail.MailMessage message = BuildMessage(replaceTerms))
            {
                message.To.Add(new System.Net.Mail.MailAddress(toEmail, toName, Encoding.UTF8));
                this.Smtp.Send(message);
            }
        }

        public void Load(string fileName)
        {
            if (!fileName.Contains('\\'))
            {

                string directoryName = System.IO.Path.Combine(
                            System.IO.Path.GetDirectoryName(this.GetType().Assembly.CodeBase),
                            "EmailTemplates");

                directoryName = directoryName.Replace('/', '\\').Replace("file:\\", "");

                if (!System.IO.Directory.Exists(directoryName))
                    System.IO.Directory.CreateDirectory(directoryName);

                fileName = System.IO.Path.Combine(directoryName, fileName);

            }

            System.Xml.XmlDocument document = new System.Xml.XmlDocument();
            document.Load(fileName);
            Load(document);
        }

        public void Load(System.Xml.XmlDocument document)
        {
            Load(document["EmailTemplate"]);
        }

        public void Load(System.Xml.XmlElement templateElement)
        {

            System.Xml.XmlElement smtpElement = templateElement["Smtp"];
            if (smtpElement == null)
                this.Smtp = new System.Net.Mail.SmtpClient();
            else
            {
                this.Smtp.Host = smtpElement.GetAttribute("host");
                this.Smtp.Port = int.Parse(smtpElement.GetAttribute("port"));
                this.Smtp.EnableSsl = bool.Parse(smtpElement.GetAttribute("enableSSL"));
                this.Smtp.DeliveryMethod = (System.Net.Mail.SmtpDeliveryMethod)
                    Enum.Parse(typeof(System.Net.Mail.SmtpDeliveryMethod), smtpElement.GetAttribute("deliveryMethod"));
                this.Smtp.Credentials = new System.Net.NetworkCredential(
                    smtpElement.GetAttribute("username"),
                    smtpElement.GetAttribute("password"));


            }

            System.Xml.XmlElement subjectElement = templateElement["Subject"];
            this.Subject = subjectElement == null ? null : subjectElement.InnerText;

            System.Xml.XmlElement bodyElement = templateElement["Body"];
            this.Body = bodyElement == null ? null : bodyElement.InnerText;
            this.BodyIsHtml = bodyElement == null || !bodyElement.HasAttribute("isHtml") ? false : bool.Parse(bodyElement.GetAttribute("isHtml"));

            System.Xml.XmlElement fromElement = templateElement["From"];
            if (fromElement == null)
                this.From = null;
            else
            {
                this.From = new System.Net.Mail.MailAddress(
                    fromElement.GetAttribute("address"),
                    fromElement.GetAttribute("name"),
                    System.Text.Encoding.UTF8);
            }

            System.Xml.XmlElement senderElement = templateElement["Sender"];
            if (senderElement == null)
                this.Sender = null;
            else
            {
                this.Sender = new System.Net.Mail.MailAddress(
                    senderElement.GetAttribute("address"),
                    senderElement.GetAttribute("name"),
                    System.Text.Encoding.UTF8);
            }

            System.Xml.XmlElement replyTo = templateElement["ReplyTo"];
            if (replyTo == null)
                this.ReplyTo = null;
            else
            {
                this.ReplyTo = new System.Net.Mail.MailAddress(
                    replyTo.GetAttribute("address"),
                    replyTo.GetAttribute("name"),
                    System.Text.Encoding.UTF8);
            }
        }

        public void Save(string fileName)
        {
            System.Xml.XmlDocument document = CreateXmlDocument();
            if (!fileName.Contains('\\'))
            {

                string directoryName = System.IO.Path.Combine(
                            System.IO.Path.GetDirectoryName(this.GetType().Assembly.CodeBase),
                            "EmailTemplates");

                directoryName = directoryName.Replace('/', '\\').Replace("file:\\", "");

                if (!System.IO.Directory.Exists(directoryName))
                    System.IO.Directory.CreateDirectory(directoryName);

                fileName = System.IO.Path.Combine(directoryName, fileName);

            }

            document.Save(fileName);
        }


        public System.Xml.XmlDocument CreateXmlDocument()
        {
            System.Xml.XmlDocument document = new System.Xml.XmlDocument();
            document.AppendChild(document.CreateXmlDeclaration("1.0", "utf-8", "yes"));
            document.AppendChild(CreateXmlElement(document));
            return document;
        }

        public System.Xml.XmlElement CreateXmlElement(System.Xml.XmlDocument document)
        {
            System.Xml.XmlElement templateElement = document.CreateElement("EmailTemplate");

            System.Xml.XmlElement smtpElement = document.CreateElement("Smtp");
            smtpElement.SetAttribute("host", this.Smtp.Host);
            smtpElement.SetAttribute("port", this.Smtp.Port.ToString());
            smtpElement.SetAttribute("enableSSL", this.Smtp.EnableSsl.ToString());
            smtpElement.SetAttribute("deliveryMethod", this.Smtp.DeliveryMethod.ToString());
            smtpElement.SetAttribute("username", ((System.Net.NetworkCredential)this.Smtp.Credentials).UserName);
            smtpElement.SetAttribute("password", ((System.Net.NetworkCredential)this.Smtp.Credentials).Password);
            templateElement.AppendChild(smtpElement);

            System.Xml.XmlElement subjectElement = document.CreateElement("Subject");
            subjectElement.AppendChild(document.CreateCDataSection(this.Subject));
            templateElement.AppendChild(subjectElement);

            System.Xml.XmlElement bodyElement = document.CreateElement("Body");
            bodyElement.AppendChild(document.CreateCDataSection(this.Body));
            bodyElement.SetAttribute("isHtml", this.BodyIsHtml.ToString());

            templateElement.AppendChild(bodyElement);

            if (this.Sender != null)
            {
                System.Xml.XmlElement senderElement = document.CreateElement("Sender");
                senderElement.SetAttribute("address", this.Sender.Address);
                senderElement.SetAttribute("displayName", this.Sender.DisplayName);
                templateElement.AppendChild(senderElement);
            }

            if (this.From != null)
            {
                System.Xml.XmlElement fromElement = document.CreateElement("From");
                fromElement.SetAttribute("address", this.From.Address);
                fromElement.SetAttribute("displayName", this.From.DisplayName);
                templateElement.AppendChild(fromElement);
            }

            if (this.ReplyTo != null)
            {
                System.Xml.XmlElement replyToElement = document.CreateElement("ReplyTo");
                replyToElement.SetAttribute("address", this.ReplyTo.Address);
                replyToElement.SetAttribute("displayName", this.ReplyTo.DisplayName);
                templateElement.AppendChild(replyToElement);
            }

            return templateElement;
        }
    }
}
