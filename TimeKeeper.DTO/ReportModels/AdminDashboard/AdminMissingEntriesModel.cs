namespace TimeKeeper.DTO.ReportModels.AdminDashboard
{
    public class AdminMissingEntriesModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal MissingEntriesHours { get; set; }
    }
}