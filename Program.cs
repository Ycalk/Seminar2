﻿// See https://aka.ms/new-console-template for more information

using System.Threading.Channels;
using Seminar2.Generator;

var generator = new Generator(1);
var counter = 0;

foreach (var el in generator.GenerateExpression())
{
    Console.WriteLine(el);
    counter++;
}

Console.WriteLine(counter);
    



