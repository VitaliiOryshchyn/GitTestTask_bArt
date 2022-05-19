using Task_bArt.Data.Entity;
using Task_bArt.Service.Dtos.Request;
using Task_bArt.Service.Dtos.Respons;

namespace Task_bArt.Service.Services.Abstracts
{
    public interface IAccountService
    {
        public Task<List<ResponseAccountDto>> GetAllAccountsAsync();
        public Task<ResponseAccountDto> GetAccountAsync(int id);
        public Task<bool> UpdateAccountAsync(int id, UpdateAccountDto accountDto);
        public Task<string> CreateAccountAsync(string incedentName, CreateAccountDto accountDto);

        public Task<string> CreateInitialData(CreateDataForDBDto createDataForDB);
    }
}
