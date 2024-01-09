using fastwin.Entities;
using fastwin.Models;
using fastwin.Requests;
using MediatR;

public class GenerateCodesCommand : IRequest<Codes>
{
    public GenerateCodesRequest CodeRequest { get; }

    public GenerateCodesCommand(GenerateCodesRequest codeRequest)
    {
        CodeRequest = codeRequest;
    }
}
