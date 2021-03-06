using ApiProject.Models;
using ApiProject.Models.DTOs;
using ApiProject.Models.Exceptions;
using System;
using System.Collections.Generic;
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

        public IList<GetGoodCategoryDto> GetAll()
        {
            return _context.GoodCategories.Select
                  (_ => new GetGoodCategoryDto
                  {
                      Id = _.Id,
                      Title = _.Title
                  }).ToList();
        }

        public GoodCategory Find(int id)
        {
            return _context.GoodCategories.Find(id);
        }

        public void Update(GoodCategory category, string Title)
        {
            category.Title = Title;
        }
    }
}