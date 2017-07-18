using TheHttp;

namespace PSNLib.Models
{
    internal static class Extensions
    {
        public static dynamic AsJson(this HttpResult httpResult)
        {
            try
            {
                return Json.Read(httpResult.ToString());
            }
            catch
            {
                return default(dynamic);
            }
        }

        public static dynamic AsJson(this string httpResult)
        {
            try
            {
                return Json.Read(httpResult.ToString());
            }
            catch
            {
                return default(dynamic);
            }
        }
    }
}

