using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Interfaces.Services.Identity;

internal interface IRoleClient : IRoleStore<Role> { }

