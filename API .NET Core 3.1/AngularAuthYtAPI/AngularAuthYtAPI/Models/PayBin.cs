using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AngularAuthYtAPI.Models
{
    public partial class PayBin
    {
        [Required]
        public string BinId { get; set; } = null!;
        public string? BankName { get; set; }
        [Required]
        public string BankCode { get; set; } = null!;
        public string? CodeApp { get; set; }
        public string? Privilege { get; set; }
    }
}
/*
 using System;
using System.Collections.Generic;

namespace AngularAuthYtAPI.ModelsT
{
    public partial class PayBin
    {
        public string BinId { get; set; } = null!;
        public string? BankName { get; set; }
        public string BankCode { get; set; } = null!;
        public string? CodeApp { get; set; }
        public string? Privilege { get; set; }
    }
}
 
 */