using Ecommerce_BE.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_BE.Contract.Repositories.Entities
{
    public class User : BaseModel
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public string? Role { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

    }
}
