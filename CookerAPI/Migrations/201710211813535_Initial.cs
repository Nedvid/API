namespace CookerAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Black_Item",
                c => new
                    {
                        Id_Black_Item = c.Int(nullable: false, identity: true),
                        Id_User = c.Int(nullable: false),
                        Id_Product = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Black_Item)
                .ForeignKey("dbo.UserDetails", t => t.Id_User)
                .ForeignKey("dbo.Products", t => t.Id_Product)
                .Index(t => t.Id_User)
                .Index(t => t.Id_Product);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id_Product = c.Int(nullable: false, identity: true),
                        Name_Product = c.String(),
                        Visible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Product);
            
            CreateTable(
                "dbo.Elements",
                c => new
                    {
                        Id_Element = c.Int(nullable: false, identity: true),
                        Id_Product = c.Int(nullable: false),
                        Id_Recipe = c.Int(nullable: false),
                        Quantity = c.String(),
                    })
                .PrimaryKey(t => t.Id_Element)
                .ForeignKey("dbo.Products", t => t.Id_Product)
                .ForeignKey("dbo.Recipes", t => t.Id_Recipe)
                .Index(t => t.Id_Product)
                .Index(t => t.Id_Recipe);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        Id_Recipe = c.Int(nullable: false, identity: true),
                        Id_User = c.Int(nullable: false),
                        Id_Category_Main = c.Int(nullable: false),
                        Name_Recipe = c.String(),
                        Rate = c.Int(nullable: false),
                        Level = c.String(),
                        Date_Recipe = c.DateTime(nullable: false),
                        URL_Photo = c.String(),
                        Time = c.Int(nullable: false),
                        Number_Person = c.Int(nullable: false),
                        Steps = c.Int(nullable: false),
                        Instruction = c.String(),
                        Visible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Recipe)
                .ForeignKey("dbo.Category_Main", t => t.Id_Category_Main)
                .ForeignKey("dbo.UserDetails", t => t.Id_User)
                .Index(t => t.Id_User)
                .Index(t => t.Id_Category_Main);
            
            CreateTable(
                "dbo.Category_Recipe",
                c => new
                    {
                        Id_Category_Recipe = c.Int(nullable: false, identity: true),
                        Id_Category = c.Int(nullable: false),
                        Id_Recipe = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Category_Recipe)
                .ForeignKey("dbo.Categories", t => t.Id_Category)
                .ForeignKey("dbo.Recipes", t => t.Id_Recipe)
                .Index(t => t.Id_Category)
                .Index(t => t.Id_Recipe);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id_Category_Recipe = c.Int(nullable: false, identity: true),
                        Name_Category_Recipe = c.String(),
                    })
                .PrimaryKey(t => t.Id_Category_Recipe);
            
            CreateTable(
                "dbo.Category_Main",
                c => new
                    {
                        Id_Category_Main = c.Int(nullable: false, identity: true),
                        Name_Category_Main = c.String(),
                    })
                .PrimaryKey(t => t.Id_Category_Main);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id_Comment = c.Int(nullable: false, identity: true),
                        Id_User = c.Int(nullable: false),
                        Id_Recipe = c.Int(nullable: false),
                        Text = c.String(),
                        Date_Comment = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Comment)
                .ForeignKey("dbo.Recipes", t => t.Id_Recipe)
                .ForeignKey("dbo.UserDetails", t => t.Id_User)
                .Index(t => t.Id_User)
                .Index(t => t.Id_Recipe);
            
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        Id_User = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Email = c.String(),
                        Id_List = c.Int(nullable: false),
                        Social_Account = c.Boolean(nullable: false),
                        URL_Avatar = c.String(),
                    })
                .PrimaryKey(t => t.Id_User)
                .ForeignKey("dbo.Lists", t => t.Id_List)
                .Index(t => t.Id_List);
            
            CreateTable(
                "dbo.Lists",
                c => new
                    {
                        Id_List = c.Int(nullable: false, identity: true),
                        Items = c.String(),
                    })
                .PrimaryKey(t => t.Id_List);
            
            CreateTable(
                "dbo.Rates",
                c => new
                    {
                        Id_Rate = c.Int(nullable: false, identity: true),
                        Id_User = c.Int(nullable: false),
                        Id_Recipe = c.Int(nullable: false),
                        Value_Rate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Rate)
                .ForeignKey("dbo.Recipes", t => t.Id_Recipe)
                .ForeignKey("dbo.UserDetails", t => t.Id_User)
                .Index(t => t.Id_User)
                .Index(t => t.Id_Recipe);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Black_Item", "Id_Product", "dbo.Products");
            DropForeignKey("dbo.Elements", "Id_Recipe", "dbo.Recipes");
            DropForeignKey("dbo.Recipes", "Id_User", "dbo.UserDetails");
            DropForeignKey("dbo.Rates", "Id_User", "dbo.UserDetails");
            DropForeignKey("dbo.Rates", "Id_Recipe", "dbo.Recipes");
            DropForeignKey("dbo.UserDetails", "Id_List", "dbo.Lists");
            DropForeignKey("dbo.Comments", "Id_User", "dbo.UserDetails");
            DropForeignKey("dbo.Black_Item", "Id_User", "dbo.UserDetails");
            DropForeignKey("dbo.Comments", "Id_Recipe", "dbo.Recipes");
            DropForeignKey("dbo.Recipes", "Id_Category_Main", "dbo.Category_Main");
            DropForeignKey("dbo.Category_Recipe", "Id_Recipe", "dbo.Recipes");
            DropForeignKey("dbo.Category_Recipe", "Id_Category", "dbo.Categories");
            DropForeignKey("dbo.Elements", "Id_Product", "dbo.Products");
            DropIndex("dbo.Rates", new[] { "Id_Recipe" });
            DropIndex("dbo.Rates", new[] { "Id_User" });
            DropIndex("dbo.UserDetails", new[] { "Id_List" });
            DropIndex("dbo.Comments", new[] { "Id_Recipe" });
            DropIndex("dbo.Comments", new[] { "Id_User" });
            DropIndex("dbo.Category_Recipe", new[] { "Id_Recipe" });
            DropIndex("dbo.Category_Recipe", new[] { "Id_Category" });
            DropIndex("dbo.Recipes", new[] { "Id_Category_Main" });
            DropIndex("dbo.Recipes", new[] { "Id_User" });
            DropIndex("dbo.Elements", new[] { "Id_Recipe" });
            DropIndex("dbo.Elements", new[] { "Id_Product" });
            DropIndex("dbo.Black_Item", new[] { "Id_Product" });
            DropIndex("dbo.Black_Item", new[] { "Id_User" });
            DropTable("dbo.Rates");
            DropTable("dbo.Lists");
            DropTable("dbo.UserDetails");
            DropTable("dbo.Comments");
            DropTable("dbo.Category_Main");
            DropTable("dbo.Categories");
            DropTable("dbo.Category_Recipe");
            DropTable("dbo.Recipes");
            DropTable("dbo.Elements");
            DropTable("dbo.Products");
            DropTable("dbo.Black_Item");
        }
    }
}
