namespace Service.Common.Testing
{
    public class RestResult<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }

        public static RestResult<T> Ok(T data)
        {
            return new RestResult<T>
            {
                Data = data,
                Success = true
            };
        }

        public static RestResult<T> Error(string code, string message)
        {
            return new RestResult<T>
            {
                Success = false,
                ErrorCode = code,
                ErrorMessage = message
            };
        }
    }
}
