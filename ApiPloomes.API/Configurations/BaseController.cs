using ApiPloomes.DOMAIN.Interfaces.INotifier;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiPloomes.API.Configurations
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly INotifier _notifier;
        private readonly IMapper _mapper;

        public BaseController(INotifier notifier,
                                IMapper mapper)
        {
            _notifier = notifier;
            _mapper = mapper;
        }

        protected bool HasError()
        {
            if (!_notifier.HasNotifications()) return false;

            foreach (var notification in _notifier.GetNotifications())
                ModelState.AddModelError(notification.Field, notification.Message);

            return true;
        }

        protected IActionResult ReturnBadRequest()
        {
            return ValidationFIlter.ControllerBadRequestResponse(ModelState);
        }
    }
}
