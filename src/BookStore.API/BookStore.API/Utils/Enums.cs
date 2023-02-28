using System.ComponentModel;
using System.Reflection;

namespace BookStore.API.Utils
{
    public static class Enums
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType()
                .GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public enum StatusCode
        {
            // Books codes....
            [Description("Libro non trovato.")]
            BookNotFound = 100,

            [Description("Libro aggiornato correttamente.")]
            BookSuccessUpdate = 200,

            [Description("Libro cancellato correttamente.")]
            BookSuccessDeleted = 300,

            [Description("Codice (id) non corrispondente ad id in archivio.")]
            BookNotMatch = 400,

            [Description("Stato operazione OK.")]
            BookSuccessOK = 500,

            [Description("Creazione nuovo libro non riuscita.")]
            BookSuccessKO = 600,


            // Categories codes...
            [Description("Categoria non trovata.")]
            CategoryNotFound = 100,

            [Description("Categoria aggiornato correttamente.")]
            CategorySuccessUpdate = 200,

            [Description("Categoria cancellata correttamente.")]
            CategorySuccessDeleted = 300,

            [Description("Codice (id) non corrispondente ad id in archivio.")]
            CategoryNotMatch = 400,

            [Description("Stato operazione OK.")]
            CategorySuccessOK = 500,

            [Description("Cancellazione Categoria non riuscita.")]
            CategoryDeletedKO = 600
        }

        public enum Genre
        {
            Fantasy,
            Adventure,
            Romance,
            Contemporary,
            Dystopian,
            Mystery,
            Horror,
            Thriller,
            Paranormal,
            Historical_fiction,
            Science_Fiction,
            Childrens,
            Memoir,
            Cooking,
            Art,
            Self_help,
            Personal,
            Development,
            Motivational,
            Health,
            History,
            Travel,
            How_to,
            Families_Relationships,
            Humor,
            Biography
        }
    }
}
