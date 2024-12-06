namespace Application.Dto.Note;

public class NoteDto
{
    public Guid? Id { get; init; }
    public required string Text { get; init; }
    public required string LastModified { get; init; }
}