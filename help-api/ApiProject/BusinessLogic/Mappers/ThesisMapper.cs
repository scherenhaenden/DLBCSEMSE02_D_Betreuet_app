using ApiProject.BusinessLogic.Models;
using ApiProject.DatabaseAccess.Entities;

namespace ApiProject.BusinessLogic.Mappers
{
    public static class ThesisMapper
    {
        public static Thesis ToBusinessModel(ThesisDataAccessModel dataAccessModel)
        {
            if (dataAccessModel == null)
            {
                return null;
            }

            return new Thesis
            {
                Id = dataAccessModel.Id,
                Title = dataAccessModel.Title,
                SubjectArea = dataAccessModel.SubjectArea,
                Status = dataAccessModel.Status?.Name,
                BillingStatus = dataAccessModel.BillingStatus?.Name,
                OwnerId = dataAccessModel.OwnerId,
                TutorId = dataAccessModel.TutorId,
                SecondSupervisorId = dataAccessModel.SecondSupervisorId,
                TopicId = dataAccessModel.TopicId,
                DocumentFileName = dataAccessModel.Document?.FileName
            };
        }
    }
}
