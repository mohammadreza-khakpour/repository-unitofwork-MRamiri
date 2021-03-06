using ApiProject.Models;
using ApiProject.Models.Exceptions;
using System;
using System.Linq;

namespace ApiProject.Controllers
{
    public class GoodCategoriesRepository : IGoodCategoriesRepository
    {
        private ApiDbContext _context;
        public GoodCategoriesRepository(ApiDbContext context)
        {
            _context = context;
        }

        public void CheckForDuplicatedTitle(string title)
        {
            if (_context.GoodCategories.Any(_ => _.Title == title))
            {
                throw new GoodCategoryTitleCantBeDuplicatedExcption();
            }
        }

        public void Add(string Title)
        {
            var goodCategory = new GoodCategory
            {
                Title = Title
            };
            _context.GoodCategories.Add(goodCategory);
        }
    }
}