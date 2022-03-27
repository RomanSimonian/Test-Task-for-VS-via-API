using Nancy.Json;
using RestSharp;
using System.IO;
using System.Threading.Tasks;

namespace Test_Task_for_VS_via_API
{
    public class RegisterForm
    {
        private RestClient restClient;

        public RegisterForm()
        {
            restClient = new RestClient("https://tarasmysko89.wixsite.com/_api/wix-forms/v1/submit-form");
        }

        public async Task<RestResponse> SendRequest<N, E>(N name, E email)
        {
            dynamic body = GetRequestBody(name, email);

            return await MakePostReq(body);
        }

        private async Task<RestResponse> MakePostReq(object body)
        {
            RestRequest request = new RestRequest();
            string token = GetToken();

            request.Method = Method.Post;
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", token);
            request.AddJsonBody(body);

            return await restClient.ExecuteAsync(request);
        }

        private string GetToken()
        {
            string filePath = @"D:\Test-Task-for-VS-via-API\Test-Task-for-VS-via-API\token.txt";

            return File.ReadAllText(filePath).Trim();
        }

        private object GetRequestBody<N, E>(N name, E email)
        {
            string jsonBodyPath = @"D:\Test-Task-for-VS-via-API\Test-Task-for-VS-via-API\form-request.json";
            string stringifiedJsonBody = File.ReadAllText(jsonBodyPath).Trim();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            dynamic jsonObject = serializer.Deserialize<dynamic>(stringifiedJsonBody);

            jsonObject["fields"][0]["firstName"]["value"] = name;
            jsonObject["fields"][1]["email"]["value"] = email;

            return jsonObject;
        }
    }
}
