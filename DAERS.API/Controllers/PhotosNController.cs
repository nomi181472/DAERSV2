using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DAERS.API.Data;
using DAERS.API.Dtos.DtosForNutritionFacts;
using DAERS.API.Helpers;
using DAERS.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DAERS.API.Controllers
{
    [Authorize]
    [Route("api/NutritionFact/{NutritionFactId}/photosN")]
    public class PhotosNController : ControllerBase
    {
        private readonly INutritionFactRepository _repo;
        private readonly IMapper _mapper;
        private Cloudinary _cloudinary;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        public PhotosNController(INutritionFactRepository repo, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            this._cloudinaryConfig = cloudinaryConfig;
            this._mapper = mapper;
            this._repo = repo;
            Account account = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(account);


        }
        [HttpGet("{id}", Name = "GetPhotoN")]
        public async Task<IActionResult> GetPhotoN(int id)
        {
            var photofromrepo = await _repo.GetPhotoN(id);
            var photo = _mapper.Map<PhotosNForReturnDto>(photofromrepo);
            return Ok(photo);
        }
           [HttpPost]
        public async Task<IActionResult> AddPhotoForNutritionFact(int NutritionFactId,[FromForm]PhotosNForCreationDto pdto)
        {
            var nutritionfromrepo=await _repo.GetNutritionFact(NutritionFactId);
            var file=pdto.File;
            var uploadResult=new ImageUploadResult();
            if(file.Length>0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadparam=new ImageUploadParams()
                    {
                      File=new FileDescription(file.Name,stream),
                      Transformation=new Transformation().Width(500).Height(500).Crop("fill")
                    };
                    uploadResult=_cloudinary.Upload(uploadparam);
                    
                }
            }
                    pdto.Url=uploadResult.Uri.ToString();
                    pdto.PublicNId=uploadResult.PublicId;
                    var photo=_mapper.Map<PhotoN>(pdto);
                     if(!nutritionfromrepo.PhotosE.Any(u=>u.IsMain))
                        photo.IsMain=true;
                    nutritionfromrepo.PhotosE.Add(photo);
                    if(await _repo.SaveAllN())
                    {
                        var photoToReturn=_mapper.Map<PhotosNForReturnDto>(photo);
                        return CreatedAtRoute("GetPhotoN",new {id=photo.Id},photoToReturn);
                    }
                    return BadRequest("Could not Add the photo");
          
            }
                 [HttpPost("{id}/setMainN")]
         public async Task<IActionResult> SetMainPhoto(int NutritionFactId,int id)
         {
            var nutrition=await _repo.GetNutritionFact(NutritionFactId);
            if(!nutrition.PhotosE.Any(p=>p.Id==id))
            return Unauthorized();
            var photofromrepo=await _repo.GetPhotoN(id);
            if(photofromrepo.IsMain)
            return BadRequest("This is already the main photo");
            var currentmainphoto=await  _repo.GetMainPhotoForNutritionFact(NutritionFactId);
            currentmainphoto.IsMain=false;
            photofromrepo.IsMain=true;
            if(await _repo.SaveAllN())
            {
                return NoContent();
            }
            return BadRequest("could not Set photo to main");

    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePhoto(int NutritionFactId,int id)
    {
        var nutrition=await _repo.GetNutritionFact(NutritionFactId);
        if(!nutrition.PhotosE.Any(p=>p.Id==id))
        return Unauthorized();
        var photofromrepo=await _repo.GetPhotoN(id);
        if(photofromrepo.IsMain)
        return BadRequest("You Cannot delete your main photo");
        if (photofromrepo.PublicNId!=null)
        {
            var deleteParams=new DeletionParams(photofromrepo.PublicNId);
            var result=_cloudinary.Destroy(deleteParams);
        if(result.Result=="ok")
        _repo.DeleteN(photofromrepo);
        }
        if(photofromrepo.PublicNId==null)
        {
            _repo.DeleteN(photofromrepo);
        }
        if (await _repo.SaveAllN())
        return Ok();
        
        return BadRequest("Failed to delete the photo");
    }
      
    }
}