using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections.Specialized;


public class Telegram : MonoBehaviour
{
    public string chat_id = "0000000"; // ID (you can know your id via @userinfobot)
    public string TOKEN = "00000:aaaaaa"; // bot token (@BotFather)
    public string urlParse = "";
    public TextMeshProUGUI txtDebugs;
    private void Start()
    {
#if UNITY_EDITOR
        return;
#endif

        var uri = new System.Uri(Application.absoluteURL);
        //string _url = uri.AbsoluteUri;
        // Debug.LogError("The domain name is: " + _url);
        //urlParse = urlParse.Replace("%", ":");
        //var uri = new System.Uri(urlParse);
        //Debug.LogError( GetUsernameFromURL(urlParse));

        //string formattedUrl = FormatUrl(urlParse);
        ////Debug.LogError(ComputeSha256Hash(urlParse)); 
        //formattedUrl = formattedUrl.Replace("%22%2C%22", ",");
        //Debug.LogError(formattedUrl);

        urlParse = uri.AbsoluteUri;

        string decode1 = UrlDecode(urlParse);

        //Debug.LogError(decode1);

        string decode2 = FormatUrl(decode1);
        Debug.LogError(decode2);

        

        Dictionary<string, string> keyValuePairs = ParseURL(decode2);
        foreach (KeyValuePair<string, string> pair in keyValuePairs)
        {
            //Debug.LogError(pair.Key + ": " + pair.Value);
            txtDebugs.text += pair.Key + ": " + pair.Value +"\n";
        }

       

    }
    public Dictionary<string, string> ParseURL(string url)
    {
        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

        // Tách chuỗi URL thành các phần bằng dấu &
        string[] parts = url.Split('&');

        foreach (string part in parts)
        {
            // Tách mỗi phần thành key và value bằng dấu =
            string[] pair = part.Split('=');
            string key = pair[0];
            string value = pair[1];

            // Thêm các cặp key và value vào Dictionary
            keyValuePairs[key] = value;
        }

        return keyValuePairs;
    }

    public static string UrlDecode(string input)
    {
        return Uri.UnescapeDataString(input);
    }


    string GetValueFromQueryString(string queryString, string key)
    {
        int index = queryString.IndexOf(key + "=");
        if (index >= 0)
        {
            index += key.Length + 1;
            int endIndex = queryString.IndexOf("&", index);
            if (endIndex == -1)
            {
                endIndex = queryString.Length;
            }
            // Kiểm tra nếu vị trí bắt đầu và kết thúc nằm trong giới hạn của chuỗi
            if (index < queryString.Length && endIndex <= queryString.Length)
            {
                return queryString.Substring(index, endIndex - index);
            }
        }
        return null;
    }
    string FormatUrl(string originalUrl)
    {
        try
        {
            // Tách phần query string và fragment
            string[] parts = originalUrl.Split(new char[] { '#' }, 2);
            string queryString = parts[1];
           
            // Giải mã query string
            string decodedQueryString = HttpUtility.UrlDecode(queryString);

            // Trả về URL đã được format lại
            return parts[0] + "#" + decodedQueryString;
        }
        catch (Exception ex)
        {
            // Xử lý nếu có lỗi xảy ra
            Debug.LogError("An error occurred: " + ex.Message);
            return null;
        }
    }

    public string GetUsernameFromURL(string url)
    {
        try
        {
            // Tạo đối tượng Uri từ URL đã cho
            Uri uri = new Uri(url);

            // Lấy query string từ URL
            string queryString = uri.Query;

            // Kiểm tra xem query string có chứa thông tin về người dùng không
            if (queryString.Contains("user"))
            {
                // Tách chuỗi query thành các cặp key-value
                string[] queryParams = queryString.TrimStart('?').Split('&');

                // Duyệt qua các cặp key-value để tìm username
                foreach (string param in queryParams)
                {
                    string[] keyValue = param.Split('=');
                    if (keyValue.Length == 2 && keyValue[0] == "user")
                    {
                        // Decode giá trị của key "user" để lấy username
                        string userValue = Uri.UnescapeDataString(keyValue[1]);

                        // Tìm và trả về giá trị của key "username"
                        int startIndex = userValue.IndexOf("\"username\"") + "\"username\"".Length + 3;
                        int endIndex = userValue.IndexOf("\"", startIndex);
                        string username = userValue.Substring(startIndex, endIndex - startIndex);
                        return username;
                    }
                }
            }
        }
        catch (UriFormatException ex)
        {
            // Xử lý nếu URL không hợp lệ
            Console.WriteLine("Invalid URL: " + ex.Message);
        }
        catch (Exception ex)
        {
            // Xử lý ngoại lệ khác nếu có
            Console.WriteLine("An error occurred: " + ex.Message);
        }

        // Trả về null nếu không tìm thấy username hoặc có lỗi xảy ra
        return null;
    }
   

    public string API_URL
    {
        get
        {
            return string.Format("https://api.telegram.org/bot{0}/", TOKEN);
        }
    }

    public void GetMe()
    {
        WWWForm form = new WWWForm();
        UnityWebRequest www = UnityWebRequest.Post(API_URL + "getMe", form);
        StartCoroutine(SendRequest(www));
    }

    public void SendFile(byte[] bytes, string filename, string caption = "")
    {
        WWWForm form = new WWWForm();
        form.AddField("chat_id", chat_id);
        form.AddField("caption", caption);
        form.AddBinaryData("document", bytes, filename, "filename");
        UnityWebRequest www = UnityWebRequest.Post(API_URL + "sendDocument?", form);
        StartCoroutine(SendRequest(www));
    }

    public void SendPhoto(byte[] bytes, string filename, string caption = "")
    {
        WWWForm form = new WWWForm();
        form.AddField("chat_id", chat_id);
        form.AddField("caption", caption);
        form.AddBinaryData("photo", bytes, filename, "filename");
        UnityWebRequest www = UnityWebRequest.Post(API_URL + "sendPhoto?", form);
        StartCoroutine(SendRequest(www));
    }

    public new void SendMessage(string text)
    {
        WWWForm form = new WWWForm();
        form.AddField("chat_id", chat_id);
        form.AddField("text", text);
        UnityWebRequest www = UnityWebRequest.Post(API_URL + "sendMessage?", form);
        StartCoroutine(SendRequest(www));
    }

    IEnumerator SendRequest(UnityWebRequest www)
    {
        yield return www.SendWebRequest();
        if (www.isNetworkError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            var w = www;
            Debug.LogError("Success!\n" + www.downloadHandler.text);
        }
    }

  

}