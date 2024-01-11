using fastwin.Interfaces;
using fastwin.Models;
using MediatR;

namespace fastwin.Features.Code.Queries.GetAllCodes
{
    public class GetAllCodesQueryHandler : IRequestHandler<GetAllCodesQuery, IEnumerable<Codes>>
    {
        private readonly IRepository<Codes> _codeRepository;

        public GetAllCodesQueryHandler(IRepository<Codes> codeRepository)
        {
            _codeRepository = codeRepository;
        }

        public async Task<IEnumerable<Codes>> Handle(GetAllCodesQuery request, CancellationToken cancellationToken)
        {
            return await _codeRepository.GetAllAsync();
        }
    }
}
