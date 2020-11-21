using AutoMapper;
using CodersAcademy.API.Model;
using CodersAcademy.API.Respository;
using CodersAcademy.API.ViewModel.Request;
using CodersAcademy.API.ViewModel.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodersAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserRepository UserRepository { get; set; }
        private IMapper Mapper { get; set; }
        private AlbumRepository AlbumRep0ository { get; set; }
        public byte[] Encodin { get; private set; }

        public UserController(UserRepository userRepository,IMapper mapper, AlbumRepository albumRepository)
        {
            this.UserRepository = userRepository;
            Mapper = mapper;
            AlbumRep0ository = albumRepository;
        }

        //Authentic User
        [HttpPost("authenticate")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var password = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.Password));

            var user = await this.UserRepository.AuthenticateAsync(request.Email, password);

            if(user == null)
            {
                return UnprocessableEntity(new
                {
                    Message = "Email/Senha inválidos"
                });
            }

            var result = this.Mapper.Map<UserResponse>(user);

            return Ok(result);

        }
        //Register User
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)            
                return BadRequest(ModelState);

            var user = this.Mapper.Map<User>(request);

            user.Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Password));
            user.Photo = $"https://robohash.org/{Guid.NewGuid()}.png?bgset=any";

            await this.UserRepository.CreateAsync(user);

            var result = this.Mapper.Map<UserResponse>(user);

            return Created($"{result.Id}", result);
            
        }
        //API of Favorite
        //API remove Favorite
        //API ALL Favorite of Users

    }
}
