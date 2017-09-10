using DDit.Core.Data.Entity.FormEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository.Mapping.FormMapping
{
    public class ElementInfoMapping : EntityTypeConfiguration<ElementInfo>
    {
        public ElementInfoMapping() {

            HasKey(a => a.ElementId);
            ToTable("ElementInfo", "Form");

            Property(a => a.ElementText).HasColumnName("ElementText");
            Property(a => a.ElementIoc).HasColumnName("ElementIoc");
            Property(a => a.ElementType).HasColumnName("ElementType");

        }
    }
}
