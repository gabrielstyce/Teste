using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WideWorldImporters.API.Models
{

    //Entity
    public partial class StockItem
    {
        public StockItem()
        { }

        public StockItem(int? stockItemID)
        {
            StockItemID = stockItemID;
        }

        public int? StockItemID { get; set; }
        public string StockItemName { get; set; }
        public int? FornecedorID { get; set; }
        public int? ColorID { get; set; }
        public int? PacoteUnidadeID { get; set; }
        public int? EmbalagemID { get; set; }
        public string Marca { get; set; }
        public string Tamanho { get; set; }
        public int? LeadTimeDays { get; set; }
        public int? QuantityPerOuter { get; set; }
        public bool? Refrigeracao { get; set; }
        public string CodBarras { get; set; }
        public decimal? Imposto { get; set; }
        public decimal? PrecoUnidade { get; set; }
        public decimal? RecommendedRetailPrice { get; set; }
        public decimal? TypicalWeightPerUnit { get; set; }
        public string MarketingComments { get; set; }
        public string InternalComments { get; set; }
        public string CustomFields { get; set; }
        public string Tags { get; set; }
        public string SearchDetails { get; set; }
        public int? LastEditedBy { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? UltimaEdicao { get; set; }
    }

    public class StockItemsConfiguration : IEntityTypeConfiguration<StockItem>
    {
        // Usando fluent API para configurar a MasterTable do BD
        public void Configure(EntityTypeBuilder<StockItem> builder)
        {
            // Seta o nome e o esquema da tabela
            builder.ToTable("StockItems", "Warehouse");

            // Seta a 'key' da entidade
            builder.HasKey(p => p.StockItemID);

            // Seta a configuração das colunas
            builder.Property(p => p.StockItemName).HasColumnType("nvarchar(200)").IsRequired();
            builder.Property(p => p.FornecedorID).HasColumnType("int").IsRequired();
            builder.Property(p => p.ColorID).HasColumnType("int");
            builder.Property(p => p.PacoteUnidadeID).HasColumnType("int").IsRequired();
            builder.Property(p => p.EmbalagemID).HasColumnType("int").IsRequired();
            builder.Property(p => p.Marca).HasColumnType("nvarchar(100)");
            builder.Property(p => p.Tamanho).HasColumnType("nvarchar(40)");
            builder.Property(p => p.LeadTimeDays).HasColumnType("int").IsRequired();
            builder.Property(p => p.QuantityPerOuter).HasColumnType("int").IsRequired();
            builder.Property(p => p.Refrigeracao).HasColumnType("bit").IsRequired();
            builder.Property(p => p.CodBarras).HasColumnType("nvarchar(100)");
            builder.Property(p => p.Imposto).HasColumnType("decimal(18, 3)").IsRequired();
            builder.Property(p => p.PrecoUnidade).HasColumnType("decimal(18, 2)").IsRequired();
            builder.Property(p => p.RecommendedRetailPrice).HasColumnType("decimal(18, 2)");
            builder.Property(p => p.TypicalWeightPerUnit).HasColumnType("decimal(18, 2)").IsRequired();
            builder.Property(p => p.MarketingComments).HasColumnType("nvarchar(max)");
            builder.Property(p => p.InternalComments).HasColumnType("nvarchar(max)");
            builder.Property(p => p.CustomFields).HasColumnType("nvarchar(max)");
            builder.Property(p => p.LastEditedBy).HasColumnType("int").IsRequired();

            // Configurando e iterando valor padrão da coluna 'StockItemID'
            builder
                .Property(p => p.StockItemID)
                .HasColumnType("int")
                .IsRequired()
                .HasDefaultValueSql("NEXT VALUE FOR [Sequences].[StockItemID]");

            // Computando as colunas (coluna computada é uma coluna virtual que não está fisicamente armazenada na tabela)
            builder
                .Property(p => p.Tags)
                .HasColumnType("nvarchar(max)")
                .HasComputedColumnSql("json_query([CustomFields],N'$.Tags')");

            builder
                .Property(p => p.SearchDetails)
                .HasColumnType("nvarchar(max)")
                .HasComputedColumnSql("concat([StockItemName],N' ',[MarketingComments])");

            // Colunas com valores gerados automaticamente ao add ou update 
            builder
                .Property(p => p.DataCadastro)
                .HasColumnType("datetime2")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder
                .Property(p => p.UltimaEdicao)
                .HasColumnType("datetime2")
                .IsRequired()
                .ValueGeneratedOnAddOrUpdate();
        }
    }

    public class WideWorldImportersDbContext : DbContext
    {
        public WideWorldImportersDbContext(DbContextOptions<WideWorldImportersDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Aplicando a configuração à entidade
            modelBuilder
                .ApplyConfiguration(new StockItemsConfiguration());

            base.OnModelCreating(modelBuilder);

        }
        public DbSet<StockItem> StockItems { get; set; }
    }
}
