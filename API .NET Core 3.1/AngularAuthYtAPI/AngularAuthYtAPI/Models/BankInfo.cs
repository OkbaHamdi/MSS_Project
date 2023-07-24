namespace AngularAuthYtAPI.Models
{
    public partial class Bankinfo
    {
        public int Id { get; set; }
        public string? BankCode { get; set; }
        public string? CodeApp { get; set; }
        public string? Organisation { get; set; }
        public string? Smsid { get; set; }
        public string? Smspwd { get; set; }
        public string? ServiceProvider { get; set; }
    }
}
