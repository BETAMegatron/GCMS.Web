using GCMS.Web.Data;
using GCMS.Web.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GCMS.Web.Areas.Admin.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesApiController : ControllerBase
    {
        private readonly GCMSContext _context;

        public RolesApiController(GCMSContext context)
        {
            _context = context;
        }

        //GET: Api/RolesApi/GetAllRoles
        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _context.RoleShowViewModels
                            .FromSqlRaw($"EXEC {MetaData.GetAllRoles}")
                            .ToListAsync();
            return Ok(roles);
        }
    }
}
