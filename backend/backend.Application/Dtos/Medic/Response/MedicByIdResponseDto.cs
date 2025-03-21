namespace backend.Application.Dtos.Medic.Response;

public class MedicByIdResponseDto
{
    public int MedicId { get; set; }
    public int UserId  { get; set; }
    public string? Specialty { get; set; }
    public string? Phone { get; set; }
    public int State { get; set; }
}