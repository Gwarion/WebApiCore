using FluentValidation;
using FluentValidation.Results;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlaceHolder.Application.Services.Cqrs.MediatrPipeline
{
    public class MediatrRequestValidationPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public MediatrRequestValidationPreProcessor(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            if(_validators.Any())
            {
                IEnumerable<ValidationFailure> failures = null;

                await Task.Run(() =>
                {
                    failures = _validators.Select(v => v.Validate(request))
                    .SelectMany(vr => vr.Errors)
                    .Where(vf => vf != null);
                }, cancellationToken);

                if(failures.Any())
                {
                    throw new ValidationException(String.Join(Environment.NewLine, failures));
                }
            }
        }
    }
}
