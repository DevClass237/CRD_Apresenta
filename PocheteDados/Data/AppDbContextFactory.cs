using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PocheteDados.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseMySql(
            "Server=localhost;Database=pochetesenai;User=root;Password=;Port=3306;",
            ServerVersion.AutoDetect("Server=localhost;Database=pochetesenai;User=root;Password=;Port=3306;")
        );

        return new AppDbContext(optionsBuilder.Options);
    }
}
