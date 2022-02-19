namespace Recetario_EF_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comentarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CuerpoDelComentario = c.String(nullable: false, maxLength: 255),
                        FechaDelComentario = c.DateTime(nullable: false),
                        IdPublicacion = c.Int(nullable: false),
                        IdUsuario = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Publicaciones", t => t.IdPublicacion, cascadeDelete: true)
                .Index(t => t.IdPublicacion);
            
            CreateTable(
                "dbo.Publicaciones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FechaDePublicacion = c.DateTime(nullable: false),
                        Descripcion = c.String(),
                        IdReceta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recetas", t => t.IdReceta, cascadeDelete: true)
                .Index(t => t.IdReceta);
            
            CreateTable(
                "dbo.Recetas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false, maxLength: 50),
                        Imagen = c.Binary(),
                        Ingredientes = c.String(nullable: false),
                        Procedimiento = c.String(nullable: false),
                        FechaDeCreacion = c.DateTime(nullable: false),
                        Oculto = c.Boolean(nullable: false),
                        IdUsuario = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Valoraciones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdUsuario = c.Int(nullable: false),
                        IdReceta = c.Int(nullable: false),
                        Valor = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recetas", t => t.IdReceta, cascadeDelete: true)
                .Index(t => t.IdReceta);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false),
                        Contraseña = c.String(nullable: false, maxLength: 80),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RecetaCategorias",
                c => new
                    {
                        IdReceta = c.Int(nullable: false),
                        IdCategoria = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdReceta, t.IdCategoria })
                .ForeignKey("dbo.Recetas", t => t.IdReceta, cascadeDelete: true)
                .ForeignKey("dbo.Categorias", t => t.IdCategoria, cascadeDelete: true)
                .Index(t => t.IdReceta)
                .Index(t => t.IdCategoria);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Publicaciones", "IdReceta", "dbo.Recetas");
            DropForeignKey("dbo.Valoraciones", "IdReceta", "dbo.Recetas");
            DropForeignKey("dbo.RecetaCategorias", "IdCategoria", "dbo.Categorias");
            DropForeignKey("dbo.RecetaCategorias", "IdReceta", "dbo.Recetas");
            DropForeignKey("dbo.Comentarios", "IdPublicacion", "dbo.Publicaciones");
            DropIndex("dbo.RecetaCategorias", new[] { "IdCategoria" });
            DropIndex("dbo.RecetaCategorias", new[] { "IdReceta" });
            DropIndex("dbo.Valoraciones", new[] { "IdReceta" });
            DropIndex("dbo.Publicaciones", new[] { "IdReceta" });
            DropIndex("dbo.Comentarios", new[] { "IdPublicacion" });
            DropTable("dbo.RecetaCategorias");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Valoraciones");
            DropTable("dbo.Recetas");
            DropTable("dbo.Publicaciones");
            DropTable("dbo.Comentarios");
            DropTable("dbo.Categorias");
        }
    }
}
