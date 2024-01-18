using fastwin.Entities;
using fastwin.Requests;
using MediatR;

public class CreateAssetCommand : IRequest<Unit>
    {
        public CreateAssetRequest CreateAssetRequest { get; set; }

        public CreateAssetCommand(CreateAssetRequest createAssetRequest)
        {
            CreateAssetRequest = createAssetRequest;
        }
}

