namespace Ottoman.Mapper.Extensions
{

    public static class MapperExtensions
    {
        public static T To<T>(this object obj)
        {
            return AutoMapper.Mapper.Map<T>(obj);
        }
    }
}
