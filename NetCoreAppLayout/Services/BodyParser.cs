using NetCoreAppLayout.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NetCoreAppLayout.Services
{
    /// <summary>
    /// Herhangi bir class çevrilmesi için generic tipte tanımladık.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public static class BodyParser<TModel>
    {
        public async static Task<TModel> ParseAsync(Stream requestBody, string contentType)
        {

            using (StreamReader stream = new StreamReader(requestBody))
            {
                string body =  await stream.ReadToEndAsync();

                if (contentType == RequestContentType.FormData)
                {
                    // UserName=ali&Email=test%40.test.com.
                    List<string> bodyParams = body.Split("&").ToList();

                    string objectStart = "{";
                    string objectEnd = "}";

                    string keyValuePair = string.Empty;

                    for (int i = 0; i < bodyParams.Count; i++)
                    {
                        bodyParams[i] = HttpUtility.UrlDecode(bodyParams[i]);

                        int equalIndex = bodyParams[i].IndexOf('=');
                        string propertyName = bodyParams[i].Substring(0, equalIndex);

                        int totalLength = bodyParams[i].Length - 1;
                        int charCount = totalLength - equalIndex;

                        string propertValue = bodyParams[i].Substring(equalIndex + 1, charCount);

                        // {Email:ali@test.com,UserName:ali

                        if ((bodyParams.Count - 1) == i) // son değer ise i o zaman , atmayız
                        {
                            keyValuePair += $"\"{propertyName}\":\"{propertValue}\"";
                        }
                        else
                        {
                            keyValuePair += $"\"{propertyName}\":\"{propertValue}\",";
                        }

                    }

                    // objectStart = {  keyValuePair = Email:ali@test.com,UserName:ali objectEnd = }
                    string jsonString = $"{objectStart} {keyValuePair}  {objectEnd}";
                    var response = System.Text.Json.JsonSerializer.Deserialize<TModel>(jsonString);

                    return await Task.FromResult(response);
                }
                else if (contentType == RequestContentType.JSON)
                {
                    var stringJson = System.Text.Json.JsonSerializer.Deserialize<TModel>(body);

                    return await Task.FromResult(stringJson);
                }


            }

            // içiboş bir instance üret.
            return await Task.FromResult(default(TModel));

        }
    }
}
