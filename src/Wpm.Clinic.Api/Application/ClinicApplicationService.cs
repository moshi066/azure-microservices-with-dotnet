using System;
using Wpm.Clinic.Controllers;
using Wpm.Clinic.DataAccess;
using Wpm.Clinic.ExternelServices;

namespace Wpm.Clinic.Application;

public class ClinicApplicationService(ClinicDbContext dbContext,
    ManagementService managementService)
{
    public async Task<Consultation> Handle(StartConsultationCommand command)
    {
        var petInfo = await managementService.GetPetInfo(command.PatientId);
        var newConsultaion = new Consultation(Guid.NewGuid(),
                                              command.PatientId,
                                              petInfo.Name,
                                              petInfo.Age,
                                              DateTime.UtcNow);

        await dbContext.Consultations.AddAsync(newConsultaion);
        await dbContext.SaveChangesAsync();
        return newConsultaion;
    }
}

