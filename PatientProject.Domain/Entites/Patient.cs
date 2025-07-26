using PatientProject.Domain.Enums;

namespace PatientProject.Domain.Entites;

public class Patient
{
    public Name Name { get; set; }

    public Gender Gender { get; set; }

    public DateTime BirthDate { get; set; }

    public Active Active { get; set; }
}
