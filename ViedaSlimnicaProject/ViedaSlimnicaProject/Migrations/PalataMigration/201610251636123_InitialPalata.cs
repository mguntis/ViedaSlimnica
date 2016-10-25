namespace ViedaSlimnicaProject.Migrations.PalataMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialPalata : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Palatas", "Nodala", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Palatas", "Stavs", c => c.Int(nullable: false));
            AlterColumn("dbo.Palatas", "PalatasIetilpiba", c => c.Int(nullable: false));
            AlterColumn("dbo.Palatas", "GultasNr", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Palatas", "GultasNr", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Palatas", "PalatasIetilpiba", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Palatas", "Stavs", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Palatas", "Nodala", c => c.String(maxLength: 50));
        }
    }
}
