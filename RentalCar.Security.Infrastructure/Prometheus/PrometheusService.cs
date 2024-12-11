using RentalCar.Security.Core.Services;

namespace RentalCar.Security.Infrastructure.Prometheus;

public class PrometheusService : IPrometheusService
{
    //private static readonly Counter RequestAccountCounter = Metrics.CreateCounter("account_total", "Total requisições de criação de conta", ["status_code"]);
    //private static readonly Counter RequestRoleCounter = Metrics.CreateCounter("role_total", "Total requisições de criação de perfil", ["status_code"]);
    //private static readonly Counter RequestLoginCounter = Metrics.CreateCounter("login_total", "Total requisições de login (acesso dos utilizadores)", ["status_code"]);
    //private static readonly Counter RequestLoginCounter = Metrics.CreateCounter("login_total", "Total requisições de login (acesso dos utilizadores)", ["status_code"]);
    
    
    public void AddAccountCounter(string statusCodes)
    {
        System.Diagnostics.Debug.Print(statusCodes);
    }

    public void AddRoleCounter(string statusCodes)
    {
        System.Diagnostics.Debug.Print(statusCodes);
    }

    public void AddPasswordCounter(string statusCodes)
    {
        System.Diagnostics.Debug.Print(statusCodes);
    }

    public void AddLoginCounter(string statusCodes)
    {
        System.Diagnostics.Debug.Print(statusCodes);
    }
    
    
}