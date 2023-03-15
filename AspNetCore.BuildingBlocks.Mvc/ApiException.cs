namespace AspNetCore.BuildingBlocks.Mvc
{
    public class ApiException : Exception
    {
        public string Code { get; set; }

        public ApiException(string message, string code) :base(message)
        {
            Code = code;
        }
    }
}