using fastwin.Interfaces;
using fastwin.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;


public class UpdateCodeCommandHandler : IRequestHandler<UpdateCodeCommand, Codes>
{
    private readonly IRepository<Codes> _codeRepository;
    private readonly IValidator<Codes> _validator;

    public UpdateCodeCommandHandler(IRepository<Codes> codeRepository,IValidator<Codes> codeValidator)
    {
        _codeRepository = codeRepository;
        _validator = codeValidator;
    }

    public async Task<Codes> Handle(UpdateCodeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var codeToUpdate = await _codeRepository.GetByIdAsync(request.Id);

            codeToUpdate.Code = request.NewCode;
            codeToUpdate.IsActive = request.IsActive;

           

            var validationContext = new ValidationContext<Codes>(codeToUpdate);
            var validationResult = await _validator.ValidateAsync(validationContext);

            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(error => error.ErrorMessage);
                throw new ValidationException(validationResult.Errors);
            }

            await _codeRepository.UpdateAsync(codeToUpdate);

            var updatedCode = await _codeRepository.GetByIdAsync(request.Id);

            return updatedCode;
        }
        catch (ValidationException ex)
        {
            Console.WriteLine($"Validation failed: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            throw;
        }
    }

}

