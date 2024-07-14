using AutoMapper;
using AutoMapper.Internal;
using Ecommerce.Application.Interfaces.AutoMapper;

namespace Ecommerce.Persistence.AutoMapper;

public class CustomMapper : ICustomMapper
{
    public static List<TypePair> typePairs = new();
    private IMapper MapperContainer;

    public TDestination Map<TDestination, TSource>(TSource source, string? ignore = null)
    {
        Config<TDestination, TSource>(5, ignore);
        return MapperContainer.Map<TSource, TDestination>(source);
    }

    public IList<TDestination> Map<TDestination, TSource>(IList<TSource> source, string? ignore = null)
    {
        Config<TDestination, TSource>(5, ignore);
        return MapperContainer.Map<IList<TSource>, IList<TDestination>>(source);
    }

    public TDestination Map<TDestination>(object source, string? ignore = null)
    {
        Config<TDestination, object>(5, ignore);
        return MapperContainer.Map<TDestination>(source);
    }

    public IList<TDestination> Map<TDestination>(IList<object> source, string? ignore = null)
    {
        Config<TDestination, IList<object>>(5, ignore);
        return MapperContainer.Map<IList<TDestination>>(source);
    }

    protected void Config<TDestination, TSource>(int depth = 5, string? ignore = null)
    {
        var typePair = new TypePair(typeof(TSource), typeof(TDestination));
        if (typePairs.Any(x => x.DestinationType == typePair.DestinationType && x.SourceType == typePair.SourceType) && ignore == null)
            return;

        typePairs.Add(typePair);
        var config = new MapperConfiguration(x =>
        {
            foreach (var item in typePairs)
            {
                if (ignore != null)
                    x.CreateMap(item.SourceType, item.DestinationType).MaxDepth(depth).ForMember(ignore, x => x.Ignore()).ReverseMap();
                else
                    x.CreateMap(item.SourceType, item.DestinationType).MaxDepth(depth).ReverseMap();
            }
        });

        MapperContainer = config.CreateMapper(); 
    }
}
