namespace Application.Dto.Note;

public class UpdateNoteDto
{
    public Guid NoteId { get; init; }
    public string Text { get; init; }
}