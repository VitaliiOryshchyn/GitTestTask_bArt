using Microsoft.EntityFrameworkCore;
using Task_bArt.Data.Context;
using Task_bArt.Data.Entity;
using Task_bArt.Service.Dtos.Request;
using Task_bArt.Service.Dtos.Respons;
using Task_bArt.Service.Services.Abstracts;

namespace Task_bArt.Service.Services.Implements
{
    public class ContactService : IContactService
    {
        private readonly AppDbContext _db;

        public ContactService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<ResponseContactDto>> GetAllContactsAsync()
        {
            var contacts = await _db.Contacts.ToListAsync();
            if (contacts is null)
            {
                return null;
            }
            List<ResponseContactDto> responseContacts = new List<ResponseContactDto>();
            foreach (var contact in contacts)
            {
                responseContacts.Add(
                    new ResponseContactDto() 
                    { 
                        Id = contact.Id,
                        FirstName = contact.FirstName,
                        LastName = contact.LastName,
                        Email = contact.Email,
                        AccountId = contact.AccountId,
                    });
            }
            return responseContacts;
        }

        public async Task<ResponseContactDto> GetContactAsync(int id) 
        {
            var contact = await _db.Contacts.FirstOrDefaultAsync(x => x.Id == id);
            if (contact is null) 
            {
                return null;
            }
            var responseContact = new ResponseContactDto()
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                AccountId = contact.AccountId,
            };
            return responseContact;
        }

        public async Task<bool> UpdateContactAsync(int id, UpdateContactDto contactDto)
        {
            var contact = await _db.Contacts.FirstOrDefaultAsync(x => x.Id == id);
            if (contact is null)
            {
                return false;
            }

            contact.FirstName = contactDto.FirstName;
            contact.LastName = contactDto.LastName;
            contact.Email = contactDto.Email;
            await _db.SaveChangesAsync();
            return true;

        }

        public async Task<bool> CreateContactAsync(CreateContactDto createContact)
        {
            var account = await _db.Accounts.FirstOrDefaultAsync(x => x.Name == createContact.AccountName);
            if (account is null)
            {
                return false;
            }
            var contact = await _db.Contacts.FirstOrDefaultAsync(x => x.Email == createContact.ContactEmail);

            if (contact is not null)
            {
                contact.FirstName = createContact.ContactFirstName;
                contact.LastName = createContact.ContactLastName;
                contact.Email = createContact.ContactEmail;
                contact.Account = account;
                await _db.SaveChangesAsync();
            }
            else
            {
                await _db.Contacts.AddAsync(
                    new Contact()
                    {
                        FirstName = createContact.ContactFirstName,
                        LastName = createContact.ContactLastName,
                        Email = createContact.ContactEmail,
                        Account = account,
                    });
                await _db.SaveChangesAsync();

                var newIncident = new Incident() { Description = createContact.IncidentDescription };
                await _db.Incidents.AddAsync(newIncident);
                account.Incident = newIncident;
                await _db.SaveChangesAsync();
            }
            return true;
        }
    }
}
