using ControleEstoque.src.Modelo;
using ControleEstoque.src.Servico;


Console.OutputEncoding = System.Text.Encoding.UTF8;

var storage = new FileStorage("data");
var estoque = storage.LoadAll();

int NextId() => estoque.Any() ? estoque.Max(c => c.Id) + 1 : 1;

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
    Funcao.txt("7 - Movimentar produto");
    Funcao.txt("8 - Backup Geral");
    Funcao.txt("9 - Sair");
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
            case "1": // Cadastro de produto
                Funcao.txt("Produto: ");
                var prod = Console.ReadLine() ?? "";

                Funcao.txt("Categoria: ");
                var cat = Console.ReadLine() ?? "";

                Funcao.txt("Quantidade: ");
                string qnd = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(prod))
                {
                    Funcao.txt("Nome é obrigatório.");
                    Console.ReadKey();
                    break;
                }
                if (!int.TryParse(qnd, out int quantidade))
                {
                    Console.WriteLine("Quantidade inválida.");
                    Console.ReadKey();
                }
                if (quantidade == 0)
                {
                    Funcao.txt("Quantidade deve ser maior que zero.");
                    Console.ReadKey();
                    break;
                }
                else if (quantidade < 0)
                {
                    Funcao.txt("Valor negativo!!");
                    Console.ReadKey();
                    break;
                }
                else if (quantidade <= 10)
                {
                    Funcao.txt("Estoque baixo! Repor estoque em breve.");
                    Console.ReadKey();

                }

                var novo = new Produtos(NextId(), prod.Trim(), cat.Trim(), quantidade);
                estoque.Add(novo);
                Funcao.txt($"Produto criado com ID: {novo.Id}");
                Console.ReadKey();
                break;

            case "2": // Listagem de produtos
                if (!estoque.Any())
                {
                    Funcao.txt("Sem produtos cadastrados.");
                    Console.ReadKey();
                    break;
                }
                Funcao.txt("");
                Funcao.txt("ID".PadRight(5) + "PRODUTO".PadRight(20) + "CATEGORIA".PadRight(20) + "QUANTIDADE");
                Funcao.txt(new string('-', 70));

                foreach (var c in estoque.OrderBy(c => c.Produto))
                {
                    Funcao.txt(
                        c.Id.ToString().PadRight(5) +
                        c.Produto.PadRight(20) +
                        c.Categoria.PadRight(20) +
                        c.Quantidade.ToString().PadRight(10)
                     );
                }
                Funcao.txt("");
                Console.ReadKey();
                break;

            case "3": // Busca de produtos por parte do nome
                Funcao.txt("Parte do nome: ");
                var part = Console.ReadLine() ?? "";
                var achados = estoque.Where(c => c.Produto.Contains(part, StringComparison.OrdinalIgnoreCase)).ToList();

                if (!achados.Any())
                {
                    Funcao.txt("Nenhum produto encontrado.");
                    Console.ReadKey();
                    break;
                }

                Funcao.txt("");
                Funcao.txt($"Encontrados: {achados.Count} Produto(s)");
                Funcao.txt("");
                Funcao.txt("ID".PadRight(5) + "PRODUTO".PadRight(20) + "CATEGORIA".PadRight(20) + "QUANTIDADE");
                Funcao.txt(new string('-', 70));

                // Listagem dos encontrados
                foreach (var c in achados.OrderBy(c => c.Produto))
                {
                    Funcao.txt(
                        c.Id.ToString().PadRight(5) +
                        c.Produto.PadRight(20) +
                        c.Categoria.PadRight(20) +
                        c.Quantidade.ToString().PadRight(10)
                     );
                }
                Funcao.txt("");
                Console.ReadKey();
                break;

            case "4": // Atualizar cadastro
                Funcao.txt($"ID do produto: ");
                if (!int.TryParse(Console.ReadLine(), out var idUp))
                {
                    Funcao.txt("ID inválido.");
                    Console.ReadKey();
                    break;
                }

                var idx = estoque.FindIndex(c => c.Id == idUp);
                if (idx < 0)
                {
                    Funcao.txt("Produto não encontrado.");
                    Console.ReadKey();
                    break;
                }

                var novoProduto = estoque[idx];
                Funcao.txt($"Atualizando ID: {novoProduto.Id}");
                Funcao.txt("");

                // Produto
                string np;
                do
                {
                    Funcao.txt($"Produto atual: {novoProduto.Produto}");
                    Funcao.txt("Novo produto (Digite o nome do produto): ");
                    np = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(np))
                    {
                        Funcao.txt(" O nome do produto é obrigatório!\n");
                    }
                } while (string.IsNullOrWhiteSpace(np));

                // Categoria
                string nc;
                do
                {
                    Funcao.txt($"Categoria atual: {novoProduto.Categoria}");
                    Funcao.txt("Nova categoria (Digite a categoria): ");
                    nc = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(nc))
                    {
                        Funcao.txt(" O nome da categoria é obrigatório!\n");
                    }
                } while (string.IsNullOrWhiteSpace(nc));

                // Quantidade
                string nq;
                do
                {
                    Funcao.txt($"Quantidade atual: {novoProduto.Quantidade}");
                    Funcao.txt("Nova quantidade (Digite apenas números): ");
                    string entradaNq = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(entradaNq))
                    {
                        Funcao.txt("A quantidade é obrigatória!\n");
                        continue; // volta pro início do loop
                    }

                    if (int.TryParse(entradaNq, out int novaQnd))
                    {
                        nq = novaQnd.ToString();
                        break; // sai do loop se deu certo
                    }
                    else
                    {
                        Funcao.txt("Valor inválido! Digite apenas números.\n");
                    }

                } while (true);

                var atual = estoque[idx];
                int novaQuantidade;
                if (string.IsNullOrWhiteSpace(nq))
                {
                    novaQuantidade = atual.Quantidade;
                }
                else if (!int.TryParse(nq, out novaQuantidade))
                {

                    Funcao.txt("Quantidade inválida.");
                    Console.ReadKey();
                    break;
                }
                var edit = new Produtos(
                    atual.Id,
                    string.IsNullOrWhiteSpace(np) ? atual.Produto : np.Trim(),
                    string.IsNullOrWhiteSpace(nc) ? atual.Categoria : nc.Trim(),
                    novaQuantidade
                    );

                estoque[idx] = edit;

                Funcao.txt("Atualizado.");
                Console.ReadKey();
                break;

            case "5":
                int idDel;
                while (true)
                {
                    Funcao.txt("ID: ");
                    string entrada = Console.ReadLine();

                    // Verifica se o usuário digitou algo
                    if (string.IsNullOrWhiteSpace(entrada))
                    {
                        Funcao.txt(" O ID não pode ficar em branco!\n");
                        continue; // volta para pedir novamente
                    }

                    // Verifica se o valor é um número inteiro válido
                    if (int.TryParse(entrada, out idDel))
                    {
                        break; // sai do loop se for válido
                    }
                    else
                    {
                        Funcao.txt(" ID inválido! Digite apenas números.\n");
                    }
                }

                // Agora que o ID é válido, tenta remover do estoque
                var removido = estoque.RemoveAll(c => c.Id == idDel);
                Funcao.txt(removido > 0 ? " Produto excluído." : " Produto não encontrado.");
                Console.ReadKey();
                break;

            case "6":
                storage.SaveAll(estoque);
                Funcao.txt("Salvo em CSV.");
                Console.ReadKey();
                break;
            case "7":
                /*
                 lógica de movimentação de produto;
                 pede o id;
                 mostra o produto do id;
                 pedir a quantidade que quer movimentar;
                 enter ele faz a movimentação para  arquivo movimento.csv;
                */
                break;
            case "8":
                var b = storage.Backup();
                Funcao.txt($"Backup criado: {b}");
                Console.ReadKey();
                break;

            case "9":
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