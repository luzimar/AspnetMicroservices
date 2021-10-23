using FluentValidation;
using MediatR;
using Ordering.Application.Contracts.Notifications;
using Ordering.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly INotificationService _notificationService;
        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators, INotificationService notificationService)
        {
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
            _notificationService = notificationService ?? throw new ArgumentException(nameof(notificationService));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
                if (failures.Any())
                {
                    failures.ForEach(failure => _notificationService.Handle(new Notification(failure.ErrorMessage)));
                    return await Task.FromResult<TResponse>(default);
                }
            }
            return await next();
        }
    }
}
