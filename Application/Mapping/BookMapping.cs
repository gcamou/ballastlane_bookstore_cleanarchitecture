using Application.Books.Commands.CreateBook;
using Application.Books.Commands.UpdateBook;
using Domain.Entities;
using Mapster;

namespace Application.Mapping
{
    public class BookMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config = TypeAdapterConfig.GlobalSettings;

            TypeAdapterConfig<CreateBookCommand, Book>.NewConfig()
                .NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);
                
            TypeAdapterConfig<UpdateBookCommand, Book>.NewConfig()
                .NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);
        }
    }
}