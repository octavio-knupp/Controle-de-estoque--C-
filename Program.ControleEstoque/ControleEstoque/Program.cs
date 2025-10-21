using ControleEstoque.src.Modelo;
using ControleEstoque.src.Servico;


Console.OutputEncoding = System.Text.Encoding.UTF8;

var storage = new FileStorage("data");
var contatos = storage.LoadAll();

int NextId() => contatos.Any() ? contatos.Max(c => c.Id) + 1 : 1;

while (true)
{
    Console.Clear();
    Funcao.txt("");
    Funcao.txt("==== Controle de Estoque ====");
    Funcao.txt("1 - Cadastrar Produto");
    Funcao.txt("2 - Listar Produtos");
    Funcao.txt("3 - Buscar Produtos");
    Funcao.txt("4 - Atualizar Cadastro");
    Funcao.txt("5 - Excluir Produto");
    Funcao.txt("6 - Salvar Cadastro");
    Funcao.txt("7 - Backup Geral");
    Funcao.txt("8 - Sair");
    Funcao.txt("");
    Funcao.txt("Integrantes:");
    Funcao.txt("");
    Funcao.txt("Octavio Henrique Knupp Lucio");
    Funcao.txt("Alexandre Aielo Lima");
    Funcao.txt("Nícolas Joly Mussi");
    Funcao.txt("Eduardo da Cunha");
    var op = Console.ReadLine();

    try
    {
        switch (op)
        {
            case "1":
                Funcao.txt("Produto: "); var prod = Console.ReadLine() ?? "";
                Funcao.txt("Categoria: "); var cat = Console.ReadLine() ?? "";
                if (string.IsNullOrWhiteSpace(prod))
                {
                    Funcao.txt("Nome é obrigatório.");
                    break;
                }
                var novo = new Contato(NextId(), prod.Trim(), cat.Trim());
                contatos.Add(novo);
                Funcao.txt($"Produto criado: {novo.Id}");
                Console.ReadKey();
                break;

            case "2":
                if (!contatos.Any())
                {
                    Funcao.txt("Sem produtos."); Console.ReadKey();
                    break;
                }
                Funcao.txt("ID | PRODUTO | CATEGORIA");
                foreach (var c in contatos.OrderBy(c => c.Produto))
                    Funcao.txt($"{c.Id} | {c.Produto} | {c.Categoria} ");
                Console.ReadKey();
                break;

            case "3":
                Funcao.txt("Parte do nome: "); var part = Console.ReadLine() ?? "";
                var achados = contatos.Where(c => c.Produto.Contains(part, StringComparison.OrdinalIgnoreCase)).ToList();
                if (!achados.Any())
                {
                    Funcao.txt("Nenhum encontrado.");
                    break;
                }
                foreach (var c in achados)
                    Funcao.txt($"{c.Id} | {c.Produto} | {c.Categoria}");
                Console.ReadKey();
                break;

            case "4":
                Funcao.txt("ID: ");
                if (!int.TryParse(Console.ReadLine(), out var idUp))
                {
                    Funcao.txt("ID inválido.");
                    break;
                }
                var idx = contatos.FindIndex(c => c.Id == idUp);
                if (idx < 0)
                {
                    Funcao.txt("Não encontrado.");
                    break;
                }
                Funcao.txt("Novo produto (Precione enter para atualizar o produto): "); var np = Console.ReadLine();
                Funcao.txt("Novo categoria (Precione enter para atualizar a categoria): "); var nc = Console.ReadLine();
                var atual = contatos[idx];
                var edit = new Contato(
                    atual.Id,
                    string.IsNullOrWhiteSpace(np) ? atual.Produto : np.Trim(),
                    string.IsNullOrWhiteSpace(nc) ? atual.Categoria : nc.Trim()
                    );
                contatos[idx] = edit;
                Funcao.txt("Atualizado.");
                Console.ReadKey();
                break;

            case "5":
                Funcao.txt("ID: ");
                if (!int.TryParse(Console.ReadLine(), out var idDel))
                {
                    Funcao.txt("ID inválido.");
                    break;
                }
                var removido = contatos.RemoveAll(c => c.Id == idDel);
                Funcao.txt(removido > 0 ? "Excluído." : "Não encontrado.");
                Console.ReadKey();
                break;

            case "6":
                storage.SaveAll(contatos);
                Funcao.txt("Salvo em CSV.");
                Console.ReadKey();
                break;

            case "7":
                var b = storage.Backup();
                Funcao.txt($"Backup criado: {b}");
                Console.ReadKey();
                break;

            case "8":
                Funcao.txt("Saindo do Programa");
                Console.ReadKey();
                return;

            default:
                Funcao.txt("Opção inválida.");
                Console.ReadKey();
                break;
        }
    }
    catch (Exception ex)
    {
        Funcao.txt($"Erro: {ex.Message}");
    }
}