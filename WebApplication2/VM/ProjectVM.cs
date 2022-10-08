namespace WebApplication2.VM
{
    public class ProjectVM
    {
        public int Id { get; set; }=0;
        public int ProjectId { get; set; }= 0;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Hours { get; set; } = 0;
        public string Employees { get; set; } = string.Empty;
        public List<EmployeeVM> EmployeeList { get; set; } = new();
    }
}
