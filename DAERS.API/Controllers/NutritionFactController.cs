using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DAERS.API.Data;
using DAERS.API.Dtos.DtosForNutritionFacts;
using DAERS.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DAERS.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionFactController : ControllerBase
    {
        private readonly INutritionFactRepository _repo;
        private readonly IMapper _mapper;
        public NutritionFactController(INutritionFactRepository repo, IMapper mapper)
        {
            this._mapper = mapper;
            this._repo = repo;
        }
        [HttpPost("AddNutritionFact")]
        public async Task<IActionResult> AddNutritionFact(NutritionFactForAddDto nutritionFactForAdd)
        {
            nutritionFactForAdd.Name=nutritionFactForAdd.Name.ToLower();
            if(await _repo.NutritionFactExists(nutritionFactForAdd.Name))
            return BadRequest("NutritionFact already Exists");
            var nutritionFactForCreate=_mapper.Map<NutritionFact>(nutritionFactForAdd);
            var CreatedNutritionFact=await _repo.AddNutritionFact(nutritionFactForCreate);
            var nutritionFactreturn=_mapper.Map<NutritionFactForDetailedDto>(CreatedNutritionFact);
            
            return CreatedAtRoute("GetNutritionFact",new{Controller="NutritionFact",Id=CreatedNutritionFact.Id},nutritionFactreturn);
        }
        


        [HttpGet("{id}",Name="GetNutritionFact")]
        public async Task<IActionResult> GetNutritionFact(int id)
        {
           var nutritionFact=await _repo.GetNutritionFact(id);
           var nutritionFactForReturn=_mapper.Map<NutritionFactForDetailedDto>(nutritionFact);
           return Ok(nutritionFactForReturn);
        }

         [HttpGet]
        public async Task<IActionResult> GetListNutritionFacts(){
            var  nutritions=await _repo.GetListNutritionFacts();
            var nutritionFactForReturn=_mapper.Map<IEnumerable<NutritionFactsForListDto>>(nutritions);
            return Ok(nutritionFactForReturn);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNutritionFact(int id,NutritionFactForUpdateDto udto)
        {
            var nutritionfromRepo=await _repo.GetNutritionFact(id);
            _mapper.Map(udto,nutritionfromRepo);
            if(await _repo.SaveAllN())
            return NoContent();
            throw new System.Exception($"Updating Exercise:{id} failed on save");

        }

    }
}