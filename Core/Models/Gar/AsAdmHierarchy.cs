using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.Gar;

/// <summary>
/// Сведения по иерархии в административном делении
/// </summary>
[Table("as_adm_hierarchy", Schema = "fias")]
public partial class AsAdmHierarchy
{
    /// <summary>
    /// Уникальный идентификатор записи. Ключевое поле
    /// </summary>
    [Key]
    [Column("id")]
    public long Id { get; set; }

    /// <summary>
    /// Глобальный уникальный идентификатор объекта
    /// </summary>
    [Column("objectid")]
    public long? Objectid { get; set; }

    /// <summary>
    /// Идентификатор родительского объекта
    /// </summary>
    [Column("parentobjid")]
    public long? Parentobjid { get; set; }

    /// <summary>
    /// ID изменившей транзакции
    /// </summary>
    [Column("changeid")]
    public long? Changeid { get; set; }

    /// <summary>
    /// Код региона
    /// </summary>
    [Column("regioncode")]
    public string? Regioncode { get; set; }

    /// <summary>
    /// Код района
    /// </summary>
    [Column("areacode")]
    public string? Areacode { get; set; }

    /// <summary>
    /// Код города
    /// </summary>
    [Column("citycode")]
    public string? Citycode { get; set; }

    /// <summary>
    /// Код населенного пункта
    /// </summary>
    [Column("placecode")]
    public string? Placecode { get; set; }

    /// <summary>
    /// Код ЭПС
    /// </summary>
    [Column("plancode")]
    public string? Plancode { get; set; }

    /// <summary>
    /// Код улицы
    /// </summary>
    [Column("streetcode")]
    public string? Streetcode { get; set; }

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
    /// Признак действующего адресного объекта
    /// </summary>
    [Column("isactive")]
    public int? Isactive { get; set; }

    /// <summary>
    /// Материализованный путь к объекту (полная иерархия)
    /// </summary>
    [Column("path")]
    public string? Path { get; set; }
}
