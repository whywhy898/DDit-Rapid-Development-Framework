
using DDit.Core.Data.Entity.SystemEntity;
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
            ToTable("DICTIONARY", "Base");
            this.Property(a => a.ID).HasColumnName("ID");
            this.Property(a => a.DicCategoryID).HasColumnName("DICCATEGORYID");
            this.Property(a => a.DicValue).HasColumnName("DICVALUE");
            this.Property(a => a.Enabled).HasColumnName("ENABLED");
            this.Property(a => a.CreateTime).HasColumnName("CREATE_TIME");
            this.Property(a => a.UpdateTime).HasColumnName("UPDATE_TIME");

            this.HasRequired(t => t.DicCategory)
               .WithMany(t => t.DicValueList)
               .HasForeignKey(t => t.DicCategoryID)
               .WillCascadeOnDelete(false);
        }

    }
}
