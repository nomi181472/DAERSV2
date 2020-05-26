using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DAERS.API.Data;
using DAERS.API.Dtos;
using DAERS.API.Dtos.DtosForExercise;
using DAERS.API.Helpers;
using DAERS.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DAERS.API.Controllers
{
    [Authorize]
    [Route("api/Exercise/{ExerciseId}/photosE")]
    public class PhotosEController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;
        private readonly IExerciseRepository _repo;
        public PhotosEController(IExerciseRepository repo, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            this._repo = repo;
            _cloudinaryConfig = cloudinaryConfig;
            this._mapper = mapper;
    
            Account account = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(account);

        }
         [HttpGet("{id}", Name = "GetPhotoE")]
        public async Task<IActionResult> GetPhotoE(int id)
        {
            var photofromrepo = await _repo.GetPhotoE(id);
            var photo = _mapper.Map<PhotosEForReturnDto>(photofromrepo);
            return Ok(photo);
        }
        
         [HttpPost]
        public async Task<IActionResult> AddPhotoForExercise(int exerciseId,[FromForm]PhotosEForCreationDto pdto)
        {
            var userfromrepo=await _repo.GetExercise(exerciseId);
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
                    pdto.PublicEId=uploadResult.PublicId;
                    var photo=_mapper.Map<PhotoE>(pdto);
                     if(!userfromrepo.PhotosE.Any(u=>u.IsMain))
                        photo.IsMain=true;
                    userfromrepo.PhotosE.Add(photo);
                    if(await _repo.SaveAllE())
                    {
                        var photoToReturn=_mapper.Map<PhotosEForReturnDto>(photo);
                        return CreatedAtRoute("GetPhotoE",new {id=photo.Id},photoToReturn);
                    }
                    return BadRequest("Could not Add the photo");
          
            }
             [HttpPost("{id}/setMainE")]
         public async Task<IActionResult> SetMainPhoto(int exerciseId,int id)
         {
            var exercise=await _repo.GetExercise(exerciseId);
            if(!exercise.PhotosE.Any(p=>p.Id==id))
            return Unauthorized();
            var photofromrepo=await _repo.GetPhotoE(id);
            if(photofromrepo.IsMain)
            return BadRequest("This is already the main photo");
            var currentmainphoto=await  _repo.GetMainPhotoForExercise(exerciseId);
            currentmainphoto.IsMain=false;
            photofromrepo.IsMain=true;
            if(await _repo.SaveAllE())
            {
                return NoContent();
            }
            return BadRequest("could not Set photo to main");

    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePhoto(int exerciseId,int id)
    {
        var exercise=await _repo.GetExercise(exerciseId);
        if(!exercise.PhotosE.Any(p=>p.Id==id))
        return Unauthorized();
        var photofromrepo=await _repo.GetPhotoE(id);
        if(photofromrepo.IsMain)
        return BadRequest("You Cannot delete your main photo");
        if (photofromrepo.PublicEId!=null)
        {
        var deleteParams=new DeletionParams(photofromrepo.PublicEId);

        var result=_cloudinary.Destroy(deleteParams);
        if(result.Result=="ok")
        _repo.DeleteE(photofromrepo);
        }
        if(photofromrepo.PublicEId==null)
        {
            _repo.DeleteE(photofromrepo);
        }
        if (await _repo.SaveAllE())
        return Ok();
        
        return BadRequest("Failed to delete the photo");

    }

                   

        }
    }
