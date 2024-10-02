using System;
using Microsoft.Extensions.Caching.Memory;
using Wpm.Clinic.Controllers;
using Wpm.Clinic.DataAccess;
using Wpm.Clinic.ExternelServices;

namespace Wpm.Clinic.Application;

public class ClinicApplicationService(ClinicDbContext dbContext,
    ManagementService managementService, IMemoryCache memoryCache)
{
    public async Task<Consultation> Handle(StartConsultationCommand command)
    {
        var petInfo = await memoryCache.GetOrCreateAsync(command.PatientId,
            async cacheEntry =>
            {
                cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                return await managementService.GetPetInfo(command.PatientId);
            });

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

