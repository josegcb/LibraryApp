namespace LibraryAp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agencias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(nullable: false),
                        Codigo = c.String(nullable: false, maxLength: 5, unicode: false),
                        Nombre = c.String(nullable: false, maxLength: 100, unicode: false),
                        Direccion = c.String(maxLength: 250, unicode: false),
                        Telefono = c.String(maxLength: 50, unicode: false),
                        StatusRegistro = c.Int(nullable: false),
                        ComisionPor = c.Int(nullable: false),
                        PorcentajePorPremio = c.Decimal(nullable: false, precision: 18, scale: 3),
                        Comentario = c.String(maxLength: 300, unicode: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpTenants", t => t.TenantId, cascadeDelete: true)
                .Index(t => new { t.TenantId, t.Codigo }, unique: true, name: "U_Agencia");
            
            CreateTable(
                "dbo.Cajas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AgenciaId = c.Int(nullable: false),
                        Codigo = c.String(nullable: false, maxLength: 5, unicode: false),
                        Descripcion = c.String(nullable: false, maxLength: 100, unicode: false),
                        StatusRegistro = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Agencias", t => t.AgenciaId, cascadeDelete: true)
                .Index(t => new { t.AgenciaId, t.Codigo }, unique: true, name: "U_Caja");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cajas", "AgenciaId", "dbo.Agencias");
            DropForeignKey("dbo.Agencias", "TenantId", "dbo.AbpTenants");
            DropIndex("dbo.Cajas", "U_Caja");
            DropIndex("dbo.Agencias", "U_Agencia");
            DropTable("dbo.Cajas");
            DropTable("dbo.Agencias");
        }
    }
}
