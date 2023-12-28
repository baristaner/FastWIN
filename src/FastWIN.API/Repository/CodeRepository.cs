using fastwin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace fastwin.Repository
{
    public class CodeRepository
    {
        private readonly CodeDbContext _context;

        public CodeRepository(CodeDbContext context)
        {
            _context = context;
        }

        public async Task GenerateCodesAsync()
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC dbo.GenerateCodes");
            
        }

        public async Task<IEnumerable<Codes>> GetCodesAsync()
        {
            return await _context.Codes.ToListAsync();
        }

        public async Task<Codes> GetCodeByIdAsync(int id)
        {
            return await _context.Codes.FindAsync(id);
        }

        public async Task<Codes> UpdateCodeAsync(int id, string newCode, bool isActive)
        {
            var codeToUpdate = await _context.Codes.FindAsync(id);

            if (codeToUpdate != null)
            {
                codeToUpdate.Code = newCode;
                codeToUpdate.IsActive = isActive;

                await _context.SaveChangesAsync();
                return codeToUpdate; 
            }

            return null; 
        }

    }
}
