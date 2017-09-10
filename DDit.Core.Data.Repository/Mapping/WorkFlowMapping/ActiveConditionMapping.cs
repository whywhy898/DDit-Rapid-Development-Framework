using DDit.Core.Data.Entity.WorkFlowEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository.Mapping.WorkFlowMapping
{
   public class ActiveConditionMapping : EntityTypeConfiguration<ActiveCondition>
    {
       public ActiveConditionMapping() {

           HasKey(a => a.ConditionId);
           ToTable("ACTIVECONDITION", "FLOW");

           this.Property(a => a.ConditionId).HasColumnName("ConditionId");
           this.Property(a => a.ActiveId).HasColumnName("FlowId");
           this.Property(a => a.ActiveId).HasColumnName("ActiveId");
           this.Property(a => a.Index).HasColumnName("ConditionIndex");

           this.Property(a => a.Field).HasColumnName("Field");
           this.Property(a => a.Compare).HasColumnName("Compare");
           this.Property(a => a.CompareValue).HasColumnName("CompareValue");
           this.Property(a => a.Logic).HasColumnName("Logic");
           this.Property(a => a.Group).HasColumnName("Combine");
       
       }
    }
}
