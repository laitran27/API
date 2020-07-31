using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestApi.Models;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ApiController : ControllerBase
    {
        protected readonly ILogger Logger;
        protected readonly WideWorldImportersDbContext DbContext;

        public ApiController(ILogger<ApiController> logger, WideWorldImportersDbContext dbContext)
        {
            Logger = logger;
            DbContext = dbContext;
        }

        // GET
        // /api/api/GetAllChannels
        /// <summary>
        /// Retrieves stock items
        /// </summary>
        /// <returns>A response with stock items list</returns>
        /// <response code="200">Returns the stock items list</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("GetAllChannels")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public   IQueryable<AR_Channel>  GetShopTypesAll()
        {
            //var response = new AR_ShopType();
            var query = DbContext.AR_Channels.AsQueryable();
            return  query;
        }

        // GET
        // api/Api/ShopType

        /// <summary>
        /// Retrieves stock items
        /// </summary>
        /// <param name="options"></param>
        /// <returns>A response with stock items list</returns>
        /// <response code="200">Returns the stock items list</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("ShopType")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public  object GetShopTypes(DataSourceLoadOptions options)
        {
            //Logger?.LogDebug("'{0}' has been invoked", nameof(GetShopTypes));

            var response = new PagedResponse<AR_ShopType>();

            try
            {
                var data = DataSourceLoader.Load(DbContext.AR_ShopTypes.AsQueryable(), options);
      
                //return data;
                return data;

     
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(GetShopTypes), ex);
            }

            return response.ToHttpResponse();
        }

        [HttpGet("Channels")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public object GetChanels(DataSourceLoadOptions options)
        {
            var response = new PagedResponse<AR_Channel>();
            try
            {
                var data = DataSourceLoader.Load(DbContext.AR_Channels.AsQueryable(), options);
                return data;
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(GetChanels), ex);
            }
            return response.ToHttpResponse();
        }

        // GET
        // api/ShopType/5

        /// <summary>
        /// Retrieves a stock item by ID
        /// </summary>
        /// <param code="code">Stock item id</param>
        /// <returns>A response with stock item</returns>
        /// <response code="200">Returns the stock items list</response>
        /// <response code="404">If stock item is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("ShopType/{code}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<AR_ShopType> GetShopTypeByCode(string code)
        {
            var response = await DbContext.GetShopTypesAsync(new AR_ShopType(code));
            return response;
        }

        // POST
        // api/ShopType/

        /// <summary>
        /// Creates a new stock item
        /// </summary>
        /// <param name="request">Request model</param>
        /// <returns>A response with new ShopType</returns>
        /// <response code="200">Returns the stock items list</response>
        /// <response code="201">A response as creation of stock item</response>
        /// <response code="400">For bad request</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("ShopType")]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostShopTypeAsync([FromBody]PostAR_ShopTypeRequests request)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(PostShopTypeAsync));

            var response = new SingleResponse<AR_ShopType>();

            try
            {
                var existingEntity = await DbContext
                    .GetShopTypesByNameAsync(new AR_ShopType { Descr = request.Descr });

                if (existingEntity != null)
                    ModelState.AddModelError("StockItemName", "Shop Type name already exists");

                if (!ModelState.IsValid)
                    return BadRequest();

                // Create entity from request model
                var entity = request.ToEntity();

                // Add entity to repository
                DbContext.Add(entity);

                // Save entity in database
                await DbContext.SaveChangesAsync();

                // Set the entity to response model
                response.Model = entity;
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";
                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(PostShopTypeAsync), ex);
            }

            return response.ToHttpResponse();
        }

        // PUT
        // api/ShopType/5

        /// <summary>
        /// Updates an existing ShopType
        /// </summary>
        /// <param name="code">Stock item ID</param>
        /// <param name="request">Request model</param>
        /// <returns>A response as update stock item result</returns>
        /// <response code="200">If stock item was updated successfully</response>
        /// <response code="400">For bad request</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPut("ShopType/{code}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutShopTypeAsync(string code, [FromBody]PutAR_ShopTypeRequest request)
         {
            Logger?.LogDebug("'{0}' has been invoked", nameof(PutShopTypeAsync));

            var response = new Response();

            try
            {
                var entity = await DbContext.GetShopTypesAsync(new AR_ShopType(code));
                if (entity == null)
                {
                    //return NotFound();
                    entity = request.ToEntity();
                    DbContext.Add(entity);
                }
                else
                {
                    entity.Descr = request.Descr;
                    DbContext.Update(entity);
                }
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";
                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(PutShopTypeAsync), ex);
            }
            return response.ToHttpResponse();
        }

        // DELETE
        // api/ShopType/5

        /// <summary>
        /// Deletes an existing SHOPTYPE
        /// </summary>
        /// <param name="code">Stock item ID</param>
        /// <returns>A response as delete stock item result</returns>
        /// <response code="200">If stock item was deleted successfully</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpDelete("ShopType/{code}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteShopTypeAsync(string code)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(DeleteShopTypeAsync));

            var response = new Response();

            try
            {
                // Get stock item by id
                var entity = await DbContext.GetShopTypesAsync(new AR_ShopType(code));

                // Validate if entity exists
                if (entity == null)
                    return NotFound();

                // Remove entity from repository
                DbContext.Remove(entity);

                // Delete entity in database
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(DeleteShopTypeAsync), ex);
            }

            return response.ToHttpResponse();
        }
    }
}
