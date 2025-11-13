using ControleEstoque.src.Modelo;
using System.Text;

namespace ControleEstoque.src.Servico
{
    public class CsvArmazenamento
    {
        private readonly string _path;

        public CsvArmazenamento(string baseDir = "data")
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

            // Lê todas as linhas e ignora o cabeçalho
            var linhas = File.ReadAllLines(_path, Encoding.UTF8).Skip(1);

            foreach (var line in linhas)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var p = line.Split(';');

                // Inicializa valores padrão
                int id = 0, min = 0, saldo = 0;
                string nome = "Indefinido", categoria = "Indefinida";

                // Recupera os dados, mesmo se alguma coluna estiver inválida
                if (p.Length > 0 && int.TryParse(p[0], out int tmpId)) id = tmpId;
                if (p.Length > 1 && !string.IsNullOrWhiteSpace(p[1])) nome = p[1];
                if (p.Length > 2 && !string.IsNullOrWhiteSpace(p[2])) categoria = p[2];
                if (p.Length > 3 && int.TryParse(p[3], out int tmpMin)) min = tmpMin;
                if (p.Length > 4 && int.TryParse(p[4], out int tmpSaldo)) saldo = tmpSaldo;

                // Adiciona o produto mesmo que incompleto
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

        // Salva todos os produtos em CSV com sistema de tentativas
        public void SaveAll(IEnumerable<Produtos> produtos)
        {
            var tmp = _path + ".tmp";
            var maxTentativas = 3;

            for (int tentativa = 1; tentativa <= maxTentativas; tentativa++)
            {
                try
                {
                    using (var w = new StreamWriter(tmp, false, Encoding.UTF8))
                    {
                        w.WriteLine("Id;Produto;Categoria;Min;Saldo");
                        foreach (var c in produtos)
                        {
                            w.WriteLine($"{c.Id};{c.Produto};{c.Categoria};{c.EstoqueMinimo};{c.Saldo}");
                        }
                    }

                    // Substitui o arquivo original de forma segura
                    if (File.Exists(_path))
                        File.Replace(tmp, _path, null);
                    else
                        File.Move(tmp, _path);

                    return; // Sucesso
                }
                catch (IOException ioEx)
                {
                    Console.WriteLine($"⚠️ Tentativa {tentativa} falhou ao salvar: {ioEx.Message}");

                    if (tentativa == maxTentativas)
                        throw new IOException("❌ Não foi possível salvar após várias tentativas. Feche o arquivo CSV e tente novamente.", ioEx);

                    Thread.Sleep(1000); // Espera antes de tentar novamente
                }
            }
        }

        // Cria um backup automático do arquivo CSV
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
