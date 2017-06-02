using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration;
using DDit.Core.Data.Entity;
using DDit.Core.Data.SystemEntity.Entity;

namespace DDit.Core.Data.Repository.Mapping.SystemMapping
{
    public class UserMapping : EntityTypeConfiguration<User>
    {
        public UserMapping() {

            this.HasKey(a => a.UserID);

            this.ToTable("USERINFOMATION", "Base");
            this.Property(a => a.UserID).HasColumnName("USER_ID");
            this.Property(a => a.UserPassword).HasColumnName("USER_PASSWORD");
            this.Property(a => a.UserName).HasColumnName("USER_NAME");
            this.Property(a => a.UserReallyname).HasColumnName("USER_REALLYNAME");
            this.Property(a => a.DepartmentID).HasColumnName("DEPARTMENT_ID");
            this.Property(a => a.CreateTime).HasColumnName("CREATE_TIME");
            this.Property(a => a.UpdateTime).HasColumnName("UPDATE_TIME");
            this.Property(a => a.Remark).HasColumnName("REMARK");
            this.Property(a => a.IsEnable).HasColumnName("ISENABLE");
            this.Property(a => a.HeadPortrait).HasColumnName("HEADPORTRAIT");

            HasMany(u => u.RoleList)
                .WithMany(p => p.UserList)
                .Map(m => {
                    m.ToTable("USER_ROLE", "Base");
                   m.MapLeftKey("USER_ID");
                   m.MapRightKey("ROLE_ID");
            });
   
        }
    } 
}
