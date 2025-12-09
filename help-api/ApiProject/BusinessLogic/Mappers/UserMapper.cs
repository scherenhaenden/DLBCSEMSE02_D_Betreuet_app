using ApiProject.BusinessLogic.Models;
using ApiProject.DatabaseAccess.Entities;


namespace ApiProject.BusinessLogic.Mappers
{
    public static class UserMapper
    {
        public static User ToBusinessModel(UserDataAccessModel dataAccessModel)
        {
            if (dataAccessModel == null)
            {
                return null;
            }

            return new User
            {
                Id = dataAccessModel.Id,
                FirstName = dataAccessModel.FirstName,
                LastName = dataAccessModel.LastName,
                Email = dataAccessModel.Email,
                Roles = dataAccessModel.UserRoles.Select(ur => ur.Role.Name).ToList()
            };
        }

        public static UserDataAccessModel ToDataAccessModel(User businessModel)
        {
            if (businessModel == null)
            {
                return null;
            }

            // This is a simplified mapping. In a real application, you would handle
            // password hashing and other complexities here or in the service layer.
            return new UserDataAccessModel
            {
                Id = businessModel.Id,
                FirstName = businessModel.FirstName,
                LastName = businessModel.LastName,
                Email = businessModel.Email,
                PasswordHash = "" // Password should be hashed in the service
            };
        }
    }
}
