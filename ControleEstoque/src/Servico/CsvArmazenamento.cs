using ControleEstoque.src.Modelo;
using System.Text;

namespace ControleEstoque.src.Servico
{
    public class CsvArmazenamento
    {
        private readonly string _path;

        public CsvArmazenamento(string baseDir = @"C:\Users\cunha\Controle-de-estoque--C-\ControleEstoque\data")
        {
            Directory.CreateDirectory(baseDir);
            _path = Path.Combine(baseDir, "produtos.csv");

            // Cria o arquivo com cabeçalho se não existir
            if (!File.Exists(_path))
            {
                File.WriteAllText(_path, "Id;Produto;Categoria;Min;Saldo\n", Encoding.UTF8);
            }
        }

        // Carrega todos os produtos do CSV com recuperação de erros
        public List<Produtos> LoadAll()
        {
            var list = new List<Produtos>();

            var linhas = File.ReadAllLines(_path, Encoding.UTF8).Skip(1);

            foreach (var line in linhas)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var p = line.Split(';');

                int id = 0, min = 0, saldo = 0;
                string nome = "Indefinido", categoria = "Indefinida";

                int.TryParse(p[0], out id);
                if (!string.IsNullOrWhiteSpace(p[1])) nome = p[1];
                if (!string.IsNullOrWhiteSpace(p[2])) categoria = p[2];
                int.TryParse(p[3], out min);
                int.TryParse(p[4], out saldo);

                list.Add(new Produtos(
                    Id: id,
                    Produto: nome,
                    Categoria: categoria,
                    EstoqueMinimo: min,
                    Saldo: saldo
                ));
            }

            return list;
        }

        // Salva todos os produtos em CSV
        public void SaveAll(IEnumerable<Produtos> produtos)
        {
            var tmp = _path + ".tmp";

            using (var w = new StreamWriter(tmp, false, Encoding.UTF8))
            {
                w.WriteLine("Id;Produto;Categoria;Min;Saldo");
                foreach (var c in produtos)
                {
                    w.WriteLine($"{c.Id};{c.Produto};{c.Categoria};{c.EstoqueMinimo};{c.Saldo}");
                }
            }

            if (File.Exists(_path))
                File.Replace(tmp, _path, null);
            else
                File.Move(tmp, _path);
        }

        // Pro método de backup
        public string Backup()
        {
            var backupDir = Path.Combine(Path.GetDirectoryName(_path)!, "backup");
            Directory.CreateDirectory(backupDir);

            var stamp = DateTime.Now.ToString("yyyy_MM_dd_HH-mm-ss");
            var dest = Path.Combine(backupDir, $"estoque_{stamp}.csv");

            File.Copy(_path, dest, true);
            return dest;
        }
    }
}
