
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace zad3_apbd;

public class Medicament
{
    [Key]
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }

    public ICollection<Prescription_Medicament> Prescription_Medicaments { get; set; }
}

public class Prescription_Medicament
{
    [Key, Column(Order = 0)]
    public int IdMedicament { get; set; }

    [Key, Column(Order = 1)]
    public int IdPrescription { get; set; }

    public int Dose { get; set; }
    public string Details { get; set; }

    public Medicament Medicament { get; set; }
    public Prescription Prescription { get; set; }
}

public class Doctor
{
    [Key]
    public int IdDoctor { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public ICollection<Prescription> Prescriptions { get; set; }
}

public class Patient
{
    [Key]
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }

    public ICollection<Prescription> Prescriptions { get; set; }
}

public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }

    public int IdPatient { get; set; }
    public int IdDoctor { get; set; }

    public Patient Patient { get; set; }
    public Doctor Doctor { get; set; }
    public ICollection<Prescription_Medicament> Prescription_Medicaments { get; set; }
}
