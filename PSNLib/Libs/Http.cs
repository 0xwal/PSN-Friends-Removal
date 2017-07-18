using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
namespace TheHttp
{

    public class HttpResult
    {
        private string _input;
        public HttpResult(string input)
        {
            _input = input;
        }
        public override string ToString()
        {
            return _input;
        }
        public string ErrorMessage { get; internal set; }
        public bool HasError { get; internal set; }
        public Header[] ResponseHeaders { get; internal set; }
    }
    public struct Header
    {
        public object Value { get; set; }
        public string Key { get; set; }
    }
    public class Headers : IEnumerable<Header>
    {
        private Header[] _headers = new Header[0];
        private int _length;
        public void Add(string header, object value)
        {
            if (_length == _headers.Length)
            {
                Array.Resize(ref _headers, _length + 1);
            }
            _headers[_length++] = new Header() { Key = header, Value = value };
        }
        public IEnumerator<Header> GetEnumerator()
        {
            foreach (var item in _headers)
            {
                yield return item;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public string this[string key]
        {
            get
            {
                for (int i = 0; i < _length; i++)
                {
                    if (key == _headers[i].Key)
                        return _headers[i].Value.ToString();
                }
                return "Not Found";
            }
        }
        public int Length
        {
            get
            {
                return _length;
            }
        }
    }
    public static class Http
    {
        #region private
        private enum RequestMethod
        {
            POST, GET, DELETE, PUT
        }

        private static WebRequest _webRequest;
        private static string _parseCookie(object cookies)
        {
            if (cookies == null)
                return "";
            var allCookies = cookies.GetType().GetProperties();
            StringBuilder returnValue = new StringBuilder();
            foreach (var item in allCookies)
            {
                returnValue.Append(string.Format("{0}={1};", item.Name, item.GetValue(cookies)));
            }
            return returnValue.ToString();
        }
        private static string _urlQuery(object properties)
        {
            if (properties == null)
                return "";
            var allProperties = properties.GetType().GetProperties();
            if (properties.GetType().GetProperties().Length < 1)
                return "";
            StringBuilder returnValue = new StringBuilder();
            int len = allProperties.Length;
            int step = 0;
            foreach (var item in allProperties)
            {
                returnValue.Append((step == 0 ? "?" : "") + item.Name + "=" + item.GetValue(properties) + (step < (len - 1) ? "&" : ""));
                step++;
            }
            return returnValue.ToString();
        }

        private static HttpResult _request(string url, object parameters = null, object cookie = null, object headers = null, RequestMethod requestMethod = RequestMethod.POST)
        {
            HttpResult httpResult = null;
            try
            {
                #region Special Checking
                if (requestMethod == RequestMethod.GET && parameters != null)
                {
                    url += _urlQuery(parameters);
                }
                #endregion
                #region Init Object
                _webRequest = WebRequest.Create(url); 
                #endregion
                #region Method
                _webRequest.Method = requestMethod.ToString(); 
                #endregion
                #region Cookies
                if (cookie != null)
                    _webRequest.Headers.Add("cookie", _parseCookie(cookie));
                #endregion
                #region Headers
                if (headers != null)
                {
                    headers.GetType().GetProperties().ToList().ForEach(x =>
                    {
                        if (x.Name.ToLower().Contains("contenttype"))
                            _webRequest.ContentType = x.GetValue(headers).ToString();
                        else
                            _webRequest.Headers.Add(x.Name, x.GetValue(headers).ToString());
                    });
                }
                #endregion
                #region Data
                if (requestMethod != RequestMethod.GET && parameters != null)
                {
                    _webRequest.ContentType = "application/x-www-form-urlencoded";
                    var allValues = parameters.GetType().GetProperties();
                    int lenofParams = allValues.Length;
                    int step = 1;
                    string allparams = allValues.Aggregate<PropertyInfo, string>(null,
                        (current, item) => current + string.Format("{0}={1}{2}",
                        item.Name, item.GetValue(parameters),
                        (step++ == lenofParams) ? "" : "&"));
                    byte[] buffer = Encoding.UTF8.GetBytes(allparams);
                    _webRequest.ContentLength = buffer.Length;
                    using (Stream rStream = _webRequest.GetRequestStream())
                    {
                        rStream.Write(buffer, 0, buffer.Length);
                    }
                }

                #endregion
                #region Finalizing
                HttpWebResponse response = (HttpWebResponse)_webRequest.GetResponse();
                string resultData = null;
                using (StreamReader sR = new StreamReader(response.GetResponseStream()))
                {
                    resultData = sR.ReadToEnd();
                }
                int headersLength = response.Headers.Count;
                httpResult = new HttpResult(resultData)
                {
                    ResponseHeaders = new Header[headersLength]
                };
                string[] allHeadersKeys = response.Headers.AllKeys;
                for (int i = 0; i < headersLength; i++)
                {
                    httpResult.ResponseHeaders[i] = new Header() { Key = allHeadersKeys[i], Value = response.Headers[i] };
                } 
                #endregion
                return httpResult;
            }
            catch (WebException ex)
            {
                StreamReader sr = null;
                httpResult = new HttpResult((ex.Response != null) ? (((HttpWebResponse)ex.Response).StatusCode).ToString() : ex.Status.ToString())
                {

                    ErrorMessage =
                    ((ex.Response != null)
                    ? (sr = new StreamReader(ex.Response.GetResponseStream())).ReadToEnd()
                    : ex.Message),
                    HasError = true,
                    ResponseHeaders = (ex.Response != null) ? new Header[ex.Response.Headers.Count] : null
                };
                //return if not resolved
                if (sr == null)
                    return httpResult;
                sr.Dispose();
                if (httpResult.ResponseHeaders != null)
                {
                    string[] allHeadersKeys = ex.Response.Headers.AllKeys;
                    for (int i = 0; i < ex.Response.Headers.Count; i++)
                    {
                        httpResult.ResponseHeaders[i] =
                            new Header() {Key = allHeadersKeys[i], Value = ex.Response.Headers[i]};
                    }
                }
                return httpResult;
            }
        }
        #endregion
        public static bool ServerCertificateValidation
        {
            set
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate { return value; };
            }
        }
        public static HttpResult Post(string url, object parameters = null, object cookie = null, object headers = null)
        {
            return _request(url, parameters, cookie, headers, RequestMethod.POST);
        }
        public static HttpResult Get(string url, object parameters = null, object cookie = null, object headers = null)
        {
            return _request(url, parameters, cookie, headers, RequestMethod.GET);
        }
        public static HttpResult Delete(string url, object parameters = null, object cookie = null, object headers = null)
        {
            return _request(url, parameters, cookie, headers, RequestMethod.DELETE);
        }
        public static HttpResult Put(string url, object parameters = null, object cookie = null, object headers = null)
        {
            return _request(url, parameters, cookie, headers, RequestMethod.PUT);
        }
        public static string ParseFileDataAsValue(string filename)
        {
            return Convert.ToBase64String(File.ReadAllBytes(filename));
        }
    }
}