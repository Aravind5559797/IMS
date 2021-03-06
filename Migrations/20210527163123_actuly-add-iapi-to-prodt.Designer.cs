// <auto-generated />
using System;
using IMS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IMS.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210527163123_actuly-add-iapi-to-prodt")]
    partial class actulyaddiapitoprodt
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IMS.Models.HistOrder", b =>
                {
                    b.Property<int>("HistOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("HistDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HistImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HistName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("HistPrice")
                        .HasColumnType("float");

                    b.Property<int>("HistProductId")
                        .HasColumnType("int");

                    b.Property<int>("HistQuantity")
                        .HasColumnType("int");

                    b.Property<string>("HistShortDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("HistOrderId");

                    b.HasIndex("OrderId");

                    b.ToTable("HistOrder");
                });

            modelBuilder.Entity("IMS.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HistShopId")
                        .HasColumnType("int");

                    b.Property<string>("HistShopName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderTotalPrice")
                        .HasColumnType("int");

                    b.Property<string>("UserIdentifier")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("OrderId");

                    b.HasIndex("UserIdentifier");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("IMS.Models.Platform", b =>
                {
                    b.Property<int>("PlatformId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlatformName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlatformId");

                    b.ToTable("Platform");
                });

            modelBuilder.Entity("IMS.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsApi")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserIdentifier")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ProductId");

                    b.HasIndex("UserIdentifier");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("IMS.Models.Shop", b =>
                {
                    b.Property<int>("ShopId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlatformId")
                        .HasColumnType("int");

                    b.Property<string>("ShopName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserIdentifier")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ShopId");

                    b.HasIndex("PlatformId");

                    b.HasIndex("UserIdentifier");

                    b.ToTable("Shop");
                });

            modelBuilder.Entity("IMS.Models.User", b =>
                {
                    b.Property<string>("UserIdentifier")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserIdentifier");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ProductShop", b =>
                {
                    b.Property<int>("ProductsProductId")
                        .HasColumnType("int");

                    b.Property<int>("ShopsShopId")
                        .HasColumnType("int");

                    b.HasKey("ProductsProductId", "ShopsShopId");

                    b.HasIndex("ShopsShopId");

                    b.ToTable("ProductShop");
                });

            modelBuilder.Entity("IMS.Models.HistOrder", b =>
                {
                    b.HasOne("IMS.Models.Order", null)
                        .WithMany("Orders")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("IMS.Models.Order", b =>
                {
                    b.HasOne("IMS.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserIdentifier");

                    b.Navigation("User");
                });

            modelBuilder.Entity("IMS.Models.Product", b =>
                {
                    b.HasOne("IMS.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserIdentifier");

                    b.Navigation("User");
                });

            modelBuilder.Entity("IMS.Models.Shop", b =>
                {
                    b.HasOne("IMS.Models.Platform", "Platform")
                        .WithMany()
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IMS.Models.User", "User")
                        .WithMany("Shops")
                        .HasForeignKey("UserIdentifier");

                    b.Navigation("Platform");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProductShop", b =>
                {
                    b.HasOne("IMS.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IMS.Models.Shop", null)
                        .WithMany()
                        .HasForeignKey("ShopsShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IMS.Models.Order", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("IMS.Models.User", b =>
                {
                    b.Navigation("Shops");
                });
#pragma warning restore 612, 618
        }
    }
}
