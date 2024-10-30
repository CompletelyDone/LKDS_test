namespace Model
{
    public class Company
    {
        private Company(Guid id, string title)
        {
            Id = id;
            Title = title;
        }
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<Employee> Employees { get; set; } = [];
        public static (Company Company, string Error) Create(Guid id, string title)
        {
            var error = string.Empty;

            var company = new Company(id, title);

            return (company, error);
        }
    }
}
