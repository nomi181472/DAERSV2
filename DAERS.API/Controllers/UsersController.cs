using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DAERS.API.Data;
using DAERS.API.Dtos;
using DAERS.API.Helpers;
using DAERS.API.Helpers.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DAERS.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
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
        public async Task<IActionResult> GetUsers([FromQuery]UParams uParams)
        {
           var currentUserId= int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
           var userfromrepo=await _repo.GetUser(currentUserId);
           uParams.UserId=currentUserId;
           if(string.IsNullOrEmpty(uParams.Category))
           {
               uParams.Category=userfromrepo.Category;
           }

            var users = await _repo.GetUsers(uParams);
            var userToReturn=_mapper.Map<IEnumerable<UserForListDto>>(users);
            Response.AddPagination(users.CurrentPage,users.PageSize,users.TotalCount,users.TotalPage);
            return Ok(userToReturn);
        }
        [HttpGet("{id}",Name="GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);
            var userToReturn=_mapper.Map<UserForDetailedDto>(user);
            return Ok(userToReturn);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id,UserForUpdateDto udto)
        {
            if(id!=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            return Unauthorized();
            var userfromrepo=await _repo.GetUser(id);
            _mapper.Map(udto,userfromrepo);
            if(await _repo.SaveAll())
            return NoContent();
            throw new System.Exception($"Updating user{id} failed on save");

        }
    }
}