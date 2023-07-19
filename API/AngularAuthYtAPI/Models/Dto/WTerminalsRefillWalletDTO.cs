namespace AngularAuthYtAPI.Models.Dto
{
    public class WTerminalsRefillWalletDTO
    {
        public string WpTerminalDistributorId { get; set; } = null!;
        public string? WpTerminalDistributorAffiliation { get; set; }
        public string? WpTerminalDistributorBatchNum { get; set; }
        public string? WpTerminalDistributorSeqNum { get; set; }
        public int? WpTerminalDistributorDispoCode { get; set; }
        public int? WpTerminalDistributorStatus1 { get; set; }
        public int? WpTerminalDistributorStatus2 { get; set; }
        public string? WpTerminalDistributorPwd { get; set; }

    }
}
