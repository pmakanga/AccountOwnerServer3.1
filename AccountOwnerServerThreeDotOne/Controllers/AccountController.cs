using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace AccountOwnerServerThreeDotOne.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public AccountController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            try
            {
                var accounts = await _repository.Account.GetAllAccounts();
                _logger.LogInfo($"Returned all accounts from the database.");

                var accountsResult = _mapper.Map<IEnumerable<AccountDto>>(accounts);
                return Ok(accountsResult);
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Something went wrong inside GetAllAccounts action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{Id}", Name ="AccountById")]
        public async Task<IActionResult>GetAccountById(Guid id)
        {
            try
            {
                var account = await _repository.Account.GetAccountById(id);
                if(account == null)
                {
                    _logger.LogError($"Account with id: {id}, hasn't been fount in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned account with id: {id}");
                    var accountResult = _mapper.Map<AccountDto>(account);
                    return Ok(accountResult);
                }

            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Something went wrong inside GetAccountById action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody]AccountForCreationDto account)
        {
            try
            {
                if(account == null)
                {
                    _logger.LogInfo("Account object sent from client is null");
                    return BadRequest("Account object is null");
                }
                if(!ModelState.IsValid)
                {
                    _logger.LogInfo("Invalid Account object sent from client.");
                    return BadRequest("Ivalid model object");
                }

                var accountEntity = _mapper.Map<Account>(account);
                _repository.Account.Create(accountEntity);
                await _repository.SaveAsync();
                var createdAccount = _mapper.Map<AccountDto>(accountEntity);
                return CreatedAtRoute("AccountById", new { createdAccount.Id }, createdAccount);
    
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Something went wrong inside CreateAccount action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(Guid id, [FromBody]AccountForUpdateDto account)
        {
            try
            {
                if(account == null)
                {
                    _logger.LogInfo("Account object sent from client is null");
                    return BadRequest("Account object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid account object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var accountEntity = await _repository.Account.GetAccountById(id);
                if(accountEntity == null)
                {
                    _logger.LogInfo($"Account with id: {id} hasn't been found in the db");
                }

                _mapper.Map(account, accountEntity);
                _repository.Account.UpdateAccount(accountEntity);
                await _repository.SaveAsync();
                return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside UpdateAccount action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            try
            {
                var account = await _repository.Account.GetAccountById(id);
                if(account == null)
                {
                    _logger.LogInfo($"Account with id {id}, hasn't been found in the db");
                    return NotFound();
                }

                _repository.Account.DeleteAccount(account);
                await _repository.SaveAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteAccount action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}