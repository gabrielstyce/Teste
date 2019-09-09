using System;
using System.ComponentModel.DataAnnotations;

namespace WideWorldImporters.API.Models
{
    public class PostStockItemsRequest
    {
        [Key]
        public int? StockItemID { get; set; }
        [Required]
        [StringLength(200)]
        public string StockItemName { get; set; }
        [Required]
        public int? FornecedorID { get; set; }
        public int? ColorID { get; set; }
        [Required]
        public int? PacoteUnidadeID { get; set; }
        [Required]
        public int? EmbalagemID { get; set; }
        [StringLength(100)]
        public string Marca { get; set; }
        [StringLength(40)]
        public string Tamanho { get; set; }
        [Required]
        public int? LeadTimeDays { get; set; }
        [Required]
        public int? QuantityPerOuter { get; set; }
        [Required]
        public bool? Refrigeracao { get; set; }
        [StringLength(100)]
        public string CodBarras { get; set; }
        [Required]
        public decimal? Imposto { get; set; }
        [Required]
        public decimal? PrecoUnidade { get; set; }
        public decimal? RecommendedRetailPrice { get; set; }
        [Required]
        public decimal? TypicalWeightPerUnit { get; set; }
        public string MarketingComments { get; set; }
        public string InternalComments { get; set; }
        public string CustomFields { get; set; }
        public string Tags { get; set; }
        [Required]
        public string SearchDetails { get; set; }
        [Required]
        public int? LastEditedBy { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? UltimaEdicao { get; set; }
    }

    public class PutStockItemsRequest
    {
        [Required]
        [StringLength(200)]
        public string StockItemName { get; set; }
        [Required]
        public int? FornecedorID { get; set; }
        public int? ColorID { get; set; }
        [Required]
        public decimal? PrecoUnidade { get; set; }
    }

    public static class Extensions
    {
        public static StockItem ToEntity(this PostStockItemsRequest request)
            => new StockItem
            {
                StockItemID = request.StockItemID,
                StockItemName = request.StockItemName,
                FornecedorID = request.FornecedorID,
                ColorID = request.ColorID,
                PacoteUnidadeID = request.PacoteUnidadeID,
                EmbalagemID = request.EmbalagemID,
                Marca = request.Marca,
                Tamanho = request.Tamanho,
                LeadTimeDays = request.LeadTimeDays,
                QuantityPerOuter = request.QuantityPerOuter,
                Refrigeracao = request.Refrigeracao,
                CodBarras = request.CodBarras,
                Imposto = request.Imposto,
                PrecoUnidade = request.PrecoUnidade,
                RecommendedRetailPrice = request.RecommendedRetailPrice,
                TypicalWeightPerUnit = request.TypicalWeightPerUnit,
                MarketingComments = request.MarketingComments,
                InternalComments = request.InternalComments,
                CustomFields = request.CustomFields,
                Tags = request.Tags,
                SearchDetails = request.SearchDetails,
                LastEditedBy = request.LastEditedBy,
                DataCadastro = request.DataCadastro,
                UltimaEdicao = request.UltimaEdicao
            };
    }
}
