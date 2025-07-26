namespace PatientProject.Seeder.Models;

internal class CreatePatientDto
{
    public NameDto Name { get; set; }
    public string Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public string Active { get; set; }
}
