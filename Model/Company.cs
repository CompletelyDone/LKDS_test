namespace Model
{
    public class Company
    {
        private const int MIN_TITLE_LENGTH = 3;
        public const int MAX_TITLE_LENGTH = 100;
        public Company() { }
        public Company(Guid id, string title)
        {
            Id = id;
            Title = title;
        }
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<Employee> Employees { get; set; } = [];
        public static string IsValid(Company company)
        {
            string error = string.Empty;

            if(string.IsNullOrEmpty(company.Title))
            {
                return "Название не может быть пустым.";
            }
            if(company.Title.Length < MIN_TITLE_LENGTH)
            {
                return $"Название не может быть короче {MIN_TITLE_LENGTH} символов.";
            }
            if(company.Title.Length > MAX_TITLE_LENGTH)
            {
                return $"Название не может быть длиннее {MAX_TITLE_LENGTH} символов.";
            }

            return error;
        }
    }
}
