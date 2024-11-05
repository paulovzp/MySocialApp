using Newtonsoft.Json;

namespace MySocialApp.Application;

public class ErrorResponse
{
    public string Message { get; set; }
    public IDictionary<string, string[]> ErrorList { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
