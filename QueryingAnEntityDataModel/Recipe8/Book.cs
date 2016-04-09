namespace QueryingAnEntityDataModel.Recipe8
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}