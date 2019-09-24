namespace Mocker.Core
{
    public static class Mocker
    {
        public static MockerInstance<T> ForEntity<T>()
            where T : class, new()
        {
            return new MockerInstance<T>();
        }
    }
}
