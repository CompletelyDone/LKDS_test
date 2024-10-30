namespace Model
{
    public class Employee
    {
        private Employee(Guid id, string lastname, string firstname, string? patronymic, string? photo, Company? company)
        {
            Id = id;
            LastName = lastname;
            FirstName = firstname;
            Patronymic = patronymic;
            PhotoPath = photo;
        }
        public Guid Id { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? Patronymic { get; set; }
        public string? PhotoPath { get; set; }
        public Company? Company { get; set; }
        public static (Employee Employee, string Error)
            Create(Guid id, string lastname, string firstname, string? patronymic, string? photo, Company company)
        {
            var error = string.Empty;

            var employee = new Employee(id, lastname, firstname, patronymic, photo, company);

            return (employee, error);
        }
    }
}
