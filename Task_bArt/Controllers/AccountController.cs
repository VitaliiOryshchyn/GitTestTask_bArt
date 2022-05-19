using Microsoft.AspNetCore.Mvc;
using Task_bArt.Service.Dtos.Request;
using Task_bArt.Service.Services.Abstracts;

namespace Task_bArt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAcounts()
        {
            var accounts = await accountService.GetAllAccountsAsync();
            return accounts is null ? NotFound() : Ok(accounts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAcount(int id)
        {
            var account = await accountService.GetAccountAsync(id);
            return account is null ? NotFound() : Ok(account);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, UpdateAccountDto accountDto)
        {
            var account = await accountService.UpdateAccountAsync(id, accountDto);
            return account ? Ok("Account updated") : NotFound("Account dosen't exist");
        }

        [HttpPost("{incidentName}")]
        public async Task<IActionResult> CreateAccount(string incidentName, CreateAccountDto accountDto)
        {
            var newAccount = await accountService.CreateAccountAsync(incidentName, accountDto);
            return newAccount.Contains("Error") ? NotFound(newAccount) : Ok(newAccount);
        }

        [HttpPost("InitData")]
        public async Task<IActionResult> CreateInitDataDB(CreateDataForDBDto createDataForDB)
        {
            var newAccount = await accountService.CreateInitialData(createDataForDB);
            return newAccount.Contains("Error") ? BadRequest(newAccount) : Ok(newAccount);
        }
    }
}
