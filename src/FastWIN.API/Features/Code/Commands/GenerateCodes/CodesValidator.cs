﻿using fastwin.Models;
using FluentValidation;
public class CodesValidator : AbstractValidator<Codes>
{
    public CodesValidator()
    {
        RuleFor(c => c.Code).NotEmpty().Length(10).WithMessage("Code must have a length of 10 characters");
        RuleFor(c => c.Status).NotNull().WithMessage("Status Can't Be null");
    }
}