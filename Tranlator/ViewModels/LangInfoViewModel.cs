namespace Tranlator.ViewModels
{
    public class LangInfoViewModel
    {
        public LangInfoViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; }
    }
}