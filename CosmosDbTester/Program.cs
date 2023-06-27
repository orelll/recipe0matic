// See https://aka.ms/new-console-template for more information

using CosmosDbTester;
using Microsoft.Azure.Cosmos;

Console.WriteLine("Hello, World!");
var reader = new DataReader();

var partitionKey = "TestId";

try
{

    reader.Init();
    var container = reader.Container("TestContainer", "TestDB");

    var testItem = new TestClassB
    {
        Id = Guid.NewGuid().ToString(),
        TestId = Guid.NewGuid().ToString(),
        Dupa = "DUPA xD",
        TestInt = 2137,
        TestBool = true
    };
    var result = await container.CreateItemAsync(testItem, new PartitionKey(testItem.TestId));

    var foundA = await container.ReadItemAsync<TestClassA>(testItem.Id, new PartitionKey(testItem.TestId));
    var foundB = await container.ReadItemAsync<TestClassB>(testItem.Id, new PartitionKey(testItem.TestId));
    var foundC = await container.ReadItemAsync<TestClassC>(testItem.Id, new PartitionKey(testItem.TestId));
}
catch (Exception e)
{
    Console.WriteLine(e);
}


Console.ReadKey();
Console.WriteLine("End!");
