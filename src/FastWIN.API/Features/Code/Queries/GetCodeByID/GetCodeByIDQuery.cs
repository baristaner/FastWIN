using fastwin.Models;
using MediatR;

public class GetCodeByIdQuery : IRequest<Codes>
{
   public int Id { get; set; }

   public GetCodeByIdQuery(int id)
     {
        Id = id;
     }
}
