namespace DigitalIndoorAPI.DTOs.Response
{
    public record MessageViewDto(string Message);
    public record ValidationErrorDto(string FieldName, string Message);
}
