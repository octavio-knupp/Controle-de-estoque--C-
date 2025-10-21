using System.Text;
using ControleEstoque.src.Modelo;
using System.Threading;

namespace ControleEstoque.src.Servico;

public class FileStorage
{
    private readonly string _path;
    public FileStorage(string baseDir = "data")
    {
        Directory.CreateDirectory(baseDir);
        _path = Path.Combine(baseDir, "contatos.csv");
        if (!File.Exists(_path))
        {
            File.WriteAllText(_path, "Id;Produto;Categoria;\n", Encoding.UTF8);
        }
    }

    public List<Contato> LoadAll()
    {
        var list = new List<Contato>();
        foreach (var line in File.ReadAllLines(_path, Encoding.UTF8).Skip(1))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var p = line.Split(';');
            list.Add(new Contato(
                Id: int.Parse(p[0]),
                Produto: p[1],
                Categoria: p[2]
            ));
        }
        return list;
    }

    public void SaveAll(IEnumerable<Contato> contato)
    {
        var tmp = _path + ".tmp";
        var maxTentativas = 3;

        for (int tentativa = 1; tentativa <= maxTentativas; tentativa++)
        {
            try
            {
                // Cria arquivo temporário
                using (var w = new StreamWriter(tmp, false, Encoding.UTF8))
                {
                    w.WriteLine("id;produto;categoria");
                    foreach (var c in contato)
                    {
                        w.WriteLine($"{c.Id};{c.Produto};{c.Categoria}");
                    }
                }

                // Substitui o arquivo original
                if (File.Exists(_path))
                    File.Replace(tmp, _path, null);
                else
                    File.Move(tmp, _path);

                // Se chegou aqui, salvou com sucesso
                return;
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"Tentativa {tentativa}: Erro ao salvar - {ioEx.Message}");

                // Se for a última tentativa, lança o erro para o catch principal
                if (tentativa == maxTentativas)
                {
                    throw new IOException("Não foi possível salvar o arquivo após várias tentativas. Verifique se ele está aberto em outro programa.", ioEx);
                }

                // Espera 1 segundo antes de tentar novamente
                Thread.Sleep(1000);
            }
        }
    }


    public string Backup()
    {
        var backupDir = Path.Combine(Path.GetDirectoryName(_path)!, "backup");
        Directory.CreateDirectory(backupDir);
        var stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        var dest = Path.Combine(backupDir, $"contatos_{stamp}.csv");
        File.Copy(_path, dest, true);
        return dest;
    }
}