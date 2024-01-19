using fastwin.Entities;
using fastwin.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace fastwin.Requests
{
    public class CreateAssetRequest
    {
        public int ProductId { get; set; }
        public string Code { get; set; }

        [BindNever]
        public string UserId { get; set; }
    }
}
