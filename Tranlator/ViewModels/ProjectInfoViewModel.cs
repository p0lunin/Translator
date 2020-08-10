namespace Tranlator.ViewModels
{
    public class ProjectInfoViewModel
    {
        public ProjectInfoViewModel(int id, string name, bool isPublic)
        {
            Id = id;
            Name = name;
            IsPublic = isPublic;
        }

        public int Id { get; }
        public string Name { get; }
        public bool IsPublic { get; }
    }
}