using ApiProject.Models;
using ApiProject.Models.DTOs;
using ApiProject.Models.Exceptions;
using System;
using System.Linq;

namespace ApiProject.Controllers
{
    public class GoodsRepository : IGoodsRepository
    {
        private ApiDbContext _context;
        public GoodsRepository(ApiDbContext context)
        {
            _context = context;
        }

        public void CheckForDuplicatedTitle(string code)
        {
            if (_context.Goods.Any(_ => _.Code == code))
            {
                throw new GoodCodeCantBeDuplicateException();
            }
        }

        public void Add(AddGoodDto dto)
        {
            var good = new Good
            {
                Title = dto.Title,
                Code = dto.Code,
                Count = 0,
                CategoryId = dto.CategoryId
            };

            _context.Goods.Add(good);
        }

        public Good Find(int id)
        {
            return _context.Goods.Find(id);
        }

        public void Update(Good theGood, UpdateGoodDto dto)
        {
            theGood.Title = dto.Title;
            theGood.Code = dto.Code;

            _context.Goods.Update(theGood);
        }
    }
}