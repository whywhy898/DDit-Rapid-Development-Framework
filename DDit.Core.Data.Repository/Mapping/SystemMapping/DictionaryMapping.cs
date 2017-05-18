
using DDit.Core.Data.SystemEntity.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository.Mapping.SystemMapping
{
    public class DictionaryMapping : EntityTypeConfiguration<Dictionary>
    {
        public DictionaryMapping()
        {
            HasKey(a => a.ID);
            ToTable("Dictionary", "Base");
            this.Property(a => a.ID).HasColumnName("ID");
            this.Property(a => a.DicCategoryID).HasColumnName("DicCategoryID");
            this.Property(a => a.DicValue).HasColumnName("DicValue");
            this.Property(a => a.Enabled).HasColumnName("Enabled");
            this.Property(a => a.CreateTime).HasColumnName("Create_Time");
            this.Property(a => a.UpdateTime).HasColumnName("Update_Time");

            this.HasRequired(t => t.DicCategory)
               .WithMany(t => t.DicValueList)
               .HasForeignKey(t => t.DicCategoryID)
               .WillCascadeOnDelete(false);
        }

    }
}
