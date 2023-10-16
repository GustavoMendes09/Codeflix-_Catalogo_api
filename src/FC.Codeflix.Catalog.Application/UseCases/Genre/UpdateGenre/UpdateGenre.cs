using FC.Codeflix.Catalog.Application.Exceptions;
using FC.Codeflix.Catalog.Application.Interfaces;
using FC.Codeflix.Catalog.Application.UseCases.Genre.Common;
using FC.Codeflix.Catalog.Application.UseCases.Genre.CreateGenre;
using FC.Codeflix.Catalog.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.Codeflix.Catalog.Application.UseCases.Genre.UpdateGenre
{
    public class UpdateGenre : IUpdateGenre
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;

        public UpdateGenre(IGenreRepository genreRepository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _genreRepository = genreRepository;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }
        public async Task<GenreModelOutput> Handle(UpdateGenreInput request, CancellationToken cancellationToken)
        {
            var genre = await _genreRepository.GetAsync(request.Id, cancellationToken);
            genre.Update(request.Name);

            if(request.IsActive is not null && request.IsActive != genre.IsActive)
            {
                if((bool)request.IsActive) genre.Activate();
                else genre.Deactivate();
            }

            if(request.CategoriesIds is not null)
            {
                genre.RemoveAllCategories();

                if(request.CategoriesIds.Count > 0)
                {
                    await ValidateCategoriesIds(request, cancellationToken);
                    request.CategoriesIds?.ForEach(genre.AddCategory);
                }
            }

            await _genreRepository.Update(genre, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return GenreModelOutput.FromGenre(genre);
        }

        private async Task ValidateCategoriesIds(UpdateGenreInput request, CancellationToken cancellationToken)
        {
            var idsInPersistence = await _categoryRepository.GetIdsListByIds(request.CategoriesIds!, cancellationToken);

            if (idsInPersistence.Count < request.CategoriesIds!.Count)
            {
                var notFoundIds = request.CategoriesIds.FindAll(x => !idsInPersistence.Contains(x));
                var notFoundIdsAsString = string.Join(", ", notFoundIds);
                throw new RelatedAggregateException($"Related category id (or ids) not found: {notFoundIdsAsString}");
            }
        }
    }
}
