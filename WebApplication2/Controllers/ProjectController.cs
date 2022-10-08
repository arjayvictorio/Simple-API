using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApplication2.VM;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        [HttpPost]
        public ActionResult Save(ProjectVM vm)
        {
            new MSSQL(Helper.Con)
                .SendSP("Save",
                "ProjectId", vm.ProjectId.ToString(),
                "Title", vm.Title,
                "Description", vm.Description,
                "Hours", vm.Hours.ToString(),
                "Employees", JsonSerializer.Serialize<List<EmployeeVM>>(vm.EmployeeList));

            return Ok(vm);
        }

        [HttpGet]
        public List<ProjectVM> Search(string title, int page)
        {
            List<ProjectVM> ProjectList = new List<ProjectVM>();

            ProjectList = new MSSQL(Helper.Con)
                .GetRowsSP<ProjectVM>("Projects",
                "GetAll",
                "Title", title,
                "Page", page.ToString());

            foreach (var item in ProjectList)
            {
                List<EmployeeVM> EmployeeList = new List<EmployeeVM>();

                foreach (var emp in item.EmployeeList)
                {
                    if (emp.Estimate==emp.Actual)
                    {
                        EmployeeList.Add(emp);
                    }
                }

                foreach (var tmpemp in EmployeeList)
                {
                    item.EmployeeList.Remove(tmpemp);
                }
            }

            return ProjectList;
        }
    }
}
