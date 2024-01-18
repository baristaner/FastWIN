using fastwin.Entities;
using fastwin.Interfaces;
using fastwin.Models;
using MediatR;

public class LockAndInsertUserCodesCommandHandler : IRequestHandler<LockAndInsertUserCodesCommand, Unit>
{
    private readonly IRepository<UserCode> _userCodeRepository;
    private readonly ICodeRepository<Codes> _codeRepository;

    public LockAndInsertUserCodesCommandHandler(
        ICodeRepository<Codes> codeRepository,
        IRepository<UserCode> userCodeRepository)
    {
        _codeRepository = codeRepository;
        _userCodeRepository = userCodeRepository;
    }

    public async Task<Unit> Handle(LockAndInsertUserCodesCommand request, CancellationToken cancellationToken)
    {
        var selectedcode = await _codeRepository.GetByCodeAsync(request.Code);

        if (selectedcode == null)
        {
            throw new ArgumentNullException(nameof(selectedcode), "Code cannot be null for this operation");
        }

        if (selectedcode.ExpirationDate < DateTime.UtcNow)
        {
            throw new InvalidOperationException("The code has expired");
        }

        if (selectedcode.Status == StatusCode.Passive)
        {
            throw new InvalidOperationException("This code is already used");
        }

        selectedcode.Status = StatusCode.Locked;

        await _codeRepository.UpdateAsync(selectedcode, cancellationToken);

        var userCode = new UserCode { CodeId = selectedcode.Id, UserId = request.UserId };
        await _userCodeRepository.AddAsync(userCode, cancellationToken);

        return Unit.Value;
    }
}
