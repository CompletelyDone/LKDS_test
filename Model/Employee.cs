namespace Model
{
    public class Employee
    {
        public Employee() { }
        public Employee(Guid id, string lastname, string firstname, string? patronymic, string? photo, Guid? companyId)
        {
            Id = id;
            LastName = lastname;
            FirstName = firstname;
            Patronymic = patronymic;
            PhotoPath = photo;
            CompanyId = companyId;
        }
        public Guid Id { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? Patronymic { get; set; }
        public string? PhotoPath { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
