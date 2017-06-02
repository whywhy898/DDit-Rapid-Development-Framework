
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
           this.ToTable("ROLE", "Base");
           this.Property(a => a.RoleID).HasColumnName("ROLE_ID");
           this.Property(a => a.RoleName).HasColumnName("ROLE_NAME");
           this.Property(a => a.RoleDescription).HasColumnName("ROLE_DESCRIPTION");
           this.Property(a => a.CreateTime).HasColumnName("CREATE_TIME");
           this.Property(a => a.UpdateTime).HasColumnName("UPDATE_TIME");


           HasMany(a => a.MenuList).WithMany(a => a.RoleList).Map(m =>
           {
               m.ToTable("ROLE_MENU", "Base");
               m.MapLeftKey("ROLE_ID");
               m.MapRightKey("MENU_ID");
           });

       }
    }
}
