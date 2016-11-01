namespace ViedaSlimnicaProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedClasses : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Pacients", "Nodala");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pacients", "Nodala", c => c.String(nullable: false, maxLength: 30));
        }
    }
}
