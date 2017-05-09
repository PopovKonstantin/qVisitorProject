using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using qVisitor.Data;

namespace qVisitor.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170331163740_My_Migration")]
    partial class My_Migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("qVisitor.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("qVisitor.Models.qvBranch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CityId");

                    b.Property<int>("CompanyId");

                    b.Property<string>("HighBranchId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("CompanyId");

                    b.ToTable("qvBranch");
                });

            modelBuilder.Entity("qVisitor.Models.qvCheckPoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("ObjectId");

                    b.HasKey("Id");

                    b.HasIndex("ObjectId");

                    b.ToTable("qvCheckPoint");
                });

            modelBuilder.Entity("qVisitor.Models.qvCity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CountryID");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CountryID");

                    b.ToTable("qvCity");
                });

            modelBuilder.Entity("qVisitor.Models.qvCompany", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CounryId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CounryId");

                    b.ToTable("qvCompany");
                });

            modelBuilder.Entity("qVisitor.Models.qvCountry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("qvCountry");
                });

            modelBuilder.Entity("qVisitor.Models.qvDepartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BranchId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.ToTable("qvDepartment");
                });

            modelBuilder.Entity("qVisitor.Models.qvEntrance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ActionDate");

                    b.Property<int>("CheckPointId");

                    b.Property<int>("EntranceTypeId");

                    b.Property<int>("OrderId");

                    b.Property<int?>("qvVisitorId");

                    b.HasKey("Id");

                    b.HasIndex("CheckPointId");

                    b.HasIndex("EntranceTypeId");

                    b.HasIndex("OrderId");

                    b.HasIndex("qvVisitorId");

                    b.ToTable("qvEntrance");
                });

            modelBuilder.Entity("qVisitor.Models.qvEntranceDoc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EntranceId");

                    b.Property<byte[]>("Scan");

                    b.HasKey("Id");

                    b.HasIndex("EntranceId");

                    b.ToTable("qvEntranceDoc");
                });

            modelBuilder.Entity("qVisitor.Models.qvEntrancePhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EntranceId");

                    b.Property<byte[]>("Photo");

                    b.HasKey("Id");

                    b.HasIndex("EntranceId");

                    b.ToTable("qvEntrancePhoto");
                });

            modelBuilder.Entity("qVisitor.Models.qvEntranceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("qvEntranceType");
                });

            modelBuilder.Entity("qVisitor.Models.qvHotEntrance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Attendant");

                    b.Property<string>("Comment");

                    b.Property<int>("DepartmentId");

                    b.Property<string>("DocumentNumber");

                    b.Property<string>("Name");

                    b.Property<string>("Organization");

                    b.Property<string>("Patronymic");

                    b.Property<string>("Surname");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("UserId");

                    b.ToTable("qvHotEntrance");
                });

            modelBuilder.Entity("qVisitor.Models.qvHotEntranceDoc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Document");

                    b.Property<int>("HotEntranceId");

                    b.HasKey("Id");

                    b.HasIndex("HotEntranceId");

                    b.ToTable("qvHotEntranceDoc");
                });

            modelBuilder.Entity("qVisitor.Models.qvHotEntrancePhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("HotEntranceId");

                    b.Property<byte[]>("Photo");

                    b.HasKey("Id");

                    b.HasIndex("HotEntranceId");

                    b.ToTable("HotEntrancePhoto");
                });

            modelBuilder.Entity("qVisitor.Models.qvNotRecognizedDoc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CheckPointId");

                    b.Property<byte[]>("Scan");

                    b.HasKey("Id");

                    b.HasIndex("CheckPointId");

                    b.ToTable("qvNotRecognizedDoc");
                });

            modelBuilder.Entity("qVisitor.Models.qvObject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CityId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("qvObject");
                });

            modelBuilder.Entity("qVisitor.Models.qvOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CloseTime");

                    b.Property<DateTime>("EndDate");

                    b.Property<DateTime>("OpenTime");

                    b.Property<int>("OrderStausid");

                    b.Property<int>("OrderTypeid");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("OrderStausid");

                    b.HasIndex("OrderTypeid");

                    b.ToTable("qvOrder");
                });

            modelBuilder.Entity("qVisitor.Models.qvOrderComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<DateTime>("CommentDate");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("qvOrderComment");
                });

            modelBuilder.Entity("qVisitor.Models.qvOrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("qvOrderStatus");
                });

            modelBuilder.Entity("qVisitor.Models.qvOrderStatusHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ActionDate");

                    b.Property<int>("NewStatusId");

                    b.Property<int>("OldStatusId");

                    b.Property<int>("OrderId");

                    b.HasKey("Id");

                    b.HasIndex("NewStatusId");

                    b.HasIndex("OldStatusId");

                    b.HasIndex("OrderId");

                    b.ToTable("qvOrderStatusHistory");
                });

            modelBuilder.Entity("qVisitor.Models.qvOrderType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("qvOrderType");
                });

            modelBuilder.Entity("qVisitor.Models.qvUserPassport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Birthdate");

                    b.Property<string>("Gender");

                    b.Property<string>("Name");

                    b.Property<string>("Patronymic");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.ToTable("qvUserPassport");
                });

            modelBuilder.Entity("qVisitor.Models.qvUserPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Photo");

                    b.Property<DateTime>("PhotoDate");

                    b.HasKey("Id");

                    b.ToTable("qvUserPhoto");
                });

            modelBuilder.Entity("qVisitor.Models.qvVisitiorPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Photo");

                    b.Property<DateTime>("PhotoDate");

                    b.Property<int>("VisitorId");

                    b.HasKey("Id");

                    b.HasIndex("VisitorId");

                    b.ToTable("qvVisitorPhoto");
                });

            modelBuilder.Entity("qVisitor.Models.qvVisitor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Gender");

                    b.Property<DateTime>("birthdate");

                    b.Property<string>("name");

                    b.Property<string>("patronymic");

                    b.Property<string>("surname");

                    b.HasKey("Id");

                    b.ToTable("qvVisitor");
                });

            modelBuilder.Entity("qVisitor.Models.qvVisitorDoc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ExpireDate");

                    b.Property<DateTime>("IssueDate");

                    b.Property<string>("Name");

                    b.Property<string>("Number");

                    b.Property<string>("Surname");

                    b.Property<int>("VisitorId");

                    b.HasKey("Id");

                    b.HasIndex("VisitorId");

                    b.ToTable("qvVisitorDoc");
                });

            modelBuilder.Entity("qVisitor.Models.qvVisitorLuggage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Luggage");

                    b.Property<int>("OrderId");

                    b.Property<int>("VisitorId");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("VisitorId");

                    b.ToTable("qvVisitorLuggage");
                });

            modelBuilder.Entity("qVisitor.Models.qvVisitorScan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Scan");

                    b.Property<int>("VisitorId");

                    b.HasKey("Id");

                    b.HasIndex("VisitorId");

                    b.ToTable("qvVisitorScan");
                });

            modelBuilder.Entity("qVisitor.Models.refOrderVisitor", b =>
                {
                    b.Property<int>("VisitorId");

                    b.Property<int>("OrderId");

                    b.HasKey("VisitorId", "OrderId");

                    b.HasIndex("OrderId");

                    b.HasIndex("VisitorId");

                    b.ToTable("refOrderVisitor");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("qVisitor.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("qVisitor.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("qVisitor.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("qVisitor.Models.qvBranch", b =>
                {
                    b.HasOne("qVisitor.Models.qvCity", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("qVisitor.Models.qvCompany", "Company")
                        .WithMany("Branches")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("qVisitor.Models.qvCheckPoint", b =>
                {
                    b.HasOne("qVisitor.Models.qvObject", "Object")
                        .WithMany("CheckPoints")
                        .HasForeignKey("ObjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("qVisitor.Models.qvCity", b =>
                {
                    b.HasOne("qVisitor.Models.qvCountry", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("qVisitor.Models.qvCompany", b =>
                {
                    b.HasOne("qVisitor.Models.qvCountry", "Country")
                        .WithMany("Companies")
                        .HasForeignKey("CounryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("qVisitor.Models.qvDepartment", b =>
                {
                    b.HasOne("qVisitor.Models.qvBranch", "Branch")
                        .WithMany("Departments")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("qVisitor.Models.qvEntrance", b =>
                {
                    b.HasOne("qVisitor.Models.qvCheckPoint", "CheckPoint")
                        .WithMany("Entrances")
                        .HasForeignKey("CheckPointId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("qVisitor.Models.qvEntranceType", "EntranceType")
                        .WithMany("Entrances")
                        .HasForeignKey("EntranceTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("qVisitor.Models.qvOrder", "Order")
                        .WithMany("Entrances")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("qVisitor.Models.qvVisitor")
                        .WithMany("Entrances")
                        .HasForeignKey("qvVisitorId");
                });

            modelBuilder.Entity("qVisitor.Models.qvEntranceDoc", b =>
                {
                    b.HasOne("qVisitor.Models.qvEntrance", "Entrance")
                        .WithMany("EntranceDocs")
                        .HasForeignKey("EntranceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("qVisitor.Models.qvEntrancePhoto", b =>
                {
                    b.HasOne("qVisitor.Models.qvEntrance", "Entrance")
                        .WithMany("EntrancePhotoes")
                        .HasForeignKey("EntranceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("qVisitor.Models.qvHotEntrance", b =>
                {
                    b.HasOne("qVisitor.Models.qvDepartment", "Department")
                        .WithMany("HotEntrances")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("qVisitor.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("qVisitor.Models.qvHotEntranceDoc", b =>
                {
                    b.HasOne("qVisitor.Models.qvHotEntrance", "HotEntrance")
                        .WithMany("HotEntranceDocs")
                        .HasForeignKey("HotEntranceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("qVisitor.Models.qvHotEntrancePhoto", b =>
                {
                    b.HasOne("qVisitor.Models.qvHotEntrance", "HotEntrance")
                        .WithMany("HotEntrancePhotoes")
                        .HasForeignKey("HotEntranceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("qVisitor.Models.qvNotRecognizedDoc", b =>
                {
                    b.HasOne("qVisitor.Models.qvCheckPoint", "CheckPoint")
                        .WithMany("NotRecognizedDocs")
                        .HasForeignKey("CheckPointId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("qVisitor.Models.qvObject", b =>
                {
                    b.HasOne("qVisitor.Models.qvCity", "City")
                        .WithMany("Objects")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("qVisitor.Models.qvOrder", b =>
                {
                    b.HasOne("qVisitor.Models.qvOrderStatus", "OrderStatus")
                        .WithMany("Orders")
                        .HasForeignKey("OrderStausid")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("qVisitor.Models.qvOrderType", "OrderType")
                        .WithMany("Orders")
                        .HasForeignKey("OrderTypeid")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("qVisitor.Models.qvOrderComment", b =>
                {
                    b.HasOne("qVisitor.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("qVisitor.Models.qvOrderStatusHistory", b =>
                {
                    b.HasOne("qVisitor.Models.qvOrderStatus", "NewStatus")
                        .WithMany()
                        .HasForeignKey("NewStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("qVisitor.Models.qvOrderStatus", "OldStatus")
                        .WithMany()
                        .HasForeignKey("OldStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("qVisitor.Models.qvOrder", "Orders")
                        .WithMany("OrderStatusHistories")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("qVisitor.Models.qvVisitiorPhoto", b =>
                {
                    b.HasOne("qVisitor.Models.qvVisitor", "Visitor")
                        .WithMany("VisitorPhotoes")
                        .HasForeignKey("VisitorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("qVisitor.Models.qvVisitorDoc", b =>
                {
                    b.HasOne("qVisitor.Models.qvVisitor", "Visitor")
                        .WithMany("VisitorDocs")
                        .HasForeignKey("VisitorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("qVisitor.Models.qvVisitorLuggage", b =>
                {
                    b.HasOne("qVisitor.Models.qvOrder", "Order")
                        .WithMany("VisitorLuggages")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("qVisitor.Models.qvVisitor", "Visitor")
                        .WithMany("VisitorLuggages")
                        .HasForeignKey("VisitorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("qVisitor.Models.qvVisitorScan", b =>
                {
                    b.HasOne("qVisitor.Models.qvVisitor", "Visitor")
                        .WithMany("VisitorScans")
                        .HasForeignKey("VisitorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("qVisitor.Models.refOrderVisitor", b =>
                {
                    b.HasOne("qVisitor.Models.qvOrder", "Order")
                        .WithMany("RefOrderVisitors")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("qVisitor.Models.qvVisitor", "Visitor")
                        .WithMany("RefOrderVisitors")
                        .HasForeignKey("VisitorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
