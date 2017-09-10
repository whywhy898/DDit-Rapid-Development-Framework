using DDit.Core.Data.Entity.WorkFlowEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository.Mapping.WorkFlowMapping
{
    public class FlowApproveMapping : EntityTypeConfiguration<FlowApprove>
    {
        public FlowApproveMapping()
        {
            HasKey(a => a.ApproveId);
            ToTable("FLOWAPPROVE", "FLOW");

            this.Property(a => a.ApproveId).HasColumnName("ApproveId");
            this.Property(a => a.FlowInfoId).HasColumnName("FlowInfoId");
            this.Property(a => a.ApproveUser).HasColumnName("ApproveUser");

            this.Property(a => a.FlowStepId).HasColumnName("FlowStepId");
            this.Property(a => a.ApproveTime).HasColumnName("ApproveTime");
            this.Property(a => a.ApproveResult).HasColumnName("ApproveResult");

            this.Property(a => a.ReallyApproveUser).HasColumnName("ReallyApproveUser");
            
            this.Property(a => a.ApproveRemark).HasColumnName("ApproveRemark");

            HasOptional(a => a.ReallyApproveUserInfo).WithMany().HasForeignKey(a => a.ReallyApproveUser);

            HasRequired(a => a.FlowStepInfo).WithMany().HasForeignKey(a => a.FlowStepId);
        
        }
    }
}
