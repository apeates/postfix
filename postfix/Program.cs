using System;

internal class Program
{
    static char[] stack = new char[100]; // Yığın, char tipinde olmalı. Int olursa sürekli çevirmek zorunda kalırız.
    static int stackpointer = -1;

    static void Main(string[] args)
    {
        Push('$'); // Yığın başlangıcı için bir sembol. $ en düşük değere sahip char'imiş.
        string operatör = "-+/*"; // Operatörler)
        string infix = Console.ReadLine(); // İnfix girişi
        string postfix = ""; // Postfix sonucu             
        
        for (int i = 0; i < infix.Length; i++)
        {
            char ch = infix[i];
            if (char.IsLetterOrDigit(ch) )
            {
                postfix += ch; // Postfix'e ekle
            }
            else if (ch == '(') // Açık parantez
            {
                Push(ch);
            }
            else if (ch == ')') // Kapalı parantez
            {
                while (stackpointer > 0 && Top() != '(')
                {
                    postfix += Pop(); // Parantez içindeki operatörleri postfix'e ekle
                }
                Pop(); // Açık parantezi yığından çıkar
            }
            else if (operatör.Contains(ch)) // Operatör mü?
            {
                while (stackpointer > 0 && Oncelik(Top()) >= Oncelik(ch))
                {
                    postfix += Pop(); // Daha yüksek öncelikli operatörleri postfix'e ekle
                }
                Push(ch); // Yeni operatörü yığına ekle
            }
        }
        
        while (stackpointer > 0 && stack[stackpointer] != '$') // Kalan operatörleri ekle
        {
            postfix += Pop();
        }
        
        Console.WriteLine("Postfix: " + postfix);
    }

    // Operatör önceliği kontrolü. Bunu char olarak ascıı koduna göre yaptım çalışmıyor. Böyle daha sağlıklı.
    static int Oncelik(char op)
    {
        if (op == '-'||op == '+') return 1; 
        if (op == '*' || op == '/') return 2;
        return 0; // Parantez
    }
     

    static char Top()
    {
        return stack[stackpointer];
    }

    // Yığına eleman ekleme
    static void Push(char data)
    {
        stackpointer++;
        stack[stackpointer] = data;
    }

    // Yığından eleman çıkarma
    static char Pop()
    {
        char value = stack[stackpointer];
        stackpointer--;
        return value;                    
    }
}





