using AutoMapper;

namespace TeslaRent.Application.Common.AutoMapper;

public interface IMapFrom<T>
{
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType()).ReverseMap();
}