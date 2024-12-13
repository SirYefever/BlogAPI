using Core.Models.Gar;
using Microsoft.EntityFrameworkCore;

namespace Persistence.GarContext;

public partial class GarDbContext : DbContext
{
    public GarDbContext()
    {
    }

    public GarDbContext(DbContextOptions<GarDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AsAddrObj> AsAddrObj { get; set; }

    public virtual DbSet<AsAdmHierarchy> AsAdmHierarchy { get; set; }

    public virtual DbSet<AsHouses> AsHouses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=Gar;Username=postgres;Password=qwerty");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AsAddrObj>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Addr_Objs");

            entity.ToTable("as_addr_obj", "fias",
                tb => tb.HasComment("Сведения классификатора адресообразующих элементов"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("Уникальный идентификатор записи. Ключевое поле");
            entity.Property(e => e.Changeid).HasComment("ID изменившей транзакции");
            entity.Property(e => e.Enddate).HasComment("Окончание действия записи");
            entity.Property(e => e.Isactive).HasComment("Признак действующего адресного объекта");
            entity.Property(e => e.Isactual).HasComment("Статус актуальности адресного объекта ФИАС");
            entity.Property(e => e.Level).HasComment("Уровень адресного объекта");
            entity.Property(e => e.Name).HasComment("Наименование");
            entity.Property(e => e.Nextid)
                .HasComment("Идентификатор записи связывания с последующей исторической записью");
            entity.Property(e => e.Objectguid)
                .HasComment("Глобальный уникальный идентификатор адресного объекта типа UUID");
            entity.Property(e => e.Objectid)
                .HasComment("Глобальный уникальный идентификатор адресного объекта типа INTEGER");
            entity.Property(e => e.Opertypeid).HasComment("Статус действия над записью – причина появления записи");
            entity.Property(e => e.Previd)
                .HasComment("Идентификатор записи связывания с предыдущей исторической записью");
            entity.Property(e => e.Startdate).HasComment("Начало действия записи");
            entity.Property(e => e.Typename).HasComment("Краткое наименование типа объекта");
            entity.Property(e => e.Updatedate).HasComment("Дата внесения (обновления) записи");
        });

        modelBuilder.Entity<AsAdmHierarchy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Adm_Hier");

            entity.ToTable("as_adm_hierarchy", "fias",
                tb => tb.HasComment("Сведения по иерархии в административном делении"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("Уникальный идентификатор записи. Ключевое поле");
            entity.Property(e => e.Areacode).HasComment("Код района");
            entity.Property(e => e.Changeid).HasComment("ID изменившей транзакции");
            entity.Property(e => e.Citycode).HasComment("Код города");
            entity.Property(e => e.Enddate).HasComment("Окончание действия записи");
            entity.Property(e => e.Isactive).HasComment("Признак действующего адресного объекта");
            entity.Property(e => e.Nextid)
                .HasComment("Идентификатор записи связывания с последующей исторической записью");
            entity.Property(e => e.Objectid).HasComment("Глобальный уникальный идентификатор объекта");
            entity.Property(e => e.Parentobjid).HasComment("Идентификатор родительского объекта");
            entity.Property(e => e.Path).HasComment("Материализованный путь к объекту (полная иерархия)");
            entity.Property(e => e.Placecode).HasComment("Код населенного пункта");
            entity.Property(e => e.Plancode).HasComment("Код ЭПС");
            entity.Property(e => e.Previd)
                .HasComment("Идентификатор записи связывания с предыдущей исторической записью");
            entity.Property(e => e.Regioncode).HasComment("Код региона");
            entity.Property(e => e.Startdate).HasComment("Начало действия записи");
            entity.Property(e => e.Streetcode).HasComment("Код улицы");
            entity.Property(e => e.Updatedate).HasComment("Дата внесения (обновления) записи");
        });

        modelBuilder.Entity<AsHouses>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Houses");

            entity.ToTable("as_houses", "fias",
                tb => tb.HasComment("Сведения по номерам домов улиц городов и населенных пунктов"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("Уникальный идентификатор записи. Ключевое поле");
            entity.Property(e => e.Addnum1).HasComment("Дополнительный номер дома 1");
            entity.Property(e => e.Addnum2).HasComment("Дополнительный номер дома 1");
            entity.Property(e => e.Addtype1).HasComment("Дополнительный тип дома 1");
            entity.Property(e => e.Addtype2).HasComment("Дополнительный тип дома 2");
            entity.Property(e => e.Changeid).HasComment("ID изменившей транзакции");
            entity.Property(e => e.Enddate).HasComment("Окончание действия записи");
            entity.Property(e => e.Housenum).HasComment("Основной номер дома");
            entity.Property(e => e.Housetype).HasComment("Основной тип дома");
            entity.Property(e => e.Isactive).HasComment("Признак действующего адресного объекта");
            entity.Property(e => e.Isactual).HasComment("Статус актуальности адресного объекта ФИАС");
            entity.Property(e => e.Nextid)
                .HasComment("Идентификатор записи связывания с последующей исторической записью");
            entity.Property(e => e.Objectguid)
                .HasComment("Глобальный уникальный идентификатор адресного объекта типа UUID");
            entity.Property(e => e.Objectid).HasComment("Глобальный уникальный идентификатор объекта типа INTEGER");
            entity.Property(e => e.Opertypeid).HasComment("Статус действия над записью – причина появления записи");
            entity.Property(e => e.Previd)
                .HasComment("Идентификатор записи связывания с предыдущей исторической записью");
            entity.Property(e => e.Startdate).HasComment("Начало действия записи");
            entity.Property(e => e.Updatedate).HasComment("Дата внесения (обновления) записи");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}