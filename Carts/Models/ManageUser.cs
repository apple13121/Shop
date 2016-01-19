using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Carts.Models
{
    public class ManageUser
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "{0}的長度至少必須為{2}個字元", MinimumLength = 1)]
        [Display(Name = "暱稱")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "電子郵件")]
        public string Email { get; set; }
    }
}