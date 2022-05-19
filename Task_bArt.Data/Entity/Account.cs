namespace Task_bArt.Data.Entity
{
    public class Account
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? IncidentName { get; set; }
        public Incident? Incident { get; set; }
        public List<Contact>? Contacts { get; set; }
    }
}
