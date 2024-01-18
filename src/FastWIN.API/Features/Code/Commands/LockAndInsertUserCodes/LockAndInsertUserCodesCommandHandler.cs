using fastwin.Entities;
using fastwin.Infrastructure.UnitOfWork;
using fastwin.Interfaces;
using fastwin.Models;
using MediatR;

public class LockAndInsertUserCodesCommandHandler : IRequestHandler<LockAndInsertUserCodesCommand, Unit>
{
    private readonly IRepository<UserCode> _userCodeRepository;
    private readonly ICodeRepository<Codes> _codeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LockAndInsertUserCodesCommandHandler(
        ICodeRepository<Codes> codeRepository,
        IRepository<UserCode> userCodeRepository,
        IUnitOfWork unitOfWork)
    {
        _codeRepository = codeRepository;
        _userCodeRepository = userCodeRepository;
        _unitOfWork = unitOfWork;
    }

        public async Task<Unit> Handle(LockAndInsertUserCodesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);

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

                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw new InvalidOperationException("An error occurred during the transaction. Details: " + ex.Message, ex);
        }
            finally
            {
                _unitOfWork.Dispose();
            }
        }
}
