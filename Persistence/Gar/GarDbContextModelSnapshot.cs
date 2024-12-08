﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence.GarContext;

#nullable disable

namespace Persistence.Gar
{
    [DbContext(typeof(GarDbContext))]
    partial class GarDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Core.Models.Gar.AsAddrObj", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasComment("Уникальный идентификатор записи. Ключевое поле");

                    b.Property<long?>("Changeid")
                        .HasColumnType("bigint")
                        .HasColumnName("changeid")
                        .HasComment("ID изменившей транзакции");

                    b.Property<DateOnly?>("Enddate")
                        .HasColumnType("date")
                        .HasColumnName("enddate")
                        .HasComment("Окончание действия записи");

                    b.Property<int?>("Isactive")
                        .HasColumnType("integer")
                        .HasColumnName("isactive")
                        .HasComment("Признак действующего адресного объекта");

                    b.Property<int?>("Isactual")
                        .HasColumnType("integer")
                        .HasColumnName("isactual")
                        .HasComment("Статус актуальности адресного объекта ФИАС");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("level")
                        .HasComment("Уровень адресного объекта");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name")
                        .HasComment("Наименование");

                    b.Property<long?>("Nextid")
                        .HasColumnType("bigint")
                        .HasColumnName("nextid")
                        .HasComment("Идентификатор записи связывания с последующей исторической записью");

                    b.Property<Guid>("Objectguid")
                        .HasColumnType("uuid")
                        .HasColumnName("objectguid")
                        .HasComment("Глобальный уникальный идентификатор адресного объекта типа UUID");

                    b.Property<long>("Objectid")
                        .HasColumnType("bigint")
                        .HasColumnName("objectid")
                        .HasComment("Глобальный уникальный идентификатор адресного объекта типа INTEGER");

                    b.Property<int?>("Opertypeid")
                        .HasColumnType("integer")
                        .HasColumnName("opertypeid")
                        .HasComment("Статус действия над записью – причина появления записи");

                    b.Property<long?>("Previd")
                        .HasColumnType("bigint")
                        .HasColumnName("previd")
                        .HasComment("Идентификатор записи связывания с предыдущей исторической записью");

                    b.Property<DateOnly?>("Startdate")
                        .HasColumnType("date")
                        .HasColumnName("startdate")
                        .HasComment("Начало действия записи");

                    b.Property<string>("Typename")
                        .HasColumnType("text")
                        .HasColumnName("typename")
                        .HasComment("Краткое наименование типа объекта");

                    b.Property<DateOnly?>("Updatedate")
                        .HasColumnType("date")
                        .HasColumnName("updatedate")
                        .HasComment("Дата внесения (обновления) записи");

                    b.HasKey("Id")
                        .HasName("PK_Addr_Objs");

