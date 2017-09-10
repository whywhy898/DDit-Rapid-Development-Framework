using DDit.Core.Data.Entity.SystemEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository.Mapping.SystemMapping
{
   public class MassageMapping : EntityTypeConfiguration<Message>
    {
       public MassageMapping() {

           HasKey(k => k.MessageID);
           ToTable("MESSAGE", "BASE");

           this.Property(a => a.MessageID).HasColumnName("MESSAGEID");
           this.Property(a => a.MessageTitle).HasColumnName("MESSAGETITLE");
           this.Property(a => a.MessageText).HasColumnName("MESSAGETEXT");
           this.Property(a => a.SendUser).HasColumnName("SENDUSER");
           this.Property(a => a.RecUser).HasColumnName("RECUSER"); 
           this.Property(a => a.SendTime).HasColumnName("SENDTIME");
           this.Property(a => a.ExpirationTime).HasColumnName("EXPIRATIONTIME");
           this.Property(a => a.IsSendEmail).HasColumnName("ISSENDEMAIL");

           this.Property(a => a.SendEmailState).HasColumnName("SENDEMAILSTATE");

           HasRequired(u => u.SendUserInfo).WithMany(m => m.Messages).HasForeignKey(m => m.SendUser);
           
       }
    }
}
