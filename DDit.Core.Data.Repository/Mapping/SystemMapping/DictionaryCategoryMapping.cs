
using DDit.Core.Data.SystemEntity.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository.Mapping.SystemMapping
{
    public class DictionaryCategoryMapping : EntityTypeConfiguration<DictionaryCategory>
    {
        public DictionaryCategoryMapping() {

            HasKey(a => a.ID);
            ToTable("DictionaryCategory", "Base");
            this.Property(a => a.ID).HasColumnName("ID");
            this.Property(a => a.Category).HasColumnName("Category");
            this.Property(a => a.Enabled).HasColumnName("Enabled");
            this.Property(a => a.CreateTime).HasColumnName("Create_Time");
            this.Property(a => a.UpdateTime).HasColumnName("Update_Time");

           
        }

    }
}
