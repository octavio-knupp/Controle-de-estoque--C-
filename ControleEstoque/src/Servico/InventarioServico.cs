using ControleEstoque.src.Modelo;
using System.Text;

namespace ControleEstoque.src.Servico
{
    public class InventarioServico
    {
        private readonly string _path;
        private readonly string _baseDir = @"C:\Users\cunha\Controle-de-estoque--C-\ControleEstoque\data";

        public InventarioServico()
        {
            Directory.CreateDirectory(_baseDir);

            _path = Path.Combine(_baseDir, "movimentos.csv");

            if (!File.Exists(_path))
            {
                File.WriteAllText(_path, "Id;ProdutoId;Tipo;Quantidade;Data;Observacao\n", Encoding.UTF8);
            }
        }

        private int NextId()
        {
            var lines = File.ReadAllLines(_path, Encoding.UTF8)
                .Skip(1)
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .ToList();

            if (!lines.Any()) return 1;

            var lastId = int.Parse(lines.Last().Split(';')[0]);
            return lastId + 1;
        }

        // REGISTRA movimentação + atualiza o CSV de produtos
        public void Movimentar(List<Produtos> produtos, int produtoId, string Tipo, int quantidade, string observacao)
        {
            var produto = produtos.FirstOrDefault(p => p.Id == produtoId);
            if (produto.Id == 0)
                throw new Exception("Produto não encontrado.");

            if (quantidade <= 0)
                throw new Exception("Quantidade inválida.");

            if (Tipo.ToUpper() == "ENTRADA")
            {
                produto = produto with { Saldo = produto.Saldo + quantidade };
            }
            else if (Tipo.ToUpper() == "SAIDA")
            {
                if (produto.Saldo < quantidade)
                    throw new Exception("Saldo insuficiente para saída.");

                produto = produto with { Saldo = produto.Saldo - quantidade };
            }
            else
            {
                throw new Exception("Tipo inválido (use ENTRADA ou SAÍDA).");
            }

            // Atualiza o produto na lista
            var idx = produtos.FindIndex(p => p.Id == produtoId);
            produtos[idx] = produto;

            // 🔥 SALVA NO CSV DE PRODUTOS
            var repo = new CsvArmazenamento(_baseDir);
            repo.SaveAll(produtos);

            // SALVA NO MOVIMENTOS.CSV
            RegistrarMovimento(produtoId, Tipo, quantidade, observacao);
        }

        // Apenas grava a linha
        private void RegistrarMovimento(int produtoId, string Tipo, int quantidade, string observacao)
        {
            var id = NextId();
            var data = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var linha = $"{id};{produtoId};{Tipo};{quantidade};{data};{observacao}";
            File.AppendAllText(_path, linha + "\n", Encoding.UTF8);
        }

        public List<Movimentos> ExtratoPorProduto(int produtoId)
        {
            if (!File.Exists(_path))
                return new List<Movimentos>();

            return File.ReadAllLines(_path, Encoding.UTF8)
                .Skip(1)
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l =>
                {
                    var p = l.Split(';');
                    return new Movimentos
                    {
                        Id = int.Parse(p[0]),
                        ProdutoId = int.Parse(p[1]),
                        Tipo = p[2],
                        Quantidade = int.Parse(p[3]),
                        Data = DateTime.Parse(p[4]),
                        Observacao = p.Length > 5 ? p[5] : ""
                    };
                })
                .Where(m => m.ProdutoId == produtoId)
                .OrderBy(m => m.Data)
                .ToList();
        }
    }
}
