using ApiProject.Models;
using ApiProject.Models.DTOs;
using ApiProject.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject.Controllers
{
    [ApiController]
    [Route("api/good-categories")]
    public class GoodCategoriesController : Controller
    {
        private readonly ApiDbContext _context;
        private IGoodCategoriesRepository _goodCategoriesRepository;
        private IUnitOfWork _unitOfWork;

        public GoodCategoriesController(ApiDbContext context, IGoodCategoriesRepository goodCategoriesRepository, IUnitOfWork unitOfWork)
        {
            _context = context;
            _goodCategoriesRepository = goodCategoriesRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public void Add([Required][FromBody] string Title)
        {
            _goodCategoriesRepository.CheckForDuplicatedTitle(Title);
            
            _goodCategoriesRepository.Add(Title);

            _unitOfWork.Complete();
        }

        [HttpGet]
        public IList<GetGoodCategoryDto> GetAll()
        {
            return _goodCategoriesRepository.GetAll();
            
        }

        [HttpPut("{id}")]
        public void Update(int id, UpdateGoodCategoryDto dto)
        {
            GoodCategory category = _goodCategoriesRepository.Find(id);

            _goodCategoriesRepository.Update(category, dto.Title);

            _unitOfWork.Complete();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var category = _context.GoodCategories.Find(id);
            _context.GoodCategories.Remove(category);

            /* var goodCategory = new GoodCategory { Id = id };
             _context.Entry(goodCategory).State = EntityState.Deleted;*/

            _context.SaveChanges();
        }
    }
}
