using DDit.Core.Data.Entity.WorkFlowEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository.Mapping.WorkFlowMapping
{
   public class FlowStepMapping : EntityTypeConfiguration<FlowStep>
    {
       public FlowStepMapping()
       {
           HasKey(a => a.StepId);
           ToTable("FLOWSTEP", "FLOW");

           this.Property(a => a.StepId).HasColumnName("StepId");
           this.Property(a => a.FlowId).HasColumnName("FlowId");
           this.Property(a => a.name).HasColumnName("name");
           this.Property(a => a.left).HasColumnName("Viewleft");
           this.Property(a => a.top).HasColumnName("Viewtop");
           this.Property(a => a.type).HasColumnName("type");
           this.Property(a => a.width).HasColumnName("width");
           this.Property(a => a.height).HasColumnName("height");
           this.Property(a => a.alt).HasColumnName("alt");
           this.Property(a => a.stepUser).HasColumnName("stepUser");
           this.Property(a => a.stepName).HasColumnName("stepName");
           this.Property(a => a.flowNodeName).HasColumnName("flowNodeName");
           this.Property(a => a.remark).HasColumnName("remark");
       }
    }
}
