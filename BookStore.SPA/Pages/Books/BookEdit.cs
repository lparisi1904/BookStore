using Blazored.Toast.Services;
using BookStore.SPA.Interfaces;
using BookStore.SPA.Models;
using Microsoft.AspNetCore.Components;

namespace BookStore.SPA.Pages.Books
{
    public partial class BookEdit : ComponentBase
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

        [Parameter]
        public Book Book { get; set; } = new Book();

        [Parameter]
        public EventCallback<bool> OnClose { get; set; }


        public IEnumerable<Category> CategoryList { get; set; } = new List<Category>();
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Book> CompleteListBooks { get; set; }

        protected string Title = "Nuovo Libro";

        protected async override Task OnInitializedAsync()
        {
            CategoryList = await CategoryService.GetAll();

            Books = (await BookService.GetAll()).ToList();
            CompleteListBooks = Books;

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
           
            //StateHasChanged();
        }


        protected async Task EditBook(int id) 
        {
            var book = await BookService.GetById(id);

            if (book != null)
            {
                Book = book;
                Title = $"Modifica {Book.Title}";
            }
        }

        protected async Task AddBook()
        {
            var result = await BookService.Add(Book);
            if (result != null)
            {
                ToastService.ShowSuccess($"Il libro {Book.Title} è stato aggiunto.");
                NavigateToBooksPage();
            }
            else
            {
                ToastService.ShowError("Qualcosa è andato storto durante l'aggiunta del libro. Si prega di riprovare.");
            }
        }

        protected async Task UpdateBook()
        {
            var result = await BookService.Update(Book);
            if (result)
            {
                ToastService.ShowSuccess($"Il libro {Book.Title} è stato aggiornato.");
                StateHasChanged();
                NavigateToBooksPage();
            }
            else
            {
                ToastService.ShowError("Qualcosa è andato storto durante l'aggiunta del libro. Si prega di riprovare.");
            }
        }

        protected async Task DeleteBook(Book book)
        {
            if (book.Id == 0) return;

            if (await BookService.Delete(book.Id))
            {
                Books = (await BookService.GetAll()).ToList();
                CompleteListBooks = Books;

                ToastService.ShowSuccess("Cancellato");
                StateHasChanged();
            }
            else
            {
                ToastService.ShowError("errore");
            }
        }
        protected void NavigateToBooksPage()
        {
            NavigationManager.NavigateTo("/books");
        }
            
        protected Task ModalCancel()
        {
            return OnClose.InvokeAsync(false);
        }
        protected Task ModalOk()
        {
            return OnClose.InvokeAsync(true);
        }
    }
}