using System;
using System.Collections;

ArrayList a = new ArrayList();

a.Add("Hello");
a.Add("Zoo");
a.Add("Bye");
a.Add("Ehab");

Console.WriteLine(a[1]);    

// enhanced for loop

foreach (string i in a)
{
    Console.WriteLine(i);
}
Console.WriteLine(a.Contains("Hello"));

Console.WriteLine("After sorting");
a.Sort();

foreach (string i in a)
{
    Console.WriteLine(i);
}