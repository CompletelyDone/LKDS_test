namespace Model
{
    public class Company
    {
        public Company() { }
        public Company(Guid id, string title)
        {
            Id = id;
            Title = title;
        }
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<Employee> Employees { get; set; } = [];
    }
}
