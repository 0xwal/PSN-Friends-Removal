
using System.Web;
using PSNLib.Models;
using TheHttp;

namespace PSNLib
{
    public partial class PSAPI
    {
        private string _npsso { get; set; }
        private string _npGrantCodeRequest { set; get; }
        private bool getNpsso(string email, string password)
        {
            HttpResult res = Http.Post(Consts.APIEndpoints.SSO_COOKIE_URL, new
            {
                authentication_type = Consts.UrlParamaters.Authentication_Type,
                username = email,
                password = password,
                client_id = "71a7beb8-f21a-47d9-a604-2e71bee24fe0"
            });
            if (Json.IsKeyExist("error", res.ToString()) || res.HasError)
                return false;
            dynamic theRes = res.AsJson();
            _npsso = theRes["npsso"].ToString();
            return true;
        }
        private bool getNpGrantCodeRequest(string npsso)
        {
            string res = Http.Get(Consts.APIEndpoints.NP_GRANT_URL, new
            {
                state = Consts.UrlParamaters.State,
                duid = Consts.UrlParamaters.Duid,
                app_context = Consts.UrlParamaters.AppContext,
                client_id = Consts.UrlParamaters.ClientId,
                scope = Consts.UrlParamaters.Scope,
                response_type = Consts.UrlParamaters.ResponseType
            },
                   new
                   {
                       npsso = npsso
                   }
               ).ToString();
            string uri = HttpUtility.UrlDecode(res);
            string code = uri.Remove(0, uri.IndexOf("code=") + "code=".Length);
            code = code.Substring(0, code.IndexOf("&"));
            if (code.Contains("DOCTYPE"))
                return false;
            _npGrantCodeRequest = code;
            return true;
        }
        private bool oauthRequest(string grant)
        {
            var result =
                Http.Post
                (
                    Consts.APIEndpoints.OAUTH_URL,
                        new
                        {
                            app_context = Consts.UrlParamaters.AppContext,
                            client_id = Consts.UrlParamaters.ClientId,
                            client_secret = Consts.UrlParamaters.ClientSecret,
                            code = grant,
                            duid = Consts.UrlParamaters.Duid,
                            grant_type = Consts.UrlParamaters.GrantType,
                            scope = Consts.UrlParamaters.Scope
                        }
                );
            if (Json.IsKeyExist("error", result.ToString()) || result.HasError)
                return false;
            dynamic theRes = result.AsJson();
            Variables.AccessToken = theRes["access_token"].ToString();
            Variables.RefreshToken = theRes["refresh_token"].ToString();
            return true;
        }

        //need to fix
        private bool oauthRequestWithRefreshToken(string refreshToken)
        {
            var result =
                Http.Post
                (
                    Consts.APIEndpoints.OAUTH_URL,
                        new
                        {
                            app_context = Consts.UrlParamaters.AppContext,
                            client_id = Consts.UrlParamaters.ClientId,
                            client_secret = Consts.UrlParamaters.ClientSecret,
                            refresh_token = refreshToken,
                            duid = Consts.UrlParamaters.Duid,
                            grant_type = "refresh_token",
                            scope = Consts.UrlParamaters.Scope

                        }
                );
            if (Json.IsKeyExist("error", result.ToString()) || result.HasError)
                return false;
            dynamic theRes = result.AsJson();
            Variables.AccessToken = (theRes)["access_token"].ToString();
            Variables.RefreshToken = (theRes)["refresh_token"].ToString();
            return true;
        }
    }

}