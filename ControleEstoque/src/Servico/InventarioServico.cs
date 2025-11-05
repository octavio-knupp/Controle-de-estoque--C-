//Pasta src: Pasta Servico: Inventario.cs

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

        // Gera o próximo ID automaticamente
        private int NextId()
        {
            var lines = File.ReadAllLines(_path, Encoding.UTF8);
            return lines.Length <= 1 ? 1 : lines.Length;
        }

        // Método para registrar movimentações
        public void RegistrarMovimento(int produtoId, string tipo, int quantidade, string observacao)
        {
            var id = NextId();
            var data = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            var linha = $"{id};{produtoId};{tipo};{quantidade};{data};{observacao}";
            File.AppendAllText(_path, linha + "\n", Encoding.UTF8);
        }
    }
}