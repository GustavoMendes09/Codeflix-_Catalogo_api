﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.Codeflix.Catalog.Application.UseCases.Genre.DeleteGenre
{
    public interface IDeleteGenre : IRequestHandler<DeleteGenreInput>
    {

    }
}
