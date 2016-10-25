namespace ViedaSlimnicaProject.Migrations.PacientsMigration
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
                        Vards = c.String(maxLength: 50),
                        Uzvards = c.String(maxLength: 50),
                        PersKods = c.String(maxLength: 12),
                        Simptomi = c.String(maxLength: 50),
                        Nodala = c.String(maxLength: 50),
                        PalatasID = c.Int(nullable: false),
                        IerasanasDatums = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PacientaID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Pacients");
        }
    }
}
