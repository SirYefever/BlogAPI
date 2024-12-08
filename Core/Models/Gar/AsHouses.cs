using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.Gar;

/// <summary>
/// Сведения по номерам домов улиц городов и населенных пунктов
/// </summary>
[Table("as_houses", Schema = "fias")]
public partial class AsHouses
{
    /// <summary>
    /// Уникальный идентификатор записи. Ключевое поле
    /// </summary>
    [Key]
    [Column("id")]
    public long Id { get; set; }

    /// <summary>
    /// Глобальный уникальный идентификатор объекта типа INTEGER
    /// </summary>
    [Column("objectid")]
    public long Objectid { get; set; }

    /// <summary>
    /// Глобальный уникальный идентификатор адресного объекта типа UUID
    /// </summary>
    [Column("objectguid")]
    public Guid Objectguid { get; set; }

    /// <summary>
    /// ID изменившей транзакции
    /// </summary>
    [Column("changeid")]
    public long? Changeid { get; set; }

    /// <summary>
    /// Основной номер дома
    /// </summary>
    [Column("housenum")]
    public string? Housenum { get; set; }

    /// <summary>
    /// Дополнительный номер дома 1
    /// </summary>
    [Column("addnum1")]
    public string? Addnum1 { get; set; }

    /// <summary>
    /// Дополнительный номер дома 1
    /// </summary>
    [Column("addnum2")]
    public string? Addnum2 { get; set; }

    /// <summary>
    /// Основной тип дома
    /// </summary>
    [Column("housetype")]
    public int? Housetype { get; set; }

    /// <summary>
    /// Дополнительный тип дома 1
    /// </summary>
    [Column("addtype1")]
    public int? Addtype1 { get; set; }

    /// <summary>
    /// Дополнительный тип дома 2
    /// </summary>
    [Column("addtype2")]
    public int? Addtype2 { get; set; }

    /// <summary>
    /// Статус действия над записью – причина появления записи
    /// </summary>
    [Column("opertypeid")]
    public int? Opertypeid { get; set; }

    /// <summary>
    /// Идентификатор записи связывания с предыдущей исторической записью
    /// </summary>
    [Column("previd")]
    public long? Previd { get; set; }

    /// <summary>
    /// Идентификатор записи связывания с последующей исторической записью
    /// </summary>
    [Column("nextid")]
    public long? Nextid { get; set; }

    /// <summary>
    /// Дата внесения (обновления) записи
    /// </summary>
    [Column("updatedate")]
    public DateOnly? Updatedate { get; set; }

    /// <summary>
    /// Начало действия записи
    /// </summary>
    [Column("startdate")]
    public DateOnly? Startdate { get; set; }

    /// <summary>
    /// Окончание действия записи
    /// </summary>
    [Column("enddate")]
    public DateOnly? Enddate { get; set; }

    /// <summary>
    /// Статус актуальности адресного объекта ФИАС
    /// </summary>
    [Column("isactual")]
    public int? Isactual { get; set; }

    /// <summary>
    /// Признак действующего адресного объекта
    /// </summary>
    [Column("isactive")]
    public int? Isactive { get; set; }
}
