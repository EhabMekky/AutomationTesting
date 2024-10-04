using System.Runtime.InteropServices.JavaScript;
using Newtonsoft.Json.Linq;

namespace CSharpSelFramework.Tests;

public class JsonReader
{
    public string ExtractData(String tokenName)
    {
       var myJson = File.ReadAllText("Utilties/TestData.json");
       var jsonObj = JToken.Parse(myJson);
       return jsonObj.SelectToken(tokenName).Value<string>();
    }

    public IEnumerable<string?> ExtractDataArray(String tokenName)
    {
        var myJson = File.ReadAllText("Utilties/TestData.json");
        var jsonObj = JToken.Parse(myJson);
        List<String> list = jsonObj.SelectTokens(tokenName).Values<string>().ToList();
        return list.ToArray();

    }
}