using Ecommerce_BE.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_BE.Core.Base
{
    public abstract class BaseModel
    {
        public BaseModel()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = UpdatedAt = CoreHelper.SystemTimeNow;
        }
        [Key]
        [StringLength(450)]
        public string Id { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? DeletedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public DateTimeOffset DeletedAt { get; set; }   

    }
}
