using SoapServiceClient;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace SoapClient
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            SampleServiceClient client = new SampleServiceClient();
            using (new OperationContextScope(client.InnerChannel))
            {
                // We will use a custom class called UserInfo to be passed in as a MessageHeader
                string bearerToken = "Bearer 23y7289387893728938792309023092";
                // Add a SOAP Header to an outgoing request
                MessageHeader aMessageHeader = MessageHeader.CreateHeader("Authorization", "http://tempuri.org", bearerToken);
                OperationContext.Current.OutgoingMessageHeaders.Add(aMessageHeader);

                // Add a HTTP Header to an outgoing request
                HttpRequestMessageProperty requestMessage = new HttpRequestMessageProperty();
                requestMessage.Headers["Authorization"] = "Bearer 23y7289387893728938792309023092";
                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestMessage;


                var result = await client.PingAsync("Hello");
               
            }
        }
    }
}
