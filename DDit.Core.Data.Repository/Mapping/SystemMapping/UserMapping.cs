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

            this.ToTable("User", "Base");
            this.Property(a => a.UserID).HasColumnName("User_ID");
            this.Property(a => a.UserPassword).HasColumnName("User_Password");
            this.Property(a => a.UserName).HasColumnName("User_Name");
            this.Property(a => a.UserReallyname).HasColumnName("User_Reallyname");
            this.Property(a => a.DepartmentID).HasColumnName("Department_ID");
            this.Property(a => a.CreateTime).HasColumnName("Create_Time");
            this.Property(a => a.UpdateTime).HasColumnName("Update_Time");
            this.Property(a => a.Remark).HasColumnName("Remark");
            this.Property(a => a.IsEnable).HasColumnName("IsEnable");
            this.Property(a => a.HeadPortrait).HasColumnName("HeadPortrait");

            HasMany(u => u.RoleList)
                .WithMany(p => p.UserList)
                .Map(m => {
                   m.ToTable("User_Role","Base");
                   m.MapLeftKey("User_ID");
                   m.MapRightKey("Role_ID");
            });
   
        }
    } 
}
