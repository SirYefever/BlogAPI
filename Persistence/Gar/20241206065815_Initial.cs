using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Gar
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "fias");

            migrationBuilder.CreateTable(
                name: "as_addr_obj",
                schema: "fias",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Уникальный идентификатор записи. Ключевое поле"),
                    objectid = table.Column<long>(type: "bigint", nullable: false, comment: "Глобальный уникальный идентификатор адресного объекта типа INTEGER"),
                    objectguid = table.Column<Guid>(type: "uuid", nullable: false, comment: "Глобальный уникальный идентификатор адресного объекта типа UUID"),
                    changeid = table.Column<long>(type: "bigint", nullable: true, comment: "ID изменившей транзакции"),
                    name = table.Column<string>(type: "text", nullable: false, comment: "Наименование"),
                    typename = table.Column<string>(type: "text", nullable: true, comment: "Краткое наименование типа объекта"),
                    level = table.Column<string>(type: "text", nullable: false, comment: "Уровень адресного объекта"),
                    opertypeid = table.Column<int>(type: "integer", nullable: true, comment: "Статус действия над записью – причина появления записи"),
                    previd = table.Column<long>(type: "bigint", nullable: true, comment: "Идентификатор записи связывания с предыдущей исторической записью"),
                    nextid = table.Column<long>(type: "bigint", nullable: true, comment: "Идентификатор записи связывания с последующей исторической записью"),
                    updatedate = table.Column<DateOnly>(type: "date", nullable: true, comment: "Дата внесения (обновления) записи"),
                    startdate = table.Column<DateOnly>(type: "date", nullable: true, comment: "Начало действия записи"),
                    enddate = table.Column<DateOnly>(type: "date", nullable: true, comment: "Окончание действия записи"),
                    isactual = table.Column<int>(type: "integer", nullable: true, comment: "Статус актуальности адресного объекта ФИАС"),
                    isactive = table.Column<int>(type: "integer", nullable: true, comment: "Признак действующего адресного объекта")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addr_Objs", x => x.id);
                },
                comment: "Сведения классификатора адресообразующих элементов");

            migrationBuilder.CreateTable(
                name: "as_adm_hierarchy",
                schema: "fias",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Уникальный идентификатор записи. Ключевое поле"),
                    objectid = table.Column<long>(type: "bigint", nullable: true, comment: "Глобальный уникальный идентификатор объекта"),
                    parentobjid = table.Column<long>(type: "bigint", nullable: true, comment: "Идентификатор родительского объекта"),
                    changeid = table.Column<long>(type: "bigint", nullable: true, comment: "ID изменившей транзакции"),
                    regioncode = table.Column<string>(type: "text", nullable: true, comment: "Код региона"),
                    areacode = table.Column<string>(type: "text", nullable: true, comment: "Код района"),
                    citycode = table.Column<string>(type: "text", nullable: true, comment: "Код города"),
                    placecode = table.Column<string>(type: "text", nullable: true, comment: "Код населенного пункта"),
                    plancode = table.Column<string>(type: "text", nullable: true, comment: "Код ЭПС"),
                    streetcode = table.Column<string>(type: "text", nullable: true, comment: "Код улицы"),
                    previd = table.Column<long>(type: "bigint", nullable: true, comment: "Идентификатор записи связывания с предыдущей исторической записью"),
                    nextid = table.Column<long>(type: "bigint", nullable: true, comment: "Идентификатор записи связывания с последующей исторической записью"),
                    updatedate = table.Column<DateOnly>(type: "date", nullable: true, comment: "Дата внесения (обновления) записи"),
                    startdate = table.Column<DateOnly>(type: "date", nullable: true, comment: "Начало действия записи"),
                    enddate = table.Column<DateOnly>(type: "date", nullable: true, comment: "Окончание действия записи"),
                    isactive = table.Column<int>(type: "integer", nullable: true, comment: "Признак действующего адресного объекта"),
                    path = table.Column<string>(type: "text", nullable: true, comment: "Материализованный путь к объекту (полная иерархия)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adm_Hier", x => x.id);
                },
                comment: "Сведения по иерархии в административном делении");

            migrationBuilder.CreateTable(
                name: "as_houses",
                schema: "fias",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Уникальный идентификатор записи. Ключевое поле"),
                    objectid = table.Column<long>(type: "bigint", nullable: false, comment: "Глобальный уникальный идентификатор объекта типа INTEGER"),
                    objectguid = table.Column<Guid>(type: "uuid", nullable: false, comment: "Глобальный уникальный идентификатор адресного объекта типа UUID"),
                    changeid = table.Column<long>(type: "bigint", nullable: true, comment: "ID изменившей транзакции"),
                    housenum = table.Column<string>(type: "text", nullable: true, comment: "Основной номер дома"),
                    addnum1 = table.Column<string>(type: "text", nullable: true, comment: "Дополнительный номер дома 1"),
                    addnum2 = table.Column<string>(type: "text", nullable: true, comment: "Дополнительный номер дома 1"),
                    housetype = table.Column<int>(type: "integer", nullable: true, comment: "Основной тип дома"),
                    addtype1 = table.Column<int>(type: "integer", nullable: true, comment: "Дополнительный тип дома 1"),
                    addtype2 = table.Column<int>(type: "integer", nullable: true, comment: "Дополнительный тип дома 2"),
                    opertypeid = table.Column<int>(type: "integer", nullable: true, comment: "Статус действия над записью – причина появления записи"),
                    previd = table.Column<long>(type: "bigint", nullable: true, comment: "Идентификатор записи связывания с предыдущей исторической записью"),
                    nextid = table.Column<long>(type: "bigint", nullable: true, comment: "Идентификатор записи связывания с последующей исторической записью"),
                    updatedate = table.Column<DateOnly>(type: "date", nullable: true, comment: "Дата внесения (обновления) записи"),
                    startdate = table.Column<DateOnly>(type: "date", nullable: true, comment: "Начало действия записи"),
                    enddate = table.Column<DateOnly>(type: "date", nullable: true, comment: "Окончание действия записи"),
                    isactual = table.Column<int>(type: "integer", nullable: true, comment: "Статус актуальности адресного объекта ФИАС"),
                    isactive = table.Column<int>(type: "integer", nullable: true, comment: "Признак действующего адресного объекта")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.id);
                },
                comment: "Сведения по номерам домов улиц городов и населенных пунктов");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "as_addr_obj",
                schema: "fias");

            migrationBuilder.DropTable(
                name: "as_adm_hierarchy",
                schema: "fias");

            migrationBuilder.DropTable(
                name: "as_houses",
                schema: "fias");
        }
    }
}
