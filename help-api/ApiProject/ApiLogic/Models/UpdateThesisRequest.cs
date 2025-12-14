namespace ApiProject.ApiLogic.Models
{
    public class UpdateThesisRequest
    {
        public string? Title { get; set; }
        public Guid? TopicId { get; set; }
        // Note: Status and Tutor assignments are handled by the request workflow, not direct updates.
    }
}
