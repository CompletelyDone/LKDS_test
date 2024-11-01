using Data.Abstractions;
using Model;
using System.Text;

namespace ViewModels.Services
{
    public class DataService
    {
        private readonly ICompanyRepository companyRepository;
        private readonly IEmployeeRepository employeeRepository;
        public DataService(ICompanyRepository companyRepository, IEmployeeRepository employeeRepository)
        {
            this.companyRepository = companyRepository;
            this.employeeRepository = employeeRepository;
        }
        public async Task CreateRandomAsync()
        {
            Random random = new Random();
            List<Company> companies = new List<Company>();
            for (int i = 0; i < 10; i++)
            {
                var title = GetStringWithRandom("Организация", random);
                var company = new Company(Guid.NewGuid(), title);
                for (int j = 0; j < 100; j++)
                {
                    var employee = new Employee(
                        Guid.NewGuid(),
                        GetStringWithRandom("Фамилия", random),
                        GetStringWithRandom("Имя", random),
                        GetStringWithRandom("Отчество", random),
                        null,
                        company.Id);
                    company.Employees.Add(employee);
                }
                companies.Add(company);
            }
            await companyRepository.CreateRangeAsync(companies);
        }
        private string GetStringWithRandom(string str, Random random)
        {
            var sb = new StringBuilder();
            sb.Append(str);
            sb.Append('_');
            sb.Append(random.Next().ToString());
            return sb.ToString();
        }

    }
}
