using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.Codeflix.Catalog.Application.UseCases.Category.GetCategory
{
    public class GetCategoryInputValidator : AbstractValidator<GetCategoryInput>
    {
        public GetCategoryInputValidator() =>
            RuleFor(x => x.Id).NotEmpty();
    }
}
    