using System.ComponentModel.DataAnnotations;

namespace AngularAuthYtAPI.Models.Dto
{
    public class AddWithdrawalTerminalDTO
    {
        [Required]
        public string? MPayAtmId { get; set; }
        public string? MPayAtmAffiliation { get; set; }
        public string? MPayAtmBatchNum { get; set; }
        public string? MPayAtmSeqNum { get; set; }
        public int? MPayAtmDispoCode { get; set; }
        public int? MPayAtmStatus1 { get; set; }
        public int? MPayAtmStatus2 { get; set; }
        public string? MPayAtmCreationDate { get; set; }
        public string? MPayAtmPwd { get; set; }
     }
}
