namespace SolgerySystem2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        narration = c.String(),
                        GrpId = c.Int(nullable: false),
                        amount = c.Int(nullable: false),
                        FromUsrId = c.Int(),
                        ToUsrId = c.Int(),
                        paid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentId)
                .ForeignKey("dbo.Users", t => t.FromUsrId)
                .ForeignKey("dbo.Users", t => t.ToUsrId)
                .ForeignKey("dbo.Groups", t => t.GrpId, cascadeDelete: true)
                .Index(t => t.GrpId)
                .Index(t => t.FromUsrId)
                .Index(t => t.ToUsrId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "GrpId", "dbo.Groups");
            DropForeignKey("dbo.Payments", "ToUsrId", "dbo.Users");
            DropForeignKey("dbo.Payments", "FromUsrId", "dbo.Users");
            DropIndex("dbo.Payments", new[] { "ToUsrId" });
            DropIndex("dbo.Payments", new[] { "FromUsrId" });
            DropIndex("dbo.Payments", new[] { "GrpId" });
            DropTable("dbo.Payments");
        }
    }
}
