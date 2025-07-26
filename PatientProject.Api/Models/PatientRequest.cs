namespace PatientProject.Api.Models;

public record PatientRequest
{
    public NameRequest Name { get; set; }

    public string Gender { get; set; }

    public DateTime BirthDate { get; set; }

    public string Active { get; set; }
}
