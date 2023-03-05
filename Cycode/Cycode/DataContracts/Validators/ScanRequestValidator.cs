using System;
using FluentValidation;

namespace Cycode.DataContracts.Validators
{
    public class ScanRequestValidator : AbstractValidator<ScanRequest>
    {
        public ScanRequestValidator()
        {
            RuleFor(x => x.Ecosystem).NotNull().NotEmpty();
            RuleFor(x => x.FileContent).NotNull().NotEmpty();
        }
    }
}

