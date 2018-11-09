namespace LibraryAp.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class HAVE2 : DbMigration
    {
        public override void Up()
        {
            AlterTableAnnotations(
                "dbo.Cajas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AgenciaId = c.Int(nullable: false),
                        Codigo = c.String(nullable: false, maxLength: 5, unicode: false),
                        Descripcion = c.String(nullable: false, maxLength: 100, unicode: false),
                        StatusRegistro = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Caja_IMustHaveAgency",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
        }
        
        public override void Down()
        {
            AlterTableAnnotations(
                "dbo.Cajas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AgenciaId = c.Int(nullable: false),
                        Codigo = c.String(nullable: false, maxLength: 5, unicode: false),
                        Descripcion = c.String(nullable: false, maxLength: 100, unicode: false),
                        StatusRegistro = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Caja_IMustHaveAgency",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
        }
    }
}
