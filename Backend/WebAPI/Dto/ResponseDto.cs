using System.Text.Json;

namespace WebAPI.Dto;

public class ResponseDto
{
    public Boolean Success { get; set; }
    public int Code { get; set; }
    public Object[] Data { get; set; }
    public Exception Error { get; set; }

    public ResponseDto(bool success = true, int code = 200, Exception error = null, params Object[] data)
    {
        this.Success = success;
        this.Code = code;
        this.Data = data;
        this.Error = error;
    }

    public ResponseDto(params Object[] data)
    {
        this.Success = true;
        this.Code = 200;
        this.Data = data;
        this.Error = null;
    }

    public ResponseDto(int code, Exception error)
    {
        this.Success = false;
        this.Code = code;
        this.Data = null;
        this.Error = error;
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}