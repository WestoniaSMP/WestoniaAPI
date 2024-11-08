using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestoniaAPI.DataLayer.Entities.Security;

namespace WestoniaAPI.DataLogic
{
    public class MinecraftUserLogic(IUserStore<MinecraftUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<MinecraftUser> passwordHasher, IEnumerable<IUserValidator<MinecraftUser>> userValidators, IEnumerable<IPasswordValidator<MinecraftUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<MinecraftUser>> logger) : UserManager<MinecraftUser>(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
    {
    }
}
