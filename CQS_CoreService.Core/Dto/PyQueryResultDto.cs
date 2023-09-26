namespace CQS_CoreService.Core.Dto;

public class PyQueryResultDto
{
    public bool Success { get; set; }
    public string RawJson { get; set; }
    public string GeoJson { get; set; }
}