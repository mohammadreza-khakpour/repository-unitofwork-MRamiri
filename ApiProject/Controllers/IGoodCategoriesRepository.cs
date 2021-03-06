using ApiProject.Models;
using ApiProject.Models.DTOs;
using System.Collections.Generic;

namespace ApiProject.Controllers
{
    public interface IGoodCategoriesRepository
    {
        void Add(string Title);
        void CheckForDuplicatedTitle(string title);
        IList<GetGoodCategoryDto> GetAll();
        GoodCategory Find(int id);
        void Update(GoodCategory category, string Title);
    }
}