using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProductsService.Helpers.Pagination;

namespace ProductsService.Helpers
{
    public static class HttpResponseExtensions
    {
        public static void AddPagination(this HttpResponse response, int currentPage, int pageSize, int totalCount, int totalPages, bool hasNext, bool hasPrevious)
        {
            var paginationHeader = new PaginationHeader(currentPage, pageSize, totalCount, totalPages, hasNext, hasPrevious);

            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();

            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFormatter));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}
