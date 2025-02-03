using RoadLink.Application.Abstractions.Messaging;
using RoadLink.Domain.Abstractions;
using RoadLink.Domain.Usuarios;

namespace RoadLink.Application.Usuarios.RegisterUsuario;

internal class RegisterUsuarioCommandHandler : ICommandHandler<RegisterUsuarioCommand, Guid>
{
    private readonly IUsuarioRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUsuarioCommandHandler(IUsuarioRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RegisterUsuarioCommand request, CancellationToken cancellationToken)
    {
        //1. Validar que el usuario no exista en la base de datos.
        var email = new Email(request.Email);
        var isUserExists = await _repository.IUserExists(email);
        if (isUserExists)
        {
            return Result.Failure<Guid>(UsuariosErrors.AlreadyExists);
        }
        //2. Encriptar el password plano del usuario que envio el cliente.
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        
        //3. Crear un objeto de tipo usuario.
        var usuario = Usuario.Create(
            new Nombre(request.Nombre),
            new Apellido(request.Apellido),
            new Email(request.Email),
            new PasswordHash(passwordHash)
        );

        //4. Insertar el usuario a la base de datos.
        _repository.Add(usuario);
        await _unitOfWork.SaveChangesAsync();
        return usuario.Id!.Value;
    }
}