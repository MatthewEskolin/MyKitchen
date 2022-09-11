﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyKitchen.Data;

#nullable disable

namespace MyKitchen.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("MyKitchen.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FavoriteFood")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("MealImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("MyKitchen.Data.Calendar.Events", b =>
                {
                    b.Property<int>("EventID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EventID"), 1L, 1);

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FoodItemID")
                        .HasColumnType("int");

                    b.Property<bool>("IsFullDay")
                        .HasColumnType("bit");

                    b.Property<int?>("MealID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ThemeColor")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EventID");

                    b.HasIndex("AppUserId");

                    b.HasIndex("FoodItemID");

                    b.HasIndex("MealID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("MyKitchen.Data.FileUpload", b =>
                {
                    b.Property<int>("FileUploadID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FileUploadID"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EntityType")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("FileEntityID")
                        .HasColumnType("int");

                    b.Property<string>("FileName")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("OriginalFileName")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("FileUploadID");

                    b.ToTable("FileUploads");
                });

            modelBuilder.Entity("MyKitchen.Data.FoodGroup", b =>
                {
                    b.Property<int>("FoodGroupID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FoodGroupID"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FoodGroupID");

                    b.ToTable("FoodGroups");
                });

            modelBuilder.Entity("MyKitchen.Data.FoodItem", b =>
                {
                    b.Property<int>("FoodItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FoodItemID"), 1L, 1);

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("FoodDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FoodGroupID")
                        .HasColumnType("int");

                    b.Property<string>("FoodItemName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResturantName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FoodItemID");

                    b.HasIndex("FoodGroupID");

                    b.ToTable("FoodItems");
                });

            modelBuilder.Entity("MyKitchen.Data.GroceryListItem", b =>
                {
                    b.Property<int>("GroceryListItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GroceryListItemID"), 1L, 1);

                    b.Property<string>("Item")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Shopped")
                        .HasColumnType("bit");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("GroceryListItemID");

                    b.HasIndex("UserID");

                    b.ToTable("GroceryListItems");
                });

            modelBuilder.Entity("MyKitchen.Data.LastCookedMeal", b =>
                {
                    b.Property<DateTime>("LastMeal")
                        .HasColumnType("datetime2");

                    b.HasKey("LastMeal");

                    b.ToTable("LastCookedMeal");
                });

            modelBuilder.Entity("MyKitchen.Data.Meal", b =>
                {
                    b.Property<int>("MealID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MealID"), 1L, 1);

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Comments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsFavorite")
                        .HasColumnType("bit");

                    b.Property<bool>("IsQueued")
                        .HasColumnType("bit");

                    b.Property<string>("MealName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Recipe")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MealID");

                    b.HasIndex("AppUserId");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("MyKitchen.Data.MealFoodItems", b =>
                {
                    b.Property<int>("MealFoodItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MealFoodItemId"), 1L, 1);

                    b.Property<DateTime>("FoodAddedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("FoodItemId")
                        .HasColumnType("int");

                    b.Property<int>("MealId")
                        .HasColumnType("int");

                    b.HasKey("MealFoodItemId");

                    b.HasIndex("MealId");

                    b.HasIndex("FoodItemId", "MealId")
                        .IsUnique();

                    b.ToTable("MealFoodItems");
                });

            modelBuilder.Entity("MyKitchen.Data.UserFoodItem", b =>
                {
                    b.Property<int>("UserFoodItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserFoodItemID"), 1L, 1);

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("FoodItemID")
                        .HasColumnType("int");

                    b.HasKey("UserFoodItemID");

                    b.HasIndex("AppUserId");

                    b.HasIndex("FoodItemID");

                    b.ToTable("UserFoodItems");
                });

            modelBuilder.Entity("MyKitchen.Data.vwsMealItems", b =>
                {
                    b.Property<string>("Comments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FooditemId")
                        .HasColumnType("int");

                    b.Property<string>("Fooditemname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MealAppUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Mealid")
                        .HasColumnType("int");

                    b.Property<string>("Mealname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserFoodItemsAppUser")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("vwsMealItems");
                });

            modelBuilder.Entity("MyKitchen.Data.vwsMealsAndFoodItems", b =>
                {
                    b.Property<int>("ItemID")
                        .HasColumnType("int");

                    b.Property<string>("ItemName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemType")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("vwsMealsAndFoodItems");
                });

            modelBuilder.Entity("MyKitchen.Data.vwsUserMealsAndFoodItem", b =>
                {
                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<string>("ItemName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemType")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("vwsUserMealsAndFoodItems");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MyKitchen.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MyKitchen.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyKitchen.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MyKitchen.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyKitchen.Data.Calendar.Events", b =>
                {
                    b.HasOne("MyKitchen.Data.ApplicationUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId");

                    b.HasOne("MyKitchen.Data.FoodItem", "FoodItem")
                        .WithMany()
                        .HasForeignKey("FoodItemID");

                    b.HasOne("MyKitchen.Data.Meal", "Meal")
                        .WithMany()
                        .HasForeignKey("MealID");

                    b.Navigation("AppUser");

                    b.Navigation("FoodItem");

                    b.Navigation("Meal");
                });

            modelBuilder.Entity("MyKitchen.Data.FoodItem", b =>
                {
                    b.HasOne("MyKitchen.Data.FoodGroup", "FoodGroup")
                        .WithMany("FoodItem")
                        .HasForeignKey("FoodGroupID");

                    b.Navigation("FoodGroup");
                });

            modelBuilder.Entity("MyKitchen.Data.GroceryListItem", b =>
                {
                    b.HasOne("MyKitchen.Data.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyKitchen.Data.Meal", b =>
                {
                    b.HasOne("MyKitchen.Data.ApplicationUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId");

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("MyKitchen.Data.MealFoodItems", b =>
                {
                    b.HasOne("MyKitchen.Data.FoodItem", "FoodItems")
                        .WithMany()
                        .HasForeignKey("FoodItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyKitchen.Data.Meal", "Meals")
                        .WithMany("MealFoodItems")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodItems");

                    b.Navigation("Meals");
                });

            modelBuilder.Entity("MyKitchen.Data.UserFoodItem", b =>
                {
                    b.HasOne("MyKitchen.Data.ApplicationUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId");

                    b.HasOne("MyKitchen.Data.FoodItem", "FoodItems")
                        .WithMany()
                        .HasForeignKey("FoodItemID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("FoodItems");
                });

            modelBuilder.Entity("MyKitchen.Data.FoodGroup", b =>
                {
                    b.Navigation("FoodItem");
                });

            modelBuilder.Entity("MyKitchen.Data.Meal", b =>
                {
                    b.Navigation("MealFoodItems");
                });
#pragma warning restore 612, 618
        }
    }
}
