using PatientProject.Seeder.Models;

namespace PatientProject.Seeder;

internal static class PatientGenerator
{
    public static CreatePatientDto GenerateRandomPatient()
    {
        var random = new Random();
        var genderOptions = new[] { "male", "female", "other", "unknown" };
        var useOptions = new[] { "official", "usual", "nickname" };
        var familyNames = new[] { "Иванов", "Петров", "Сидоров", "Козлов" };
        var givenNames = new[] { "Иван", "Пётр", "Алексей", "Ольга", "Анна", "Мария" };

        return new CreatePatientDto
        {
            Name = new NameDto
            {
                Use = useOptions[random.Next(useOptions.Length)],
                Family = familyNames[random.Next(familyNames.Length)],
                Given = new List<string>
            {
                givenNames[random.Next(givenNames.Length)],
                givenNames[random.Next(givenNames.Length)]
            }
            },
            Gender = genderOptions[random.Next(genderOptions.Length)],
            BirthDate = RandomDate(random, new DateTime(1950, 1, 1), DateTime.Today),
            Active = random.Next(2) == 0 ? "true" : "false"
        };
    }

    static DateTime RandomDate(Random random, DateTime from, DateTime to)
    {
        var range = (to - from).Days;
        return from.AddDays(random.Next(range)).AddHours(random.Next(0, 24)).AddMinutes(random.Next(0, 60));
    }
}
