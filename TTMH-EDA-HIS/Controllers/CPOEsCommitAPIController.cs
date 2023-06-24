using HISDB.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTMH_EDA_HIS.ViewModels;

namespace TTMH_EDA_HIS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CPOEsCommitAPIController : ControllerBase
    {
        private readonly HisdbContext _context;
        public CPOEsCommitAPIController(HisdbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> CommitDrugs(CPOEsCommitAPICommitDrugsViewModel vm)
        {
            

           

            return Ok(new {
                Status="Success",
                message=""
            });
        }


        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> GeneratePrescription()
        {



            return Ok(new{
                Status = "Success",
                message = ""
            });
        }



    }
}
