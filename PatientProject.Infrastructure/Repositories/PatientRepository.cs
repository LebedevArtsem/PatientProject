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
        await _dataContext.Patients.AddAsync(patient, token);

        await _dataContext.SaveChangesAsync(token);
    }

    public async Task Delete(Patient patient, CancellationToken token = default)
    {
        _dataContext.Patients.Remove(patient);

        await _dataContext.SaveChangesAsync(token);
    }

    public Task<Patient> Get(Guid id, CancellationToken token = default)
    {
        return _dataContext
            .Patients
            .FirstOrDefaultAsync(x => x.Name.Id == id, token);
    }

    public async Task<IEnumerable<Patient>> GetAll(CancellationToken token = default)
    {
        return await _dataContext
            .Patients
            .ToListAsync(token);
    }

    public async Task<IEnumerable<Patient>> SearchByBirthDate(string prefix, DateTime date, CancellationToken token = default)
    {
        IQueryable<Patient> query = _dataContext.Patients;

        switch (prefix)
        {
            case "eq":
                var nextDay = date.Date.AddDays(1);
                query = query.Where(p => p.BirthDate >= date.Date && p.BirthDate < nextDay);
                break;
            case "ne":
                var next = date.Date.AddDays(1);
                query = query.Where(p => p.BirthDate < date.Date || p.BirthDate >= next);
                break;
            case "gt":
                query = query.Where(p => p.BirthDate > date);
                break;
            case "ge":
                query = query.Where(p => p.BirthDate >= date);
                break;
            case "lt":
                query = query.Where(p => p.BirthDate < date);
                break;
            case "le":
                query = query.Where(p => p.BirthDate <= date);
                break;
            default:
                throw new ArgumentException($"Unsupported prefix '{prefix}'");
        }

        return await query.ToListAsync(token);
    }

    public async Task Update(Patient patient, CancellationToken token = default)
    {
        _dataContext
            .Patients
            .Update(patient);

        await _dataContext.SaveChangesAsync(token);
    }
}
