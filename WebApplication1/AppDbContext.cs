namespace zad3_apbd;

using Microsoft.EntityFrameworkCore;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Prescription_Medicament>()
            .HasKey(pm => new { pm.IdMedicament, pm.IdPrescription });

        modelBuilder.Entity<Prescription_Medicament>()
            .HasOne(pm => pm.Medicament)
            .WithMany(m => m.Prescription_Medicaments)
            .HasForeignKey(pm => pm.IdMedicament);

        modelBuilder.Entity<Prescription_Medicament>()
            .HasOne(pm => pm.Prescription)
            .WithMany(p => p.Prescription_Medicaments)
            .HasForeignKey(pm => pm.IdPrescription);


    }
}