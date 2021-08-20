namespace sample01.Models
{
    public class ViewModelBase<T> where T : class
    {
        public string msg { get; set; }

        public int code { get; set; }

        public T data { get; set; }
    }
}
