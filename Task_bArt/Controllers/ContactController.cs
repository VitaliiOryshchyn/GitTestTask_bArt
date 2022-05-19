using Microsoft.AspNetCore.Mvc;
using Task_bArt.Service.Dtos.Request;
using Task_bArt.Service.Services.Abstracts;

namespace Task_bArt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            var contacts = await _contactService.GetAllContactsAsync();
            return contacts is null ? NotFound() : Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact(int id)
        {
            var contacts = await _contactService.GetContactAsync(id);
            return contacts is null ? NotFound() : Ok(contacts);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, UpdateContactDto contactDto)
        {
            var contacts = await _contactService.UpdateContactAsync(id, contactDto);
            return !contacts ? NotFound("Contact doesn't exist") : Ok("Contact updated");
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto createContact)
        {
            var newContact = await _contactService.CreateContactAsync(createContact);
            return newContact ? Ok("Contact added") : NotFound("Account doesn't exist");
        }
    }
}
