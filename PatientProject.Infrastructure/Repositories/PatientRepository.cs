using Microsoft.EntityFrameworkCore;
using PatientProject.Domain.Entites;
using PatientProject.Domain.Interfaces;
using PatientProject.Infrastructure.Persistance;

namespace PatientProject.Infrastructure.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly DataContext _dataContext;

    public PatientRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task Create(Patient patient, CancellationToken token = default)
    {
        await _dataContext.Patitents.AddAsync(patient, token);

        await _dataContext.SaveChangesAsync(token);
    }

    public async Task Delete(Patient patient, CancellationToken token = default)
    {
        _dataContext.Patitents.Remove(patient);

        await _dataContext.SaveChangesAsync(token);
    }

    public Task<Patient> Get(Guid id, CancellationToken token = default)
    {
        return _dataContext
            .Patitents
            .FirstOrDefaultAsync(x => x.Name.Id == id, token);
    }

    public async Task Update(Patient patient, CancellationToken token = default)
    {
        _dataContext
            .Patitents
            .Update(patient);

        await _dataContext.SaveChangesAsync(token);
    }
}
