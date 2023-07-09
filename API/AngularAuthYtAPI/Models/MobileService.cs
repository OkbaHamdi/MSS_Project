using System;
using System.Collections.Generic;

namespace AngularAuthYtAPI.Models
{
    public partial class MobileService
    {
        public int Id { get; set; }
        public string? BankCode { get; set; }
        public string? BankName { get; set; }
        public string? ServiceProvider { get; set; }
        public string? IdEtablissement { get; set; }
    }
}
