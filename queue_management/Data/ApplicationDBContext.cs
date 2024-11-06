using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using queue_management.Models;

namespace queue_management.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        // Definición de Modelos del Sistema
        public DbSet<Agent> Agents { get; set; } = default!;
        public DbSet<Appointment> Appointments { get; set; } = default!;
        public DbSet<Area> Areas { get; set; } = default!;
        public DbSet<City> Cities { get; set; } = default!;
        public DbSet<Comment> Comments { get; set; } = default!;
        public DbSet<Country> Countries { get; set; } = default!;
        public DbSet<Department> Departments { get; set; } = default!;
        public DbSet<Location> Locations { get; set; } = default!;
        public DbSet<Municipality> Municipalities { get; set; } = default!;
        public DbSet<Queue> Queues { get; set; } = default!;
        public DbSet<QueueAssignment> QueueAssignments { get; set; } = default!;
        public DbSet<QueueStatus> QueueStatus { get; set; } = default!;
        public DbSet<QueueStatusAssignment> QueueStatusAssignments { get; set; } = default!;
        public DbSet<Rating> Ratings { get; set; } = default!;
        public DbSet<Region> Regions { get; set; } = default!;
        public DbSet<Role> Roles { get; set; } = default!;
        public DbSet<Service> Services { get; set; } = default!;
        public DbSet<ServiceWindow> ServiceWindows { get; set; } = default!;
        public DbSet<Status> Status { get; set; } = default!;
        public DbSet<Ticket> Tickets { get; set; } = default!;
        public DbSet<TicketStatus> TicketStatus { get; set; } = default!;
        public DbSet<TicketStatusAssignment> TicketStatusAssignments { get; set; } = default!;
        public DbSet<Unit> Units { get; set; } = default!;

        // Resolución de relaciones de Identity
        // API Fluente
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar reiterar los nombres de tablas pluralizadas
            modelBuilder.Entity<Agent>().ToTable("Agents");
            modelBuilder.Entity<Appointment>().ToTable("Appointments");
            modelBuilder.Entity<Area>().ToTable("Areas");
            modelBuilder.Entity<City>().ToTable("Cities");
            modelBuilder.Entity<Comment>().ToTable("Comments");
            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<Department>().ToTable("Departments");
            modelBuilder.Entity<Location>().ToTable("Locations");
            modelBuilder.Entity<Municipality>().ToTable("Municipalities");
            modelBuilder.Entity<Queue>().ToTable("Queues");
            modelBuilder.Entity<QueueAssignment>().ToTable("QueueAssignments");
            modelBuilder.Entity<QueueStatus>().ToTable("QueueStatus");
            modelBuilder.Entity<QueueStatusAssignment>().ToTable("QueueStatusAssignments");
            modelBuilder.Entity<Rating>().ToTable("Ratings");
            modelBuilder.Entity<Region>().ToTable("Regions");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Service>().ToTable("Services");
            modelBuilder.Entity<ServiceWindow>().ToTable("ServiceWindows");
            modelBuilder.Entity<Status>().ToTable("Status");
            modelBuilder.Entity<Ticket>().ToTable("Tickets");
            modelBuilder.Entity<TicketStatus>().ToTable("TicketStatus");
            modelBuilder.Entity<TicketStatusAssignment>().ToTable("TicketStatusAssignments");
            modelBuilder.Entity<Unit>().ToTable("Units");

            // Sección de Detalle de Relaciones
            // Relaciones con eliminaciones restringidas para evitar ciclos
            //-------------------------------------------------------------

            // Relación Agent -> Appointment
            modelBuilder.Entity<Appointment>()
                .HasOne(ap => ap.Agent)
                .WithMany(ag => ag.Appointments)
                .HasForeignKey(ap => ap.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Appointment -> Service
            modelBuilder.Entity<Appointment>()
                .HasOne(ap => ap.Service)
                .WithMany(se => se.Appointments)
                .HasForeignKey(ap => ap.ServiceID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de la relación Area - Unit
            modelBuilder.Entity<Unit>()
                .HasOne(u => u.Area)
                .WithMany(a => a.Units)
                .HasForeignKey(u => u.AreaID)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Comment -> Ticket
            modelBuilder.Entity<Comment>()
                .HasOne(cm => cm.Ticket)
                .WithMany(ti => ti.Comments)
                .HasForeignKey(cm => cm.TicketID)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Country -> Department
            modelBuilder.Entity<Department>()
                .HasOne(de => de.Country)
                .WithMany(co => co.Departments)
                .HasForeignKey(de => de.CountryID)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Department -> Region
            modelBuilder.Entity<Region>()
                .HasOne(re => re.Department)
                .WithMany(de => de.Regions)
                .HasForeignKey(re => re.DepartmentID)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Location -> Service
            modelBuilder.Entity<Service>()
                .HasOne(se => se.Location)
                .WithMany(lo => lo.Services)
                .HasForeignKey(se => se.LocationID)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Queue -> QueueAssignment
            modelBuilder.Entity<QueueAssignment>()
                .HasOne(qa => qa.Queue)
                .WithMany(qu => qu.QueueAssignments)
                .HasForeignKey(qa => qa.QueueID)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Queue -> Ticket
            modelBuilder.Entity<Queue>()
                .HasMany(qu => qu.Tickets)
                .WithOne(ti => ti.Queue)
                .HasForeignKey(ti => ti.QueueID)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación QueueStatus -> QueueStatusAssignment
            modelBuilder.Entity<QueueStatusAssignment>()
                .HasOne(qsa => qsa.QueueStatus)
                .WithMany(qs => qs.QueueStatusAssignments)
                .HasForeignKey(qsa => qsa.QueueStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Region -> Municipality
            modelBuilder.Entity<Municipality>()
                .HasOne(mu => mu.Region)
                .WithMany(re => re.Municipalities)
                .HasForeignKey(mu => mu.RegionID)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Service -> Rating
            modelBuilder.Entity<Rating>()
                .HasOne(ra => ra.Service)
                .WithMany(se => se.Ratings)
                .HasForeignKey(ra => ra.ServiceID)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Service>()
            //  .HasMany(se => se.Ratings)
            //  .WithOne(ra => ra.Service)
            //  .HasForeignKey(ra => ra.ServiceID)
            //  .OnDelete(DeleteBehavior.Restrict);

            // Configuración de la relación City - Country
            modelBuilder.Entity<City>()
                .HasOne(c => c.Country)
                .WithMany()
                .HasForeignKey(c => c.CountryID)
                .OnDelete(DeleteBehavior.Restrict); // Restringe la eliminación en cascada

            // Configuración de la relación City - Department
            modelBuilder.Entity<City>()
                .HasOne(c => c.Department)
                .WithMany()
                .HasForeignKey(c => c.DepartmentID)
                .OnDelete(DeleteBehavior.Restrict); // Restringe la eliminación en cascada

            // Configuración de la relación City - Region
            modelBuilder.Entity<City>()
                .HasOne(c => c.Region)
                .WithMany()  
                .HasForeignKey(c => c.RegionID)
                .OnDelete(DeleteBehavior.Restrict); // Cambia el comportamiento de cascada por Restrict

            // Relación Country -> Location  
            modelBuilder.Entity<Location>()
                  .HasOne(l => l.Country)
                  .WithMany()
                  .HasForeignKey(l => l.CountryID)
                  .OnDelete(DeleteBehavior.Restrict);

            // Relación Department -> Location  
            modelBuilder.Entity<Location>()
                  .HasOne(l => l.Department)
                  .WithMany()
                  .HasForeignKey(l => l.DepartmentID)
                  .OnDelete(DeleteBehavior.Restrict);

            // Relación Region -> Location  
            modelBuilder.Entity<Location>()
                .HasOne(l => l.Region)
                .WithMany()
                .HasForeignKey(l => l.RegionID)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Municipality -> Location  
            modelBuilder.Entity<Location>()
                .HasOne(l => l.Municipality)
                .WithMany()
                .HasForeignKey(l => l.MunicipalityID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de la relación City - Municipality
            modelBuilder.Entity<City>()
                .HasOne(c => c.Municipality)
                .WithMany(m => m.Cities)
                .HasForeignKey(c => c.MunicipalityID)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación City -> Location
            modelBuilder.Entity<Location>()
                .HasOne(lo => lo.City)
                .WithMany(ci => ci.Locations)
                .HasForeignKey(lo => lo.CityID)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Service -> ServiceWindow
            modelBuilder.Entity<ServiceWindow>()
                .HasOne(sw => sw.Service)
                .WithMany(se => se.ServiceWindows)
                .HasForeignKey(sw => sw.ServiceID)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Ticket -> TicketStatusAssignment
            modelBuilder.Entity<TicketStatusAssignment>()
                .HasOne(tsa => tsa.Ticket)
                .WithMany(ti => ti.TicketStatusAssignments)
                .HasForeignKey(tsa => tsa.TicketID)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación TicketStatus -> TicketStatusAssignment
            modelBuilder.Entity<TicketStatusAssignment>()
                .HasOne(tsa => tsa.TicketStatus)
                .WithMany(ts => ts.TicketStatusAssignments)
                .HasForeignKey(tsa => tsa.TicketStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación 
            modelBuilder.Entity<Service>()
                .HasMany(se => se.Queues)
                .WithOne(qu => qu.Service)
                .HasForeignKey(qu => qu.ServiceID)
                .OnDelete(DeleteBehavior.Restrict);

            //--------------------------------------------------------------------
            // Relación Rol - User
            // modelBuilder.Entity<Rol>()
            //    .HasMany(r => r.Users)
            //    .WithOne(u => u.Rol)
            //    .HasForeignKey(u => u.RoleId);

            // Definición de Claves primarias con Identity
            //--------------------------------------------
            modelBuilder.Entity<Agent>()
                .Property(ag => ag.AgentID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Appointment>()
                .Property(ap => ap.AppointmentID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<City>()
                .Property(ci => ci.CityID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Comment>()
                .Property(cm => cm.CommentID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Country>()
                .Property(co => co.CountryID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Department>()
                .Property(d => d.DepartmentID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Location>()
                .Property(lo => lo.LocationID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Municipality>()
                .Property(mu => mu.MunicipalityID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Queue>()
                .Property(qu => qu.QueueID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<QueueStatus>()
                .Property(qs => qs.QueueStatusID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<QueueStatusAssignment>()
                .Property(qsa => qsa.QueueStatusAssignmentID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Rating>()
                .Property(ra => ra.RatingID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Region>()
                .Property(re => re.RegionID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Role>()
                .Property(ro => ro.RoleId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Service>()
                .Property(se => se.ServiceID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ServiceWindow>()
                .Property(sw => sw.WindowID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Status>()
                .Property(st => st.StatusID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Ticket>()
                .Property(ti => ti.TicketID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<TicketStatus>()
                .Property(ts => ts.TicketStatusID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<TicketStatusAssignment>()
                .Property(tsa => tsa.TicketStatusAssignmentID)
                .ValueGeneratedOnAdd();

        }
    }
}

