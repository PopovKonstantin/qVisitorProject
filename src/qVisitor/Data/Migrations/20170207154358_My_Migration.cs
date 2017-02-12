using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace qVisitor.Data.Migrations
{
    public partial class My_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "qvCountry",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qvCountry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "qvEntranceType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Code = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qvEntranceType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "qvGender",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qvGender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "qvOrderComment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Comment = table.Column<string>(nullable: true),
                    CommentDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qvOrderComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_qvOrderComment_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "qvOrderStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Code = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qvOrderStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "qvOrderType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Code = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qvOrderType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "qvUserPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Photo = table.Column<byte[]>(nullable: true),
                    PhotoDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qvUserPhoto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "qvCity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    CountryID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qvCity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_qvCity_qvCountry_CountryID",
                        column: x => x.CountryID,
                        principalTable: "qvCountry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "qvCompany",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    CounryId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    qvCompanyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qvCompany", x => x.Id);
                    table.ForeignKey(
                        name: "FK_qvCompany_qvCountry_CounryId",
                        column: x => x.CounryId,
                        principalTable: "qvCountry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_qvCompany_qvCompany_qvCompanyId",
                        column: x => x.qvCompanyId,
                        principalTable: "qvCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "qvUserPassport",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Birthdate = table.Column<DateTime>(nullable: false),
                    GenderId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Patronymic = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qvUserPassport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_qvUserPassport_qvGender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "qvGender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "qvVisitor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    GenderId = table.Column<int>(nullable: false),
                    birthdate = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    patronymic = table.Column<string>(nullable: true),
                    surname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qvVisitor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_qvVisitor_qvGender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "qvGender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "qvOrder",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    CloseTime = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    OpenTime = table.Column<DateTime>(nullable: false),
                    OrderStausid = table.Column<int>(nullable: false),
                    OrderTypeid = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qvOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_qvOrder_qvOrderStatus_OrderStausid",
                        column: x => x.OrderStausid,
                        principalTable: "qvOrderStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_qvOrder_qvOrderType_OrderTypeid",
                        column: x => x.OrderTypeid,
                        principalTable: "qvOrderType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "qvObject",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    CityId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qvObject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_qvObject_qvCity_CityId",
                        column: x => x.CityId,
                        principalTable: "qvCity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "qvBranch",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    CityId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    HighBranchId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qvBranch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_qvBranch_qvCity_CityId",
                        column: x => x.CityId,
                        principalTable: "qvCity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_qvBranch_qvCompany_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "qvCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "qvVisitorPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Photo = table.Column<byte[]>(nullable: true),
                    PhotoDate = table.Column<DateTime>(nullable: false),
                    VisitorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qvVisitorPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_qvVisitorPhoto_qvVisitor_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "qvVisitor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "qvVisitorDoc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    ExpireDate = table.Column<DateTime>(nullable: false),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    VisitorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qvVisitorDoc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_qvVisitorDoc_qvVisitor_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "qvVisitor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "qvVisitorScan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Scan = table.Column<byte[]>(nullable: true),
                    VisitorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qvVisitorScan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_qvVisitorScan_qvVisitor_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "qvVisitor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "qvOrderStatusHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    ActionDate = table.Column<DateTime>(nullable: false),
                    NewStatusId = table.Column<int>(nullable: false),
                    OldStatusId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qvOrderStatusHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_qvOrderStatusHistory_qvOrderStatus_NewStatusId",
                        column: x => x.NewStatusId,
                        principalTable: "qvOrderStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_qvOrderStatusHistory_qvOrderStatus_OldStatusId",
                        column: x => x.OldStatusId,
                        principalTable: "qvOrderStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_qvOrderStatusHistory_qvOrder_OrderId",
                        column: x => x.OrderId,
                        principalTable: "qvOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "qvVisitorLuggage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Luggage = table.Column<string>(nullable: true),
                    OrderId = table.Column<int>(nullable: false),
                    VisitorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qvVisitorLuggage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_qvVisitorLuggage_qvOrder_OrderId",
                        column: x => x.OrderId,
                        principalTable: "qvOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_qvVisitorLuggage_qvVisitor_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "qvVisitor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "refOrderVisitor",
                columns: table => new
                {
                    VisitorId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refOrderVisitor", x => new { x.VisitorId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_refOrderVisitor_qvOrder_OrderId",
                        column: x => x.OrderId,
                        principalTable: "qvOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_refOrderVisitor_qvVisitor_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "qvVisitor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "qvCheckPoint",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    ObjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qvCheckPoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_qvCheckPoint_qvObject_ObjectId",
                        column: x => x.ObjectId,
                        principalTable: "qvObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "qvDepartment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    BranchId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qvDepartment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_qvDepartment_qvBranch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "qvBranch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "qvEntrance",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    ActionDate = table.Column<DateTime>(nullable: false),
                    CheckPointId = table.Column<int>(nullable: false),
                    EntranceTypeId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                    VisitorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qvEntrance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_qvEntrance_qvCheckPoint_CheckPointId",
                        column: x => x.CheckPointId,
                        principalTable: "qvCheckPoint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_qvEntrance_qvEntranceType_EntranceTypeId",
                        column: x => x.EntranceTypeId,
                        principalTable: "qvEntranceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_qvEntrance_qvOrder_OrderId",
                        column: x => x.OrderId,
                        principalTable: "qvOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_qvEntrance_qvVisitor_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "qvVisitor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "qvNotRecognizedDoc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    CheckPointId = table.Column<int>(nullable: false),
                    Scan = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qvNotRecognizedDoc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_qvNotRecognizedDoc_qvCheckPoint_CheckPointId",
                        column: x => x.CheckPointId,
                        principalTable: "qvCheckPoint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "qvHotEntrance",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Attendant = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: false),
                    DocumentNumber = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Organization = table.Column<string>(nullable: true),
                    Patronymic = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qvHotEntrance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_qvHotEntrance_qvDepartment_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "qvDepartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_qvHotEntrance_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "qvEntranceDoc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    EntranceId = table.Column<int>(nullable: false),
                    Scan = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qvEntranceDoc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_qvEntranceDoc_qvEntrance_EntranceId",
                        column: x => x.EntranceId,
                        principalTable: "qvEntrance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "qvEntrancePhoto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    EntranceId = table.Column<int>(nullable: false),
                    Photo = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qvEntrancePhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_qvEntrancePhoto_qvEntrance_EntranceId",
                        column: x => x.EntranceId,
                        principalTable: "qvEntrance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "qvHotEntranceDoc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Document = table.Column<byte[]>(nullable: true),
                    HotEntranceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qvHotEntranceDoc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_qvHotEntranceDoc_qvHotEntrance_HotEntranceId",
                        column: x => x.HotEntranceId,
                        principalTable: "qvHotEntrance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotEntrancePhoto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    HotEntranceId = table.Column<int>(nullable: false),
                    Photo = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotEntrancePhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotEntrancePhoto_qvHotEntrance_HotEntranceId",
                        column: x => x.HotEntranceId,
                        principalTable: "qvHotEntrance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_qvBranch_CityId",
                table: "qvBranch",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_qvBranch_CompanyId",
                table: "qvBranch",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_qvCheckPoint_ObjectId",
                table: "qvCheckPoint",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_qvCity_CountryID",
                table: "qvCity",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_qvCompany_CounryId",
                table: "qvCompany",
                column: "CounryId");

            migrationBuilder.CreateIndex(
                name: "IX_qvCompany_qvCompanyId",
                table: "qvCompany",
                column: "qvCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_qvDepartment_BranchId",
                table: "qvDepartment",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_qvEntrance_CheckPointId",
                table: "qvEntrance",
                column: "CheckPointId");

            migrationBuilder.CreateIndex(
                name: "IX_qvEntrance_EntranceTypeId",
                table: "qvEntrance",
                column: "EntranceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_qvEntrance_OrderId",
                table: "qvEntrance",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_qvEntrance_VisitorId",
                table: "qvEntrance",
                column: "VisitorId");

            migrationBuilder.CreateIndex(
                name: "IX_qvEntranceDoc_EntranceId",
                table: "qvEntranceDoc",
                column: "EntranceId");

            migrationBuilder.CreateIndex(
                name: "IX_qvEntrancePhoto_EntranceId",
                table: "qvEntrancePhoto",
                column: "EntranceId");

            migrationBuilder.CreateIndex(
                name: "IX_qvHotEntrance_DepartmentId",
                table: "qvHotEntrance",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_qvHotEntrance_UserId",
                table: "qvHotEntrance",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_qvHotEntranceDoc_HotEntranceId",
                table: "qvHotEntranceDoc",
                column: "HotEntranceId");

            migrationBuilder.CreateIndex(
                name: "IX_HotEntrancePhoto_HotEntranceId",
                table: "HotEntrancePhoto",
                column: "HotEntranceId");

            migrationBuilder.CreateIndex(
                name: "IX_qvNotRecognizedDoc_CheckPointId",
                table: "qvNotRecognizedDoc",
                column: "CheckPointId");

            migrationBuilder.CreateIndex(
                name: "IX_qvObject_CityId",
                table: "qvObject",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_qvOrder_OrderStausid",
                table: "qvOrder",
                column: "OrderStausid");

            migrationBuilder.CreateIndex(
                name: "IX_qvOrder_OrderTypeid",
                table: "qvOrder",
                column: "OrderTypeid");

            migrationBuilder.CreateIndex(
                name: "IX_qvOrderComment_UserId",
                table: "qvOrderComment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_qvOrderStatusHistory_NewStatusId",
                table: "qvOrderStatusHistory",
                column: "NewStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_qvOrderStatusHistory_OldStatusId",
                table: "qvOrderStatusHistory",
                column: "OldStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_qvOrderStatusHistory_OrderId",
                table: "qvOrderStatusHistory",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_qvUserPassport_GenderId",
                table: "qvUserPassport",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_qvVisitorPhoto_VisitorId",
                table: "qvVisitorPhoto",
                column: "VisitorId");

            migrationBuilder.CreateIndex(
                name: "IX_qvVisitor_GenderId",
                table: "qvVisitor",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_qvVisitorDoc_VisitorId",
                table: "qvVisitorDoc",
                column: "VisitorId");

            migrationBuilder.CreateIndex(
                name: "IX_qvVisitorLuggage_OrderId",
                table: "qvVisitorLuggage",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_qvVisitorLuggage_VisitorId",
                table: "qvVisitorLuggage",
                column: "VisitorId");

            migrationBuilder.CreateIndex(
                name: "IX_qvVisitorScan_VisitorId",
                table: "qvVisitorScan",
                column: "VisitorId");

            migrationBuilder.CreateIndex(
                name: "IX_refOrderVisitor_OrderId",
                table: "refOrderVisitor",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_refOrderVisitor_VisitorId",
                table: "refOrderVisitor",
                column: "VisitorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "qvEntranceDoc");

            migrationBuilder.DropTable(
                name: "qvEntrancePhoto");

            migrationBuilder.DropTable(
                name: "qvHotEntranceDoc");

            migrationBuilder.DropTable(
                name: "HotEntrancePhoto");

            migrationBuilder.DropTable(
                name: "qvNotRecognizedDoc");

            migrationBuilder.DropTable(
                name: "qvOrderComment");

            migrationBuilder.DropTable(
                name: "qvOrderStatusHistory");

            migrationBuilder.DropTable(
                name: "qvUserPassport");

            migrationBuilder.DropTable(
                name: "qvUserPhoto");

            migrationBuilder.DropTable(
                name: "qvVisitorPhoto");

            migrationBuilder.DropTable(
                name: "qvVisitorDoc");

            migrationBuilder.DropTable(
                name: "qvVisitorLuggage");

            migrationBuilder.DropTable(
                name: "qvVisitorScan");

            migrationBuilder.DropTable(
                name: "refOrderVisitor");

            migrationBuilder.DropTable(
                name: "qvEntrance");

            migrationBuilder.DropTable(
                name: "qvHotEntrance");

            migrationBuilder.DropTable(
                name: "qvCheckPoint");

            migrationBuilder.DropTable(
                name: "qvEntranceType");

            migrationBuilder.DropTable(
                name: "qvOrder");

            migrationBuilder.DropTable(
                name: "qvVisitor");

            migrationBuilder.DropTable(
                name: "qvDepartment");

            migrationBuilder.DropTable(
                name: "qvObject");

            migrationBuilder.DropTable(
                name: "qvOrderStatus");

            migrationBuilder.DropTable(
                name: "qvOrderType");

            migrationBuilder.DropTable(
                name: "qvGender");

            migrationBuilder.DropTable(
                name: "qvBranch");

            migrationBuilder.DropTable(
                name: "qvCity");

            migrationBuilder.DropTable(
                name: "qvCompany");

            migrationBuilder.DropTable(
                name: "qvCountry");
        }
    }
}
