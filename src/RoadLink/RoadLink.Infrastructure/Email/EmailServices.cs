using RoadLink.Application.Abstractions.Email;

namespace RoadLink.Infrastructure.Email;

public sealed class EmailServices : IEmailService
{
    public Task SendEmailAsync(Domain.Usuarios.Email recipent, string subject, string body)
    {
        return Task.CompletedTask;
    }
}