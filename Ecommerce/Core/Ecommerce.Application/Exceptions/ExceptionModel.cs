using Newtonsoft.Json;

namespace Ecommerce.Application.Exceptions;

public class ExceptionModel
{
    public IEnumerable<string> Errors { get; set; }
    public int StatusCode { get; set; }
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
