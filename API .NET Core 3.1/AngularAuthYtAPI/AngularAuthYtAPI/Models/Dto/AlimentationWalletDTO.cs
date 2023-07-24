namespace AngularAuthYtAPI.Models.Dto
{
    public class AlimentationWalletDTO
    {
        public string MPayTransferTpeIdDebit { get; set; } = null!;
        public string? MPayTransferTpeSeqNumDebit { get; set; }
        public string? MPayTransferTpeSeqNumCredit { get; set; }
        public string? MPayTransferTpeDispoCode { get; set; }
        public string? MPayTransferTpeStatus { get; set; }
        public string? MPayTransferTpeIdTrsct { get; set; }
        public string? MPayTransferTpeBatchNumDebit { get; set; }
        public string? MPayTransferTpeBatchNumCredit { get; set; }

    }
}
