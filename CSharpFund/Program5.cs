using System;

string[] a = {"a",  "b", "c"};
int[] b = {1,2,3};

string[] a1 = new string[4];
a1[0] = "Hello";
a1[1] = "Bye";

Console.WriteLine(a1[0]);

for (int i = 0; i < a.Length; i++)
{
    Console.WriteLine(a[i]);

    if (a[i] == "b")
    {
        Console.WriteLine(true);
        break;
    }
    else { Console.WriteLine(false); }
}
