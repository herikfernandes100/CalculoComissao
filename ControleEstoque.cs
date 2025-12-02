using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;

class Program
{
    public class Produto
    {
        public int codigoProduto { get; set; }
        public string descricaoProduto { get; set; } = string.Empty;
        public int estoque { get; set; }
    }

    public class Root
    {
        public List<Produto> estoque { get; set; } = new();
    }

    public class Movimentacao
    {
        public int id { get; set; }
        public string descricao { get; set; } = string.Empty;
        public int quantidade { get; set; }
        public int codigoProduto { get; set; }
        public DateTime data { get; set; }
    }

    static void Main()
    {
        string json = @"
        {
            ""estoque"": [
                { ""codigoProduto"": 101, ""descricaoProduto"": ""Caneta Azul"", ""estoque"": 150 },
                { ""codigoProduto"": 102, ""descricaoProduto"": ""Caderno Universitário"", ""estoque"": 75 },
                { ""codigoProduto"": 103, ""descricaoProduto"": ""Borracha Branca"", ""estoque"": 200 },
                { ""codigoProduto"": 104, ""descricaoProduto"": ""Lápis Preto HB"", ""estoque"": 320 },
                { ""codigoProduto"": 105, ""descricaoProduto"": ""Marcador de Texto Amarelo"", ""estoque"": 90 }
            ]
        }";

        Root dados = JsonSerializer.Deserialize<Root>(json)!;
        List<Movimentacao> movimentacoes = new();
        int ultimoId = 1;

        while (true)
        {
            Console.WriteLine("\n===== MOVIMENTAÇÃO DE ESTOQUE =====");
            Console.WriteLine("1 - Realizar movimentação");
            Console.WriteLine("2 - Listar produtos");
            Console.WriteLine("3 - Sair");
            Console.Write("Escolha: ");
            var escolha = Console.ReadLine();

            if (escolha == "3") break;

            if (escolha == "2")
            {
                Console.WriteLine("\nPRODUTOS DISPONÍVEIS:");
                foreach (var p in dados.estoque)
                    Console.WriteLine($"{p.codigoProduto} - {p.descricaoProduto} | Estoque: {p.estoque}");
                continue;
            }

            if (escolha == "1")
            {
                Console.Write("\nDigite o código do produto: ");
                int codigo = int.Parse(Console.ReadLine()!);

                var produto = dados.estoque.FirstOrDefault(p => p.codigoProduto == codigo);

                if (produto == null)
                {
                    Console.WriteLine("❌ Produto não encontrado!");
                    continue;
                }

                Console.WriteLine($"Produto selecionado: {produto.descricaoProduto}");
                Console.Write("Quantidade (positivo = entrada / negativo = saída): ");
                int qtd = int.Parse(Console.ReadLine()!);

                Console.Write("Descrição da movimentação: ");
                string desc = Console.ReadLine()!;

                int novoEstoque = produto.estoque + qtd;

                if (novoEstoque < 0)
                {
                    Console.WriteLine("❌ Estoque insuficiente!");
                    continue;
                }

                produto.estoque = novoEstoque;

                movimentacoes.Add(new Movimentacao
                {
                    id = ultimoId++,
                    descricao = desc,
                    quantidade = qtd,
                    codigoProduto = codigo,
                    data = DateTime.Now
                });

                Console.WriteLine("\n✔ Movimentação registrada com sucesso!");
                Console.WriteLine($"Estoque final do produto ({produto.descricaoProduto}): {produto.estoque}");
            }
        }
    }
}
