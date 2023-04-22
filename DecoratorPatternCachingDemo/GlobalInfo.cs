namespace DecoratorPatternCachingExample
{
    // Warning: Sometimes, global members does not a good practice. It is necessary caution to use them
    // This can be against Single Responsibility Principle and/or against OOP
    // The code bellow is only to help this DEMO
    public static class GlobalInfo
    {
        private static bool isDataFromCache;
        public static bool IsDataFromCache
        {
            get => isDataFromCache;
            set => isDataFromCache = value;
        }
    }
}
