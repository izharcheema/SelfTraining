using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SelfTraining.DTO;
using SelfTraining.Repositries;

namespace SelfTraining.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IzharController : ControllerBase
    {
        private readonly IIzharRepo _izharrepo;

        public IzharController(IIzharRepo izharrepo)
        {
            _izharrepo = izharrepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllFamily()
        {
            var family = await _izharrepo.GetAllFamily();
            return Ok(family);
        }
        [HttpGet("id")]
        public async Task<IActionResult>GetFamilyById(int id)
        {
            var family = await _izharrepo.GetFamilyById(id);
            if(family == null)
                return NotFound();
            return Ok(family);
        }
        [HttpPost]
        public async Task<IActionResult>CreateCompany(CreateFamilyDTO createFamily)
        {
            var createdFamily=await _izharrepo.CreateFamily(createFamily);
            return Ok(createdFamily);
        }
        [HttpPut("id")]
        public async Task<IActionResult> UpdateFamily(int id, UpdateFamilyDTO updateFamily)
        {
            var dbFamily = await _izharrepo.GetFamilyById(id);
            if (dbFamily == null)
                return NotFound();
            await _izharrepo.UpdateFamily(id, updateFamily);
            return Ok(updateFamily);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteFamily(int id)
        {
            var dbFamily=await _izharrepo.GetFamilyById(id);
            if (dbFamily == null)
                return NotFound();
            await _izharrepo.DeleteFamily(id);
            return NoContent();
        }
    }
}
