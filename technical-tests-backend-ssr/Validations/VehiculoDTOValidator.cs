using FluentValidation;

/// <summary>
/// Validador para la entidad VehiculoDTO.
/// </summary>
public class VehiculoDTOValidator : AbstractValidator<VehiculoDTO>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="VehiculoDTOValidator"/>.
    /// </summary>
    public VehiculoDTOValidator()
    {
        RuleFor(v => v.Marca)
            .NotEmpty().WithMessage("La marca es obligatoria.")
            .Length(2, 50).WithMessage("La marca debe tener entre 2 y 50 caracteres.");

        RuleFor(v => v.Modelo)
            .NotEmpty().WithMessage("El modelo es obligatorio.")
            .Length(1, 50).WithMessage("El modelo debe tener entre 1 y 50 caracteres.");

        RuleFor(v => v.Año)
            .InclusiveBetween(1900, 2100).WithMessage("El año debe estar entre 1900 y 2100.");

        RuleFor(v => v.Precio)
            .GreaterThanOrEqualTo(0).WithMessage("El precio debe ser mayor o igual a 0.");

        RuleFor(v => v.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("El stock debe ser mayor o igual a 0.");
    }
}