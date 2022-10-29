namespace Multiple.Models
{
    [Serializable]
    public class ServiceResponse<T>
    {
        public ServiceResponse()
        {
            this.List = new List<T>();
        }
        public ServiceResponse(HttpContext context)
        {
            this.List = new List<T>();
        }

        public bool HasExceptionError { get; set; }
        public string ExceptionMessage { get; set; }
        public IList<T> List { get; set; }
        public T Entity { get; set; }
        public int Count { get; set; }
        public bool IsValid => !HasExceptionError && string.IsNullOrEmpty(ExceptionMessage);
        public bool IsSuccessful { get; set; }
    }
}
