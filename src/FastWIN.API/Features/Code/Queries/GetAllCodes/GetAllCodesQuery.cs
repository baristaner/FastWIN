using fastwin.Models;
using fastwin.Requests;
using MediatR;

namespace fastwin.Features.Code.Queries.GetAllCodes
{
    public class GetAllCodesQuery : IRequest<IEnumerable<Codes>> {} 
}
