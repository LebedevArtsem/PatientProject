using PatientProject.Domain.Entites;

namespace PatientProject.Domain.Interfaces;

public interface IPatientRepository
{
    Task<Patient> Get(Guid id, CancellationToken token = default);

    Task Create(Patient patient, CancellationToken token = default);

    Task Delete(Patient patient, CancellationToken token = default);

    Task Update(Patient patient, CancellationToken token = default);

    Task<IEnumerable<Patient>> GetAll(CancellationToken token = default);

    Task<IEnumerable<Patient>> SearchByBirthDate(string prefix, DateTime date, CancellationToken token = default);
}
