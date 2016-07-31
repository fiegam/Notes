using EmitMapper;
using EmitMapper.MappingConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Core.Infrastructure.Extensions
{
   public static class MappingExtension
    {
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
