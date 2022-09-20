using FluentValidation;

namespace Webcore.API.Validators
{
    public class UpdateWalkDifficultyRequestValidator: AbstractValidator<Models.DTO.UpdateWalkDifficultyRequest>
    {
        public UpdateWalkDifficultyRequestValidator()
        {
            RuleFor(x=>x.Code).NotEmpty();
        }
    }
}
