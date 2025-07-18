namespace Moonforged.GatesAndFences
{
    public static class Localizer
    {
        public static void Add(string key, string value)
        {
            LocalizationCache.Add(key, value);
        }

        public static void Register()
        {
            LocalizationCache.RegisterToJotunnLocalization();
        }
    }
}
