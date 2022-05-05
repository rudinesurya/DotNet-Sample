using NSwag;
using NSwag.CodeGeneration.CSharp;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace NSwagClientGenerator
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Generating client...");
            await ClientGenerator.GenerateCSharpClient();
            Console.WriteLine("Generated client.");
        }
    }

    public static class ClientGenerator
    {
        static string className = "GeneratedClient";
        static string apiNamespace = "MyClient";

        public async static Task GenerateCSharpClient() =>
            GenerateClient(
                document: await GetDocumentFromFile("mynswag.json"),
                generatedLocation: "GeneratedClient.cs",
                generateCode: (OpenApiDocument document) =>
                {
                    var settings = new CSharpClientGeneratorSettings
                    {
                        ClassName = className,
                        CSharpGeneratorSettings =
                        {
                            Namespace = apiNamespace
                        }
                    };

                    var generator = new CSharpClientGenerator(document, settings);
                    var code = generator.GenerateFile();
                    return code;
                }
            );

        private static void GenerateClient(OpenApiDocument document, string generatedLocation, Func<OpenApiDocument, string> generateCode)
        {
            var root = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var location = Path.GetFullPath(Path.Combine(root, @"../../", generatedLocation));

            Console.WriteLine($"Generating {location}...");

            var code = generateCode(document);

            System.IO.File.WriteAllText(location, code);
        }

        private static async Task<OpenApiDocument> GetDocumentFromFile(string swaggerJsonFilePath)
        {
            var root = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var swaggerJson = File.ReadAllText(Path.GetFullPath(Path.Combine(root, @"../../", swaggerJsonFilePath)));
            var document = await OpenApiDocument.FromJsonAsync(swaggerJson);

            return document;
        }
    }
}
