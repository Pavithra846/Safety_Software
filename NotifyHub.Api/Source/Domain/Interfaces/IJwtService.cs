using NotifyHub.Api.Source.Domain.Entities;

namespace NotifyHub.Api.Source.Domain.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Users user);
    }
}
