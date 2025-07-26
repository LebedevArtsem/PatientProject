using PatientProject.Application.Models;
namespace PatientProject.Application.Interfaces;

public interface IPatientService
{
    Task<PatientDto> Get(Guid id, CancellationToken token = default);

    Task<PatientDto> Create(PatientDto dto, CancellationToken token = default);

    Task Delete(Guid id, CancellationToken token = default);

    Task Update(PatientDto dto, CancellationToken token = default);

    Task<IEnumerable<PatientDto>> Search(string birthDateParam, CancellationToken token = default);
}
