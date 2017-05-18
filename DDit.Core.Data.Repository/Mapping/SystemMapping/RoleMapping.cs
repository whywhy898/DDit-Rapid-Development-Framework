
using DDit.Core.Data.SystemEntity.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository.Mapping.SystemMapping
{
   public class RoleMapping : EntityTypeConfiguration<Role>
    {
       public RoleMapping() {

           HasKey(a => a.RoleID);
           this.ToTable("Role", "Base");
           this.Property(a => a.RoleID).HasColumnName("Role_ID");
           this.Property(a => a.RoleName).HasColumnName("Role_Name");
           this.Property(a => a.RoleDescription).HasColumnName("Role_Description");
           this.Property(a => a.CreateTime).HasColumnName("Create_Time");
           this.Property(a => a.UpdateTime).HasColumnName("Update_Time");


           HasMany(a => a.MenuList).WithMany(a => a.RoleList).Map(m =>
           {
               m.ToTable("Role_Menu", "Base");
               m.MapLeftKey("Role_ID");
               m.MapRightKey("Menu_ID");
           });

       }
    }
}
