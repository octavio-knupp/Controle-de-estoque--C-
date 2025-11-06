using ControleEstoque.src.Modelo;
using System.Text;

namespace ControleEstoque.src.Servico
{
    public class InventarioServico
    {
        private readonly string _path;

        public InventarioServico(string baseDir = "data")
        {
            Directory.CreateDirectory(baseDir);
            _path = Path.Combine(baseDir, "movimentos.csv");

            if (!File.Exists(_path))
            {
                File.WriteAllText(_path, "Id;ProdutoId;Tipo;Quantidade;Data;Observacao\n", Encoding.UTF8);
            }
        }

        private int NextId()
        {
            var lines = File.ReadAllLines(_path, Encoding.UTF8);
            return lines.Length <= 1 ? 1 : lines.Length;
        }

        // REGISTRA movimentação (entrada ou saída) e altera o saldo do produto
        public void Movimentar(List<Produtos> estoque, int produtoId, string tipo, int quantidade, string observacao)
        {
            var produto = estoque.FirstOrDefault(p => p.Id == produtoId);
            if (produto.Id == 0)
                throw new Exception("Produto não encontrado.");

            if (quantidade <= 0)
                throw new Exception("Quantidade inválida.");

            if (tipo.ToUpper() == "ENTRADA")
                produto = produto with { Saldo = produto.Saldo + quantidade };
            else if (tipo.ToUpper() == "SAIDA")
            {
                if (produto.Saldo < quantidade)
                    throw new Exception("Saldo insuficiente para saída.");
                produto = produto with { Saldo = produto.Saldo - quantidade };
            }
            else
                throw new Exception("Tipo de movimento inválido (use ENTRADA ou SAIDA).");

            // Atualiza na lista de estoque
            var idx = estoque.FindIndex(p => p.Id == produtoId);
            estoque[idx] = produto;

            // Registra no CSV de movimentos
            RegistrarMovimento(produtoId, tipo, quantidade, observacao);
        }

        // Apenas grava a linha no CSV
        public void RegistrarMovimento(int produtoId, string tipo, int quantidade, string observacao)
        {
            var id = NextId();
            var data = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            var linha = $"{id};{produtoId};{tipo};{quantidade};{data};{observacao}";
            File.AppendAllText(_path, linha + "\n", Encoding.UTF8);
        }
    }
}
