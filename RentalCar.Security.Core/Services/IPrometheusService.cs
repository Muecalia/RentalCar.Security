namespace RentalCar.Security.Core.Services;

public interface IPrometheusService
{
    void AddAccountCounter(string statusCodes);
    void AddRoleCounter(string statusCodes);
    void AddPasswordCounter(string statusCodes);
    void AddLoginCounter(string statusCodes);
}