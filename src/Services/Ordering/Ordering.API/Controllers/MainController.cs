using Microsoft.AspNetCore.Mvc;
using Ordering.API.ViewModels;
using Ordering.Application.Contracts.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.API.Controllers
{
    public abstract class MainController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public MainController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        protected ActionResult<ResponseViewModelFull<T>> CustomResponseFull<T>(T result = null, List<T> results = null) where T : class
        {
            if (_notificationService.HasNotications())
            {
                return BadRequest(new ResponseViewModelFull<T>
                {
                    Success = false,
                    Errors = _notificationService.GetNotifications().Select(x => x.Message)
                });
            }

            return Ok(new ResponseViewModelFull<T>
            {
                Success = true,
                Result = result,
                Results = results
            });
        }

        protected ActionResult<ResponseViewModelBasic> CustomResponseBasic()
        {
            if (_notificationService.HasNotications())
            {
                return BadRequest(new ResponseViewModelBasic
                {
                    Success = false,
                    Errors = _notificationService.GetNotifications().Select(x => x.Message)
                });
            }

            return Ok(new ResponseViewModelBasic
            {
                Success = true
            });
        }
    }
}
