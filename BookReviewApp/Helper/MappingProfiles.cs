// Basicamente nós teríamos que fazer o mapping de cada
// um dos modelos para DTO, porém utilizaremos um automapper
// para que essa tarefa seja feita automaticamente.

using AutoMapper;
using BookReviewApp.Dto;
using BookReviewApp.Models;

namespace BookReviewApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Book
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book >();
            // Category
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            // Country
            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();
            // Owner
            CreateMap<Owner, OwnerDto>();
            CreateMap<OwnerDto, Owner>();
            // Review
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();
            // Reviewer
            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<ReviewerDto, Reviewer>();
        }
    }
}
