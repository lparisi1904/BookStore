using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Services;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using Moq;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Xunit;

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

            _categoryRepositoryMock.Setup(cat =>
                cat.GetById(category.Id))
                .ReturnsAsync(category);

            var result = await _categoryService.GetById(category.Id);

            Assert.NotNull(result);
            Assert.IsType<Category>(result);
        }


        [Fact]
        public async void GetById_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            _categoryRepositoryMock
                .Setup(cat => cat.GetById(1))
                .ReturnsAsync((Category)null);

            var resul = await _categoryService.GetById(1);

            Assert.Null(resul);
        }

        //Il metodo chiama il metodo GetById dalla classe repository solo una volta
        [Fact]
        public async void GetById_ShouldCallGetByIdFromRepository_OnlyOnce()
        {
            _categoryRepositoryMock.Setup(x => x.GetById(1)).ReturnsAsync((Category)null);

            await _categoryService.GetById(1);

            _categoryRepositoryMock.Verify(x => x.GetById(1), Times.Once);

        }

        // Il metodo non aggiunge una categoria quando il nome della categoria esiste già
        [Fact]
        public async void Add_ShouldAddCategory_WhenCategoryNameDoesNotExist()
        {
            var category = CreateCategory();

            _categoryRepositoryMock.Setup(c =>
                c.Search(c => c.Name == category.Name))
                .ReturnsAsync(new List<Category>());

            _categoryRepositoryMock.Setup(c => c.Add(category));

            var result = await _categoryService.Add(category);

            Assert.NotNull(result);
            Assert.IsType<Category>(result);
        }

              
        //Il metodo chiama il metodo Add dalla classe repository solo una volta
        [Fact]
        public async void Add_ShouldCallAddFromRepository_OnlyOnce()
        {
            var category = CreateCategory();

            _categoryRepositoryMock.Setup(c =>
                    c.Search(c => c.Name == category.Name))
                .ReturnsAsync(new List<Category>());
            _categoryRepositoryMock.Setup(c => c.Add(category));

            await _categoryService.Add(category);

            _categoryRepositoryMock.Verify(mock => mock.Add(category), Times.Once);
        }


        [Fact]
        public async void Update_ShouldUpdateCategory_WhenCategoryNameDoesNotExist()
        {
            var category = CreateCategory();

            _categoryRepositoryMock.Setup(c =>
                c.Search(c => c.Name == category.Name && c.Id != category.Id))
                .ReturnsAsync(new List<Category>());
            _categoryRepositoryMock.Setup(c => c.Update(category));

            var result = await _categoryService.Update(category);

            Assert.NotNull(result);
            Assert.IsType<Category>(result);
        }

        [Fact]
        public async void Update_ShouldNotUpdateCategory_WhenCategoryDoesNotExist()
        {
            var category = CreateCategory();
            var categoryList = new List<Category>()
            {
                new Category()
                {
                    Id = 2,
                    Name = "Test Category 2"
                }
            };

            _categoryRepositoryMock.Setup(c =>
                    c.Search(c => c.Name == category.Name && c.Id != category.Id))
                .ReturnsAsync(categoryList);

            var result = await _categoryService.Update(category);

            Assert.Null(result);
        }


        [Fact]
        public async void Update_ShouldCallUpdateFromRepository_OnlyOnce()
        {
            var category = CreateCategory();

            _categoryRepositoryMock.Setup(c =>
                    c.Search(c => c.Name == category.Name && c.Id != category.Id))
                .ReturnsAsync(new List<Category>());

            await _categoryService.Update(category);

            _categoryRepositoryMock.Verify(mock => mock.Update(category), Times.Once);
        }

        [Fact]
        public async void Remove_ShouldRemoveCategory_WhenCategoryDoNotHaveRelatedBooks()
        {
            var category = CreateCategory();

            _bookServiceMock.Setup(b =>
                b.GetBooksByCategory(category.Id)).ReturnsAsync(new List<Book>());

            var result = await _categoryService.Remove(category);

            Assert.True(result);
        }

        [Fact]
        public async void Remove_ShouldNotRemoveCategory_WhenCategoryHasRelatedBooks()
        {
            var category = CreateCategory();

            var books = new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Title = "Test title 1",
                    Author = "Test Author 1",
                    CategoryId = category.Id
                }
            };

            _bookServiceMock.Setup(b => b.GetBooksByCategory(category.Id)).ReturnsAsync(books);

            var result = await _categoryService.Remove(category);

            Assert.False(result);
        }

        [Fact]
        public async void Remove_ShouldCallRemoveFromRepository_OnlyOnce()
        {
            var category = CreateCategory();

            _bookServiceMock.Setup(b =>
                b.GetBooksByCategory(category.Id)).ReturnsAsync(new List<Book>());

            await _categoryService.Remove(category);

            _categoryRepositoryMock.Verify(mock => mock.Remove(category), Times.Once);
        }

        [Fact]
        public async void Search_ShouldReturnAListOfCategory_WhenCategoriesWithSearchedNameExist()
        {
            var categoryList = CreateCategoryList();
            var searchedCategory = CreateCategory();
            var categoryName = searchedCategory.Name;

            _categoryRepositoryMock.Setup(c =>
                c.Search(c => c.Name.Contains(categoryName)))
                .ReturnsAsync(categoryList);

            var result = await _categoryService.Search(searchedCategory.Name);

            Assert.NotNull(result);
            Assert.IsType<IEnumerable<Category>>(result);
        }



        [Fact]
        public async void Search_ShouldCallSearchFromRepository_OnlyOnce()
        {
            var categoryList = CreateCategoryList();
            var searchedCategory = CreateCategory();
            var categoryName = searchedCategory.Name;

            _categoryRepositoryMock.Setup(c =>
                    c.Search(c => c.Name.Contains(categoryName)))
                .ReturnsAsync(categoryList);

            await _categoryService.Search(searchedCategory.Name);

            _categoryRepositoryMock.Verify(mock => mock.Search(c => c.Name.Contains(categoryName)), Times.Once);
        }
    }
}
