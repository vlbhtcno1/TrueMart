using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace TrueMart.Infrastructure.Migrations
{
    //Init database
    [Migration(202011011000)]
    public class Migration_202011011000 : Migration
    {
        public override void Up()
        {
            Create.Table("Role")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("Name").AsString(50).NotNullable();

            Create.Table("User")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("Name").AsString(255)
                .WithColumn("Username").AsString(255)
                .WithColumn("FullName").AsString(255)
                .WithColumn("Gender").AsInt16()
                .WithColumn("Email").AsString(255)
                .WithColumn("PhoneNumber").AsString(25)
                .WithColumn("Address").AsString()
                .WithColumn("Subscribe").AsBoolean()
                .WithColumn("RoleId").AsInt32().ForeignKey("FK_User_Role", "Role", "Id")
                .WithColumn("RecordStatus").AsInt16()
                .WithColumn("Note").AsString(5000)
                .WithColumn("CreatedBy").AsInt32()
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("LastModifiedBy").AsInt32()
                .WithColumn("LastModifiedAt").AsDateTime();

            Create.Table("Category")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("Name").AsString(255)
                .WithColumn("UrlSlag").AsString(255)
                .WithColumn("RecordStatus").AsInt16()
                .WithColumn("Note").AsString(5000)
                .WithColumn("CreatedBy").AsInt32()
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("LastModifiedBy").AsInt32()
                .WithColumn("LastModifiedAt").AsDateTime();

            Create.Table("Product")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("Name").AsString(255)
                .WithColumn("UrlSlag").AsString(255)
                .WithColumn("CategoryId").AsInt32().ForeignKey("FK_Product_Category","Category","Id")
                .WithColumn("Price").AsDecimal()
                .WithColumn("SalePrice").AsDecimal()
                .WithColumn("RecordStatus").AsInt16()
                .WithColumn("Note").AsString(5000)
                .WithColumn("CreatedBy").AsInt32()
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("LastModifiedBy").AsInt32()
                .WithColumn("LastModifiedAt").AsDateTime();

            Create.Table("ProductImage")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("ProductId").AsInt32().ForeignKey("FK_ProductImage_Product","Product","Id")
                .WithColumn("Name").AsString(255)
                .WithColumn("Url").AsString(255)
                .WithColumn("RecordStatus").AsInt16()
                .WithColumn("Note").AsString(5000)
                .WithColumn("CreatedBy").AsInt32()
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("LastModifiedBy").AsInt32()
                .WithColumn("LastModifiedAt").AsDateTime();

            Create.Table("Promotion")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("Name").AsString(255)
                .WithColumn("StartDate").AsDateTime()
                .WithColumn("EndDate").AsDateTime()
                .WithColumn("DiscountValue").AsDecimal()
                .WithColumn("Description").AsString(5000)
                .WithColumn("RecordStatus").AsInt16()
                .WithColumn("Note").AsString(5000)
                .WithColumn("CreatedBy").AsInt32()
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("LastModifiedBy").AsInt32()
                .WithColumn("LastModifiedAt").AsDateTime();

            Create.Table("Promotion_Product")
                .WithColumn("PromotionId").AsInt32().NotNullable().ForeignKey("PK_Promotion_Product_Promotion","Promotion", "Id")
                .WithColumn("ProductId").AsInt32().NotNullable().ForeignKey("PK_Promotion_Product_Product","Product","Id");
            Create.PrimaryKey("PK_Promotion_Product").OnTable("Promotion_Product")
                .Columns("PromotionId", "ProductId");

            Create.Table("Order")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("CustomerId").AsInt32().ForeignKey("PK_Order_User","User","Id")
                .WithColumn("OrderDate").AsDateTime()
                .WithColumn("TotalAmount").AsDecimal()
                .WithColumn("OrderNote").AsString(5000)
                .WithColumn("Note").AsString(5000)
                .WithColumn("CreatedBy").AsInt32()
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("LastModifiedBy").AsInt32()
                .WithColumn("LastModifiedAt").AsDateTime();

            Create.Table("OrderDetail")
                .WithColumn("OrderId").AsInt32().NotNullable().ForeignKey("FK_OrderDetail_Order", "Order", "Id")
                .WithColumn("ProductId").AsInt32().NotNullable().ForeignKey("FK_OrderDetail_Product", "Product", "Id")
                .WithColumn("Quantity").AsInt32()
                .WithColumn("Price").AsDecimal();
            Create.PrimaryKey("PK_OrderDetail").OnTable("OrderDetail")
                .Columns("OrderId", "ProductId");

        }

        public override void Down()
        {
            Delete.Table("OrderDetail");
            Delete.Table("Promotion_Product");
            Delete.Table("Promotion");
            Delete.Table("Order");
            Delete.Table("ProductImage");
            Delete.Table("Product");
            Delete.Table("Category");
            Delete.Table("User");
            Delete.Table("Role");
        }
    }
}
