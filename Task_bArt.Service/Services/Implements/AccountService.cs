using Microsoft.EntityFrameworkCore;
using Task_bArt.Data.Context;
using Task_bArt.Data.Entity;
using Task_bArt.Service.Dtos.Request;
using Task_bArt.Service.Dtos.Respons;
using Task_bArt.Service.Services.Abstracts;

namespace Task_bArt.Service.Services.Implements
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _db;
        public AccountService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<List<ResponseAccountDto>> GetAllAccountsAsync()
        {
            var allAccounts = await _db.Accounts.ToListAsync();
            if (allAccounts is null) 
            {
                return null;
            }
            List<ResponseAccountDto> allAccountsDto = new List<ResponseAccountDto>();
            foreach (var account in allAccounts)
            {
                allAccountsDto.Add(
                    new ResponseAccountDto() 
                    {
                        Id = account.Id,
                        Name = account.Name,
                        IncidentName = account.IncidentName,
                    });
            }
            return allAccountsDto;
        }

        public async Task<ResponseAccountDto> GetAccountAsync(int id) 
        {
            var account = await _db.Accounts.FirstOrDefaultAsync(x => x.Id == id);
            if (account is null) 
            {
                return null;
            }
            return new ResponseAccountDto() { Id = account.Id, Name = account.Name, IncidentName = account.IncidentName };
            
        }

        public async Task<bool> UpdateAccountAsync(int id, UpdateAccountDto accountDto)
        {
            var account = await _db.Accounts.FirstOrDefaultAsync(x => x.Id == id);
            if (account is null)
            {
                return false;
            }

            account.Name = accountDto.Name;
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<string> CreateAccountAsync(string incedentName, CreateAccountDto accountDto)
        {

            var incident = await _db.Incidents.FirstOrDefaultAsync(x => x.IncidentName == incedentName);
            var isAccountNameExist = await _db.Accounts.AnyAsync(x => x.Name == accountDto.Name);
            if (incident is null || isAccountNameExist)
            {
                return "Error: This account exist, change account name!";
            }

            await _db.Accounts.AddAsync(
                new Account()
                {
                    Name = accountDto?.Name,
                    IncidentName = incedentName,
                });
            await _db.SaveChangesAsync();

            var newAccount = await _db.Accounts.FirstOrDefaultAsync(x => x.Name == accountDto.Name);
            if (newAccount is null)
            {
                return "Error: Account name dosen't exist!";
            }

            var contact = await _db.Contacts.FirstOrDefaultAsync(x => x.Email == accountDto.Email);
            if (contact is null)
            {
                await _db.Contacts.AddAsync(new Contact()
                {
                    FirstName = accountDto?.FirstName,
                    LastName = accountDto?.LastName,
                    Email = accountDto?.Email,
                    Account = newAccount
                });
            }
            else
            {
                contact.FirstName = accountDto?.FirstName;
                contact.LastName = accountDto?.LastName;
            }

            await _db.SaveChangesAsync();

            return "Account added";
        }

        public async Task<string> CreateInitialData(CreateDataForDBDto createDataForDB)
        {
            var account = await _db.Accounts.AnyAsync(x => x.Name == createDataForDB.AccountName);
            var contact = await _db.Contacts.AnyAsync(x => x.Email == createDataForDB.ContactEmail);
            if (account)
            {
                return "Error: This account exist, change account name";
            }
            else if (contact) 
            {
                return "Error: Contact with this email exist, change email";
            }

            await _db.Incidents.AddAsync(
                new Incident() 
                { 
                    Description = createDataForDB.IncidentDescription 
                });
            await _db.SaveChangesAsync();

            var newIncident = await _db.Incidents.FirstOrDefaultAsync(x => x.Description == createDataForDB.IncidentDescription);
            
            await _db.Accounts.AddAsync(
                new Account() 
                { 
                    Name = createDataForDB.AccountName,
                    Incident = newIncident
                });
            await _db.SaveChangesAsync();

            var newContact = await _db.Accounts.FirstOrDefaultAsync(x => x.Name == createDataForDB.AccountName);

            await _db.Contacts.AddAsync(
                new Contact() 
                {
                    FirstName = createDataForDB.ContactFirastName,
                    LastName = createDataForDB.ContactLastName,
                    Email = createDataForDB.ContactEmail,
                    Account = newContact
                });
            await _db.SaveChangesAsync();

            return "Data added!";
        }
    }
}
