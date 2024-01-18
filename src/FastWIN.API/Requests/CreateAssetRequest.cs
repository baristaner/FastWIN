using fastwin.Entities;
using fastwin.Models;

namespace fastwin.Requests
{
    public class CreateAssetRequest
    {
        public int ProductId { get; set; }
        public string Code { get; set; }
        public string UserId { get; set; }
    }
}
