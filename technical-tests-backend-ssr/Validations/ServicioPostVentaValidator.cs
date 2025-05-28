using FluentValidation;

/// <summary>
/// Validador para la entidad ServicioPostVentaDTO.
/// </summary>
public class ServicioPostVentaDTOValidator : AbstractValidator<ServicioPostVentaDTO>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="ServicioPostVentaDTOValidator"/>.
    /// </summary>
    public ServicioPostVentaDTOValidator()
    {
        RuleFor(s => s.ClienteId)
            .GreaterThan(0).WithMessage("El ClienteId es obligatorio y debe ser mayor a 0.");

        RuleFor(s => s.TipoServicio)
            .NotEmpty().WithMessage("El tipo de servicio es obligatorio.")
            .Length(3, 100).WithMessage("El tipo de servicio debe tener entre 3 y 100 caracteres.");

        RuleFor(s => s.Fecha)
            .NotEmpty().WithMessage("La fecha es obligatoria.");

        RuleFor(s => s.Estado)
            .NotEmpty().WithMessage("El estado es obligatorio.")
            .Length(3, 50).WithMessage("El estado debe tener entre 3 y 50 caracteres.");
    }
}