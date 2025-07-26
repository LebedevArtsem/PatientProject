namespace PatientProject.Api.Models;

public record NameRequest
{
    public string Use { get; set; }

    public string Family { get; set; }

    public List<string> Given { get; set; }
}
