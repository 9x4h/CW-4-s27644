namespace zad3_apbd;

public class PrescriptionRequestDto
{
    public string Date { get; set; }
    public string DueDate { get; set; }
    public DoctorDto Doctor { get; set; }
    public PatientDto Patient { get; set; }
    public List<PrescriptionMedicamentDto> Medicaments { get; set; }
}

public class DoctorDto
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; }
}
public class PatientDto
{
    public int? IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
}

public class PrescriptionMedicamentDto
{
    public int IdMedicament { get; set; }
    public int Dose { get; set; }
    public string Details { get; set; }
}
