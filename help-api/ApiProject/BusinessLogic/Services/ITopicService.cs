using ApiProject.BusinessLogic.Models;

namespace ApiProject.BusinessLogic.Services
{
    /// <summary>
    /// Interface for the Topic Service, providing CRUD operations for topics.
    /// </summary>
    public interface ITopicService
    {
        /// <summary>
        /// Returns all topics in a paginated result.
        /// </summary>
        Task<PaginatedResult<Topic>> GetAllAsync(int page, int pageSize);

        /// <summary>
        /// Returns a topic by its ID.
        /// </summary>
        Task<Topic?> GetByIdAsync(Guid id);

        /// <summary>
        /// Searches for topics by title or subject area.
        /// </summary>
        Task<PaginatedResult<Topic>> SearchAsync(string searchTerm, int page, int pageSize);

        /// <summary>
        /// Creates a new topic.
        /// </summary>
        Task<Topic> CreateTopicAsync(TopicCreateRequest request);

        /// <summary>
        /// Updates an existing topic.
        /// </summary>
        Task<Topic> UpdateTopicAsync(Guid id, TopicUpdateRequest request);

        /// <summary>
        /// Deletes a topic by its ID.
        /// </summary>
        Task<bool> DeleteTopicAsync(Guid id);
    }
}
