using FluentValidation;

/// <summary>
/// Validador para la entidad ClienteDTO.
/// </summary>
public class ClienteDTOValidator : AbstractValidator<ClienteDTO>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="ClienteDTOValidator"/>.
    /// </summary>
    public ClienteDTOValidator()
    {
        RuleFor(c => c.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .Length(3, 100).WithMessage("El nombre debe tener entre 3 y 100 caracteres.");

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("El email es obligatorio.")
            .EmailAddress().WithMessage("El formato del email no es válido.");

        RuleFor(c => c.Telefono)
            .NotEmpty().WithMessage("El teléfono es obligatorio.")
            .Matches(@"^\d{10}$").WithMessage("El teléfono debe tener 10 dígitos.");
    }
}