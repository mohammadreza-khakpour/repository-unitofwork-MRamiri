using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiProject.Models;
using ApiProject.Models.DTOs;
using ApiProject.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Controllers
{
    [ApiController,Route("api/goods")]
    public class GoodsController : Controller
    {
        private readonly ApiDbContext _context;
        private IGoodsRepository _goodsRepository;
        private IUnitOfWork _unitOfWork;

        public GoodsController(IGoodsRepository goodsRepository, IUnitOfWork unitOfWork)
        {
            _goodsRepository = goodsRepository;
            _unitOfWork = unitOfWork;
            _context = new ApiDbContext();
        }

        [HttpPost]
        public void Add([FromBody]AddGoodDto dto)
        {
            _goodsRepository.CheckForDuplicatedTitle(dto.Code);

            _goodsRepository.Add(dto);

            _unitOfWork.Complete();

        }

        [HttpPut("{id}")]
        public void Update(int id, [FromBody]UpdateGoodDto dto)
        {
            Good theGood = _goodsRepository.Find(id);

            _goodsRepository.Update(theGood, dto);

            _unitOfWork.Complete();
        }

        [HttpGet]
        public IList<GetGoodDto> GetAll()
        {
            return _context.Goods.Select(x => new GetGoodDto()
            {
                Id = x.Id,
                CategoryId = x.CategoryId,
                Code = x.Code,
                Count = x.Count,
                Title = x.Title
            }).ToList();
        }

        #region [join example]
        // join example in c#
        // here i want to create unanymous objects
        // containing matched goodEntries & goods with same code
        [HttpGet("{id}")]
        public void GetAll02(int id)
        {
            //var allGoodEntries = GetAll();
            //GoodsController c = new GoodsController();
            //var allGoods = c.GetAll();

            var innerJoinQuery =
            from goodentry in _context.GoodEntries
            join good in _context.Goods on goodentry.GoodCode equals good.Code
            select new { aa = goodentry.Id, bb = good.Id };

            foreach (var result in innerJoinQuery)
            {
                Console.WriteLine($"\"{result.aa}\"  {result.bb}");
            }

        } 
        #endregion

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _context.Goods.Remove(_context.Goods.Find(id));
            _context.SaveChanges();
        }

    }
}