using ApiProject.ApiLogic.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiProject.BusinessLogic.Services
{
    public interface IThesisRequestService
    {
        Task<ThesisRequestResponse> CreateRequestAsync(Guid requesterId, Guid thesisId, Guid receiverId, string requestType, string? message);
        Task<IEnumerable<ThesisRequestResponse>> GetRequestsForUserAsync(Guid userId);
        Task<ThesisRequestResponse?> GetRequestByIdAsync(Guid requestId);
        Task RespondToRequestAsync(Guid requestId, Guid receiverId, bool accepted, string? message);
    }
}
