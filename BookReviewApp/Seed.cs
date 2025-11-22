using BookReviewApp.Data;
using BookReviewApp.Models;

namespace BookReviewApp
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.BookOwners.Any())
            {
                var BookOwners = new List<BookOwner>()
                {
                    new BookOwner()
                    {
                        Book = new Book()
                        {
                            Title = "Como fazer amigos e influenciar pessoas",
                            PublicationDate = new DateTime(2019,10,8),
                            BookCategories = new List<BookCategory>()
                            {
                                new BookCategory { Category = new Category() { Name = "Desenvolvimento Pessoal"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review {
                                    Title="Ótimo livro",
                                    Text = "O livro possui uma fácil leitura e traz insights maravilhosos sobre liderança. É um livro de cabeceira para que você possa ler e reler várias vezes.",
                                    Rating = 5,
                                    Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" }
                                },
                                new Review {
                                    Title="Maravilhoso!",
                                    Text = "Livro necessário, todos deveriam ler algum dia na vida. Dicas e reflexões para o dia a dia, para a vida pessoal, para o trabalho... pois nos relacionamos com pessoas o tempo todo, com menor ou maior frequência. Esse livro é um diferencial de como se relacionar e ter melhor resultados.",
                                    Rating = 5,
                                    Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" }
                                },
                                new Review {
                                    Title="Livro muito bom de ler",
                                    Text = "Esse livro mudou minha mente como pensava, esse livro e muito bom",
                                    Rating = 5,
                                    Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" }
                                },
                            }
                        },
                        Owner = new Owner()
                        {
                            Name = "Dale Carnegie",
                            Profession = "Escritor e orador",
                            Country = new Country()
                            {
                                Name = "Estados Unidos"
                            }
                        }
                    },
                    new BookOwner()
                    {
                        Book = new Book()
                        {
                            Title = "Milagre da manhã",
                            PublicationDate = new DateTime(2016,07,19),
                            BookCategories = new List<BookCategory>()
                            {
                                new BookCategory { Category = new Category() { Name = "Desenvolvimento Pessoal"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review {
                                    Title= "Livro excelente",
                                    Text = "O livro tem uma visão clara sobre os hábitos, achei ele bem motivador.",
                                    Rating = 5,
                                    Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" }
                                },
                                new Review {
                                    Title= "Achei o conteúdo muito bom e enriquecedor!",
                                    Text = "Realmente um excelente livro. Muito bom e de uma estrutura excelente.",
                                    Rating = 5,
                                    Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" }
                                },
                                new Review {
                                    Title= "Livro transformação pessoal",
                                    Text = "Amei a leitura! Tem uma metodologia prática e didática para aplicação no dia a dia. Eu já tinha uma visão sobre ser mais intencional pelas manhãs, depois da leitura mais ainda.",
                                    Rating = 3,
                                    Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" }
                                },
                            }
                        },
                        Owner = new Owner()
                        {
                            Name = "Hal Elrod",
                            Profession = "Palestrante e coaching",
                            Country = new Country()
                            {
                                Name = "Estados Unidos"
                            }
                        }
                    },
                    new BookOwner()
                    {
                        Book = new Book()
                        {
                            Title = "Milagre da manhã",
                            PublicationDate = new DateTime(1532,02,01),
                            BookCategories = new List<BookCategory>()
                            {
                                new BookCategory { Category = new Category() { Name = "Water"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review {
                                    Title= "Um livro muito bonito e com aprendizados muito valiosos.",
                                    Text = "É aquele tipo de livro que se faz necessário lê-lo, no mínimo, uma vez por ano, como se fosse uma bíblia.\r\nPossui uma linguagem difícil que (ironicamente) lembra muito as primeiras traduções da Bíblia. Portanto, é necessário reler trechos por numerosas vezes para entender determinadas mensagens.\r\nÉ um livro riquíssimo e, embora possua um teor 100% político, é possível absorver tais ensinamentos para a vida pessoal, não se limitando somente à CEO's, líderes, presidentes, etc.",
                                    Rating = 4,
                                    Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" }
                                },
                                new Review {
                                    Title= "Ótimo livro, recomendo",
                                    Text = "Livro muito bonito",
                                    Rating = 4,
                                    Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" }
                                },
                                new Review {
                                    Title= "A leitura de um clássico",
                                    Text = "Um dos melhores livros que já. A leitura é densa, a edição em capa dura é excelente, valeu cada centavo.",
                                    Rating = 5,
                                    Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" }
                                },
                            }
                        },
                        Owner = new Owner()
                        {
                            Name = "Nicolau Maquiavel",
                            Profession = "filósofo, historiador, poeta, diplomata",
                            Country = new Country()
                            {
                                Name = "Florença"
                            }
                        }
                    }
                };
                dataContext.BookOwners.AddRange(BookOwners);
                dataContext.SaveChanges();
            }
        }
    }
}   