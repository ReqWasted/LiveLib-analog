using System.Runtime.CompilerServices;
using AutoMapper;
using LiveLib.Application.Commom.Mapping;
using LiveLib.Domain.Models;

namespace LiveLib.Application.Features.Books.UpdateBook
{
    public class UpdateBookDto : IMapWith<Book>
    {
        public string? Name { get; set; }
        public DateOnly? PublicatedAt { get; set; }
        public int? PageCount { get; set; }
        public string? Description { get; set; }
        public double? AverageRating { get; set; }
        public string? Isbn { get; set; }

        public Guid? GenreId { get; set; }

        public Guid? AuthorId { get; set; }

        public Guid? BookPublisherId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateBookDto, Book>()
                .ForAllMembers(opts =>
                {
                    opts.Condition((src, dest, srcMember, destMember, context) =>
                           {
                               if (srcMember == null) return false;

                               var memberType = srcMember.GetType();
                               if (memberType.IsValueType && !memberType.IsEnum)
                               {
                                   var defaultValue = RuntimeHelpers.GetUninitializedObject(memberType);
                                   return !srcMember.Equals(defaultValue);
                               }

                               return true;
                           }
                       );
                });

        }
    }
}
