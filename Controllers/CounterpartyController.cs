using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using sbs_api_2.Services;
using sbs_api_2.Models;

using LiteDB;

namespace sbs_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CounterpartyController : ControllerBase
    {
        public CounterpartyController()
        {}
        [HttpGet] 
        public List<Counterparty> GetAll() =>   CounterpartyService.GetAll();

        [HttpPost]
        public ActionResult<Counterparty> Insert(Counterparty dto)
        {
            string id = CounterpartyService.Insert(dto);
            if (! id.Equals(null))
            {
                return CreatedAtAction(nameof(Insert), new { id = id }, dto);
            }
            return BadRequest();
        }
    }

}

  