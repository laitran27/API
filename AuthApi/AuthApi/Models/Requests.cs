using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AuthApi.Models
{
    public class PostAR_ShopTypeRequests
    {
        [Key]
        public string Code { get; set; }
        [Required]
        [StringLength(200)]
        public string Descr { get; set; }
    }

    public class PutAR_ShopTypeRequest
    {
        [Key]
        public string Code { get; set; }
        [Required]
        [StringLength(200)]
        public string Descr { get; set; }
    }

    public static class Extensions
    {
        public static AR_ShopType ToEntity(this PostAR_ShopTypeRequests request)
            => new AR_ShopType
            {
                Descr = request.Descr
            };
        public static AR_ShopType ToEntity(this PutAR_ShopTypeRequest request)
            => new AR_ShopType
            {
                Code = request.Code,
                Descr = request.Descr,
                Crtd_Datetime = DateTime.Now,
                Crtd_Prog = "APITEST",
                LUpd_Datetime = DateTime.Now,
                Crtd_User = "API",
                LUpd_Prog = "APITEST",
                LUpd_User = "API"
            };
    }
}
