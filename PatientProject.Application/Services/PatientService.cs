using AutoMapper;
using PatientProject.Application.Interfaces;
using PatientProject.Application.Models;
using PatientProject.Domain.Entites;
using PatientProject.Domain.Interfaces;

namespace PatientProject.Application.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _repository;
    private readonly IMapper _mapper;

    public PatientService(IPatientRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PatientDto> Create(PatientDto dto, CancellationToken token = default)
    {
        try
        {
            var patient = _mapper.Map<Patient>(dto);

            await _repository.Create(patient);

            return _mapper.Map<PatientDto>(patient);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task Delete(Guid id, CancellationToken token = default)
    {
        var patient = await _repository.Get(id, token);

        if (patient == null)
            throw new KeyNotFoundException($"Patient with ID {id} not found.");

        await _repository.Delete(patient, token);
    }

    public async Task<PatientDto> Get(Guid id, CancellationToken token = default)
    {
        var person = await _repository.Get(id, token);

        return _mapper.Map<PatientDto>(person);
    }

    public async Task<IEnumerable<PatientDto>> Search(string birthDateParam, CancellationToken token = default)
    {
        if (string.IsNullOrWhiteSpace(birthDateParam))
            return _mapper.Map<IEnumerable<PatientDto>>(await _repository.GetAll(token));

        var (prefix, date) = DateParserHelper.ParseDateParam(birthDateParam);

        var patients = await _repository.SearchByBirthDate(prefix, date, token);
        return _mapper.Map<IEnumerable<PatientDto>>(patients);
    }

    public async Task Update(PatientDto dto, CancellationToken token = default)
    {
        var patient = await _repository.Get(dto.Name.Id, token);
        if (patient == null)
            throw new KeyNotFoundException($"Patient with ID {dto.Name.Id} not found.");

        _mapper.Map(dto, patient);

        await _repository.Update(patient, token);
    }
}
