using ControleEstoque.src.Modelo;
using System.Text;

namespace ControleEstoque.src.Servico
{
    public class InventarioServico
    {
        private readonly string _path;

        public InventarioServico(string baseDir = "../../data")
        {
            // Garante que a pasta 'data' exista na raiz do projeto
            var fullPath = Path.GetFullPath(baseDir);
            Directory.CreateDirectory(fullPath);

            // Monta o caminho completo do arquivo CSV
            _path = Path.Combine(fullPath, "movimentos.csv");

            // Cria o arquivo com cabeçalho se ainda não existir
            if (!File.Exists(_path))
            {
                File.WriteAllText(_path, "Id;ProdutoId;Tipo;Quantidade;Data;Observacao\n", Encoding.UTF8);
            }
        }


        private int NextId()
        {
            var lines = File.ReadAllLines(_path, Encoding.UTF8)
                .Skip(1) // ignora cabeçalho
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .ToList();

            if (!lines.Any()) return 1;

            var lastId = int.Parse(lines.Last().Split(';')[0]);
            return lastId + 1;
        }

        // REGISTRA movimentação (entrada ou saída) e altera o saldo do produto
        public void Movimentar(List<Produtos> produtos, int produtoId, string Tipo, int quantidade, string observacao)
        {
            var produto = produtos.FirstOrDefault(p => p.Id == produtoId);
            if (produto.Id == 0)
                throw new Exception("Produto não encontrado.");

            if (quantidade <= 0)
                throw new Exception("Quantidade inválida.");

            if (Tipo.ToUpper() == "ENTRADA")
                produto = produto with { Saldo = produto.Saldo + quantidade };
            else if (Tipo.ToUpper() == "SAIDA")
            {
                if (produto.Saldo < quantidade)
                    throw new Exception("Saldo insuficiente para saída.");
                produto = produto with { Saldo = produto.Saldo - quantidade };
            }
            else
                throw new Exception("Tipo de movimento inválido (use ENTRADA ou SAÍDA).");

            // Atualiza na lista de estoque
            var idx = produtos.FindIndex(p => p.Id == produtoId);
            produtos[idx] = produto;

            // Registra no CSV de movimentos
            RegistrarMovimento(produtoId, Tipo, quantidade, observacao);
        }

        // Apenas grava a linha no CSV
        public void RegistrarMovimento(int produtoId, string Tipo, int quantidade, string observacao)
        {
            var id = NextId();
            var data = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var linha = $"{id};{produtoId};{Tipo};{quantidade};{data};{observacao}";
            File.AppendAllText(_path, linha + "\n", Encoding.UTF8);
        }

        // Retorna o extrato de movimentações de um produto específico (ordenado por data)
        public List<Movimentos> ExtratoPorProduto(int produtoId)
        {
            if (!File.Exists(_path))
                return new List<Movimentos>();

            var linhas = File.ReadAllLines(_path, Encoding.UTF8)
                .Skip(1) // ignora cabeçalho
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

            return linhas;
        }


    }
}
