using Data.Abstractions;

namespace ViewModel.Services
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
    }
}
