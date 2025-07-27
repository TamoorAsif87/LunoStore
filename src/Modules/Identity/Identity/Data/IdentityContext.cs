namespace Identity.Data;

public class IdentityContext:IdentityDbContext<ApplicationUser>
{
    public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("identity");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}

