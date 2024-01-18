using fastwin.Interfaces;
using fastwin.Models;
using fastwin.Repository.Repositories;
using Hangfire;

namespace fastwin.Jobs
{
    public class CodeStatusJob
    {
        private readonly ICodeRepository<Codes> _codeRepository;

        public CodeStatusJob(ICodeRepository<Codes> codeRepository)
        {
            _codeRepository = codeRepository;
        }

        [AutomaticRetry(Attempts = 0)]
        public void CheckAndResetLockedCodes()
        {
            var lockedCodes = _codeRepository.GetLockedCodesAsync().Result;

            foreach (var code in lockedCodes)
            {
                if (code.Status == StatusCode.Locked)
                {
                    code.Status = StatusCode.Active;
                    _codeRepository.UpdateAsync(code).Wait();
                }
            }
        }
    }
}
