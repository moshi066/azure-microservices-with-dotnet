using System.Net;
using Microsoft.AspNetCore.Mvc;
using Wpm.Clinic.Application;

namespace Wpm.Clinic.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClinicController(ClinicApplicationService applicationService) : ControllerBase
{
    [HttpPost("start")]
    public async Task<IActionResult> Start(StartConsultationCommand command)
    {
        var result = await applicationService.Handle(command);
        return Ok(result);
    }
}

public record StartConsultationCommand(int PatientId);
