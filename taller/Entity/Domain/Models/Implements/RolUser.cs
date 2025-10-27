using Entity.Domain.Models.Base;
using Entity.Domain.Models.Implements;
using Entity.Infrastructure.Anotation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class RolUser : BaseModel
{
    public int UserId { get; set; }

    public int RolId { get; set; }
    public User User { get; set; }

    public Rol Rol { get; set; }
}
