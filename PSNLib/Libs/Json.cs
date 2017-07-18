using System.Web.Script.Serialization;
internal static class Json
{
    static JavaScriptSerializer _json;
    static Json()
    {
        _json = new JavaScriptSerializer(); 
    }
    public static dynamic Read(string jsonInput)
    {
        try
        {
            if (jsonInput == "error")
                return default(dynamic);
            return _json.Deserialize<dynamic>(jsonInput);
        }
        catch (System.Exception)
        {
            return default(dynamic);
        }
    }
    public static bool IsKeyExist(string key, string jsonInput)
    {
        try
        {
            var r = Read(jsonInput);
            foreach (var item in r.Keys)
            {
                if (key == item)
                    return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }
}
