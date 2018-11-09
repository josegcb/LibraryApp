namespace LibraryAp.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class HAVE : DbMigration
    {
        public override void Up()
        {
            AlterTableAnnotations(
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
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Agencia_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
        }
        
        public override void Down()
        {
            AlterTableAnnotations(
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
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Agencia_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
        }
    }
}
