using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtils
{
    public static class CopyClass<T> where T : class, new()
    {
        private static IMapper _Mapper;
        private static MapperConfiguration _MapperConfig;
        
        static CopyClass()
        {
            _MapperConfig = new MapperConfiguration(
                cfg => cfg.CreateMap<T, T>(MemberList.None));
            _MapperConfig.AssertConfigurationIsValid();
            _Mapper = _MapperConfig.CreateMapper();

        }
        public static T Copy(T value)
        {
            return _Mapper.Map<T>(value);
        }

        public static void Copy(T from, T to)
        {
            _Mapper.Map(from,to);
        }
    }
}
