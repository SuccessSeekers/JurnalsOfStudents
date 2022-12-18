using System.Text.Json;

namespace WebAPI.Dto;

public class ResponseDto<T>
{
    public Boolean Success { get; set; } = true;
    public int Code { get; set; } = 200;
    public T[] Data { get; set; } = null;
    public string Message { get; set; } = null;

    public ResponseDto(params T[] data)
    {
        this.Success = true;
        this.Code = 200;
        this.Data = data;
    }

    public ResponseDto(IEnumerable<T> data)
    {
        this.Success = true;
        this.Code = 200;
        this.Data = data.ToArray();
    }

    public ResponseDto(int code, Exception error)
    {
        this.Success = false;
        this.Code = code;
        this.Data = null;
        this.Message = error.Message;
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}