using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApi.Models;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        protected readonly ILogger Logger;
        protected readonly WideWorldImportersDbContext DbContext;

        public BatchController(ILogger<ApiController> logger, WideWorldImportersDbContext dbContext)
        {
            Logger = logger;
            DbContext = dbContext;
        }
        // GET
        // api/Batch/Batches
        /// <summary>
        /// Retrieves Batch items
        /// </summary>
        /// <param name="options"></param>
        /// <returns>A response with Batch items list</returns>
        /// <response code="200">Returns the stock items list</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("Batches")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public object GetBatches(DataSourceLoadOptions options)
        {
            var data = DataSourceLoader.Load(DbContext.Batches.Where(x => x.Module == "IN").OrderBy(x => x.BranchID).AsQueryable(), options);
            return data;
        }

        [HttpGet("Company")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public object GetCompanys()
        {
            var data = DbContext.VS_Company.AsQueryable();
            return data;
        }


        [HttpGet("BatchesCombo")]
        [ProducesResponseType(200)] 
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public object GetBatchesCombo(string branchID)
        {
            var data = DbContext.Batches.Where(x => x.Module == "IN" && x.BranchID == branchID).AsQueryable();
            return data;
        }

        [HttpGet("UnitConversionCombo")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public object GetUnitConversionCombo(string invtID)
        {
            var data = DbContext.IN_UnitConversion.Where(x => x.InvtID == invtID).AsQueryable();
            return data;
        }

        [HttpGet("InvtCombo")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public object GetInvt(string branchID)
        {
            var data = DbContext.IN_Inventory.AsQueryable();
            return data;
        }

        [HttpGet("Trans")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public object GetTrans(string branchID, string batNbr)
        {
            var query = DbContext.IN_Trans.Join(DbContext.IN_Inventory, x => x.InvtID, y => y.InvtID,
                        (x, y) => new {x.Qty, y.Descr, x.InvtID, x.BranchID, x.BatNbr, x.TranAmt, x.UnitPrice, x.UnitDesc }).Distinct()
                        .Where(x => x.BranchID == branchID && x.BatNbr == batNbr).ToList();
            return query;
        }

        // POST
        // api/ShopType/

        /// <summary>
        /// Creates or Update 1 Batch
        /// </summary>
        /// <returns>A response message</returns>

        [HttpPut("UpdateBatch")]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutBatch([FromBody]CompositeBatch model)
        {
            var response = new Response();
            var batch = model.Batch;

            try
            {
                var objBatch = await DbContext.GetBatchAsync(new Batch(batch.BatNbr, batch.BranchID));
                if (objBatch == null)
                {
                    objBatch = new Batch
                    {
                        BranchID = batch.BranchID,
                        BatNbr = batch.BatNbr,
                        DateEnt = batch.DateEnt,
                        Descr = batch.Descr,
                        Module = "",
                        NoteID = 0,
                        OrigBranchID = "",
                        Rlsed = 0,
                        Status = batch.Status,
                        TotAmt = batch.TotAmt,
                        Crtd_Datetime = DateTime.Now,
                        Crtd_Prog = "APITEST",
                        LUpd_Datetime = DateTime.Now,
                        Crtd_User = "API",
                        LUpd_Prog = "APITEST",
                        LUpd_User = "API"
                    };
                }
                else
                {
                    objBatch.Descr = batch.Descr;
                    objBatch.TotAmt = batch.TotAmt;
                    objBatch.LUpd_Prog = "APITEST";
                    objBatch.LUpd_Datetime = DateTime.Now;
                    objBatch.LUpd_User = "API";
                    DbContext.Update(objBatch);
                }

                foreach (var item in model.TransModel.Update)
                {
                    if (string.IsNullOrEmpty(item.InvtID))
                    {
                        continue;
                    }

                    var objTran = await DbContext.IN_Trans.FirstOrDefaultAsync(x => x.BatNbr == item.BatNbr && x.BranchID == item.BranchID && x.InvtID == item.InvtID);
                    if (objTran == null)
                    {
                        objTran = new IN_TransApi
                        {
                            BranchID = item.BranchID,
                            BatNbr = item.BatNbr,
                            InvtID = item.InvtID,
                            UnitDesc = item.UnitDesc,
                            TranAmt = item.TranAmt,
                            UnitPrice = item.UnitPrice,
                            Qty = item.Qty,
                            Crtd_Datetime = DateTime.Now,
                            Crtd_Prog = "APITEST",
                            LUpd_Datetime = DateTime.Now,
                            Crtd_User = "API",
                            LUpd_Prog = "APITEST",
                            LUpd_User = "API"
                        };
                        DbContext.Add(objTran);
                    }
                    else
                    {
                        objTran.BranchID = item.BranchID;
                        objTran.BatNbr = item.BatNbr;
                        objTran.InvtID = item.InvtID;
                        objTran.Qty = item.Qty;
                        objTran.TranAmt = item.TranAmt;
                        objTran.UnitPrice = item.UnitPrice;
                        DbContext.Update(objTran);
                    }
                }
                await DbContext.SaveChangesAsync();
                response.Message = "Success!";
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(string branchID, string batNbr)
        {
            var response = new Response();

            response.Message = "Success!";
            return response.ToHttpResponse();
        }
        public class CompositeBatch
        {
            public Batch Batch { get; set; }
            public GridModel<IN_TransApi> TransModel { get; set; }
        }
        // GET: api/Batch/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Batch
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Batch/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
