namespace ViedaSlimnicaProject.Migrations.PalataMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Palatas",
                c => new
                    {
                        PalatasID = c.Int(nullable: false, identity: true),
                        Nodala = c.String(maxLength: 50),
                        Stavs = c.Decimal(precision: 18, scale: 2),
                        PalatasIetilpiba = c.Decimal(precision: 18, scale: 2),
                        GultasNr = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PalatasID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Palatas");
        }
    }
}
