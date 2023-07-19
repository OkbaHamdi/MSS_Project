using System;
using System.Collections.Generic;

namespace AngularAuthYtAPI.Models
{
    public partial class MPayWithdrawalTerminalId
    {
        public string? MPayAtmId { get; set; }
        public string? MPayAtmAffiliation { get; set; }
        public string? MPayAtmBatchNum { get; set; }
        public string? MPayAtmSeqNum { get; set; }
        public int? MPayAtmDispoCode { get; set; }
        public int? MPayAtmStatus1 { get; set; }
        public string? MPayAtmStatusDate1 { get; set; }
        public int? MPayAtmStatus2 { get; set; }
        public string? MPayAtmStatusDate2 { get; set; }
        public string? MPayAtmCreationDate { get; set; }
        public string? MPayAtmUpdateDate { get; set; }
        public string? MPayAtmPwd { get; set; }
        public string? MPayAtmNumAgent { get; set; }
        public string? MPayAtmBankName { get; set; }
        public string? MPayAtmBankCode { get; set; }
        public string? MPayAtmPaymentType { get; set; }
        public string? MPayAtmCodeAgence { get; set; }
    }
}
