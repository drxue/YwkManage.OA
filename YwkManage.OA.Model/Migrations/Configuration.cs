namespace YwkManage.OA.Model.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using YwkManage.OA.Model.ModelClass;

    internal sealed class Configuration : DbMigrationsConfiguration<YwkManage.OA.Model.OAContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(YwkManage.OA.Model.OAContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //添加Contacts记录
            context.Contact.AddOrUpdate(e => e.EmployeeID, new Contact
            {
                EmployeeID = "2000004",
                Gender = "男",
               // Department = "医务科",
                Telephone = "",
                WorkPhone = "88931886",
                MobilePhone = "13958760606",
                ShortPhone66 = "660606",
                ShortPhone77 = "770808",
                Email = "abc@yahoo.com",
                Comment = "only for test"

            });
        }
    }
}
