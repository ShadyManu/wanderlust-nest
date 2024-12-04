using Microsoft.Extensions.Configuration;

namespace Infrastructure.Helpers;

public static class ConnectionResolver
{
    private static readonly IConfigurationRoot configuration;

    static ConnectionResolver()
    {
        // Configura il builder per caricare le configurazioni (es. da appsettings.json o altre fonti)
        configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Assicurati che sia la directory del progetto principale
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Leggi appsettings.json
            .AddEnvironmentVariables() // Supporto per variabili d'ambiente, se necessario
            .Build();
    }

    /// <summary>
    /// Ottiene una stringa di connessione specifica per nome.
    /// </summary>
    /// <param name="name">Il nome della stringa di connessione nel file di configurazione.</param>
    /// <returns>La stringa di connessione corrispondente.</returns>
    public static string GetConnectionString(string name)
    {
        return configuration.GetConnectionString(name) 
               ?? throw new InvalidOperationException($"La stringa di connessione '{name}' non è stata trovata.");
    }
    
    public static string GetApplicationDbConnectionString() => GetConnectionString("DefaultConnection");

    public static string GetIdentityDbConnectionString() => GetConnectionString("IdentityConnection");
}