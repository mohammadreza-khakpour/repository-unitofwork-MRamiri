using ApiProject.Models;
using System;

namespace ApiProject.Controllers
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApiDbContext _context;
        public UnitOfWork(ApiDbContext context)
        {
            _context = context;
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}