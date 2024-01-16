using fastwin.Models;
using MediatR;

public class UpdateCodeCommand : IRequest<Codes>
{
    public int Id { get; set; }
    public string NewCode { get; set; }
    public StatusCode Status { get; set; }
}

