using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository
{
   public class MysqlDbContext:DbContext
    {
       public MysqlDbContext()
           : base("MysqlDbContext")
        {
              //取消EF的延迟加载
              this.Configuration.ProxyCreationEnabled = false;
              this.Configuration.LazyLoadingEnabled=false;
              Database.SetInitializer<CoreDbContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
             //设置默认架构，oracle设置为空,sqlserver直接注释掉
             modelBuilder.HasDefaultSchema("");
             //移除一对多的级联删除约定，想要级联删除可以在 EntityTypeConfiguration<TEntity>的实现类中进行控制
             modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
             //多对多启用级联删除约定，不想级联删除可以在删除前判断关联的数据进行拦截
             //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

             var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                                  .Where(type => !String.IsNullOrEmpty(type.Namespace))
                                  .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
             foreach (var type in typesToRegister)
             {
                 dynamic configurationInstance = Activator.CreateInstance(type);
                 modelBuilder.Configurations.Add(configurationInstance);
             }
        }
    }
}
