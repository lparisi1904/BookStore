using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Repositories
{
    public class CategoryRepository: BaseRepository<Category> , ICategoryRepository
    {
        public CategoryRepository(BookStoreContext context) :base(context) { }

    }
}
