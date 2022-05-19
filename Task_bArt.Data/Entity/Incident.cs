namespace Task_bArt.Data.Entity
{
    public class Incident
    {
        public string? IncidentName { get; set; }
        public string? Description { get; set; }
        public List<Account>? Accounts { get; set; }
    }
}
