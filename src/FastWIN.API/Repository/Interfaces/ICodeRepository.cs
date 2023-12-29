using fastwin.Models;

namespace fastwin.Interfaces
{
    public interface ICodeRepository : IRepository<Codes>
    {
        Task GenerateCodesAsync(int numOfCodes, string characterSet, int expirationMonths, DateTime? expirationDate);
    }

}
