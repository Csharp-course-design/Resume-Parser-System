using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace FuncTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string json = File.ReadAllText("D:\\______cloneFrom_GitHub\\Resume-Parser-System\\FuncTest\\ForFactoryTest.json");
            dynamic data = JsonConvert.DeserializeObject(json);
            Console.WriteLine("Name: " + data.name);
            Console.WriteLine("Age: " + data.age);
            Console.WriteLine("City: " + data.city);
        }
    }
}
