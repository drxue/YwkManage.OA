namespace YwkManage.OA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialB : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Contact");
            DropPrimaryKey("dbo.DoctorRegister");
            DropPrimaryKey("dbo.Leave");
            DropPrimaryKey("dbo.TitleLevelInfo");
            DropColumn("dbo.Contact", "CID");
            DropColumn("dbo.DoctorRegister", "DID");
            DropColumn("dbo.Leave", "LID");
            DropColumn("dbo.TitleLevelInfo", "TitleID");
            AddColumn("dbo.Contact", "ContactID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.DoctorRegister", "DoctorRegisterID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Leave", "LeaveID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.TitleLevelInfo", "TitleLevelInfoID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Contact", "ContactID");
            AddPrimaryKey("dbo.DoctorRegister", "DoctorRegisterID");
            AddPrimaryKey("dbo.Leave", "LeaveID");
            AddPrimaryKey("dbo.TitleLevelInfo", "TitleLevelInfoID");
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.TitleLevelInfo", "TitleID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Leave", "LID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.DoctorRegister", "DID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Contact", "CID", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.TitleLevelInfo");
            DropPrimaryKey("dbo.Leave");
            DropPrimaryKey("dbo.DoctorRegister");
            DropPrimaryKey("dbo.Contact");
            DropColumn("dbo.TitleLevelInfo", "TitleLevelInfoID");
            DropColumn("dbo.Leave", "LeaveID");
            DropColumn("dbo.DoctorRegister", "DoctorRegisterID");
            DropColumn("dbo.Contact", "ContactID");
            AddPrimaryKey("dbo.TitleLevelInfo", "TitleID");
            AddPrimaryKey("dbo.Leave", "LID");
            AddPrimaryKey("dbo.DoctorRegister", "DID");
            AddPrimaryKey("dbo.Contact", "CID");
        }
    }
}
