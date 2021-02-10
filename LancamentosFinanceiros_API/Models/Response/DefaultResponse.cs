namespace LancamentosFinanceiros.Models.Response
{
    public class DefaultResponse<T>
    {
        public bool success { get; set; }
        public string message { get; set; }
        public T result { get; set; }
    }
}