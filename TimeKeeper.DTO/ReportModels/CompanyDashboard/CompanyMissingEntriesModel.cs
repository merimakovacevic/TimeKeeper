namespace TimeKeeper.DTO.ReportModels.CompanyDashboard
{
    public class CompanyMissingEntriesModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal MissingEntriesHours { get; set; }
    }
}