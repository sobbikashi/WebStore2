using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces;

namespace WebStore.WebAPI.Controllers.Identity;

[ApiController]
[Route(WebAPIAddresses.V1.Identity.Users)]
public class UsersApiController : ControllerBase
{
    private readonly UserStore<User, Role, WebStoreDB> _UserStore;
    private readonly ILogger<UsersApiController> _Logger;

    public UsersApiController(WebStoreDB db, ILogger<UsersApiController> Logger)
    {

        _UserStore = new(db);
        _Logger = Logger;
    }

    [HttpGet("all")]
    public async Task<IEnumerable<User>> GetAll() => await _UserStore.Users.ToArrayAsync();



    #region Users

    [HttpPost("UserID")]  //POST : api/v1/users/UserID
    public async Task<string> GetUserIdAsync([FromBody] User user) => await _UserStore.GetUserIdAsync(user);


    [HttpPost("UserName")]
    public async Task<string> GetUserNameAsync([FromBody] User user) => await _UserStore.GetUserNameAsync(user);

    [HttpPost("UserName/{name}")]
    public async Task<string> SetUserNameAsync([FromBody] User user, string name)
    {
        await _UserStore.SetUserNameAsync(user, name);
        await _UserStore.UpdateAsync(user);
        return user.UserName;
    }

    [HttpPost("NormalUserName")]
    public async Task<string> GetNormalizedUserNameAsync([FromBody] User user) => await _UserStore.GetNormalizedUserNameAsync(user);

    [HttpPost("NormalUserName/{name}")]
    public async Task<string> SetNormalizedUserNameAsync([FromBody] User user, string name)
    {
        await _UserStore.SetNormalizedUserNameAsync(user, name);
        await _UserStore.UpdateAsync(user);
        return user.NormalizedUserName;
    }

    #endregion
}



