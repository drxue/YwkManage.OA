using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using YwkManage.OA.Model.Mapping;
using YwkManage.OA.Model.ModelClass;
using System.Data.Entity.ModelConfiguration;

namespace YwkManage.OA.Model
{
  public  class OAContext: DbContext
    {
        public OAContext():base("name=OAContext")
        {
            //没有数据库时创建数据库
            Database.SetInitializer<OAContext>(new CreateDatabaseIfNotExists<OAContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //设置,取消创建数据库表示使用复数表名称
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //设置模型类的mapping文件，Fruent API设置
            modelBuilder.Configurations.Add(new ActionInfoMapping());
            modelBuilder.Configurations.Add(new ContactMapping());
            modelBuilder.Configurations.Add(new DepartmentMapping());
            modelBuilder.Configurations.Add(new DoctorRegisterMapping());
            modelBuilder.Configurations.Add(new EmployeeMapping());
            modelBuilder.Configurations.Add(new LeaveMapping());
            modelBuilder.Configurations.Add(new ProjectClassifyMapping());
            modelBuilder.Configurations.Add(new RoleInfoMapping());
            modelBuilder.Configurations.Add(new TitleAwardMapping());
            modelBuilder.Configurations.Add(new TitleLevelInfoMapping());
            modelBuilder.Configurations.Add(new UserInfoMapping());
            modelBuilder.Configurations.Add(new WardMapping());
            modelBuilder.Configurations.Add(new DepartmentAttributeMapping());
        }
        //注册模型类
        public virtual DbSet<ActionInfo> ActionInfo { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<DoctorRegister> DoctorRegister { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Leave> Leave { get; set; }
        public virtual DbSet<ProjectClassify> ProjectClassify { get; set; }
        public virtual DbSet<RoleInfo> RoleInfo { get; set; }
        public virtual DbSet<TitleAward> TitleAward { get; set; }
        public virtual DbSet<TitleLevelInfo> TitleLevelInfo { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<Ward> Ward { get; set; }
        public virtual DbSet<DepartmentAttribute> DepartmentAttribute { get; set; }


    }
}
