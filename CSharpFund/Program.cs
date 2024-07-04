using System.Diagnostics;

namespace CSharpFund
{
    class Program :  Program4
    {
        //field -- globally
        string name;
        string fName;
        string lName;
        // constructor is a method called once run the program
        // default constructor
        public Program(string name) 
        { 
            this.name = name;
        }

        // constructor 
        public Program(string firstName, string lastName)
        {
            this.fName= firstName;
            this.lName= lastName;
        }
        public void getName()
        {
            System.Console.WriteLine("My name is" +" " + this.fName + this.lName);
        }

        public void getData()
        {
            Console.WriteLine("Hello From void Method");
        }
        static void Main(string[] args)
        {
            // Name of the class 
            Program P = new Program("Ehab", "Khallaf");
            P.getData();
            P.getName();

            // inheritance
            P.setData();
            P.XYZ();
           

            // create object form program 4
            Program4 P4 = new Program4();
            P4.setData();
            
            Console.WriteLine("Hello, World!");

            int a = 4;
            Console.WriteLine(a + 5);

            string name = "Ehab";
            Console.WriteLine(name);

            Console.WriteLine($"Name is {name}");

            var age = 25;
            System.Console.WriteLine(age);

            dynamic height = 13.2;
            Console.WriteLine(height);
            height = "Hoba";
            Console.WriteLine(height);
        }
    }
}