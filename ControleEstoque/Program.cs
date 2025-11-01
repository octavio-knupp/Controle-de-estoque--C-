//Program.cs

using ControleEstoque.src.Modelo;
using ControleEstoque.src.Servico;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var armazenamento = new CsvArmazenamento("data");

//alterar nome da variavel....
var estoque = armazenamento.LoadAll();

// Função para gerar o próximo ID
int NextId() => estoque.Any() ? estoque.Max(c => c.Id) + 1 : 1;

while (true)
{
    // Menu principal
    Console.Clear();
    Funcao.txt("");
    Funcao.txt("==== CONTROLE DE ESTOQUE ====");
    Funcao.txt("1 - Listar produtos");
    Funcao.txt("2 - Cadastrar produto");
    Funcao.txt("3 - Editar produto (saldo)");
    Funcao.txt("4 - Excluir produto");
    Funcao.txt("5 - Dar ENTRADA em estoque");
    Funcao.txt("6 - Dar SAÍDA de estoque");
    Funcao.txt("7 - Relatório: Estoque abaixo do mínimo");
    Funcao.txt("8 - Relatório: Extrato de movimentos (futuro)");
    Funcao.txt("9 - Salvar (CSV)");
    Funcao.txt("0 - Sair");
    Funcao.txt("");
    Funcao.txt("Integrantes:");
    Funcao.txt("Octavio Henrique Knupp Lucio");
    Funcao.txt("Alexandre Aielo Lima");
    Funcao.txt("Nícolas Joly Mussi");
    Funcao.txt("Eduardo da Cunha");
    Funcao.txt("");
    Funcao.txt("Escolha uma opção: ");
    var op = Console.ReadLine();

    try
    {
        switch (op)
        {
            case "1": // Listar produtos
                if (!estoque.Any())
                {
                    Funcao.txt("Sem produtos cadastrados.");
                    Console.ReadKey();
                    break;
                }

                Funcao.txt("");
                Funcao.txt("ID".PadRight(10) + "PRODUTO".PadRight(15) + "CATEGORIA".PadRight(18) + "MIN".PadRight(15) + "SALDO".PadRight(10));
                Funcao.txt(new string('-', 70));

                foreach (var c in estoque.OrderBy(c => c.Produto))
                {
                    // Exibir detalhes do produto
                    Funcao.txt(
                        c.Id.ToString().PadRight(10) +
                        c.Produto.PadRight(15) +
                        c.Categoria.PadRight(18) +
                        c.EstoqueMinimo.ToString().PadRight(15) +
                        c.Saldo.ToString().PadRight(10)
                    );
                }
                Console.ReadKey();
                break;

            case "2": // Cadastrar produto
                Funcao.txt("Produto: ");
                var prod = Console.ReadLine() ?? "";

                Funcao.txt("Categoria: ");
                var cat = Console.ReadLine() ?? "";

                Funcao.txt("Quantidade mínima: ");
                string qMinStr = Console.ReadLine() ?? "";

                Funcao.txt("Saldo inicial: ");
                string saldoStr = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(prod))
                {
                    Funcao.txt("Nome é obrigatório.");
                    Console.ReadKey();
                    break;
                }

                if (!int.TryParse(qMinStr, out int qMin) || qMin < 0)
                {
                    Funcao.txt("Quantidade mínima inválida.");
                    Console.ReadKey();
                    break;
                }

                if (!int.TryParse(saldoStr, out int saldo) || saldo < 0)
                {
                    Funcao.txt("Saldo inválido.");
                    Console.ReadKey();
                    break;
                }

                var novo = new Produtos(NextId(), prod.Trim(), cat.Trim(), qMin, saldo);
                estoque.Add(novo);

                Funcao.txt($"✅ Produto '{novo.Produto}' cadastrado com ID {novo.Id}");
                Console.ReadKey();
                break;

            case "3": // EDITAR PRODUTO (SALDO)
                Funcao.txt("ID do produto: ");
                if (!int.TryParse(Console.ReadLine(), out int idUp))
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

                var atual = estoque[idx];
                Funcao.txt($"Produto: {atual.Produto}");
                Funcao.txt($"Categoria: {atual.Categoria}");
                Funcao.txt($"Quantidade mínima: {atual.EstoqueMinimo}");
                Funcao.txt($"Saldo atual: {atual.Saldo}");

                Funcao.txt("\nNovo saldo: ");
                var entradaSaldo = Console.ReadLine();

                if (!int.TryParse(entradaSaldo, out int novoSaldo) || novoSaldo < 0)
                {
                    Funcao.txt("Saldo inválido. Digite um número maior ou igual a zero.");
                    Console.ReadKey();
                    break;
                }

                var atualizado = new Produtos(atual.Id, atual.Produto, atual.Categoria, atual.EstoqueMinimo, novoSaldo);
                estoque[idx] = atualizado;

                Funcao.txt("Saldo atualizado com sucesso!");
                Console.ReadKey();
                break;

            case "4": // EXCLUIR PRODUTO
                Funcao.txt("ID do produto para excluir: ");
                if (!int.TryParse(Console.ReadLine(), out int idDel))
                {
                    Funcao.txt("ID inválido.");
                    Console.ReadKey();
                    break;
                }

                var removido = estoque.RemoveAll(c => c.Id == idDel);
                Funcao.txt(removido > 0 ? "Produto excluído." : "Produto não encontrado.");
                Console.ReadKey();
                break;

            case "5": // ENTRADA DE ESTOQUE
                Funcao.txt("ID do produto: ");
                if (!int.TryParse(Console.ReadLine(), out int idEnt))
                {
                    Funcao.txt("ID inválido.");
                    Console.ReadKey();
                    break;
                }

                var prodEnt = estoque.FirstOrDefault(c => c.Id == idEnt);
                if (prodEnt.Id == 0)
                {
                    Funcao.txt("Produto não encontrado.");
                    Console.ReadKey();
                    break;
                }

                Funcao.txt("Quantidade de entrada: ");
                if (!int.TryParse(Console.ReadLine(), out int qtdEnt) || qtdEnt <= 0)
                {
                    Funcao.txt("Quantidade inválida.");
                    Console.ReadKey();
                    break;
                }

                prodEnt = prodEnt with { Saldo = prodEnt.Saldo + qtdEnt };
                estoque[estoque.FindIndex(c => c.Id == idEnt)] = prodEnt;

                Funcao.txt($"✅ Entrada registrada! Novo saldo: {prodEnt.Saldo}");
                Console.ReadKey();
                break;

            case "6": // SAÍDA DE ESTOQUE
                Funcao.txt("ID do produto: ");
                if (!int.TryParse(Console.ReadLine(), out int idSai))
                {
                    Funcao.txt("ID inválido.");
                    Console.ReadKey();
                    break;
                }

                var prodSai = estoque.FirstOrDefault(c => c.Id == idSai);
                if (prodSai.Id == 0)
                {
                    Funcao.txt("Produto não encontrado.");
                    Console.ReadKey();
                    break;
                }

                Funcao.txt("Quantidade de saída: ");
                if (!int.TryParse(Console.ReadLine(), out int qtdSai) || qtdSai <= 0)
                {
                    Funcao.txt("Quantidade inválida.");
                    Console.ReadKey();
                    break;
                }

                if (qtdSai > prodSai.Saldo)
                {
                    Funcao.txt("❌ Saldo insuficiente para saída!");
                    Console.ReadKey();
                    break;
                }

                prodSai = prodSai with { Saldo = prodSai.Saldo - qtdSai };
                estoque[estoque.FindIndex(c => c.Id == idSai)] = prodSai;

                Funcao.txt($"Saída registrada! Novo saldo: {prodSai.Saldo}");
                Console.ReadKey();
                break;

            case "7": // RELATÓRIO: ESTOQUE ABAIXO DO MÍNIMO
                var abaixo = estoque.Where(c => c.Saldo < c.EstoqueMinimo).ToList();

                if (!abaixo.Any())
                {
                    Funcao.txt("Nenhum produto abaixo do estoque mínimo.");
                }
                else
                {
                    Funcao.txt("Produtos abaixo do estoque mínimo:\n");
                    foreach (var c in abaixo)
                        Funcao.txt($"- {c.Produto} (Saldo: {c.Saldo}, Mínimo: {c.EstoqueMinimo})");
                }
                Console.ReadKey();
                break;

            case "8": // RELATÓRIO: EXTRATO DE MOVIMENTOS (FUTURO)
                Funcao.txt("Essa função será implementada com o arquivo movimentos.csv.");
                Console.ReadKey();
                break;

            case "9": // SALVAR (CSV)
                armazenamento.SaveAll(estoque);
                Funcao.txt("Dados salvos em CSV.");
                Console.ReadKey();
                break;

            case "0": // Sair

                Funcao.txt("Saindo...");
                Console.ReadKey();
                return;

            default: // Opção inválida
                Funcao.txt("Opção inválida.");
                Console.ReadKey();
                break;
        }
    }
    catch (Exception ex) // Tratamento genérico de erros
    {
        Funcao.txt($"Erro: {ex.Message}");
        Console.ReadKey();
    }
}
