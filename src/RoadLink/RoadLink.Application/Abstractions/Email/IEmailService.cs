namespace RoadLink.Application.Abstractions.Email;

public interface IEmailService
{
    Task SendEmailAsync(Domain.Usuarios.Email recipent, string subject, string body);
}