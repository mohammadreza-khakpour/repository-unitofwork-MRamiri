using ApiProject.Models;
using ApiProject.Models.DTOs;

namespace ApiProject.Controllers
{
    public interface IGoodsRepository
    {
        void Add(AddGoodDto dto);
        void CheckForDuplicatedTitle(string code);
        Good Find(int id);
        void Update(Good theGood, UpdateGoodDto dto);
    }
}