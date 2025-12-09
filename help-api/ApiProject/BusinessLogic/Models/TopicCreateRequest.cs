namespace ApiProject.BusinessLogic.Models
{
    public class TopicCreateRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string SubjectArea { get; set; }
        public List<Guid> TutorIds { get; set; } = new List<Guid>();
    }
}
