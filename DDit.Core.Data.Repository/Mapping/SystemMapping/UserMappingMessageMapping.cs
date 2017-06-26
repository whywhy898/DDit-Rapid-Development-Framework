using DDit.Core.Data.Entity.SystemEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository.Mapping.SystemMapping
{
   public class UserMappingMessageMapping : EntityTypeConfiguration<UserMappingMessage>
    {
       public UserMappingMessageMapping() {

           HasKey(k => k.ID);
           ToTable("USER_MESSAGE", "Base");
           this.Property(a => a.ID).HasColumnName("ID");
           this.Property(a => a.MessageID).HasColumnName("MESSAGEID");
           this.Property(a => a.UserID).HasColumnName("USERID");
           this.Property(a => a.IsRead).HasColumnName("ISREAD");

           HasRequired(a => a.UserInfo).WithMany(w => w.MessageList).HasForeignKey(m => m.UserID);

           HasRequired(a => a.MessageInfo).WithMany().HasForeignKey(m => m.MessageID);

       }
    }
}
