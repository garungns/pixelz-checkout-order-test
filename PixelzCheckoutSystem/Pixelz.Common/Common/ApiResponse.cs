using Pixelz.Models.Constants;

namespace Pixelz.Models.Common
{
    public class ApiResponse<T>
    {
        public T Result { get; set; }
        public ApiResponStatus Status { get; set; } 
        public string Message { get; set; }

        public int? StatusCode { get; set; }

        public static ApiResponse<T> Success(T result, string message = null, int? statusCode = 200)
        {
            return new ApiResponse<T>
            {
                Result = result,
                Status = ApiResponStatus.Success,
                Message = message,
                StatusCode = statusCode
            };
        }

        public static ApiResponse<T> Fail(string message = null, int? statusCode = 400)
        {
            return new ApiResponse<T>
            {
                Result = default,
                Status = ApiResponStatus.Fail,
                Message = message,
                StatusCode = statusCode
            };
        }

        public static ApiResponse<T> Error(string message = null, int? statusCode = 500)
        {
            return new ApiResponse<T>
            {
                Result = default,
                Status = ApiResponStatus.Error,
                Message = message,
                StatusCode = statusCode
            };
        }
    }
}
