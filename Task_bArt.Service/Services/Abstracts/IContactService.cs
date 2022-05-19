using Task_bArt.Data.Entity;
using Task_bArt.Service.Dtos.Request;
using Task_bArt.Service.Dtos.Respons;

namespace Task_bArt.Service.Services.Abstracts
{
    public interface IContactService
    {
        public Task<List<ResponseContactDto>> GetAllContactsAsync();
        public Task<ResponseContactDto> GetContactAsync(int id);
        public Task<bool> UpdateContactAsync(int id, UpdateContactDto contactDto);
        public Task<bool> CreateContactAsync(CreateContactDto createContact);
    }
}
