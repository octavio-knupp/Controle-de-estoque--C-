using ControleEstoque.src.Modelo;
using System.Text;

namespace ControleEstoque.src.Servico
{
    public class CsvArmazenamento
    {
        private readonly string _path;

        //SEM caminho fixo — Program.cs escolhe a pasta
        public CsvArmazenamento(string baseDir)
        {
            // Garante que a pasta existe
            Directory.CreateDirectory(baseDir);

            // Caminho do arquivo CSV dentro da pasta escolhida
            _path = Path.Combine(baseDir, "produtos.csv");

            // Se não existe, cria com cabeçalho
            if (!File.Exists(_path))
            {
                File.WriteAllText(_path, "Id;Produto;Categoria;Min;Saldo\n", Encoding.UTF8);
            }
        }

        // Carrega todos os produtos do CSV
        public List<Produtos> LoadAll()
        {
            var list = new List<Produtos>();

            if (!File.Exists(_path))
                return list;

            var linhas = File.ReadAllLines(_path, Encoding.UTF8).Skip(1);

            foreach (var line in linhas)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var p = line.Split(';');

                int id = 0, min = 0, saldo = 0;
                string nome = "", categoria = "";

                int.TryParse(p[0], out id);
                nome = p[1];
                categoria = p[2];
                int.TryParse(p[3], out min);
                int.TryParse(p[4], out saldo);

                list.Add(new Produtos(id, nome, categoria, min, saldo));
            }

            return list;
        }

        // Salva a lista completa no CSV
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

            // Troca arquivo antigo pelo novo
            if (File.Exists(_path))
                File.Replace(tmp, _path, null);
            else
                File.Move(tmp, _path);
        }

        // Backup opcional
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
