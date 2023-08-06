using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Azure.AI.OpenAI;
using Azure;
using DotNetEnv;

class Program
{
    static async Task Main(string[] args)
    {
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:8080/");
        listener.Start();
        Console.WriteLine("Listening...");

        while (true)
        {
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            if (request.HttpMethod == "GET")
            {
                string filename = "rastastream.html";
                string responseString = System.IO.File.ReadAllText(filename);
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.ContentType = "text/html";
            }
            else if (request.HttpMethod == "POST")
            {
                Env.Load();
                var OPENAI_API_KEY = Environment.GetEnvironmentVariable("OPENAI_API_KEY"); // "f6d55158307d4baa84251390a01e9f1c";
                var OPENAI_ENDPOINT = Environment.GetEnvironmentVariable("OPENAI_ENDPOINT"); // "https://ebergerfta-use-openai.openai.azure.com/";
                var DEPLOYMENT_NAME = Environment.GetEnvironmentVariable("DEPLOYMENT_NAME"); // "gpt-35-turbo";

                var openAIClient = new OpenAIClient(
                    new Uri(OPENAI_ENDPOINT),
                    new AzureKeyCredential(OPENAI_API_KEY)
                );

                string requestBody = new System.IO.StreamReader(request.InputStream).ReadToEnd();

                var chatCompletionsOptions = new ChatCompletionsOptions
                {
                    Messages =
                    {
                        new ChatMessage(ChatRole.System, "You are a Rastafari poet. All your responses are rhyming rastafari poetry."),
                        new ChatMessage(ChatRole.User, requestBody),
                    }
                };

                var chatCompletionsResponse = await openAIClient.GetChatCompletionsStreamingAsync(
                    DEPLOYMENT_NAME,
                    chatCompletionsOptions
                );

                await foreach (var chatChoice in chatCompletionsResponse.Value.GetChoicesStreaming())
                {
                    await foreach (var chatMessage in chatChoice.GetMessageStreaming())
                    {
                        if (chatMessage.Content != null) {
                            byte[] buffer = Encoding.UTF8.GetBytes(chatMessage.Content);
                            response.OutputStream.Write(buffer, 0, buffer.Length);
                        }
                    }
                }
            }

            response.Close();
        }
    }
}
