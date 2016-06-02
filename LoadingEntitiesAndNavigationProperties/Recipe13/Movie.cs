namespace LoadingEntitiesAndNavigationProperties.Recipe13
{
    public class Movie
    {
        public int MovieId { get; set; }
       
        public string Name { get; set; }
        public string Rating { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}