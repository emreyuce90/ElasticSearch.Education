using ElasticSearch.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ElasticSearch.API.Extensions {
    public static class CustomControllerExtension {
        public static IActionResult ToGenericResult<T>(this ResponseDto<T> response) {

            if (response.Status == HttpStatusCode.NoContent) {
                return new ObjectResult(null) { StatusCode = response.Status.GetHashCode() };
            }

            return new ObjectResult(response) { StatusCode = response.Status.GetHashCode() };
        }
    }
}
