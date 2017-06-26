using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Configuration;

namespace DDit.Component.Tools
{
    /// <summary>
    /// Author      : yenange
    /// Date        : 2014-02-26
    /// Description : 邮件发送辅助类
    /// </summary>
    public class EmailHelper
    {
        #region [ 属性(发送Email相关) ]
        private string _SmtpHost = string.Empty;
        private int _SmtpPort = -1;
        private string _FromEmailAddress = string.Empty;
        private string _FormEmailPassword = string.Empty;

        /// <summary>
        /// smtp 服务器 
        /// </summary>
        public string SmtpHost
        {
            get
            {
                if (string.IsNullOrEmpty(_SmtpHost))
                {
                    _SmtpHost =ConfigurationManager.AppSettings["SmtpHost"];
                }
                return _SmtpHost;
            }
        }
        /// <summary>
        /// smtp 服务器端口  默认为25
        /// </summary>
        public int SmtpPort
        {
            get
            {
                if (_SmtpPort == -1)
                {
                    if (!int.TryParse(ConfigurationManager.AppSettings["SmtpPort"], out _SmtpPort))
                    {
                        _SmtpPort = 25;
                    }
                }
                return _SmtpPort;
            }
        }
        /// <summary>
        /// 发送者 Eamil 地址
        /// </summary>
        public string FromEmailAddress
        {
            get
            {
                if (string.IsNullOrEmpty(_FromEmailAddress))
                {
                    _FromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"];
                }
                return _FromEmailAddress;
            }
        }

        /// <summary>
        /// 发送者 Eamil 密码
        /// </summary>
        public string FormEmailPassword
        {
            get
            {
                if (string.IsNullOrEmpty(_FormEmailPassword))
                {
                    _FormEmailPassword = ConfigurationManager.AppSettings["FormEmailPassword"];
                }
                return _FormEmailPassword;
            }
        }
        #endregion

        #region [ 属性(邮件相关) ]
        /// <summary>
        /// 收件人 Email 列表，多个邮件地址之间用 半角逗号 分开
        /// </summary>
        public string ToList { get; set; }
        /// <summary>
        /// 邮件的抄送者，支持群发，多个邮件地址之间用 半角逗号 分开
        /// </summary>
        public string CCList { get; set; }
        /// <summary>
        /// 邮件的密送者，支持群发，多个邮件地址之间用 半角逗号 分开
        /// </summary>
        public string BccList { get; set; }
        /// <summary>
        /// 邮件标题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 邮件正文
        /// </summary>
        public string Body { get; set; }

        private bool _IsBodyHtml = true;
        /// <summary>
        /// 邮件正文是否为Html格式
        /// </summary>
        public bool IsBodyHtml
        {
            get { return _IsBodyHtml; }
            set { _IsBodyHtml = value; }
        }
        /// <summary>
        /// 附件列表
        /// </summary>
        public List<Attachment> AttachmentList { get; set; }
        #endregion

        #region [ 构造函数 ]
        /// <summary>
        /// 构造函数 (body默认为html格式)
        /// </summary>
        /// <param name="toList">收件人列表</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件正文</param>
        public EmailHelper(string toList, string subject, string body)
        {
            this.ToList = toList;
            this.Subject = subject;
            this.Body = body;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="toList">收件人列表</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="isBodyHtml">邮件正文是否为Html格式</param>
        /// <param name="body">邮件正文</param>
        public EmailHelper(string toList, string subject, bool isBodyHtml, string body)
        {
            this.ToList = toList;
            this.Subject = subject;
            this.IsBodyHtml = isBodyHtml;
            this.Body = body;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="toList">收件人列表</param>
        /// <param name="ccList">抄送人列表</param>
        /// <param name="bccList">密送人列表</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="isBodyHtml">邮件正文是否为Html格式</param>
        /// <param name="body">邮件正文</param>
        public EmailHelper(string toList, string ccList, string bccList, string subject, bool isBodyHtml, string body)
        {
            this.ToList = toList;
            this.CCList = ccList;
            this.BccList = bccList;
            this.Subject = subject;
            this.IsBodyHtml = isBodyHtml;
            this.Body = body;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="toList">收件人列表</param>
        /// <param name="ccList">抄送人列表</param>
        /// <param name="bccList">密送人列表</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="isBodyHtml">邮件正文是否为Html格式</param>
        /// <param name="body">邮件正文</param>
        /// <param name="attachmentList">附件列表</param>
        public EmailHelper(string toList, string ccList, string bccList, string subject, bool isBodyHtml, string body, List<Attachment> attachmentList)
        {
            this.ToList = toList;
            this.CCList = ccList;
            this.BccList = bccList;
            this.Subject = subject;
            this.IsBodyHtml = isBodyHtml;
            this.Body = body;
            this.AttachmentList = attachmentList;
        }
        #endregion

        #region [ 发送邮件 ]
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <returns></returns>
        public void Send()
        {
            SmtpClient smtp = new SmtpClient();                 //实例化一个SmtpClient
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;   //将smtp的出站方式设为 Network
            smtp.EnableSsl = false;                             //smtp服务器是否启用SSL加密
            smtp.Host = this.SmtpHost;                          //指定 smtp 服务器地址
            smtp.Port = this.SmtpPort;                          //指定 smtp 服务器的端口，默认是25，如果采用默认端口，可省去
            smtp.UseDefaultCredentials = true;                  //如果你的SMTP服务器不需要身份认证，则使用下面的方式，不过，目前基本没有不需要认证的了
            smtp.Credentials = new NetworkCredential(this.FromEmailAddress, this.FormEmailPassword);    //如果需要认证，则用下面的方式

            MailMessage mm = new MailMessage(); //实例化一个邮件类
            mm.Priority = MailPriority.Normal; //邮件的优先级，分为 Low, Normal, High，通常用 Normal即可
            mm.From = new MailAddress(this.FromEmailAddress, "管理员", Encoding.GetEncoding(936));

            //收件人
            if (!string.IsNullOrEmpty(this.ToList))
                mm.To.Add(this.ToList);
            //抄送人
            if (!string.IsNullOrEmpty(this.CCList))
                mm.CC.Add(this.CCList);
            //密送人
            if (!string.IsNullOrEmpty(this.BccList))
                mm.Bcc.Add(this.BccList);

            mm.Subject = this.Subject;                      //邮件标题
            mm.SubjectEncoding = Encoding.GetEncoding(936); //这里非常重要，如果你的邮件标题包含中文，这里一定要指定，否则对方收到的极有可能是乱码。
            mm.IsBodyHtml = this.IsBodyHtml;                //邮件正文是否是HTML格式
            mm.BodyEncoding = Encoding.GetEncoding(936);    //邮件正文的编码， 设置不正确， 接收者会收到乱码
            mm.Body = this.Body;                            //邮件正文
            //邮件附件
            if (this.AttachmentList != null && this.AttachmentList.Count > 0)
            {
                foreach (Attachment attachment in this.AttachmentList)
                {
                    mm.Attachments.Add(attachment);
                }
            }
            //发送邮件，如果不返回异常， 则大功告成了。
            smtp.Send(mm);
        }
        #endregion
    }
}
