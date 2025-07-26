using PatientProject.Seeder;
using System.Net.Http.Json;

const string apiUrl = "https://localhost:7014/api/patients";
var client = new HttpClient();

for (int i = 0; i < 100; i++)
{
    var patient = PatientGenerator.GenerateRandomPatient();

    var response = await client.PostAsJsonAsync(apiUrl, patient);
    if (response.IsSuccessStatusCode)
    {
        Console.WriteLine($"[{i + 1}] Patient created.");
    }
    else
    {
        Console.WriteLine($"[{i + 1}] Failed: {response.StatusCode}");
    }
}