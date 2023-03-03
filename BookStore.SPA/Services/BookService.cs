using BookStore.SPA.Interfaces;
using BookStore.SPA.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace BookStore.SPA.Services
{
    public class BookService : IBookService
    {
        // => IConfiguration, verrà utilizzata per ottenere l'URL dell'API dal appsettings.jsonfile.
        private readonly IConfiguration _configuration;

        // => IHttpClientFactory, che verrà utilizzato per creare il client HTTP.
        private readonly IHttpClientFactory _httpClientFactory;
        
        // => baseUri, il cui valore proviene dal file di configurazione appsettings.json.
        private readonly string _baseUri;


        // => costruttore della classe, che riceve il IConfiguratione il IHttpClientFactory via Dependency Injection.
        public BookService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            // usiamo _configuration per leggere l'URL dell'API 
            // (che proviene dal appsettings.jsonfile) e il valore viene aggiunto alla variabile _baseUri .
            _configuration = configuration;
            _baseUri = _configuration.GetSection("BookStoreApi:Url").Value;
            _httpClientFactory = httpClientFactory;
        }


        public async Task<Book?> Add(Book book)
        {
            // creiamo un'istanza di HttpClient
            var httpClient = _httpClientFactory.CreateClient(_baseUri);

            // serializza il libro su una variabile JSON.
            var bookJson = new StringContent(JsonSerializer.Serialize(book), Encoding.UTF8, "application/json");

            // faccio la richiesta Post al back-end
            var response = await httpClient.PostAsync($"{_baseUri}api/books", bookJson);

            //Chiamo il response.IsSuccessStatusCode, per sapere se la risposta è andata a buon fine o meno, in caso di Successful, restituirà true.
            if (response.IsSuccessStatusCode)
            {
                // deserializzo il contenuto della risposta alla richiesta (il libro aggiunto) e lo restituisce.
                return await JsonSerializer
                    .DeserializeAsync<Book>
                    (await response.Content.ReadAsStreamAsync());
            }
            //null, nel caso in cui la risposta non abbia successo.
            return null;
        }

        public async Task<bool> Delete(int bookId)
        {
            var httpClient = _httpClientFactory.CreateClient(_baseUri);

            // faccio la richiesta Delete comunicando l'id del libro 
            var response = await httpClient.DeleteAsync($"{_baseUri}api/books/{bookId}");

            // restituisce true, altrimenti restituisce false.
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            //creo un'istanza di HttpClientchiamando il metodo CreateClient.
            var httpClient = _httpClientFactory.CreateClient();

            //dove viene effettuata la richiesta al servizio di back-endusando GetFromJsonAsync
            //per il passaggio del tipo di dati che vogliamo deserializzare, che in questo caso è un IEnumerable<Book>,
            //in un'operazione asincrona.
            var response = await httpClient.GetFromJsonAsync<IEnumerable<Book>>($"{_baseUri}api/books");

            // restituiamo l'elenco dei libri.
            return response;
        }

        public async Task<Book?> GetById(int Bookid)
        {
            //creo un'istanza di HttpClientchiamando il metodo CreateClient.
            var httpClient = _httpClientFactory.CreateClient();

            //  faccio la richiesta Get al back-end
            var response = await httpClient.GetAsync($"{_baseUri}api/books/{Bookid}");

            // verifico se la risposta ha un codice di stato di successo e, in caso affermativo, restituirà il libro, altrimenti restituirà null.
            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer
                    .DeserializeAsync<Book>(await response.Content
                    .ReadAsStreamAsync(),
                    new JsonSerializerOptions() 
                    { PropertyNameCaseInsensitive = true });
            }

            return null;
        }

        public async Task<bool> Update(Book book)
        {
            var httpClient = _httpClientFactory.CreateClient(_baseUri);

            var bookJson = new StringContent(JsonSerializer.Serialize(book), Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync($"{_baseUri}api/books/{book.Id}", bookJson);

            return response.IsSuccessStatusCode;
        }
    }
}
