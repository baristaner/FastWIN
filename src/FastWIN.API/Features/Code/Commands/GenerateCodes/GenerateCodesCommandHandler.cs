using fastwin.Entities;
using fastwin.Features.Products.Commands.AddProduct;
using fastwin.Interfaces;
using fastwin.Models;
using FluentValidation;
using MediatR;
using Microsoft.Data.SqlClient;

public class GenerateCodesCommandHandler : IRequestHandler<GenerateCodesCommand, Codes>
{
    private readonly IRepository<Codes> _repository;
   
    public GenerateCodesCommandHandler(IRepository<Codes> repository)
    {
        _repository = repository;
    }

    public async Task<Codes> Handle(GenerateCodesCommand request, CancellationToken cancellationToken)
    {
        try
        {
            string sql = "EXEC dbo.sp_GenerateCodes @NumOfCodes, @CharacterSet, @ExpirationMonths, @ExpirationDate";

            var parameters = new[]
            {
            new SqlParameter("@NumOfCodes", request.CodeRequest.NumOfCodes),
            new SqlParameter("@CharacterSet", request.CodeRequest.CharacterSet),
            new SqlParameter("@ExpirationMonths", request.CodeRequest.ExpirationMonths),
            new SqlParameter("@ExpirationDate", (object)request.CodeRequest.ExpirationDate ?? DBNull.Value)
        };

            await _repository.ExecuteStoredProcedureAsync(sql, cancellationToken, parameters);

            return new Codes(); // i have to turn back and change the return method
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());

            throw new ApplicationException("An error occurred while generating codes.", ex);
        }
    }
}
