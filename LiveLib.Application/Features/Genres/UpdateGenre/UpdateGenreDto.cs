using AutoMapper;
using LiveLib.Application.Commom.Mapping;
using LiveLib.Domain.Models;

namespace LiveLib.Application.Features.Genres.UpdateGenre
{
    public class UpdateGenreDto : IMapWith<Genre>
    {
        public string? Name { get; set; }

        public int? AgeRestriction { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateGenreDto, Genre>()
                .ForAllMembers(opts => opts.Condition(
        (src, dest, srcMember, destMember, context) =>
        {
            if (srcMember == null)
                return false;

            // Для Guid (включая Guid?) проверяем на Guid.Empty
            if (srcMember is Guid guidValue)
                return guidValue != Guid.Empty;

            // Для остальных типов-значений проверяем на default
            var memberType = srcMember.GetType();
            if (memberType.IsValueType && !memberType.IsEnum)
            {
                var defaultValue = Activator.CreateInstance(memberType);
                return !srcMember.Equals(defaultValue);
            }

            return true;
        }
    ));
        }
    }
}
