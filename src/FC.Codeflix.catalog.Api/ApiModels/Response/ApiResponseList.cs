using FC.Codeflix.Catalog.Application.Common;

namespace FC.Codeflix.catalog.Api.ApiModels.Response
{
    public class ApiResponseList<T> : ApiResponse<IReadOnlyList<T>>
    {
        public ApiResponseListMeta Meta { get; set; }
        public ApiResponseList(
            int currentPage,
            int perPage,
            int total,
            IReadOnlyList<T> data
            ) : base(data)
        {
            Meta = new ApiResponseListMeta(currentPage, perPage,total);
        }

        public ApiResponseList(
            PaginatedListOutput<T> paginatedListOutput
            ) : base(paginatedListOutput.Items)
        {
            Meta = new ApiResponseListMeta(paginatedListOutput.Page, paginatedListOutput.PerPage, paginatedListOutput.Total);
        }
    }
}
