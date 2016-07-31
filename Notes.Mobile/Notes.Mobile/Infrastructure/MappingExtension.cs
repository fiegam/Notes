using EmitMapper;
using EmitMapper.MappingConfiguration;

namespace Notes.Mobile.Infrastructure
{
    public static class MappingExtension
    {
        public static object Mapper { get; private set; }

        public static TDest MapTo<TDest>(this object source)
        {
            if(source == null)
            {
                return default(TDest);
            }
            return (TDest)ObjectMapperManager.DefaultInstance.GetMapperImpl(source.GetType(), typeof(TDest), new DefaultMapConfig()).Map(source);
        }
    }
}