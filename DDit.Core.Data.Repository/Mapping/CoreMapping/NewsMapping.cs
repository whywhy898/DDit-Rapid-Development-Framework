using DDit.Core.Data.Entity.CoreEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository.Mapping.CoreMapping
{
    public class NewsMapping:EntityTypeConfiguration<News>
    {
        public NewsMapping()
        {
            HasKey(A => A.NewId);
            this.ToTable("New", "Test");
            this.Property(a => a.NewId).HasColumnName("NewId");
            this.Property(a => a.NewTitle).HasColumnName("NewTitle");
            this.Property(a => a.NewContent).HasColumnName("NewContent");
            this.Property(a => a.NewAuthor).HasColumnName("NewAuthor");
            this.Property(a => a.CreateTime).HasColumnName("CreateTime");

        }
    }
}
