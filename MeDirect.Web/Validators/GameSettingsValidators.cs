using FluentValidation;
using MeDirect.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeDirect.Web.Validators
{
    public class GameSettingsValidator:AbstractValidator<GameSetting>
    {
        public GameSettingsValidator()
        {
            RuleFor(x => x.Size).NotEmpty().WithMessage("Size Field Can Not Be Empty.");
            //RuleFor(x => x.Size).GreaterThan(3).WithMessage("Size Field Should Be Bigger Than 3");
        }
    }
}
