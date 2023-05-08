using ApiPloomes.DOMAIN.Interfaces.INotifier;
using ApiPloomes.DOMAIN.Interfaces.IRepository;
using ApiPloomes.DOMAIN.Interfaces.IServices;
using ApiPloomes.DOMAIN.Models;
using ApiPloomes.DOMAIN.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPloomes.DOMAIN.Services
{
    public class UserServices : BaseService, IUserServices
    {
        private readonly IUserRepository _repository;
        private readonly INotifier _notifier;

        public UserServices(INotifier notifier,
                            IUserRepository repository) : base(notifier)
        {
            _repository = repository;
            _notifier = notifier;
        }

        public async Task CreateUser(User user)
        {

            try
            {
                var validation = ExecuteValidation(new UserValidator(), user);

                if (!validation)
                    return;

                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                if (await _repository.EmailExists(user.Email))
                    NotifyErrorMessage("Email", "Este email já está cadastrado");

                if (!_notifier.HasNotifications())
                {
                    await _repository.Create(user);
                    await _repository.Save();
                }

            }
            catch (Exception e)
            {
                NotifyErrorMessage($"{e}", "Ocorreu um erro inesperado, tente novamente mais tarde.");
            }
        }

        public async Task<User> GetUserById(Guid id)
        {
            try
            {
                var user = await _repository.GetById(id);

                if (user == null)
                    NotifyErrorMessage("Email", "Este ID não está cadastrado");

                return user;
            }
            catch (Exception e)
            {
                NotifyErrorMessage($"{e}", "Ocorreu um erro inesperado, tente novamente mais tarde.");
                return new User();
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                var user = await _repository.GetUserByEmail(email);

                if (user == null)
                    NotifyErrorMessage("Email", "Este email não está cadastrado");

                return user;
            }
            catch (Exception e)
            {
                NotifyErrorMessage($"{e}", "Ocorreu um erro inesperado, tente novamente mais tarde.");
                return new User();
            }
        }

        public async Task UpdateUser(User user, Guid id)
        {
            try
            {
                user.Password = "ValidUserPassword2@";
                var validation = ExecuteValidation(new UserValidator(), user);

                if (!validation)
                    return;

                var updateUser = await _repository.GetById(id);

                updateUser.Email = user.Email;
                updateUser.Username = user.Username;

                _repository.Update(updateUser);
                await _repository.Save();
            }
            catch (Exception e)
            {
                NotifyErrorMessage($"{e}", "Ocorreu um erro inesperado, tente novamente mais tarde");
            }
        }

        public async Task DeleteUser(Guid id)
        {
            try
            {
                await _repository.Delete(id);
                await _repository.Save();
            }
            catch (Exception e)
            {
                NotifyErrorMessage($"{e}", "Ocorreu um erro inesperado, tente novamente mais tarde");
            }
        }

        public async Task ChangePassword(User user, Guid id)
        {
            try
            {
                var validation = ExecuteValidation(new UserValidator(), user);

                if (!validation)
                    return;

                var updateUser = await _repository.GetById(id);

                updateUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                _repository.Update(updateUser);
                await _repository.Save();
            }
            catch (Exception e)
            {
                NotifyErrorMessage($"{e}", "Ocorreu um erro inesperado, tente novamente mais tarde");
            }
        }
    }
}
