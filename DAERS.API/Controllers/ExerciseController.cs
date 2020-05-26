using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DAERS.API.Data;
using DAERS.API.Dtos;
using DAERS.API.Dtos.DtosForExercise;
using DAERS.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DAERS.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseRepository _repo;
        private readonly IMapper _mapper;
        public ExerciseController(IExerciseRepository repo, IMapper mapper)
        {
            this._mapper = mapper;
            this._repo = repo;

        }
        [HttpPost("AddExercise")]
        public async Task<IActionResult> AddExercise(ExerciseForAddDto exerciseForAdd)
        {
            exerciseForAdd.Name=exerciseForAdd.Name.ToLower();
            if(await _repo.ExerciseExists(exerciseForAdd.Name))
            return BadRequest("Exercise already Exists");
            var exerciseToCreate=_mapper.Map<Exercise>(exerciseForAdd);
            var createdExercise=await _repo.AddExercise(exerciseToCreate);
            var exerciseForreturn=_mapper.Map<ExerciseForDetailDto>(createdExercise);
            
            return CreatedAtRoute("GetExercise",new{Controller="Exercise",Id=createdExercise.Id},exerciseForreturn);
        }




        [HttpGet("{id}",Name="GetExercise")]
        public async Task<IActionResult> GetExercise(int id)
        {
           var exercise=await _repo.GetExercise(id);
           var exerciseForreturn=_mapper.Map<ExerciseForDetailDto>(exercise);
           return Ok(exerciseForreturn);
        }
        [HttpGet]
        public async Task<IActionResult> GetListExercises(){
            var exercises=await _repo.GetListExercises();
            var exerciseForreturn=_mapper.Map<IEnumerable<ExerciseForListDto>>(exercises);
            return Ok(exercises);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExercise(int id,ExerciseForUpdateDto udto)
        {
            var userfromrepo=await _repo.GetExercise(id);
            _mapper.Map(udto,userfromrepo);
            if(await _repo.SaveAllE())
            return NoContent();
            throw new System.Exception($"Updating Exercise:{id} failed on save");

        }
    }
}