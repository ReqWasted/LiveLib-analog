using AutoMapper;

namespace LiveLib.Application.Commom.Mapping
{
    public interface IMapWith<T>
    {
        public void Mapping(Profile profile) =>
            profile.CreateMap(typeof(T), GetType())
            .ReverseMap();
    }
}
