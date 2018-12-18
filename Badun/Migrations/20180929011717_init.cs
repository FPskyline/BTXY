using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Badun.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdBanners",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PicPath = table.Column<string>(nullable: true),
                    PicContent = table.Column<string>(nullable: true),
                    HouseId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdBanners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Applet",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Logo = table.Column<string>(nullable: true),
                    appid = table.Column<string>(nullable: true),
                    secret = table.Column<string>(nullable: true),
                    sub_mch_id = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Citys",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsHot = table.Column<bool>(nullable: false),
                    WeightValue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Logo = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HouseBanners",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PicPath = table.Column<string>(nullable: true),
                    HouseId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseBanners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HouseFoods",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    PicPath = table.Column<string>(nullable: true),
                    HouseId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseFoods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CityId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Area = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: false),
                    WeightValue = table.Column<int>(nullable: false),
                    IsHot = table.Column<bool>(nullable: false),
                    SaveNum = table.Column<int>(nullable: false),
                    Introduce = table.Column<string>(nullable: true),
                    Introduce_rim = table.Column<string>(nullable: true),
                    PeopleNum = table.Column<int>(nullable: false),
                    HowArea = table.Column<int>(nullable: false),
                    HowRoom = table.Column<int>(nullable: false),
                    HowHall = table.Column<int>(nullable: false),
                    HowWC = table.Column<int>(nullable: false),
                    HowCook = table.Column<int>(nullable: false),
                    HowBed = table.Column<int>(nullable: false),
                    BedInfo = table.Column<string>(nullable: true),
                    Logo = table.Column<string>(nullable: true),
                    Notice = table.Column<string>(nullable: true),
                    CreatDate = table.Column<DateTime>(nullable: false),
                    UpDate = table.Column<DateTime>(nullable: false),
                    Longitude = table.Column<string>(nullable: true),
                    Latitude = table.Column<string>(nullable: true),
                    ReceptionNum = table.Column<int>(nullable: false),
                    EvaluateNum = table.Column<int>(nullable: false),
                    CollectNum = table.Column<int>(nullable: false),
                    LikeNum = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HouseUnsubs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Logo = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseUnsubs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Limits",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    TypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Limits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notices",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HouseId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    BeginDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    BeginDatestr = table.Column<string>(nullable: true),
                    EndDatestr = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsDel = table.Column<bool>(nullable: false),
                    CreatDate = table.Column<DateTime>(nullable: false),
                    UpDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Account = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Salt = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Logo = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpDate = table.Column<DateTime>(nullable: false),
                    IsDel = table.Column<bool>(nullable: false),
                    RoleId = table.Column<long>(nullable: false),
                    RoleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Travelings",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Logo = table.Column<string>(nullable: true),
                    Banner = table.Column<string>(nullable: true),
                    Video = table.Column<string>(nullable: true),
                    ClickNum = table.Column<int>(nullable: false),
                    ZanNum = table.Column<int>(nullable: false),
                    CreatDate = table.Column<DateTime>(nullable: false),
                    UpDate = table.Column<DateTime>(nullable: false),
                    CityId = table.Column<long>(nullable: false),
                    IsHot = table.Column<bool>(nullable: false),
                    WeightValue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Travelings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserCollects",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    HouseId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCollects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserIntegrals",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Integral = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserIntegrals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    We_OpenId = table.Column<string>(nullable: true),
                    We_Name = table.Column<string>(nullable: true),
                    We_AvtUrl = table.Column<string>(nullable: true),
                    Integral = table.Column<int>(nullable: false),
                    Token = table.Column<string>(nullable: true),
                    IDNum = table.Column<string>(nullable: true),
                    IsReal = table.Column<int>(nullable: false),
                    PhoneNum = table.Column<string>(nullable: true),
                    IsCert = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HouseDevices",
                columns: table => new
                {
                    HouseId = table.Column<long>(nullable: false),
                    DeviceId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseDevices", x => new { x.HouseId, x.DeviceId });
                    table.UniqueConstraint("AK_HouseDevices_DeviceId_HouseId", x => new { x.DeviceId, x.HouseId });
                    table.ForeignKey(
                        name: "FK_HouseDevices_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HouseDevices_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HouseNotices",
                columns: table => new
                {
                    HouseId = table.Column<long>(nullable: false),
                    NoticeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseNotices", x => new { x.HouseId, x.NoticeId });
                    table.ForeignKey(
                        name: "FK_HouseNotices_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HouseNotices_Notices_NoticeId",
                        column: x => x.NoticeId,
                        principalTable: "Notices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Role_Limits",
                columns: table => new
                {
                    RoleId = table.Column<long>(nullable: false),
                    LimitId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role_Limits", x => new { x.RoleId, x.LimitId });
                    table.UniqueConstraint("AK_Role_Limits_LimitId_RoleId", x => new { x.LimitId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_Role_Limits_Limits_LimitId",
                        column: x => x.LimitId,
                        principalTable: "Limits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Role_Limits_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HouseServices",
                columns: table => new
                {
                    HouseId = table.Column<long>(nullable: false),
                    ServiceId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseServices", x => new { x.HouseId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_HouseServices_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HouseServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HouseNotices_NoticeId",
                table: "HouseNotices",
                column: "NoticeId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseServices_ServiceId",
                table: "HouseServices",
                column: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdBanners");

            migrationBuilder.DropTable(
                name: "Applet");

            migrationBuilder.DropTable(
                name: "Citys");

            migrationBuilder.DropTable(
                name: "HouseBanners");

            migrationBuilder.DropTable(
                name: "HouseDevices");

            migrationBuilder.DropTable(
                name: "HouseFoods");

            migrationBuilder.DropTable(
                name: "HouseNotices");

            migrationBuilder.DropTable(
                name: "HouseServices");

            migrationBuilder.DropTable(
                name: "HouseUnsubs");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Role_Limits");

            migrationBuilder.DropTable(
                name: "Sys_Users");

            migrationBuilder.DropTable(
                name: "Travelings");

            migrationBuilder.DropTable(
                name: "UserCollects");

            migrationBuilder.DropTable(
                name: "UserIntegrals");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Notices");

            migrationBuilder.DropTable(
                name: "Houses");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Limits");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
