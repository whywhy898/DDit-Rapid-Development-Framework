
using DDit.Core.Data.SystemEntity.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository.Mapping.SystemMapping
{
    public class MenuMapping : EntityTypeConfiguration<Menu>
    {
        public MenuMapping() {

            HasKey(a=>a.MenuID);

            this.ToTable("Menu","Base");

            this.Property(a => a.MenuID).HasColumnName("Menu_ID");
            this.Property(a => a.MenuName).HasColumnName("Menu_Name");
            this.Property(a => a.MenuOrder).HasColumnName("Menu_Order");
            this.Property(a => a.MenuParentID).HasColumnName("Menu_ParentID");
            this.Property(a => a.MenuUrl).HasColumnName("Menu_Url");
            this.Property(a => a.MenuIcon).HasColumnName("Menu_Icon");
            this.Property(a => a.IsVisible).HasColumnName("IsVisible");
            this.Property(a => a.CreateTime).HasColumnName("Create_Time");
            this.Property(a => a.UpdateTime).HasColumnName("Update_Time");

            HasMany(t => t.Childs)
                        .WithOptional(t => t.Father)
                        .HasForeignKey(t => t.MenuParentID);
        }

    }
}
