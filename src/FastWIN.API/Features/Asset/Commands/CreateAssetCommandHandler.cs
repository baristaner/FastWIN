using fastwin.Entities;
using fastwin.Interfaces;
using fastwin.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;

public class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, Unit>
{
    private readonly IRepository<Asset> _assetRepository;
    private readonly IRepository<UserCode> _userCodeRepository;
    private readonly ICodeRepository<Codes> _codeRepository;
    private readonly UserManager<User> _userManager;

    public CreateAssetCommandHandler(IRepository<Asset> assetRepository, IRepository<UserCode> userCodeRepository, ICodeRepository<Codes> codeRepository,UserManager<User> userManager)
    {
        _assetRepository = assetRepository;
        _userCodeRepository = userCodeRepository;
        _codeRepository = codeRepository;
        _userManager = userManager;
    }
    

    public async Task<Unit> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(request.CreateAssetRequest.UserId);
            var code = await _codeRepository.GetByCodeAsync(request.CreateAssetRequest.Code);

            if(user == null)
            {
                throw new ArgumentException("User does not exist");
            }

            if (code == null)
            {
                throw new ArgumentException("Code does not exist");
            }

            int codeId = code.Id;

            if (code.Status == StatusCode.Locked)
            {
                string sql = "EXEC dbo.sp_CreateAssetandCreateUserCodeandUpdateCodes @ProductId, @CodeId, @UserId, @CodeStatus,@UserCodeStatus";
                var parameters = new[]
                {
                new SqlParameter("@ProductId", request.CreateAssetRequest.ProductId),
                new SqlParameter("@CodeId", code.Id),
                new SqlParameter("@UserId", request.CreateAssetRequest.UserId),
                new SqlParameter("@CodeStatus", 1),
                new SqlParameter("@UserCodeStatus", 1)
            };

                await _assetRepository.ExecuteSqlAsync(sql, cancellationToken, parameters);

                return Unit.Value; 
            }
            else
            {
                throw new InvalidOperationException("The code is not locked.");
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw; 
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw; 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex}");
            throw new ApplicationException("An unexpected error occurred while generating codes.", ex);
        }
    }

}

