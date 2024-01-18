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
    private readonly IRepository<Product> _productRepository;
    private readonly ICodeRepository<Codes> _codeRepository;
    private readonly UserManager<User> _userManager;

    public CreateAssetCommandHandler(IRepository<Asset> assetRepository, IRepository<UserCode> userCodeRepository, ICodeRepository<Codes> codeRepository,UserManager<User> userManager,IRepository<Product> productRepository)
    {
        _assetRepository = assetRepository;
        _userCodeRepository = userCodeRepository;
        _codeRepository = codeRepository;
        _userManager = userManager;
        _productRepository = productRepository;
    }
    

    public async Task<Unit> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(request.CreateAssetRequest.UserId);
            var product = await _productRepository.GetByIdAsync(request.CreateAssetRequest.ProductId);
            var code = await _codeRepository.GetByCodeAsync(request.CreateAssetRequest.Code);
            var userCodes = await _userCodeRepository.FindAsync(uc => uc.CodeId == code.Id && uc.UserId == user.Id ,cancellationToken);

            if (user == null)
            {
                throw new ArgumentException("User does not exist");
            }

            if (code == null)
            {
                throw new ArgumentException("Code does not exist");
            }

            if (userCodes == null || !userCodes.Any())
            {
                throw new InvalidOperationException("The specified code does not belong to the user or is not locked.");
            }

            if (product.LastUsageDate < DateTime.UtcNow)
            {
                throw new InvalidOperationException("The product's LastUsageDate has passed. Please select another product.");
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

