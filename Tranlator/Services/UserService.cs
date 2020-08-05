using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Tranlator.Exceptions;
using Tranlator.Repositories;
using Tranlator.ViewModels;

namespace Tranlator.Services
{
    public class UserService : IUserService
    {
        private readonly string _host;
        private readonly IRandomGeneratorService<string> _randomGeneratorService;
        private readonly IUserRepository _userRepository;
        private readonly IAuthLinksRepository _authLinksRepository;
        private readonly IEmailingService _emailingService;

        public UserService(
            IRandomGeneratorService<string> randomGeneratorService,
            IUserRepository userRepository, 
            IAuthLinksRepository authLinksRepository, 
            IEmailingService emailingService,
            Settings options
            )
        {
            _randomGeneratorService = randomGeneratorService;
            _userRepository = userRepository;
            _authLinksRepository = authLinksRepository;
            _emailingService = emailingService;
            _host = options.Host;
        }

        public async Task SendAuthLink(string email)
        {
            var link = _randomGeneratorService.Generate();
            await _authLinksRepository.CreateLink(email, link, DateTime.Now.AddDays(1));
            await _authLinksRepository.SaveChanges();
            await _emailingService.SendMessage(email, $"{_host}/users/auth?key={link}");
        }

        public async Task<UserAuthorizedResult> AuthUser(string link)
        {
            // TODO: check expires field
            var linkModel = await _authLinksRepository.FindLink(link);

            try
            {
                var user = await _userRepository.FindUserByEmail(linkModel.Email);
                return new UserAuthorizedResult(user.Name);
            }
            catch (RecordNotFoundException)
            {
                var user = await _userRepository.CreateUser(linkModel.Email, linkModel.Email);
                await _userRepository.SaveChanges();
                return new UserAuthorizedResult(user.Name, isNewUser: true);
            }
        }
    }
}