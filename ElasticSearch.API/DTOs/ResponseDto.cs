using System.Net;

namespace ElasticSearch.API.DTOs {
    public record ResponseDto<T> {
        public T? Data { get; set; }
        public HttpStatusCode Status { get; set; }
        public List<string>? Errors { get; set; }

        //ResponseDto<Product>.Success(product,HttpStatusCode.Success200OK)
        //{
        //data:{}
        //errors:null
        //status:200OK
        //}
        public static ResponseDto<T> Success(T data,HttpStatusCode httpStatus) {
            return new ResponseDto<T>() { Data = data, Status = httpStatus };
        }

        public static ResponseDto<T> Error(List<string> errors, HttpStatusCode httpStatus) {
            return new ResponseDto<T>() { Errors = errors, Status = httpStatus };
        }

        public static ResponseDto<T> Error(string error, HttpStatusCode httpStatus) {
            return new ResponseDto<T>() { Errors = new List<string> { error}, Status = httpStatus };
        }
    }
}
