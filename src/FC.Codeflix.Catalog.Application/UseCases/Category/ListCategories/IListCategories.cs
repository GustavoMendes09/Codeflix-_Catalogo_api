using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.Codeflix.Catalog.Application.UseCases.Category.ListCategories
{
    public interface IListCategories : IRequestHandler<ListCategoriesInput, ListCategoriesOutput>
    {
    }
}
