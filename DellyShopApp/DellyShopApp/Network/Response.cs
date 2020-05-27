using System;
using DellyShopApp.Enum;

namespace DellyShopApp.Network
{
    class Response<T>
    {
        public Response(T responseBody)
        {
            Success = true;
            ResponseBody = responseBody;
        }

        public Response(ResponseError error, Exception e = null)
        {
            Success = false;
            Error = error;
            Exception = e;
        }

        public bool Success { get; }
        public T ResponseBody { get; }
        public ResponseError Error { get; }
        public Exception Exception { get; }

    }
}
