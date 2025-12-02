using System;

class Program
{
    static void Main()
    {
        Console.Write("Digite o valor original: R$ ");
        decimal valorOriginal = decimal.Parse(Console.ReadLine());

        Console.Write("Digite a data de vencimento (dd/mm/aaaa): ");
        DateTime vencimento = DateTime.Parse(Console.ReadLine());

        DateTime hoje = DateTime.Today;

        // Calcula dias de atraso
        int diasAtraso = (hoje - vencimento).Days;

        // Se não estiver atrasado, dias = 0
        if (diasAtraso < 0)
            diasAtraso = 0;

        // Multa diária de 2,5% por dia
        decimal multa = valorOriginal * 0.025m * diasAtraso;

        decimal valorFinal = valorOriginal + multa;

        Console.WriteLine("\n=== RESULTADO ===");
        Console.WriteLine($"Dias de atraso: {diasAtraso}");
        Console.WriteLine($"Valor da multa: R$ {multa:F2}");
        Console.WriteLine($"Valor final com juros: R$ {valorFinal:F2}");
    }
}
