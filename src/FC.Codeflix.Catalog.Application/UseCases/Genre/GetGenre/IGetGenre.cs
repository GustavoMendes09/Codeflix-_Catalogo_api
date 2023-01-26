using FC.Codeflix.Catalog.Application.UseCases.Genre.Common;
using FC.Codeflix.Catalog.Application.UseCases.Genre.UpdateGenre;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.Codeflix.Catalog.Application.UseCases.Genre.GetGenre
{
    public interface IGetGenre : IRequestHandler<GetGenreInput, GenreModelOutput>
    {
    }
}
