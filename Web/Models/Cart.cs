namespace dambrosio.pretest.api.Models
{
    public class Cart
    {
        public int Id_ { get; set; }
        public string User_ { get; set; }
        public DateTime CreatedAt_ { get; set; }
        public DateTime? CompletedAt_ { get; set; }
        public decimal? TotalPrice_ { get; set; }
    }
}
