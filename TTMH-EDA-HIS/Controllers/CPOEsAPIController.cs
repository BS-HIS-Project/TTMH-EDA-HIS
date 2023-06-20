using HISDB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using TTMH_EDA_HIS.ViewModels;

namespace TTMH_EDA_HIS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CPOEsAPIController : ControllerBase
    {
        private readonly HISDB.Data.HisdbContext _context;
        public CPOEsAPIController(HISDB.Data.HisdbContext context) 
        {
            _context = context;
        }

        [Authorize]
        [HttpPost]
        public async Task<CPOEsAPISearchKeywordsViewModel_Response> SearchDrugID(CPOEsAPISearchKeywordsViewModel vm)
        {
            string[] results = await (
                from i in _context.Drugs
                where i.DrugId.ToUpper().Contains(vm.SearchKey.ToUpper())
                select i.DrugId
            ).Skip(0).Take(10).ToArrayAsync();
            List<string> relatives = new List<string>();
            foreach (string s in results)
            {
                Drug drug = await _context.Drugs.FindAsync(s);
                relatives.Add(drug.DrugName);
            }
            if (results.Length == 0)
            {
                results = new string[1] {"No Result"};
            }
            CPOEsAPISearchKeywordsViewModel_Response response = new CPOEsAPISearchKeywordsViewModel_Response()
            {
                SearchKeyRequested = vm.SearchKey,
                Result = results,
                Relatives = relatives.ToArray()
            };
            return response;
        }

        [Authorize]
        [HttpPost]
        public async Task<CPOEsAPISearchKeywordsViewModel_Response> SearchDrugName(CPOEsAPISearchKeywordsViewModel vm)
        {
            string[] results = await (
                 from i in _context.Drugs
                 where i.DrugName.ToUpper().Contains(vm.SearchKey.ToUpper())
                 select i.DrugName
             ).Skip(0).Take(10).ToArrayAsync();
            List<string> relatives = new List<string>();
            foreach (string s in results)
            {
                Drug drug = await _context.Drugs.FirstOrDefaultAsync(x=>x.DrugName==s);
                relatives.Add(drug.DrugId);
            }
            if (results.Length == 0)
            {
                results = new string[1] { "No Result" };
            }
            CPOEsAPISearchKeywordsViewModel_Response response = new CPOEsAPISearchKeywordsViewModel_Response()
            {
                SearchKeyRequested = vm.SearchKey,
                Result = results,
                Relatives = relatives.ToArray()
            };
            return response;
        }

        [Authorize]
        [HttpPost]
        public async Task<CPOEsAPISearchKeywordsViewModel_Response> SearchDosID(CPOEsAPISearchKeywordsViewModel vm)
        {
            string[] results = await (
                from i in _context.Dosages
                where i.DosId.ToUpper().Contains(vm.SearchKey.ToUpper())
                select i.DosId
            ).Skip(0).Take(10).ToArrayAsync();
            if (results.Length == 0)
            {
                results = new string[1] { "No Result" };
            }
            CPOEsAPISearchKeywordsViewModel_Response response = new CPOEsAPISearchKeywordsViewModel_Response()
            {
                SearchKeyRequested = vm.SearchKey,
                Result = results
            };
            return response;
        }
    }
}
