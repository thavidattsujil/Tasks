using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModelLibrary;
using SQLConnectionRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipalityInfo : ControllerBase
    {
        DataRepository objrepo;
        public MunicipalityInfo()
        {
            objrepo = new DataRepository();
        }

        // GET: api/<MunicipalityInfo>
        [HttpGet]
        public IEnumerable<MunicipalityDisplayModel> Get()
        {
            var result = objrepo.GetDetails();

            return result.Result;
        }

        // GET api/<MunicipalityInfo>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MunicipalityInfo>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MunicipalityInfo>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MunicipalityInfo>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
