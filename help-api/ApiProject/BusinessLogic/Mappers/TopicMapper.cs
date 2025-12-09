using ApiProject.BusinessLogic.Models;
using ApiProject.DatabaseAccess.Entities;


namespace ApiProject.BusinessLogic.Mappers
{
    public static class TopicMapper
    {
        public static Topic ToBusinessModel(TopicDataAccessModel dataAccessModel)
        {
            if (dataAccessModel == null)
            {
                return null;
            }

            return new Topic
            {
                Id = dataAccessModel.Id,
                Title = dataAccessModel.Title,
                Description = dataAccessModel.Description,
                SubjectArea = dataAccessModel.SubjectArea,
                IsActive = dataAccessModel.IsActive,
                TutorIds = dataAccessModel.UserTopics.Select(ut => ut.UserId).ToList()
            };
        }
    }
}
