using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DAERS.API.Data;
using DAERS.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DAERS.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        public IDaersRepository _repo { get; }
        public IMapper _mapper { get; }
        public UsersController(IDaersRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }


        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();
            var userToReturn=_mapper.Map<IEnumerable<UserForListDto>>(users);
            return Ok(userToReturn);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);
            var userToReturn=_mapper.Map<UserForDetailedDto>(user);
            return Ok(userToReturn);
        }

    }
}