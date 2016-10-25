namespace ViedaSlimnicaProject.Migrations.PacientsMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialPacients : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pacients", "Epasts", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.Pacients", "TNumurs", c => c.String(nullable: false, maxLength: 8));
            AlterColumn("dbo.Pacients", "Vards", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Pacients", "Uzvards", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Pacients", "PersKods", c => c.String(nullable: false, maxLength: 12));
            AlterColumn("dbo.Pacients", "Nodala", c => c.String(nullable: false, maxLength: 30));
            DropColumn("dbo.Pacients", "Simptomi");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pacients", "Simptomi", c => c.String(maxLength: 50));
            AlterColumn("dbo.Pacients", "Nodala", c => c.String(maxLength: 50));
            AlterColumn("dbo.Pacients", "PersKods", c => c.String(maxLength: 12));
            AlterColumn("dbo.Pacients", "Uzvards", c => c.String(maxLength: 50));
            AlterColumn("dbo.Pacients", "Vards", c => c.String(maxLength: 50));
            DropColumn("dbo.Pacients", "TNumurs");
            DropColumn("dbo.Pacients", "Epasts");
        }
    }
}