                    b.ToTable("as_addr_obj", "fias", t =>
                        {
                            t.HasComment("Сведения классификатора адресообразующих элементов");
                        });
                });

            modelBuilder.Entity("Core.Models.Gar.AsAdmHierarchy", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasComment("Уникальный идентификатор записи. Ключевое поле");

                    b.Property<string>("Areacode")
                        .HasColumnType("text")
                        .HasColumnName("areacode")
                        .HasComment("Код района");

                    b.Property<long?>("Changeid")
                        .HasColumnType("bigint")
                        .HasColumnName("changeid")
                        .HasComment("ID изменившей транзакции");

                    b.Property<string>("Citycode")
                        .HasColumnType("text")
                        .HasColumnName("citycode")
                        .HasComment("Код города");

                    b.Property<DateOnly?>("Enddate")
                        .HasColumnType("date")
                        .HasColumnName("enddate")
                        .HasComment("Окончание действия записи");

                    b.Property<int?>("Isactive")
                        .HasColumnType("integer")
                        .HasColumnName("isactive")
                        .HasComment("Признак действующего адресного объекта");

                    b.Property<long?>("Nextid")
                        .HasColumnType("bigint")
                        .HasColumnName("nextid")
                        .HasComment("Идентификатор записи связывания с последующей исторической записью");

                    b.Property<long?>("Objectid")
                        .HasColumnType("bigint")
                        .HasColumnName("objectid")
                        .HasComment("Глобальный уникальный идентификатор объекта");

                    b.Property<long?>("Parentobjid")
                        .HasColumnType("bigint")
                        .HasColumnName("parentobjid")
                        .HasComment("Идентификатор родительского объекта");

                    b.Property<string>("Path")
                        .HasColumnType("text")
                        .HasColumnName("path")
                        .HasComment("Материализованный путь к объекту (полная иерархия)");

                    b.Property<string>("Placecode")
                        .HasColumnType("text")
                        .HasColumnName("placecode")
                        .HasComment("Код населенного пункта");

                    b.Property<string>("Plancode")
                        .HasColumnType("text")
                        .HasColumnName("plancode")
                        .HasComment("Код ЭПС");

                    b.Property<long?>("Previd")
                        .HasColumnType("bigint")
                        .HasColumnName("previd")
                        .HasComment("Идентификатор записи связывания с предыдущей исторической записью");

                    b.Property<string>("Regioncode")
                        .HasColumnType("text")
                        .HasColumnName("regioncode")
                        .HasComment("Код региона");

                    b.Property<DateOnly?>("Startdate")
                        .HasColumnType("date")
                        .HasColumnName("startdate")
                        .HasComment("Начало действия записи");

                    b.Property<string>("Streetcode")
                        .HasColumnType("text")
                        .HasColumnName("streetcode")
                        .HasComment("Код улицы");

                    b.Property<DateOnly?>("Updatedate")
                        .HasColumnType("date")
                        .HasColumnName("updatedate")
                        .HasComment("Дата внесения (обновления) записи");

                    b.HasKey("Id")
                        .HasName("PK_Adm_Hier");

                    b.ToTable("as_adm_hierarchy", "fias", t =>
                        {
                            t.HasComment("Сведения по иерархии в административном делении");
                        });
                });

            modelBuilder.Entity("Core.Models.Gar.AsHouses", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasComment("Уникальный идентификатор записи. Ключевое поле");

                    b.Property<string>("Addnum1")
                        .HasColumnType("text")
                        .HasColumnName("addnum1")
                        .HasComment("Дополнительный номер дома 1");

                    b.Property<string>("Addnum2")
                        .HasColumnType("text")
                        .HasColumnName("addnum2")
                        .HasComment("Дополнительный номер дома 1");

                    b.Property<int?>("Addtype1")
                        .HasColumnType("integer")
                        .HasColumnName("addtype1")
                        .HasComment("Дополнительный тип дома 1");

                    b.Property<int?>("Addtype2")
                        .HasColumnType("integer")
                        .HasColumnName("addtype2")
                        .HasComment("Дополнительный тип дома 2");

                    b.Property<long?>("Changeid")
                        .HasColumnType("bigint")
                        .HasColumnName("changeid")
                        .HasComment("ID изменившей транзакции");

                    b.Property<DateOnly?>("Enddate")
                        .HasColumnType("date")
                        .HasColumnName("enddate")
                        .HasComment("Окончание действия записи");

                    b.Property<string>("Housenum")
                        .HasColumnType("text")
                        .HasColumnName("housenum")
                        .HasComment("Основной номер дома");

                    b.Property<int?>("Housetype")
                        .HasColumnType("integer")
                        .HasColumnName("housetype")
                        .HasComment("Основной тип дома");

                    b.Property<int?>("Isactive")
                        .HasColumnType("integer")
                        .HasColumnName("isactive")
                        .HasComment("Признак действующего адресного объекта");

                    b.Property<int?>("Isactual")
                        .HasColumnType("integer")
                        .HasColumnName("isactual")
                        .HasComment("Статус актуальности адресного объекта ФИАС");

                    b.Property<long?>("Nextid")
                        .HasColumnType("bigint")
                        .HasColumnName("nextid")
                        .HasComment("Идентификатор записи связывания с последующей исторической записью");

                    b.Property<Guid>("Objectguid")
                        .HasColumnType("uuid")
                        .HasColumnName("objectguid")
                        .HasComment("Глобальный уникальный идентификатор адресного объекта типа UUID");

                    b.Property<long>("Objectid")
                        .HasColumnType("bigint")
                        .HasColumnName("objectid")
                        .HasComment("Глобальный уникальный идентификатор объекта типа INTEGER");

                    b.Property<int?>("Opertypeid")
                        .HasColumnType("integer")
                        .HasColumnName("opertypeid")
                        .HasComment("Статус действия над записью – причина появления записи");

                    b.Property<long?>("Previd")
                        .HasColumnType("bigint")
                        .HasColumnName("previd")
                        .HasComment("Идентификатор записи связывания с предыдущей исторической записью");

                    b.Property<DateOnly?>("Startdate")
                        .HasColumnType("date")
                        .HasColumnName("startdate")
                        .HasComment("Начало действия записи");

                    b.Property<DateOnly?>("Updatedate")
                        .HasColumnType("date")
                        .HasColumnName("updatedate")
                        .HasComment("Дата внесения (обновления) записи");

                    b.HasKey("Id")
                        .HasName("PK_Houses");

                    b.ToTable("as_houses", "fias", t =>
                        {
                            t.HasComment("Сведения по номерам домов улиц городов и населенных пунктов");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
