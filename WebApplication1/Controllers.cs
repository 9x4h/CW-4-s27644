using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace zad3_apbd;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly PrescriptionService _service;
    private readonly AppDbContext _context;

    public PrescriptionsController(PrescriptionService service, AppDbContext context)
    {
        _service = service;
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription(PrescriptionRequestDto dto)
    {
        try
        {
            await _service.AddPrescriptionAsync(dto);
            return Ok("Prescription created successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("patient/{id}")]
    public async Task<IActionResult> GetPatientDetails(int id)
    {
        var patient = await _context.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(r => r.Prescription_Medicaments)
            .ThenInclude(pm => pm.Medicament)
            .Include(p => p.Prescriptions)
            .ThenInclude(r => r.Doctor)
            .FirstOrDefaultAsync(p => p.IdPatient == id);

        if (patient == null) return NotFound();
        return Ok(patient);
    }
}