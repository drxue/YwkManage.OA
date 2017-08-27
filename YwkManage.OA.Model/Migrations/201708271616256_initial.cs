namespace YwkManage.OA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActionInfo",
                c => new
                    {
                        ActionInfoID = c.Int(nullable: false, identity: true),
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
                .PrimaryKey(t => t.ActionInfoID);
            
            CreateTable(
                "dbo.RoleInfo",
                c => new
                    {
                        RoleInfoID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        DelFlag = c.Short(nullable: false),
                        SubTime = c.DateTime(nullable: false),
                        Remark = c.String(),
                        ModifiedOn = c.DateTime(nullable: false),
                        Sort = c.String(),
                    })
                .PrimaryKey(t => t.RoleInfoID);
            
            CreateTable(
                "dbo.UserInfo",
                c => new
                    {
                        UserInfoID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        UserPwd = c.String(),
                        SubTime = c.DateTime(nullable: false),
                        DelFlag = c.Short(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        Remark = c.String(),
                        Sort = c.String(),
                    })
                .PrimaryKey(t => t.UserInfoID);
            
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        ContactID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.String(),
                        Name = c.String(),
                        Gender = c.String(),
                        Telephone = c.String(),
                        WorkPhone = c.String(),
                        MobilePhone = c.String(),
                        ShortPhone66 = c.String(),
                        ShortPhone77 = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        Comment = c.String(),
                        DepartmentID = c.Int(),
                    })
                .PrimaryKey(t => t.ContactID)
                .ForeignKey("dbo.Department", t => t.DepartmentID)
                .Index(t => t.DepartmentID);
            
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
                        AdministractiveFlag = c.Boolean(),
                        Comment = c.String(),
                        DepartmentDeputyDirectorID = c.String(maxLength: 128),
                        DepartmentDirectorID = c.String(maxLength: 128),
                        HeadNurseID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DepartmenID)
                .ForeignKey("dbo.Employee", t => t.DepartmentDeputyDirectorID)
                .ForeignKey("dbo.Employee", t => t.DepartmentDirectorID)
                .ForeignKey("dbo.Employee", t => t.HeadNurseID)
                .Index(t => t.DepartmentDeputyDirectorID)
                .Index(t => t.DepartmentDirectorID)
                .Index(t => t.HeadNurseID);
            
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
                        HireDate = c.DateTime(),
                        Notes = c.String(),
                        Title_TitleID = c.Int(),
                        DepartmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("dbo.Department", t => t.DepartmentID, cascadeDelete: true)
                .Index(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Leave",
                c => new
                    {
                        LeaveID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.String(nullable: false, maxLength: 128),
                        DepartmentID = c.Int(nullable: false),
                        ProjectClassifyID = c.Int(),
                        ProjectName = c.String(),
                        Destination = c.String(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.LeaveID)
                .ForeignKey("dbo.Department", t => t.DepartmentID)
                .ForeignKey("dbo.Employee", t => t.EmployeeID, cascadeDelete: true)
                .Index(t => t.EmployeeID)
                .Index(t => t.DepartmentID);
            
            CreateTable(
                "dbo.TitleAward",
                c => new
                    {
                        TitleAwardID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.String(nullable: false, maxLength: 128),
                        TitleLevelInfoID = c.Int(nullable: false),
                        AwardDate = c.DateTime(),
                        AwardHospital = c.String(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.TitleAwardID)
                .ForeignKey("dbo.Employee", t => t.EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.TitleLevelInfo", t => t.TitleLevelInfoID, cascadeDelete: true)
                .Index(t => t.EmployeeID)
                .Index(t => t.TitleLevelInfoID);
            
            CreateTable(
                "dbo.TitleLevelInfo",
                c => new
                    {
                        TitleLevelInfoID = c.Int(nullable: false, identity: true),
                        TitleName = c.String(),
                        TitleLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TitleLevelInfoID);
            
            CreateTable(
                "dbo.DoctorRegister",
                c => new
                    {
                        DoctorRegisterID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.String(),
                        CertifiedStatus = c.String(),
                        Name = c.String(),
                        Gender = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        IDCard = c.String(),
                        QualificationCertificateNo = c.String(),
                        LicensedCertificateNo = c.String(),
                        DoctorGrade = c.String(),
                        LicensedType = c.String(),
                        DoctorTitle = c.String(),
                        LicensedScope = c.String(),
                        ApprovalDate = c.DateTime(),
                        ApprovalOrganization = c.String(),
                        MedicalInstitutions = c.String(),
                        UpdateDate = c.DateTime(),
                        RegisterStatus = c.String(),
                    })
                .PrimaryKey(t => t.DoctorRegisterID);
            
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
                "dbo.Ward",
                c => new
                    {
                        WardID = c.Int(nullable: false, identity: true),
                        WardName = c.String(),
                        HeadNurseID = c.String(),
                        DeputyHeadNurseID = c.String(),
                        BedNumbers = c.Int(),
                        DesignedBedNumbers = c.Int(),
                    })
                .PrimaryKey(t => t.WardID);
            
            CreateTable(
                "dbo.RoleInfoActionInfo",
                c => new
                    {
                        RoleInfoID = c.Int(nullable: false),
                        ActionInfoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleInfoID, t.ActionInfoID })
                .ForeignKey("dbo.RoleInfo", t => t.RoleInfoID, cascadeDelete: true)
                .ForeignKey("dbo.ActionInfo", t => t.ActionInfoID, cascadeDelete: true)
                .Index(t => t.RoleInfoID)
                .Index(t => t.ActionInfoID);
            
            CreateTable(
                "dbo.UserInfoActionInfo",
                c => new
                    {
                        UserInfoID = c.Int(nullable: false),
                        ActionInfoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserInfoID, t.ActionInfoID })
                .ForeignKey("dbo.UserInfo", t => t.UserInfoID, cascadeDelete: true)
                .ForeignKey("dbo.ActionInfo", t => t.ActionInfoID, cascadeDelete: true)
                .Index(t => t.UserInfoID)
                .Index(t => t.ActionInfoID);
            
            CreateTable(
                "dbo.UserInfoRoleInfo",
                c => new
                    {
                        UserInfoID = c.Int(nullable: false),
                        RoleInfoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserInfoID, t.RoleInfoID })
                .ForeignKey("dbo.UserInfo", t => t.UserInfoID, cascadeDelete: true)
                .ForeignKey("dbo.RoleInfo", t => t.RoleInfoID, cascadeDelete: true)
                .Index(t => t.UserInfoID)
                .Index(t => t.RoleInfoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contact", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.Department", "HeadNurseID", "dbo.Employee");
            DropForeignKey("dbo.Department", "DepartmentDirectorID", "dbo.Employee");
            DropForeignKey("dbo.Department", "DepartmentDeputyDirectorID", "dbo.Employee");
            DropForeignKey("dbo.TitleAward", "TitleLevelInfoID", "dbo.TitleLevelInfo");
            DropForeignKey("dbo.TitleAward", "EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.Leave", "EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.Leave", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.Employee", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.UserInfoRoleInfo", "RoleInfoID", "dbo.RoleInfo");
            DropForeignKey("dbo.UserInfoRoleInfo", "UserInfoID", "dbo.UserInfo");
            DropForeignKey("dbo.UserInfoActionInfo", "ActionInfoID", "dbo.ActionInfo");
            DropForeignKey("dbo.UserInfoActionInfo", "UserInfoID", "dbo.UserInfo");
            DropForeignKey("dbo.RoleInfoActionInfo", "ActionInfoID", "dbo.ActionInfo");
            DropForeignKey("dbo.RoleInfoActionInfo", "RoleInfoID", "dbo.RoleInfo");
            DropIndex("dbo.UserInfoRoleInfo", new[] { "RoleInfoID" });
            DropIndex("dbo.UserInfoRoleInfo", new[] { "UserInfoID" });
            DropIndex("dbo.UserInfoActionInfo", new[] { "ActionInfoID" });
            DropIndex("dbo.UserInfoActionInfo", new[] { "UserInfoID" });
            DropIndex("dbo.RoleInfoActionInfo", new[] { "ActionInfoID" });
            DropIndex("dbo.RoleInfoActionInfo", new[] { "RoleInfoID" });
            DropIndex("dbo.TitleAward", new[] { "TitleLevelInfoID" });
            DropIndex("dbo.TitleAward", new[] { "EmployeeID" });
            DropIndex("dbo.Leave", new[] { "DepartmentID" });
            DropIndex("dbo.Leave", new[] { "EmployeeID" });
            DropIndex("dbo.Employee", new[] { "DepartmentID" });
            DropIndex("dbo.Department", new[] { "HeadNurseID" });
            DropIndex("dbo.Department", new[] { "DepartmentDirectorID" });
            DropIndex("dbo.Department", new[] { "DepartmentDeputyDirectorID" });
            DropIndex("dbo.Contact", new[] { "DepartmentID" });
            DropTable("dbo.UserInfoRoleInfo");
            DropTable("dbo.UserInfoActionInfo");
            DropTable("dbo.RoleInfoActionInfo");
            DropTable("dbo.Ward");
            DropTable("dbo.ProjectClassify");
            DropTable("dbo.DoctorRegister");
            DropTable("dbo.TitleLevelInfo");
            DropTable("dbo.TitleAward");
            DropTable("dbo.Leave");
            DropTable("dbo.Employee");
            DropTable("dbo.Department");
            DropTable("dbo.Contact");
            DropTable("dbo.UserInfo");
            DropTable("dbo.RoleInfo");
            DropTable("dbo.ActionInfo");
        }
    }
}
