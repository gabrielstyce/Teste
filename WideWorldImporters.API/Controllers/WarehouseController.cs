using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WideWorldImporters.API.Models;

namespace WideWorldImporters.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WarehouseController : ControllerBase
    {
        protected readonly ILogger Logger;
        protected readonly WideWorldImportersDbContext _context;

        public WarehouseController(ILogger<WarehouseController> logger, WideWorldImportersDbContext dbContext)
        {
            Logger = logger;
            _context = dbContext;
        }


        // GET
        // api/v1/Warehouse/StockItem

        /// <summary>
        /// Retorna todos os produtos(['StockItems']) do banco.
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageNumber">Page number</param>
        /// <param name="lastEditedBy">Last edit by (user id)</param>
        /// <param name="colorID">Color id</param>
        /// <param name="embalagemID">Embalagem id</param>
        /// <param name="fornecedorID">Fornecedor id</param>
        /// <param name="precoUnidade">id Preco por unidade</param>
        /// <returns>Retorna um stock items list</returns>
        /// <response code="200">Retorna o stock items list</response>
        /// <response code="500">Se tiver um internal server error</response>
        [HttpGet("StockItemID")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetStockItemsAsync(int pageSize = 10, int pageNumber = 1, int? lastEditedBy = null,
                                                            int? colorID = null, int? outerPackageID = null, int? supplierID = null, int? unitPackageID = null)
        {
            Logger?.LogDebug("'{0}' foi chamada", nameof(GetStockItemsAsync));

            var response = new PagedResponse<StockItem>();

            try
            {
                // Get da query "proposta" do repositório
                var query = _context.GetStockItems();

                // Seta o valor das páginas
                response.PageSize = pageSize;
                response.PageNumber = pageNumber;

                // Get o total de linhas
                response.ItemsCount = await query.CountAsync();

                // Get a página expecífica do banco
                response.Model = await query.Paging(pageSize, pageNumber).ToListAsync();

                response.Message = string.Format("Página {0} de {1}, Total de produtos: {2}.", pageNumber, response.PageCount, response.ItemsCount);

                Logger?.LogInformation("Os itens foram trazidos com sucesso.");
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "Houve um erro interno, por favor contate o suporte.";

                Logger?.LogCritical("Houve um erro na chamada '{0}' invocando: {1} ", nameof(GetStockItemsAsync), ex);
            }

            return response.ToHttpResponse();
        }

        // GET
        // api/v1/Warehouse/StockItem/5

        /// <summary>
        /// Retorna um produto(['StockItem']) expecífico pelo ID.
        /// </summary>
        /// <param name="id">Stock item id</param>
        /// <returns>Retorna um stock item</returns>
        /// <response code="200">Retorna o produto</response>
        /// <response code="404">Se o produto não existir</response>
        /// <response code="500">Se houve um erro interno</response>
        [HttpGet("StockItem/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetStockItemAsync(int id)
        {
            Logger?.LogDebug("'{0}' foi chamada.", nameof(GetStockItemAsync));

            var response = new SingleResponse<StockItem>();


            try
            {
                //Get o stock item pelo id
                response.Model = await _context.GetStockItemAsync(new StockItem(id));
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "Houve um erro interno, por favor contate o suporte.";

                Logger?.LogCritical("Houve um erro na chamada '{0}' invocando: {1} ", nameof(GetStockItemAsync), ex);
            }

            return response.ToHttpResponse();
        }

        // POST
        // api/v1/Warehouse/StockItem/

        /// <summary>
        /// Cria um novo produto(['StockItem']).
        /// </summary>
        /// <param name="request">Request model</param>
        /// <returns>Retorna o novo stock item</returns>
        /// <response code="200">Retorna o stock items list criado</response>
        /// <response code="201">Resposta da criação do stock item</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Se houve um erro interno</response>
        [HttpPost("StockItem")]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostStockItemAsync([FromBody]PostStockItemsRequest request)
        {
            Logger?.LogDebug("'{0}' foi chamada", nameof(PostStockItemAsync));

            var response = new SingleResponse<StockItem>();

            try
            {
                var existingEntity = await _context.GetStockItemsByStockItemNameAsync(new StockItem { StockItemName = request.StockItemName });

                if (existingEntity != null) ModelState.AddModelError("StockItemName", "Stock Item Name já existe!");

                if (!ModelState.IsValid) return BadRequest();

                // Setando a entidade pelo request da model
                var entity = request.ToEntity();
                // Add no BD
                _context.Add(entity);
                // Salva
                await _context.SaveChangesAsync();

                // Seta a entidade para o model de resposta
                response.Model = entity;
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "Houve um erro interno, por favor contate o suporte.";

                Logger?.LogCritical("Houve um erro na chamada '{0}' invocando: {1} ", nameof(PostStockItemAsync), ex);
            }

            return response.ToHttpResponse();
        }

        // PUT
        // api/v1/Warehouse/StockItem/5

        /// <summary>
        /// Faz o update de um produto(['StockItem']) existente.
        /// </summary>
        /// <param name="id">Stock item ID</param>
        /// <param name="request">Request model</param>
        /// <returns>Retorna o update do stock item</returns>
        /// <response code="200">Se o produto for atualizado com sucesso</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Se houve um erro interno</response>
        [HttpPut("StockItem/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutStockItemAsync(int id, [FromBody]PutStockItemsRequest request)
        {
            Logger?.LogDebug("'{0}' foi chamada", nameof(PutStockItemAsync));

            var response = new Response();

            try
            {
                // Get do produto pelo id
                var entity = await _context.GetStockItemsByStockItemNameAsync(new StockItem(id));
                // Valida existente
                if (entity == null) return NotFound();

                // Set do update
                entity.StockItemName = request.StockItemName;
                entity.FornecedorID = request.FornecedorID;
                entity.ColorID = request.ColorID;
                entity.PrecoUnidade = request.PrecoUnidade;

                // Update e save no BD
                _context.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "Houve um erro interno, por favor contate o suporte.";

                Logger?.LogCritical("Houve um erro na chamada '{0}' invocando: {1} ", nameof(PutStockItemAsync), ex);
            }

            return response.ToHttpResponse();
        }
    }
}