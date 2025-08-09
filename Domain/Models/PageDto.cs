namespace Domain.Models
{
    public class PageDto<T>
    {

       public  T[] data { get; set; }
       public int totalCount { get; set; }

        public PageDto(T[] data, int totalCount)
        {
            this.data = data;
            this.totalCount = totalCount;  
        }
    }
}
