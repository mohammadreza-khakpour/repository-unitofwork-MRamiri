namespace ApiProject.Controllers
{
    public interface IGoodCategoriesRepository
    {
        void Add(string Title);
        void CheckForDuplicatedTitle(string title);
    }
}