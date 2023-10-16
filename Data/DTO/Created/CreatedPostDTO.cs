using MeFitBackend.Data.DTO.Users;

namespace MeFitBackend.Data.DTO.Created
{
    public class CreatedPostDTO
    {
        public int EntityId { get; set; }
        public Enums.EntityType EntityType { get; set; }
        public int CreatorId { get; set; }
        public UserDTO User { get; set; }

    }
}
