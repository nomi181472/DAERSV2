using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DAERS.API.Data;
using DAERS.API.Dtos;
using DAERS.API.Helpers;
using DAERS.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DAERS.API.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/photos")]
    public class PhotosController : ControllerBase
    {
        private readonly IDaersRepository _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;
        public PhotosController(IDaersRepository repo, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _cloudinaryConfig = cloudinaryConfig;
            this._mapper = mapper;
            this._repo = repo;
            Account account=new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );
            _cloudinary=new Cloudinary(account);

        }
        [HttpGet("{id}",Name="GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
             var photofromrepo=await _repo.GetPhoto(id);
             var photo=_mapper.Map<PhotoForReturnDto>(photofromrepo);
             return Ok(photo);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int userId,[FromForm]PhotoForCreationDto pdto)
        {
            if(userId!=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var userfromrepo=await _repo.GetUser(userId);
            var file=pdto.File;
            var uploadResult=new ImageUploadResult();
            if(file.Length>0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadparam=new ImageUploadParams()
                    {
                      File=new FileDescription(file.Name,stream),
                      Transformation=new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    };
                    uploadResult=_cloudinary.Upload(uploadparam);
                    
                }
            }
                    pdto.Url=uploadResult.Uri.ToString();
                    pdto.PublicId=uploadResult.PublicId;
                    var photo=_mapper.Map<PhotoU>(pdto);
                    if(!userfromrepo.Photos.Any(u=>u.IsMain))
                    photo.IsMain=true;
                    userfromrepo.Photos.Add(photo);
                    if(await _repo.SaveAll())
                    {
                        var photoToReturn=_mapper.Map<PhotoForReturnDto>(photo);
                        return CreatedAtRoute("GetPhoto",new {id=photo.Id},photoToReturn);
                    }
                    return BadRequest("Could not Add the photo");

        }
    [HttpPost("{id}/setMain")]
    public async Task<IActionResult>SetMainPhoto(int userId,int id)
    {
        if(userId!=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
        var user=await _repo.GetUser(userId);
        if(!user.Photos.Any(p=>p.Id==id))
        return Unauthorized();
        var photofromrepo=await _repo.GetPhoto(id);
        if(photofromrepo.IsMain)
        return BadRequest("This is already the main photo");
        var currentmainphoto=await  _repo.GetMainPhotoForUser(userId);
        currentmainphoto.IsMain=false;
        photofromrepo.IsMain=true;
        if(await _repo.SaveAll())
        {
            return NoContent();
        }
        return BadRequest("could not Set photo to main");

    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePhoto(int userId,int id)
    {
        if(userId!=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
        var user=await _repo.GetUser(userId);
        if(!user.Photos.Any(p=>p.Id==id))
        return Unauthorized();
        var photofromrepo=await _repo.GetPhoto(id);
        if(photofromrepo.IsMain)
        return BadRequest("You Cannot delete your main photo");
        if (photofromrepo.PublicId!=null)
        {
        var deleteParams=new DeletionParams(photofromrepo.PublicId);

        var result=_cloudinary.Destroy(deleteParams);
        if(result.Result=="ok")
        _repo.Delete(photofromrepo);
        }
        if(photofromrepo.PublicId==null)
        {
            _repo.Delete(photofromrepo);
        }
        if (await _repo.SaveAll())
        return Ok();
        
        return BadRequest("Failed to delete the photo");

    }


    }

}