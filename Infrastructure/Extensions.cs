namespace Mahamudra.Imaging.Infrastructure
{
    public static class Extensions
    {
        public static int GetId(this string filePath)
        {
            var seg = filePath.Split("-", StringSplitOptions.TrimEntries).DefaultIfEmpty("").LastOrDefault();
            var id = seg == null ? "0" : seg.Split(".", StringSplitOptions.TrimEntries).FirstOrDefault();
            return Convert.ToInt32(id);
        }
    }
}
