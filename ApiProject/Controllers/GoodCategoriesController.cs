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
        private readonly IApiDbContext _context;
        private IGoodCategoriesRepository _goodCategoriesRepository;
        private IUnitOfWork _unitOfWork;

        public GoodCategoriesController(IApiDbContext context, IGoodCategoriesRepository goodCategoriesRepository, IUnitOfWork unitOfWork)
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

            GoodCategory category = _goodCategoriesRepository.Find(id);
            _goodCategoriesRepository.Delete(category);

            _unitOfWork.Complete();
        }
    }
}
