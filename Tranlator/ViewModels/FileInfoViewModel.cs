namespace Tranlator.ViewModels
{
    public class FileInfoViewModel
    {
        public FileInfoViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; }
    }
}