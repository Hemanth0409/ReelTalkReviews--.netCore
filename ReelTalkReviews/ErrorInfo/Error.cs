
using System.Text.Json;

namespace MiddleWare2.ErrorInfo
{
    public class Error
    {
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
    
}
