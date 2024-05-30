using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LD_4_Interneto_tech.Dto;
using LD_4_Interneto_tech.Interfaces;
using LD_4_Interneto_tech.Models;
using Microsoft.EntityFrameworkCore;

namespace LD_4_Interneto_tech.Controllers
{
    public class PropertyController : BaseController
    {
        
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IPhotoService photoService;

        public PropertyController(IUnitOfWork uow,
        IMapper mapper,IPhotoService photoService)
        {
            this.photoService = photoService;
            this.uow = uow;
            this.mapper = mapper;
        }

        //property/list/1
        /*
        [HttpGet("list/{sellRent}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyList(int sellRent)
        {
            var properties = await uow.PropertyRepository.GetPropertiesAsync(sellRent);
            var propertyListDTO = mapper.Map<IEnumerable<PropertyListDto>>(properties);
            return Ok(propertyListDTO);
        }
        */
        [HttpGet("list/{sellRent}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyList(int sellRent,
        [FromQuery] int? minPrice, [FromQuery] int? maxPrice,
        [FromQuery] int? minBuiltArea, [FromQuery] int? maxBuiltArea,
        [FromQuery] int? propertyTypeId, [FromQuery] int? furnishingTypeId,
        [FromQuery] int? minFloors, [FromQuery] int? maxFloors)
        {
            var properties = await uow.PropertyRepository.GetPropertiesAsync(sellRent);

            // Apply filtering based on query parameters
            if (minPrice.HasValue)
                properties = properties.Where(p => p.Price >= minPrice);
            if (maxPrice.HasValue)
                properties = properties.Where(p => p.Price <= maxPrice);
            if (minBuiltArea.HasValue)
                properties = properties.Where(p => p.BuiltArea >= minBuiltArea);
            if (maxBuiltArea.HasValue)
                properties = properties.Where(p => p.BuiltArea <= maxBuiltArea);
            if (propertyTypeId.HasValue)
                properties = properties.Where(p => p.PropertyTypeId == propertyTypeId);
            if (furnishingTypeId.HasValue)
                properties = properties.Where(p => p.FurnishingTypeId == furnishingTypeId);
            if (minFloors.HasValue)
                properties = properties.Where(p => p.TotalFloors >= minFloors);
            if (maxFloors.HasValue)
                properties = properties.Where(p => p.TotalFloors <= maxFloors);

            var propertyListDTO = mapper.Map<IEnumerable<PropertyListDto>>(properties);
            return Ok(propertyListDTO);
        }
        //property/detail/1
        [HttpGet("detail/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyDetail(int id)
        {
            var property = await uow.PropertyRepository.GetPropertyDetailAsync(id);
            var propertyDTO = mapper.Map<PropertyDetailDto>(property);
            return Ok(propertyDTO);
        }

        //property/add
        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddProperty(PropertyDto propertyDto)
        {
            var property = mapper.Map<Property>(propertyDto);
            var userId = GetUserId();
            property.PostedBy = userId;
            property.LastUpdatedBy = userId;
            uow.PropertyRepository.AddProperty(property);
            await uow.SaveAsync();
            return StatusCode(201);
        }
        //property/update/{id}
        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProperty(int id, [FromBody] PropertyDto propertyDto)
        {
            var property = await uow.PropertyRepository.GetPropertyByIdAsync(id);

            if (property == null)
            {
                return NotFound();
            }

            var userId = GetUserId();

            if (property.PostedBy != userId)
            {
                return Forbid();
            }

            var propertyMap = mapper.Map<Property>(propertyDto);
            propertyMap.PostedBy = userId;
            propertyMap.LastUpdatedBy = userId;
            propertyMap.LastUpdatedOn = DateTime.Now;

            var result = await uow.PropertyRepository.UpdateProperty(id, propertyMap);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        //property/delete/1
        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            var userId = GetUserId();

            var property = await uow.PropertyRepository.GetPropertyByIdAsync(id);

            if (property.PostedBy != userId)
                return BadRequest("You are not authorised to delete the property");

            if (property == null || property.PostedBy != userId)
                return BadRequest("No such property exists");

            uow.PropertyRepository.DeleteProperty(id);
            await uow.SaveAsync();
            return Ok(id);
        }
        //User Posts
        //property/user/{userId}
        [HttpGet("user/{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertiesByUserId(int userId)
        {
            // Retrieve properties by user ID from the repository
            var properties = await uow.PropertyRepository.GetPropertiesByUserIdAsync(userId);

            var propertyListDTO = mapper.Map<IEnumerable<PropertyListDto>>(properties);
            return Ok(propertyListDTO);
        }
        [HttpPost("add-favorite")]
        [Authorize]
        public async Task<IActionResult> AddFavoriteProperty(int userId, int propertyId)
        {
            await uow.UserFavoritePropertyRepository.AddFavoriteProperty(userId, propertyId);
            return Ok();
        }

        [HttpPost("remove-favorite")]
        [Authorize]
        public async Task<IActionResult> RemoveFavoriteProperty(int userId, int propertyId)
        {
            await uow.UserFavoritePropertyRepository.RemoveFavoriteProperty(userId, propertyId);
            return Ok();
        }

        [HttpGet("favorite-properties/{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFavoriteProperties(int userId)
        {
            var properties = await uow.UserFavoritePropertyRepository.GetFavoriteProperties(userId);
            var propertyListDTO = mapper.Map<IEnumerable<PropertyListDto>>(properties);
            return Ok(propertyListDTO);
        }
        //property/add/photo/1
        [HttpPost("add/photo/{propId}")]
        [Authorize]
        public async Task<ActionResult<PhotoDto>> AddPropertyPhoto(IFormFile file, int propId)
        {
            var result = await photoService.UploadPhotoAsync(file);
            if (result.Error != null)
                return BadRequest(result.Error.Message);
            var userId = GetUserId();

            var property = await uow.PropertyRepository.GetPropertyByIdAsync(propId);

            if (property.PostedBy != userId)
                return BadRequest("You are not authorised to upload photo for this property");

            var photo = new Photo
            {
                ImageUrl = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };
            if (property.Photos.Count == 0)
            {
                photo.IsPrimary = true;
            }

            property.Photos.Add(photo);
            if (await uow.SaveAsync()) return mapper.Map<PhotoDto>(photo);

            return BadRequest("Some problem occured in uploading photo..");
        }
        
        //property/set-primary-photo/42/jl0sdfl20sdf2s
        [HttpPost("set-primary-photo/{propId}/{photoPublicId}")]
        [Authorize]
        public async Task<IActionResult> SetPrimaryPhoto(int propId, string photoPublicId)
        {
            var userId = GetUserId();

            var property = await uow.PropertyRepository.GetPropertyByIdAsync(propId);

            if (property.PostedBy != userId)
                return BadRequest("You are not authorised to change the photo");

            if (property == null || property.PostedBy != userId)
                return BadRequest("No such property or photo exists");

            var photo = property.Photos.FirstOrDefault(p => p.PublicId == photoPublicId);

            if (photo == null)
                return BadRequest("No such property or photo exists");

            if (photo.IsPrimary)
                return BadRequest("This is already a primary photo");


            var currentPrimary = property.Photos.FirstOrDefault(p => p.IsPrimary);
            if (currentPrimary != null) currentPrimary.IsPrimary = false;
            photo.IsPrimary = true;

            if (await uow.SaveAsync()) return NoContent();

            return BadRequest("Failed to set primary photo");
        }
        
        
        [HttpDelete("delete-photo/{propId}/{photoPublicId}")]
        [Authorize]
        public async Task<IActionResult> DeletePhoto(int propId, string photoPublicId)
        {
            var userId = GetUserId();

            var property = await uow.PropertyRepository.GetPropertyByIdAsync(propId);

            if (property.PostedBy != userId)
                return BadRequest("You are not authorised to delete the photo");

            if (property == null || property.PostedBy != userId)
                return BadRequest("No such property or photo exists");

            var photo = property.Photos.FirstOrDefault(p => p.PublicId == photoPublicId);

            if (photo == null)
                return BadRequest("No such property or photo exists");

            if (photo.IsPrimary)
                return BadRequest("You can not delete primary photo");

            if (photo.PublicId != null)
            {
                var result = await photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) return BadRequest(result.Error.Message);
            }

            property.Photos.Remove(photo);

            if (await uow.SaveAsync()) return Ok();

            return BadRequest("Failed to delete photo");
        }
    }
}