using System;
using System.Collections.Generic;

namespace AngularAuthYtAPI.Models
{
    public partial class WTerminalsRefillWallet
    {
        public string WpTerminalDistributorId { get; set; } = null!;
        public string? WpTerminalDistributorAffiliation { get; set; }
        public string? WpTerminalDistributorBatchNum { get; set; }
        public string? WpTerminalDistributorSeqNum { get; set; }
        public int? WpTerminalDistributorDispoCode { get; set; }
        public int? WpTerminalDistributorStatus1 { get; set; }
        public string? WpTerminalDistributorStatusDate1 { get; set; }
        public int? WpTerminalDistributorStatus2 { get; set; }
        public string? WpTerminalDistributorStatusDate2 { get; set; }
        public string? WpTerminalDistributorCreationDate { get; set; }
        public string? WpTerminalDistributorUpdateDate { get; set; }
        public string? WpTerminalDistributorPwd { get; set; }
        public string WpTerminalBankCode { get; set; } = null!;
    }
}
