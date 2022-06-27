﻿using Microsoft.Extensions.Configuration;
using WebStore.WebAPI.Clients.Products;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var http = new HttpClient
{
    BaseAddress = new(configuration["WebAPI"])
};
var products_client = new ProductsClient(http);

Console.WriteLine("Ожидание запуска WebAPI, нажмите Enter для продолжения");
Console.ReadLine();

foreach (var product in products_client.GetProducts())
{
    Console.WriteLine("[{0}] {1}", product.Id, product.Name);
}

Console.ReadLine();