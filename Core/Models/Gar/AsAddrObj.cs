using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.Gar;

/// <summary>
///     Сведения классификатора адресообразующих элементов
/// </summary>
[Table("as_addr_obj", Schema = "fias")]
public class AsAddrObj
{
    /// <summary>
    ///     Уникальный идентификатор записи. Ключевое поле
    /// </summary>
    [Key]
    [Column("id")]
    public long Id { get; set; }

    /// <summary>
    ///     Глобальный уникальный идентификатор адресного объекта типа INTEGER
    /// </summary>
    [Column("objectid")]
    public long Objectid { get; set; }

    /// <summary>
    ///     Глобальный уникальный идентификатор адресного объекта типа UUID
    /// </summary>
    [Column("objectguid")]
    public Guid Objectguid { get; set; }

    /// <summary>
    ///     ID изменившей транзакции
    /// </summary>
    [Column("changeid")]
    public long? Changeid { get; set; }

    /// <summary>
    ///     Наименование
    /// </summary>
    [Column("name")]
    public string Name { get; set; } = null!;

    /// <summary>
    ///     Краткое наименование типа объекта
    /// </summary>
    [Column("typename")]
    public string? Typename { get; set; }

    /// <summary>
    ///     Уровень адресного объекта
    /// </summary>
    [Column("level")]
    public string Level { get; set; } = null!;

    /// <summary>
    ///     Статус действия над записью – причина появления записи
    /// </summary>
    [Column("opertypeid")]
    public int? Opertypeid { get; set; }

    /// <summary>
    ///     Идентификатор записи связывания с предыдущей исторической записью
    /// </summary>
    [Column("previd")]
    public long? Previd { get; set; }

    /// <summary>
    ///     Идентификатор записи связывания с последующей исторической записью
    /// </summary>
    [Column("nextid")]
    public long? Nextid { get; set; }

    /// <summary>
    ///     Дата внесения (обновления) записи
    /// </summary>
    [Column("updatedate")]
    public DateOnly? Updatedate { get; set; }

    /// <summary>
    ///     Начало действия записи
    /// </summary>
    [Column("startdate")]
    public DateOnly? Startdate { get; set; }

    /// <summary>
    ///     Окончание действия записи
    /// </summary>
    [Column("enddate")]
    public DateOnly? Enddate { get; set; }

    /// <summary>
    ///     Статус актуальности адресного объекта ФИАС
    /// </summary>
    [Column("isactual")]
    public int? Isactual { get; set; }

    /// <summary>
    ///     Признак действующего адресного объекта
    /// </summary>
    [Column("isactive")]
    public int? Isactive { get; set; }
}