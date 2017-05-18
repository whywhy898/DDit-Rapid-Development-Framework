
using DDit.Core.Data.SystemEntity.Entity;
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
            ToTable("LoginLog", "Base");
            this.Property(a => a.LoginID).HasColumnName("Login_ID");
            this.Property(a => a.LoginIP).HasColumnName("Login_IP");
            this.Property(a => a.LoginName).HasColumnName("Login_Name");
            this.Property(a => a.LoginNicker).HasColumnName("Login_Nicker");
            this.Property(a => a.LoginTime).HasColumnName("Login_Time");
        }

    }
}
