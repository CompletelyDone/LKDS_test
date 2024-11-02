namespace Model
{
    public class Employee
    {
        private const int MIN_FIELD_LENGTH = 3;
        public const int MAX_FIELD_LENGTH = 50;
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
        public virtual Company? Company { get; set; }
        public static string IsValid(Employee employee)
        {
            string error = string.Empty;

            if (string.IsNullOrEmpty(employee.LastName) || string.IsNullOrEmpty(employee.FirstName))
            {
                return "Поле не может быть пустым.";
            }
            if (employee.LastName.Length < MIN_FIELD_LENGTH || employee.FirstName.Length > MIN_FIELD_LENGTH)
            {
                return $"Длинна поля не может быть короче {MIN_FIELD_LENGTH} и длиннее {MAX_FIELD_LENGTH}.";
            }
            return error;
        }
    }
}
