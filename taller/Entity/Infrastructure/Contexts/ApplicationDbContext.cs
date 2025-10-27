using Entity.Domain.Interfaces;
using Entity.Domain.Models;
using Entity.Domain.Models.Implements;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Entity.Infrastructure.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        private readonly IAuditService _auditService;
        private readonly IHttpContextAccessor _http;

        public ApplicationDbContext(
             DbContextOptions<ApplicationDbContext> options,
             IConfiguration configuration,
             IAuditService auditService,
             IHttpContextAccessor httpContextAccessor
         ) : base(options)
        {
            _configuration = configuration;
            _auditService = auditService;
            _http = httpContextAccessor;
        }
        ///<summary>
        ///Implementación DBSet
        ///</summary>

        public DbSet<User> users {  get; set; }
        public DbSet<Person> persons {  get; set; }
        public DbSet<Rol> rols { get; set; }
        public DbSet<RolUser> rolUsers { get; set; }


        public DbSet<Form> forms { get; set; }
        public DbSet<Domain.Models.Implements.Module> modules { get; set; }
        public DbSet<Permission> permissions { get; set; }
        public DbSet<RolFormPermission> rol_form_permissions { get; set; }
        public DbSet<FormModule> form_modules { get; set; }
        //public DbSet<TouristicAttraction> TouristicAttraction { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Person)
                .WithOne(p => p.User)
                .HasForeignKey<User>(u => u.PersonId)
                .OnDelete(DeleteBehavior.Cascade); // o Restrict, según lo que desees

           

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override int SaveChanges()
        {
            // 1. Detecta cambios
            ChangeTracker.DetectChanges();
            // 2. Captura auditoría
            _auditService.CaptureAsync(ChangeTracker).GetAwaiter().GetResult();
            // 3. Persiste todo
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();
            await _auditService.CaptureAsync(ChangeTracker, cancellationToken);
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
