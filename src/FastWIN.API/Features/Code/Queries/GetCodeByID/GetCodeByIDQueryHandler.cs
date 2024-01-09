using fastwin.Interfaces;
using fastwin.Models;
using MediatR;

public class GetCodeByIdQueryHandler : IRequestHandler<GetCodeByIdQuery, Codes>
{
    private readonly IRepository<Codes> _codeRepository;

    public GetCodeByIdQueryHandler(IRepository<Codes> codeRepository)
    {
        _codeRepository = codeRepository;
    }

    public async Task<Codes> Handle(GetCodeByIdQuery request, CancellationToken cancellationToken)
    {
        return await _codeRepository.GetByIdAsync(request.Id);
    }
}

