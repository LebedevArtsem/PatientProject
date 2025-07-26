using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PatientProject.Api.Models;
using PatientProject.Application.Interfaces;
using PatientProject.Application.Models;

namespace PatientProject.Api.Controllers;

[ApiController]
[Route("api/patients")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;
    private readonly IMapper _mapper;

    public PatientController(IPatientService patientService, IMapper mapper)
    {
        _patientService = patientService;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken token)
    {
        var patient = await _patientService.Get(id, token);

        return Ok(patient);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PatientRequest request, CancellationToken token)
    {
        var dto = _mapper.Map<PatientDto>(request);
        var result = await _patientService.Create(dto);

        return CreatedAtAction(nameof(Get), new { id = dto.Name.Id }, dto);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] PatientRequest request, CancellationToken token)
    {
        var dto = _mapper.Map<PatientDto>(request);

        await _patientService.Update(dto, token);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken token)
    {
        await _patientService.Delete(id, token);

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] string birthDate, CancellationToken token)
    {
        var result = await _patientService.Search(birthDate, token);

        return Ok(result);
    }
}
