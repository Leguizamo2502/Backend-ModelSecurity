namespace Entity.Domain.Models
{
    public class Audit
    {
        public int Id { get; set; }
        public string Entity { get; set; }
        public string Action { get; set; }
        public string KeyValues { get; set; }
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
        public string? UserId { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
