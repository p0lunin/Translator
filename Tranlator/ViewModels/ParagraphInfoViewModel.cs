namespace Tranlator.ViewModels
{
    public class ParagraphInfoViewModel
    {
        public ParagraphInfoViewModel(int id, int order, string content)
        {
            Id = id;
            Order = order;
            Content = content;
        }

        public int Id { get; }
        public int Order { get; }
        public string Content { get; }
    }
}