namespace ViedaSlimnicaProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pacients",
                c => new
                    {
                        PacientaID = c.Int(nullable: false, identity: true),
                        Vards = c.String(nullable: false, maxLength: 30),
                        Uzvards = c.String(nullable: false, maxLength: 30),
                        PersKods = c.String(nullable: false, maxLength: 12),
                        Epasts = c.String(nullable: false, maxLength: 20),
                        TNumurs = c.String(nullable: false, maxLength: 8),
                        //Nodala = c.String(nullable: false, maxLength: 30),
                        IerasanasDatums = c.DateTime(nullable: false),
                        Palata_PalatasID = c.Int(),
                    })
                .PrimaryKey(t => t.PacientaID)
                .ForeignKey("dbo.Palatas", t => t.Palata_PalatasID)
                .Index(t => t.Palata_PalatasID);
            
            CreateTable(
                "dbo.Palatas",
                c => new
                    {
                        PalatasID = c.Int(nullable: false, identity: true),
                        Nodala = c.String(nullable: false, maxLength: 30),
                        Stavs = c.Int(nullable: false),
                        PalatasIetilpiba = c.Int(nullable: false),
                        GultasNr = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PalatasID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pacients", "Palata_PalatasID", "dbo.Palatas");
            DropIndex("dbo.Pacients", new[] { "Palata_PalatasID" });
            DropTable("dbo.Palatas");
            DropTable("dbo.Pacients");
        }
    }
}
