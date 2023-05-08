using ApiPloomes.DOMAIN.Interfaces.INotifier;
using ApiPloomes.DOMAIN.Models;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPloomes.DOMAIN.Services
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;

        public BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected bool ExecuteValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE: Entity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            NotifyValidationResult(validator);

            return false;
        }

        protected void NotifyValidationResult(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                NotifyErrorMessage(error.AttemptedValue.ToString(), error.ErrorMessage);
            }
        }

        protected void NotifyErrorMessage(string field, string message)
        {
            _notifier.AddNotification(new Notification(field, message));
        }

        protected bool IsValidOperation()
        {
            return !_notifier.HasNotifications();
        }
    }
}
