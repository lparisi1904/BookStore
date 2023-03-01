using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Tests
{
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly Mock<IBookService> _bookServiceMock;
        private readonly CategoryService _categoryService;

        public CategoryServiceTests()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _bookServiceMock = new Mock<IBookService>();
            _categoryService = new CategoryService(_categoryRepositoryMock.Object, _bookServiceMock.Object);
        }


        private Category CreateCategory()
        {
            return new Category()
            {
                Id = 1,
                Name = "Test Category 1"
            };
        }

        private List<Category> CreateCategoryList()
        {
            return new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name = "Test Category 1"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Test Category 2"
                },
                new Category()
                {
                    Id = 3,
                    Name = "Test Category 3"
                }
            };
        }

        [Fact]
        public async void GetAll_ShouldReturnAListOFCategories_WhenCategoriesExist()
        {
            var categories = CreateCategoryList();

            _categoryRepositoryMock.Setup(c =>
                c.GetAll()).ReturnsAsync(categories);

            var result = await _categoryService.GetAll();

            Assert.NotNull(result);
            Assert.IsType<List<Category>>(result);
        }
        
    
        [Fact]
        public async void GetAll_ShouldCallGetAllFromRepository_OnlyOnce()
        {
            _categoryRepositoryMock
                .Setup(c => c.GetAll())
                .ReturnsAsync((List<Category>)null);

            await _categoryService.GetAll();

            _categoryRepositoryMock.Verify(mock => mock.GetAll(), Times.Once);
        }

        [Fact]
        public async void GetById_ShouldReturnCategory_WhenCategoryExist()
        {
            var category = CreateCategory();

            _categoryRepositoryMock.Setup(c =>
                c.GetById(category.Id)).ReturnsAsync(category);

            var result = await _categoryService.GetById(category.Id);

            Assert.NotNull(result);
            Assert.IsType<Category>(result);
        }




        [Fact]
        public async void GetById_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            _categoryRepositoryMock
                .Setup(x => x.GetById(1))
                .ReturnsAsync((Category)null);

            var resul = await _categoryService.GetById(1);

            Assert.Null(resul);
        }

        [Fact]
        public async void GetById_ShouldCallGetByIdFromRepository_OnlyOnce()
        {
            _categoryRepositoryMock.Setup(x => x.GetById(1)).ReturnsAsync((Category)null);

            await _categoryService.GetById(1);

            _categoryRepositoryMock.Verify(x => x.GetById(1), Times.Once);

        }
    }
}
