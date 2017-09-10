
using DDit.Core.Data.Entity.SystemEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository.Mapping.SystemMapping
{
    public class LoginLogMapping : EntityTypeConfiguration<LoginLog>
    {
        public LoginLogMapping() {

            HasKey(a => a.LoginID);
            ToTable("LOGINLOG", "BASE");
            this.Property(a => a.LoginID).HasColumnName("LOGIN_ID");
            this.Property(a => a.LoginIP).HasColumnName("LOGIN_IP");
            this.Property(a => a.LoginName).HasColumnName("LOGIN_NAME");
            this.Property(a => a.LoginNicker).HasColumnName("LOGIN_NICKER");
            this.Property(a => a.LoginTime).HasColumnName("LOGIN_TIME");
        }

    }
}
