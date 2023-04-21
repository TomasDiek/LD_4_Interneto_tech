namespace LD_4_Interneto_tech.Extensions
{
    public static class StringExtension
    {
        public static bool IsEmpty(this string s)
        {
            return string.IsNullOrEmpty(s.Trim());
        }
    }
}
