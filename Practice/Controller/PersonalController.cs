using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practice.Model;
using System.Text;

namespace Practice.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalController : ControllerBase
    {
        private ApplicationDbContext _context;

        public PersonalController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("getData")]
        public async Task<List<PersonalInformation>> Get()
        {
            List<PersonalInformation> personalInformation = new List<PersonalInformation>();
            // var personalInformation = GetEmployee();
            personalInformation = await (Task.Run(() => _context.PersonalInformations.ToList()));
            return personalInformation;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] PersonalInformation personalInformation)
        {
            byte[] data = Encoding.ASCII.GetBytes("MasumBillah");
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            string hash = Encoding.ASCII.GetString(data);
            _context.AddAsync(personalInformation);
            _context.SaveChangesAsync();

            return Ok(personalInformation);
        }

        [HttpPut]
        [Route("update")] 
        public async Task<IActionResult> Update([FromBody]PersonalInformation personalInformation)
        {
            // int id = personalInformation.Id;
            PersonalInformation findPersonId = await (Task.Run(() => _context.PersonalInformations.Find(personalInformation.Id)));
            if(findPersonId != null)
            {
                findPersonId.Name = personalInformation.Name;
                findPersonId.Contact = personalInformation.Contact;
                findPersonId.Email = personalInformation.Email;
                _context.Update(findPersonId);
                _context.SaveChangesAsync();
                return Ok(findPersonId);
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var findId = await (Task.Run(() => _context.PersonalInformations.Find(id)));
            if(findId != null)
            {
                _context.Remove(findId);
                _context.SaveChangesAsync();
                return Ok(findId);
            }
            return Ok(null);
        }
    }
}
