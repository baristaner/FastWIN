using fastwin.Models;
using MediatR;

namespace fastwin.Features.Code.Queries.GetByCode
{
    public class GetByCodeQuery : IRequest<Codes>
    {
        public string Code { get; set; }

        public GetByCodeQuery(string code)
        {
            Code = code;
        }
    }
}
