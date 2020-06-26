using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProductsService.Helpers.Pagination;

namespace ProductsService.Helpers
{
    public static class HttpResponseExtensions
    {
        public static void AddPagination<T>(this HttpResponse response, PagedList<T> list)
        {
            var paginationHeader = new PaginationHeader(list.CurrentPage, list.PageSize, list.TotalCount, list.TotalPages, list.HasNext, list.HasPrevious);

            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();

            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFormatter));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}
