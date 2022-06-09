using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.Services;

namespace WebStore.WebAPI.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeesApicontroller: ControllerBase
{
    private readonly IEmployeesData _EmployeesData;
    private readonly ILogger<EmployeesApicontroller> _Logger;

    public EmployeesApicontroller(IEmployeesData EmployeesData, ILogger<EmployeesApicontroller> Logger)
    {
        _EmployeesData = EmployeesData;
        _Logger = Logger;
    }
}

