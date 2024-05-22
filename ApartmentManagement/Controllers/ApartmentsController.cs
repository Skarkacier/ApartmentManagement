using Business.Services;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentsController : ControllerBase
    {
        private readonly IApartmentService _apartmentService;

        public ApartmentsController(IApartmentService apartmentService)
        {
            _apartmentService = apartmentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateApartment([FromBody] Apartment apartment)
        {
            try
            {
                var apartmentId = await _apartmentService.CreateApartment(apartment.Block, apartment.Floor, apartment.Number, apartment.Status, apartment.UserID);
                return Ok(apartmentId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllApartments()
        {
            try
            {
                var apartments = await _apartmentService.GetAllApartments();
                return Ok(apartments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApartment(int id, [FromBody] Apartment apartment)
        {
            try
            {
                await _apartmentService.UpdateApartment(id, apartment.Block, apartment.Floor, apartment.Number, apartment.Status, apartment.UserID);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApartment(int id)
        {
            try
            {
                await _apartmentService.DeleteApartment(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
