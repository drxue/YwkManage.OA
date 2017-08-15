namespace YwkManage.OA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inital : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActionInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SubTime = c.DateTime(nullable: false),
                        DelFlag = c.Short(nullable: false),
                        ModifiedOn = c.String(),
                        Remark = c.String(),
                        Url = c.String(),
                        HttpMethod = c.String(),
                        ActionMethodName = c.String(),
                        ControllerName = c.String(),
                        ActionInfoName = c.String(),
                        Sort = c.String(),
                        ActionTypeEnum = c.Short(nullable: false),
                        MenuIcon = c.String(),
                        IconWidth = c.Int(nullable: false),
                        IconHeight = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        CID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.String(),
                        Name = c.String(),
                        Gender = c.String(),
                        Department = c.String(),
                        Telephone = c.String(),
                        WorkPhone = c.String(),
                        MobilePhone = c.String(),
                        ShortPhone66 = c.String(),
                        ShortPhone77 = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.CID);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        DepartmenID = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(),
                        BedNumbers = c.Int(),
                        StatisticsBedNumbers = c.Int(),
                        DepartmentAttributeID = c.Int(nullable: false),
                        InpatientFlag = c.Boolean(),
                        PediatricsFlag = c.Boolean(),
                        OutpatientFlag = c.Boolean(),
                        TechnicalFlag = c.Boolean(),
                        Comment = c.String(),
                        DepartmentDeputyDirectorID = c.String(),
                        DepartmentDirectorID = c.String(),
                        HeadNurseID = c.String(),
                    })
                .PrimaryKey(t => t.DepartmenID);
            
            CreateTable(
                "dbo.Leave",
                c => new
                    {
                        LID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.String(maxLength: 128),
                        DepartmentID = c.Int(nullable: false),
                        ProjectClassifyID = c.Int(),
                        ProjectName = c.String(),
                        Destination = c.String(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        Department_DepartmenID = c.Int(),
                    })
                .PrimaryKey(t => t.LID)
                .ForeignKey("dbo.Department", t => t.Department_DepartmenID)
                .ForeignKey("dbo.Employee", t => t.EmployeeID)
                .ForeignKey("dbo.ProjectClassify", t => t.ProjectClassifyID)
                .Index(t => t.EmployeeID)
                .Index(t => t.ProjectClassifyID)
                .Index(t => t.Department_DepartmenID);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        EmployeeID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Gender = c.String(),
                        BirthDay = c.DateTime(),
                        Age = c.Int(),
                        IDCard = c.String(),
                        Department = c.String(),
                        HireDate = c.DateTime(),
                        Notes = c.String(),
                        Title_TitleID = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeID);
            
            CreateTable(
                "dbo.TitleAward",
                c => new
                    {
                        TitleAwardID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.String(maxLength: 128),
                        TitleID = c.Int(nullable: false),
                        AwardDate = c.DateTime(),
                        AwardHospital = c.String(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.TitleAwardID)
                .ForeignKey("dbo.Employee", t => t.EmployeeID)
                .ForeignKey("dbo.TitleLevelInfo", t => t.TitleID, cascadeDelete: true)
                .Index(t => t.EmployeeID)
                .Index(t => t.TitleID);
            
            CreateTable(
                "dbo.TitleLevelInfo",
                c => new
                    {
                        TitleID = c.Int(nullable: false, identity: true),
                        TitleName = c.String(),
                        TitleLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TitleID);
            
            CreateTable(
                "dbo.Ward",
                c => new
                    {
                        WardID = c.Int(nullable: false, identity: true),
                        WardName = c.String(),
                        HeadNurseID = c.String(),
                        DeputyHeadNurseID = c.String(),
                        BedNumbers = c.Int(),
                        DesignedBedNumbers = c.Int(),
                        DeputyHeadNurse_EmployeeID = c.String(maxLength: 128),
                        HeadNurse_EmployeeID = c.String(maxLength: 128),
                        Employee_EmployeeID = c.String(maxLength: 128),
                        Employee_EmployeeID1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.WardID)
                .ForeignKey("dbo.Employee", t => t.DeputyHeadNurse_EmployeeID)
                .ForeignKey("dbo.Employee", t => t.HeadNurse_EmployeeID)
                .ForeignKey("dbo.Employee", t => t.Employee_EmployeeID)
                .ForeignKey("dbo.Employee", t => t.Employee_EmployeeID1)
                .Index(t => t.DeputyHeadNurse_EmployeeID)
                .Index(t => t.HeadNurse_EmployeeID)
                .Index(t => t.Employee_EmployeeID)
                .Index(t => t.Employee_EmployeeID1);
            
            CreateTable(
                "dbo.ProjectClassify",
                c => new
                    {
                        ProjectClassifyID = c.Int(nullable: false, identity: true),
                        ProjectClassifyName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ProjectClassifyID);
            
            CreateTable(
                "dbo.DoctorRegister",
                c => new
                    {
                        DID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.String(maxLength: 10),
                        CertifiedStatus = c.String(nullable: false, maxLength: 10),
                        Name = c.String(nullable: false, maxLength: 10),
                        Gender = c.String(maxLength: 1),
                        Birthday = c.DateTime(nullable: false),
                        IDCard = c.String(nullable: false, maxLength: 17),
                        QualificationCertificateNo = c.String(nullable: false, maxLength: 27),
                        LicensedCertificateNo = c.String(nullable: false, maxLength: 15),
                        DoctorGrade = c.String(nullable: false, maxLength: 6),
                        LicensedType = c.String(nullable: false, maxLength: 10),
                        DoctorTitle = c.String(maxLength: 50),
                        LicensedScope = c.String(nullable: false, maxLength: 10),
                        ApprovalDate = c.DateTime(),
                        ApprovalOrganization = c.String(maxLength: 50),
                        MedicalInstitutions = c.String(maxLength: 50),
                        UpdateDate = c.DateTime(),
                        RegisterStatus = c.String(),
                    })
                .PrimaryKey(t => t.DID);
            
            CreateTable(
                "dbo.RoleInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        DelFlag = c.Short(nullable: false),
                        SubTime = c.DateTime(nullable: false),
                        Remark = c.String(),
                        ModifiedOn = c.DateTime(nullable: false),
                        Sort = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        UserPwd = c.String(),
                        SubTime = c.DateTime(nullable: false),
                        DelFlag = c.Short(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        Remark = c.String(),
                        Sort = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.WardDepartment",
                c => new
                    {
                        Ward_WardID = c.Int(nullable: false),
                        Department_DepartmenID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Ward_WardID, t.Department_DepartmenID })
                .ForeignKey("dbo.Ward", t => t.Ward_WardID, cascadeDelete: true)
                .ForeignKey("dbo.Department", t => t.Department_DepartmenID, cascadeDelete: true)
                .Index(t => t.Ward_WardID)
                .Index(t => t.Department_DepartmenID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leave", "ProjectClassifyID", "dbo.ProjectClassify");
            DropForeignKey("dbo.Ward", "Employee_EmployeeID1", "dbo.Employee");
            DropForeignKey("dbo.Ward", "Employee_EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.Ward", "HeadNurse_EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.Ward", "DeputyHeadNurse_EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.WardDepartment", "Department_DepartmenID", "dbo.Department");
            DropForeignKey("dbo.WardDepartment", "Ward_WardID", "dbo.Ward");
            DropForeignKey("dbo.TitleAward", "TitleID", "dbo.TitleLevelInfo");
            DropForeignKey("dbo.TitleAward", "EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.Leave", "EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.Leave", "Department_DepartmenID", "dbo.Department");
            DropIndex("dbo.WardDepartment", new[] { "Department_DepartmenID" });
            DropIndex("dbo.WardDepartment", new[] { "Ward_WardID" });
            DropIndex("dbo.Ward", new[] { "Employee_EmployeeID1" });
            DropIndex("dbo.Ward", new[] { "Employee_EmployeeID" });
            DropIndex("dbo.Ward", new[] { "HeadNurse_EmployeeID" });
            DropIndex("dbo.Ward", new[] { "DeputyHeadNurse_EmployeeID" });
            DropIndex("dbo.TitleAward", new[] { "TitleID" });
            DropIndex("dbo.TitleAward", new[] { "EmployeeID" });
            DropIndex("dbo.Leave", new[] { "Department_DepartmenID" });
            DropIndex("dbo.Leave", new[] { "ProjectClassifyID" });
            DropIndex("dbo.Leave", new[] { "EmployeeID" });
            DropTable("dbo.WardDepartment");
            DropTable("dbo.UserInfo");
            DropTable("dbo.RoleInfo");
            DropTable("dbo.DoctorRegister");
            DropTable("dbo.ProjectClassify");
            DropTable("dbo.Ward");
            DropTable("dbo.TitleLevelInfo");
            DropTable("dbo.TitleAward");
            DropTable("dbo.Employee");
            DropTable("dbo.Leave");
            DropTable("dbo.Department");
            DropTable("dbo.Contact");
            DropTable("dbo.ActionInfo");
        }
    }
}
