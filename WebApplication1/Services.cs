using Microsoft.EntityFrameworkCore;

namespace zad3_apbd;

public class PrescriptionService
{
    private readonly AppDbContext _context;

    public PrescriptionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddPrescriptionAsync(PrescriptionRequestDto dto)
    {
        if (dto.Medicaments.Count > 10)
            throw new ArgumentException("Prescription cannot contain more than 10 medicaments.");

        var parsedDate = DateTime.Parse(dto.Date);
        var parsedDueDate = DateTime.Parse(dto.DueDate);

        if (parsedDueDate < parsedDate)
            throw new ArgumentException("DueDate must be after or equal to Date.");

        var doctor = await _context.Doctors.FindAsync(dto.Doctor.IdDoctor);
        if (doctor == null)
            throw new Exception("Doctor not found");

        var patient = dto.Patient.IdPatient.HasValue
            ? await _context.Patients.FindAsync(dto.Patient.IdPatient.Value)
            : null;

        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = dto.Patient.FirstName,
                LastName = dto.Patient.LastName,
                Birthdate = dto.Patient.Birthdate
            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        foreach (var m in dto.Medicaments)
        {
            if (!await _context.Medicaments.AnyAsync(x => x.IdMedicament == m.IdMedicament))
                throw new Exception($"Medicament with ID {m.IdMedicament} does not exist.");
        }

        var prescription = new Prescription
        {
            Date = parsedDate,
            DueDate = parsedDueDate,
            IdDoctor = doctor.IdDoctor,
            IdPatient = patient.IdPatient,
            Prescription_Medicaments = dto.Medicaments.Select(m => new Prescription_Medicament
            {
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Details = m.Details
            }).ToList()
        };

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();
    }
}

