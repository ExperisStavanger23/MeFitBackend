using MeFitBackend.Data.DTO.Users;
using MeFitBackend.Data.Entities;

namespace MeFitBackend.Data.DTO.Created
{
    public class CreatedDTO
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public int CreatorId { get; set; }
        public UserDTO Creator { get; set; }
        public Enums.EntityType EntityType { get; set; }
    }
}
