using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestDiplom.Migrations
{
    public partial class wer1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "authors",
                columns: table => new
                {
                    creator_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    surname_author = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    first_name_author = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    patronymic_author = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    date_birth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    country_id = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authors", x => x.creator_id);
                });

            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    book_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    year_create = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.book_id);
                });

            migrationBuilder.CreateTable(
                name: "depart",
                columns: table => new
                {
                    department_id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    appellation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    duties_description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_depart", x => x.department_id);
                });

            migrationBuilder.CreateTable(
                name: "di_city",
                columns: table => new
                {
                    city_id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    city_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_di_city", x => x.city_id);
                });

            migrationBuilder.CreateTable(
                name: "di_country",
                columns: table => new
                {
                    country_id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    country_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_di_country", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                name: "di_currency",
                columns: table => new
                {
                    currency_id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    currency_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_di_currency", x => x.currency_id);
                });

            migrationBuilder.CreateTable(
                name: "di_lang",
                columns: table => new
                {
                    lang_id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lang_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_di_lang", x => x.lang_id);
                });

            migrationBuilder.CreateTable(
                name: "di_reg",
                columns: table => new
                {
                    region_id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    region_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_di_reg", x => x.region_id);
                });

            migrationBuilder.CreateTable(
                name: "di_stor_room",
                columns: table => new
                {
                    stor_rooms_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    storage_rooms_id = table.Column<long>(type: "bigint", nullable: false),
                    room_code = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_di_stor_room", x => x.stor_rooms_id);
                });

            migrationBuilder.CreateTable(
                name: "di_them",
                columns: table => new
                {
                    theme_id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    theme_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    theme_description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_di_them", x => x.theme_id);
                });

            migrationBuilder.CreateTable(
                name: "eventi",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ThemeColor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsFullDay = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventi", x => x.EventID);
                });

            migrationBuilder.CreateTable(
                name: "issuances",
                columns: table => new
                {
                    issuance_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date_is_Book = table.Column<DateTime>(type: "datetime2", nullable: true),
                    date_re_Book = table.Column<DateTime>(type: "datetime2", nullable: true),
                    date_reser = table.Column<DateTime>(type: "datetime2", nullable: true),
                    readerticket_id = table.Column<short>(type: "smallint", nullable: true),
                    book_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_issuances", x => x.issuance_id);
                });

            migrationBuilder.CreateTable(
                name: "list",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    guid = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    title_bok = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    cost_bo = table.Column<long>(type: "bigint", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_list", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "publi_h",
                columns: table => new
                {
                    publisher_id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    publisher_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    publisher_phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publi_h", x => x.publisher_id);
                });

            migrationBuilder.CreateTable(
                name: "publi_hou_pla",
                columns: table => new
                {
                    publish_house_places_id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    addr_publihouse = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    publisher_id = table.Column<short>(type: "smallint", nullable: true),
                    city_id = table.Column<short>(type: "smallint", nullable: true),
                    region_id = table.Column<short>(type: "smallint", nullable: true),
                    country_id = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publi_hou_pla", x => x.publish_house_places_id);
                });

            migrationBuilder.CreateTable(
                name: "read_ticket",
                columns: table => new
                {
                    readerticket_id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    surname_vistor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    first_name_visitor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    patronymic_author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    home_phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vis_address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ticket_issue_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    staff_id = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_read_ticket", x => x.readerticket_id);
                });

            migrationBuilder.CreateTable(
                name: "sta_eve",
                columns: table => new
                {
                    staff_eve_id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    event_id = table.Column<short>(type: "smallint", nullable: true),
                    staff_id = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sta_eve", x => x.staff_eve_id);
                });

            migrationBuilder.CreateTable(
                name: "staffers",
                columns: table => new
                {
                    staff_id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    surname_staffer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    first_name_staffer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    patronymic_staffer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    work_telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    department_id = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staffers", x => x.staff_id);
                });

            migrationBuilder.CreateTable(
                name: "work_day",
                columns: table => new
                {
                    work_day_id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    be_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ov_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    days = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    note_time = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_day", x => x.work_day_id);
                });

            migrationBuilder.CreateTable(
                name: "work_staffs",
                columns: table => new
                {
                    work_staff_id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    staff_id = table.Column<short>(type: "smallint", nullable: true),
                    work_day_id = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_staffs", x => x.work_staff_id);
                });

            migrationBuilder.CreateTable(
                name: "author_in_books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<long>(type: "bigint", nullable: false),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_author_in_books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_author_in_books_authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "authors",
                        principalColumn: "creator_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_author_in_books_books_BookId",
                        column: x => x.BookId,
                        principalTable: "books",
                        principalColumn: "book_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "publi",
                columns: table => new
                {
                    publication_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pub_num = table.Column<short>(type: "smallint", nullable: false),
                    pub_year = table.Column<short>(type: "smallint", nullable: false),
                    book_vol = table.Column<short>(type: "smallint", nullable: false),
                    сirculation = table.Column<long>(type: "bigint", nullable: false),
                    cost_book = table.Column<long>(type: "bigint", nullable: false),
                    availability = table.Column<bool>(type: "bit", nullable: false),
                    book_storage_id = table.Column<short>(type: "smallint", nullable: true),
                    currency_id = table.Column<short>(type: "smallint", nullable: false),
                    lang_id = table.Column<short>(type: "smallint", nullable: false),
                    country_id = table.Column<short>(type: "smallint", nullable: false),
                    book_id = table.Column<long>(type: "bigint", nullable: false),
                    publisher_id = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publi", x => x.publication_id);
                    table.ForeignKey(
                        name: "FK_publi_books_book_id",
                        column: x => x.book_id,
                        principalTable: "books",
                        principalColumn: "book_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_publi_di_country_country_id",
                        column: x => x.country_id,
                        principalTable: "di_country",
                        principalColumn: "country_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_publi_di_currency_currency_id",
                        column: x => x.currency_id,
                        principalTable: "di_currency",
                        principalColumn: "currency_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_publi_di_lang_lang_id",
                        column: x => x.lang_id,
                        principalTable: "di_lang",
                        principalColumn: "lang_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "book_stor",
                columns: table => new
                {
                    book_storage_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    book_storage_code = table.Column<long>(type: "bigint", nullable: false),
                    storage_rooms_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book_stor", x => x.book_storage_id);
                    table.ForeignKey(
                        name: "FK_book_stor_di_stor_room_storage_rooms_id",
                        column: x => x.storage_rooms_id,
                        principalTable: "di_stor_room",
                        principalColumn: "stor_rooms_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_book_stor_publi_book_storage_code",
                        column: x => x.book_storage_code,
                        principalTable: "publi",
                        principalColumn: "publication_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_author_in_books_AuthorId",
                table: "author_in_books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_author_in_books_BookId_AuthorId",
                table: "author_in_books",
                columns: new[] { "BookId", "AuthorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_book_stor_book_storage_code",
                table: "book_stor",
                column: "book_storage_code");

            migrationBuilder.CreateIndex(
                name: "IX_book_stor_storage_rooms_id",
                table: "book_stor",
                column: "storage_rooms_id");

            migrationBuilder.CreateIndex(
                name: "IX_publi_book_id",
                table: "publi",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_publi_country_id",
                table: "publi",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_publi_currency_id",
                table: "publi",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "IX_publi_lang_id",
                table: "publi",
                column: "lang_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "author_in_books");

            migrationBuilder.DropTable(
                name: "book_stor");

            migrationBuilder.DropTable(
                name: "depart");

            migrationBuilder.DropTable(
                name: "di_city");

            migrationBuilder.DropTable(
                name: "di_reg");

            migrationBuilder.DropTable(
                name: "di_them");

            migrationBuilder.DropTable(
                name: "eventi");

            migrationBuilder.DropTable(
                name: "issuances");

            migrationBuilder.DropTable(
                name: "list");

            migrationBuilder.DropTable(
                name: "publi_h");

            migrationBuilder.DropTable(
                name: "publi_hou_pla");

            migrationBuilder.DropTable(
                name: "read_ticket");

            migrationBuilder.DropTable(
                name: "sta_eve");

            migrationBuilder.DropTable(
                name: "staffers");

            migrationBuilder.DropTable(
                name: "work_day");

            migrationBuilder.DropTable(
                name: "work_staffs");

            migrationBuilder.DropTable(
                name: "authors");

            migrationBuilder.DropTable(
                name: "di_stor_room");

            migrationBuilder.DropTable(
                name: "publi");

            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "di_country");

            migrationBuilder.DropTable(
                name: "di_currency");

            migrationBuilder.DropTable(
                name: "di_lang");
        }
    }
}
