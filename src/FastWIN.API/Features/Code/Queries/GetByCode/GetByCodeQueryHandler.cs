using fastwin.Interfaces;
using fastwin.Models;
using MediatR;

namespace fastwin.Features.Code.Queries.GetByCode
{
    public class GetByCodeQueryHandler : IRequestHandler<GetByCodeQuery, Codes>
    {
        private readonly ICodeRepository<Codes> _codeRepository;

        public GetByCodeQueryHandler(ICodeRepository<Codes> codeRepository)
        {
            _codeRepository = codeRepository;
        }

        public async Task<Codes> Handle(GetByCodeQuery request, CancellationToken cancellationToken)
        {
            return await _codeRepository.GetByCodeAsync(request.Code);
        }
    }
}
