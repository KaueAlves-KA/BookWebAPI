// Um DTO é uma forma de garantir que não vamos retornar a Data inteira do db, por exemplo, dados sensíveis de um usuário quando
// recuperarem uma lista de usuários por exemplo. Também podemos impedir que nossa API receba certos tipos de dados.
namespace BookReviewApp.Dto
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
