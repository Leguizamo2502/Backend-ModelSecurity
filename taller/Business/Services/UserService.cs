using AutoMapper;
using Business.Interfaces.IBusinessImplements;
using Business.Repository;
using Data.Interfaces.IDataImplement;
using Entity.Domain.Models.Implements;
using Entity.DTOs.Default;
using Entity.DTOs.Select;
using Helpers.Business.Business.Helpers.Validation;
using Helpers.Initialize;
using Microsoft.Extensions.Logging;
using Utilities.Custom;

namespace Business.Services
{
    public class UserService : BusinessBasic<UserSelectDto, UserDto, User>, IUserService
    {
        private readonly IUserRepository _dataUser;
        private readonly ILogger<UserService> _logger;
        private readonly EncriptePassword _utilities;

        private readonly IRolUserService _rolUserService;


        public UserService(IUserRepository data, ILogger<UserService> logger, EncriptePassword utilities, IMapper mapper, IRolUserService rolUserService) : base(data, mapper)
        {
            _dataUser = data;
            _utilities = utilities;
            _logger = logger;
            _rolUserService = rolUserService;
        }

        //Nuevo metodo 

            public async Task<UserDto> CreateAsyncUser(UserDto dto)
            {
                BusinessValidationHelper.ThrowIfNull(dto, "El DTO no puede ser nulo.");

                // Mapeamos primero
                var userEntity = _mapper.Map<User>(dto);
                InitializeLogical.InitializeLogicalState(userEntity); // Inicializa estado lógico (is_deleted = false)
                                                              // Inicializa estado lógico (is_deleted = false)
            var createdEntity = await _dataUser.CreateAsync(userEntity);

                _ = _rolUserService.AsignateUserRTo(createdEntity.Id);

                return _mapper.Map<UserDto>(createdEntity);

                // Luego encripto la contraseña antes de guardar
                //userEntity.password = _utilities.EncripteSHA256(userEntity.password);
                //userEntity.password = userEntity.password;


            }

        // Crear
        public async Task<User> createUserGoogle(string email, string name)
        {
            var user = await _dataUser.FindEmail(email);


            if (user != null) return user;

            var newUser = new User
            {
                Name = name,
                Password = null,
                Email = email
            };
            InitializeLogical.InitializeLogicalState(newUser); // Inicializa estado lógico (is_deleted = false)
                                                              // Inicializa estado lógico (is_deleted = false)
            await _dataUser.CreateAsync(newUser);
            return newUser;


            //password = _utilities.EncripteSHA256("hola"),
        }

        //Actalizar
        public async Task<bool> UpdateAsyncUser(UserDto dto)
        {
            BusinessValidationHelper.ThrowIfNull(dto, "El DTO no puede ser nulo.");
            var entity = _mapper.Map<User>(dto);
            return await _dataUser.UpdateAsync(entity);
            //entity.password = entity.password;
            //entity.password = _utilities.EncripteSHA256(entity.password);
        }


        
    }
}
