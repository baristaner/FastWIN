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

        public void GenerateCodes()
        {
            _context.Database.ExecuteSqlRaw("EXEC dbo.GenerateCodes");
        }

        public IEnumerable<Codes> GetCodes()
        {
            return _context.Codes.ToList();
        }

        public async Task<Codes> GetCodeById(int id)
        {
            return await _context.Codes.FindAsync(id);
        }

        public async Task UpdateCode(int id, string newCode, bool isActive)
        {
            var codeToUpdate = await _context.Codes.FindAsync(id);

            if (codeToUpdate != null)
            {
                codeToUpdate.Code = newCode;
                codeToUpdate.IsActive = isActive;
                codeToUpdate.ModifiedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }
        }


    }
}
