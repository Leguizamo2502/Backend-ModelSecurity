using Entity.Domain.Models.Base;
using Entity.Infrastructure.Anotation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Domain.Models.Implements
{
    public class User : BaseModel
    {
        public string Name { get; set; }
        public string? Password { get; set; } 
        public string Email { get; set; }


        public int? PersonId { get; set; }

        public Person? Person { get; set; }

        public ICollection<RolUser> RolUsers { get; set; } = [];
    }

}
