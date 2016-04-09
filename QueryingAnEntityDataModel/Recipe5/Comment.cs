namespace QueryingAnEntityDataModel.Recipe5
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public int? PostId { get; set; }
        public virtual BlogPost BlogPost { get; set; }
    }
}