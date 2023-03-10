using Blazored.Toast.Services;
using BookStore.SPA.Interfaces;
using BookStore.SPA.Models;
using Microsoft.AspNetCore.Components;

namespace BookStore.SPA.Pages.Books
{
    public partial class BookEdit
    {
        [Parameter]
        public int BookId { get; set; }

        [Inject]
        public IBookService BookService { get; set; }

        [Inject]
        public ICategoryService CategoryService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        public Book Book { get; set; } = new Book();

        public IEnumerable<Category> CategoryList { get; set; } = new List<Category>();

        protected string Title = "Nuovo Libro";

        protected override async Task OnInitializedAsync()
        {
            CategoryList = await CategoryService.GetAll();

            if (BookId != 0)
            {
                var book = await BookService.GetById(BookId);

                if (book != null)
                {
                    Book = book;
                    Title = $"Modifica {Book.Title}";
                }
                else
                {
                    Book = new Book();
                    ToastService.ShowError("Qualcosa di errato è avvenuto nel caricamento del libro..");
                }
            }
        }
        protected async Task HandleValidSubmit()
        {
            if (Book.Id == 0)
                await AddBook();
            else
                await UpdateBook();
        }

        private async Task AddBook()
        {
            var result = await BookService.Add(Book);
            if (result != null)
            {
                ToastService.ShowSuccess("The book was successfully added.");
                NavigateToBooksPage();
            }
            else
            {
                ToastService.ShowError("Something went wrong while adding the book. Please try again.");
            }
        }

        private async Task UpdateBook()
        {
            var result = await BookService.Update(Book);
            if (result)
            {
                ToastService.ShowSuccess("The book was successfully updated.");
                NavigateToBooksPage();
            }
            else
            {
                ToastService.ShowError("Something went wrong while updating the book. Please try again.");
            }
        }

        private void NavigateToBooksPage()
        {
            NavigationManager.NavigateTo("/books");
        }
    }
}