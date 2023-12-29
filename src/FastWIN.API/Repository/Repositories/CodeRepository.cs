using fastwin.Interfaces;
using fastwin.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace fastwin.Repository.Repositories
{
    public class CodeRepository : GenericRepository<Codes>, ICodeRepository
    {
        private readonly CodeDbContext _context;
        public CodeRepository(CodeDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task GenerateCodesAsync(int numOfCodes, string characterSet, int expirationMonths, DateTime? expirationDate)
        {
            var numOfCodesParam = new SqlParameter("@NumOfCodes", numOfCodes);
            var characterSetParam = new SqlParameter("@CharacterSet", characterSet);
            var expirationMonthsParam = new SqlParameter("@ExpirationMonths", expirationMonths);
            var expirationDateParam = new SqlParameter("@ExpirationDate", expirationDate ?? (object)DBNull.Value);

            await _context.Database.ExecuteSqlRawAsync("EXEC dbo.sp_GenerateCodes @NumOfCodes, @CharacterSet, @ExpirationMonths, @ExpirationDate",
                numOfCodesParam, characterSetParam, expirationMonthsParam, expirationDateParam);
        }
    }
}
