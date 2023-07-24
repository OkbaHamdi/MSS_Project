using System;
using System.Collections.Generic;

namespace AngularAuthYtAPI.Models
{
    public partial class MPayTransferTpe
    {
        public string MPayTransferTpeIdDebit { get; set; } = null!;
        public string? MPayTransferTpeIdCredit { get; set; }
        public string? MPayTransferTpeSeqNumDebit { get; set; }
        public string? MPayTransferTpeSeqNumCredit { get; set; }
        public string? MPayTransferTpeDispoCode { get; set; }
        public string? MPayTransferTpeDispoDate { get; set; }
        public string? MPayTransferTpeStatus { get; set; }
        public string? MPayTransferTpeStatusDate { get; set; }
        public string? MPayTransferTpeIdTrsct { get; set; }
        public string? MPayTransferTpeBatchNumDebit { get; set; }
        public string? MPayTransferTpeBatchNumCredit { get; set; }
        public string? MPayTransferTpeIdOp { get; set; }
        public string? MPayTransferTpeAffiliationNum { get; set; }
        public string PayTransferTpeBankCode { get; set; } = null!;
    }
}
