using FluentValidation;

/// <summary>
/// Validador para la entidad VentaDTO.
/// </summary>
public class VentaDTOValidator : AbstractValidator<VentaDTO>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="VentaDTOValidator"/>.
    /// </summary>
    public VentaDTOValidator()
    {
        RuleFor(v => v.ClienteId)
            .GreaterThan(0).WithMessage("El ClienteId es obligatorio y debe ser mayor a 0.");

        RuleFor(v => v.VehiculoId)
            .GreaterThan(0).WithMessage("El VehiculoId es obligatorio y debe ser mayor a 0.");

        RuleFor(v => v.Fecha)
            .NotEmpty().WithMessage("La fecha es obligatoria.");

        RuleFor(v => v.Total)
            .GreaterThanOrEqualTo(0).WithMessage("El total debe ser mayor o igual a 0.");
    }
}