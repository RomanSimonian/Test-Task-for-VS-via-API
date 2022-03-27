using NUnit.Framework;
using RestSharp;
using System.Net;
using System.Threading.Tasks;
namespace Test_Task_for_VS_via_API
{
    [TestFixture]
    public class UnitTest1
    {
        [TestCase("User4", "testmail@gmail.com", HttpStatusCode.OK)]
        [TestCase(123, "testmail@gmail.com", HttpStatusCode.BadRequest)]
        [TestCase("User4", 1234, HttpStatusCode.BadRequest)]
        public async Task RegisterRestApiTest<N, E>(N userName, E email, HttpStatusCode expectedResult)
        {
            RegisterForm registerForm = new RegisterForm();

            RestResponse reqResult = await registerForm.SendRequest(userName, email);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedResult, reqResult.StatusCode);
        }
    }
}
