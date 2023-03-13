using System.ComponentModel;
using System.Reflection;

namespace BookStore.API.Utils
{ 
    public static class ResponseMessageHttp
    {
        // codifica di messaggi status EndPoint per il livello API
        public static string GetDescription(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType()
                .GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[]) fieldInfo.
                    GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public enum StatusCode
        {
          #region Books codes

                [Description("Libro non trovato.")]
                BookNotFound = 100,

                [Description("Libro aggiornato correttamente.")]
                BookSuccessUpdate = 200,

                [Description("Libro cancellato correttamente.")]
                BookSuccessDeleted = 300,

                [Description("Codice (id) non corrispondente ad Id in archivio.")]
                BookNotMatch = 400,

                [Description("Stato operazione OK.")]
                BookSuccessOK = 500,

                [Description("Creazione nuovo libro non riuscita.")]
                BookSuccessKO = 600,

            #endregion



          #region Categories codes

                [Description("Categoria non trovata.")]
                CategoryNotFound = 700,

                [Description("Categoria aggiornato correttamente.")]
                CategorySuccessUpdate = 800,

                [Description("Categoria cancellata correttamente.")]
                CategorySuccessDeleted = 900,

                [Description("Codice (id) non corrispondente ad Id in archivio.")]
                CategoryNotMatch = 1000,

                [Description("Stato operazione OK.")]
                CategorySuccessOK = 1100,

                [Description("Cancellazione Categoria non riuscita.")]
                CategoryDeletedKO = 1200

            #endregion

        }
    }
}
