//Program.cs

using ControleEstoque.src.Modelo;
using ControleEstoque.src.Servico;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// Inicializa o armazenamento CSV e o serviço de inventário
var armazenamento = new CsvArmazenamento("data");

// Inicializa o serviço de inventário (não utilizado no momento)
var inventario = new InventarioServico("data");

//alterar nome da variavel....
var produtos = armazenamento.LoadAll();

// Função para gerar o próximo ID
int NextId() => produtos.Any() ? produtos.Max(c => c.Id) + 1 : 1;

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
    Funcao.txt("8 - Relatório: Extrato de movimentos");
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
                Console.Clear();
                Funcao.txt("======================================");
                Funcao.txt("           LISTA DE PRODUTOS           ");
                Funcao.txt("======================================\n");
                if (!produtos.Any())
                {
                    Funcao.txt("Sem produtos cadastrados.");
                    Console.ReadKey();
                    break;
                }

                Funcao.txt("");
                Funcao.txt("ID".PadRight(10) + "PRODUTO".PadRight(15) + "CATEGORIA".PadRight(18) + "MIN".PadRight(15) + "SALDO".PadRight(10));
                Funcao.txt(new string('-', 70));

                foreach (var c in produtos.OrderBy(c => c.Produto))
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
                Console.Clear();
                Funcao.txt("======================================");
                Funcao.txt("         CADASTRO DE PRODUTOS          ");
                Funcao.txt("======================================\n");
                Funcao.txt("Produto: ");
                var prod = Console.ReadLine() ?? "";

                Funcao.txt("Categoria: ");
                var cat = Console.ReadLine() ?? "";

                Funcao.txt("Quantidade mínima: ");
                string qMinStr = Console.ReadLine() ?? "";

                Funcao.txt("Saldo inicial: ");
                string saldoStr = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(prod) || string.IsNullOrWhiteSpace(cat))
                {
                    Funcao.txt("Os campos Produto e Categoria são obrigatórios.");
                    Console.ReadKey();
                    break;
                }

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
                produtos.Add(novo);

                Funcao.txt($"Produto '{novo.Produto}' cadastrado com ID {novo.Id}");
                Console.ReadKey();
                break;

            case "3": // EDITAR PRODUTO (SALDO)
                Console.Clear();
                Funcao.txt("======================================");
                Funcao.txt("            EDITAR PRODUTOS            ");
                Funcao.txt("======================================\n");
                Funcao.txt("ID do produto: ");
                if (!int.TryParse(Console.ReadLine(), out int idUp))
                {
                    Funcao.txt("ID inválido.");
                    Console.ReadKey();
                    break;
                }

                var idx = produtos.FindIndex(c => c.Id == idUp);
                if (idx < 0)
                {
                    Funcao.txt("Produto não encontrado.");
                    Console.ReadKey();
                    break;
                }

                var atual = produtos[idx];
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
                produtos[idx] = atualizado;

                Funcao.txt("Saldo atualizado com sucesso!");
                Console.ReadKey();
                break;

            case "4": // EXCLUIR PRODUTO
                Console.Clear();
                Funcao.txt("======================================");
                Funcao.txt("           EXCLUIR PRODUTOS           ");
                Funcao.txt("======================================\n");
                Funcao.txt("ID do produto para excluir: ");
                if (!int.TryParse(Console.ReadLine(), out int idDel))
                {
                    Funcao.txt("ID inválido.");
                    Console.ReadKey();
                    break;
                }

                // Verifica se o produto existe
                var prodDel = produtos.FirstOrDefault(c => c.Id == idDel);
                if (prodDel.Id == 0)
                {
                    Funcao.txt("Produto não encontrado.");
                    Console.ReadKey();
                    break;
                }

                // Verifica se o saldo é maior que zero
                if (prodDel.Saldo > 0)
                {
                    Funcao.txt($"Não é possível excluir! Produto tem saldo: {prodDel.Saldo}");
                    Funcao.txt("Dê baixa total no estoque antes de excluir.");
                    Console.ReadKey();
                    break;
                }

                // Remove o produto
                produtos.RemoveAll(c => c.Id == idDel);
                Funcao.txt("Produto excluído com sucesso.");
                Console.ReadKey();
                break;

            //var removido = estoque.RemoveAll(c => c.Id == idDel);
            //Funcao.txt(removido > 0 ? "Produto excluído." : "Produto não encontrado.");
            //Console.ReadKey();
            //break;

            case "5": // ENTRADA DE ESTOQUE
                Console.Clear();
                Funcao.txt("======================================");
                Funcao.txt("          ENTRADA DE ESTOQUE          ");
                Funcao.txt("======================================\n");
                Funcao.txt("ID do produto: ");
                if (!int.TryParse(Console.ReadLine(), out int idEnt))
                {
                    Funcao.txt("ID inválido.");
                    Console.ReadKey();
                    break;
                }

                var prodEnt = produtos.FirstOrDefault(c => c.Id == idEnt);
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

                inventario.Movimentar(produtos, prodEnt.Id, "ENTRADA", qtdEnt, "Reposição de estoque");

                Funcao.txt($"Entrada registrada! Novo saldo: {produtos.First(p => p.Id == prodEnt.Id).Saldo}");
                Console.ReadKey();
                break;


            case "6": // SAÍDA DE ESTOQUE
                Console.Clear();
                Funcao.txt("======================================");
                Funcao.txt("           SAÍDA DE ESTOQUE           ");
                Funcao.txt("======================================\n");
                Funcao.txt("ID do produto: ");
                if (!int.TryParse(Console.ReadLine(), out int idSai))
                {
                    Funcao.txt("ID inválido.");
                    Console.ReadKey();
                    break;
                }

                var prodSai = produtos.FirstOrDefault(c => c.Id == idSai);
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

                try
                {
                    inventario.Movimentar(produtos, prodSai.Id, "SAIDA", qtdSai, "Saída de estoque");
                    Funcao.txt($"Saída registrada! Novo saldo: {produtos.First(p => p.Id == prodSai.Id).Saldo}");
                }
                catch (Exception ex)
                {
                    Funcao.txt($"Erro: {ex.Message}");
                }

                Console.ReadKey();
                break;


            case "7": // RELATÓRIO: ESTOQUE ABAIXO DO MÍNIMO
                Console.Clear();
                Funcao.txt("======================================");
                Funcao.txt("  RELATÓRIO: ESTOQUE ABAIXO DO MÍNIMO  ");
                Funcao.txt("======================================\n");

                var abaixo = produtos.Where(c => c.Saldo < c.EstoqueMinimo).ToList();

                if (!abaixo.Any())
                {
                    Funcao.txt("Nenhum produto abaixo do estoque mínimo.");
                }
                else
                {
                    Funcao.txt("ID".PadRight(5) + "PRODUTO".PadRight(20) + "SALDO".PadRight(10) + "MÍNIMO");
                    Funcao.txt(new string('-', 50));

                    foreach (var c in abaixo.OrderBy(c => c.Produto))
                    {
                        Funcao.txt(
                            c.Id.ToString().PadRight(5) +
                            c.Produto.PadRight(20) +
                            c.Saldo.ToString().PadRight(10) +
                            c.EstoqueMinimo.ToString()
                        );
                    }
                }

                Funcao.txt("\n-----------------------------");
                Funcao.txt("Precisone qualquer tecla para voltar ao menu... ");
                Console.ReadKey();
                break;

            case "8": // RELATÓRIO: EXTRATO DE MOVIMENTOS (TODOS OS PRODUTOS)
                Console.Clear();
                Funcao.txt("======================================");
                Funcao.txt("   RELATÓRIO: EXTRATO DE MOVIMENTOS   ");
                Funcao.txt("======================================\n");

                // Carrega produtos e movimentos
                var produtosExtrato = armazenamento.LoadAll();
                var movimentos = new List<Movimentos>();

                foreach (var p in produtosExtrato)
                {
                    var extratoProd = inventario.ExtratoPorProduto(p.Id);
                    if (extratoProd.Any())
                        movimentos.AddRange(extratoProd);
                }

                // Verifica se há algum movimento
                if (!movimentos.Any())
                {
                    Funcao.txt("Nenhum movimento registrado no sistema.");
                    Console.ReadKey();
                    break;
                }

                // Junta os nomes dos produtos
                var movimentosComNome = movimentos
                    .Join(produtosExtrato,
                          mov => mov.ProdutoId,
                          prod => prod.Id,
                          (mov, prod) => new
                          {
                              Produto = prod.Produto,
                              mov.Tipo,
                              mov.Quantidade,
                              mov.Data,
                              mov.Observacao
                          })
                    .OrderBy(m => m.Data)
                    .ToList();

                // Cabeçalho
                Funcao.txt("PRODUTO".PadRight(18) +
                           "TIPO".PadRight(12) +
                           "QTD".PadRight(8) +
                           "DATA".PadRight(22) +
                           "OBSERVAÇÃO");
                Funcao.txt(new string('-', 85));

                // Exibe todos os movimentos formatados
                foreach (var m in movimentosComNome)
                {
                    if (m.Tipo.ToUpper() == "ENTRADA")
                        Console.ForegroundColor = ConsoleColor.Green;
                    else if (m.Tipo.ToUpper() == "SAIDA" || m.Tipo.ToUpper() == "SAIDA")
                        Console.ForegroundColor = ConsoleColor.Red;
                    else
                        Console.ResetColor();

                    Funcao.txt(
                        m.Produto.PadRight(18) +
                        m.Tipo.PadRight(12) +
                        m.Quantidade.ToString().PadRight(8) +
                        m.Data.ToString("dd/MM/yyyy HH:mm").PadRight(22) +
                        m.Observacao
                    );

                    Console.ResetColor();
                }

                Funcao.txt("\n" + new string('-', 85));
                Funcao.txt("Fim do extrato geral de movimentações.");
                Funcao.txt("\nPressione qualquer tecla para voltar ao menu...");
                Console.ReadKey();
                break;


            case "9": // SALVAR (CSV)
                Console.Clear();
                Funcao.txt("======================================");
                Funcao.txt("            SALVAR EM CSV             ");
                Funcao.txt("======================================\n");
                armazenamento.SaveAll(produtos);
                Funcao.txt("Dados salvos em CSV.");
                Console.ReadKey();
                break;

            case "0": // Sair
                Console.Clear();
                Funcao.txt("======================================");
                Funcao.txt("           SAIR DO PROGRAMA           ");
                Funcao.txt("======================================\n");
                // Pergunta para salvar antes de sair
                Funcao.txt("Deseja salvar antes de sair? (Sim/Não): ");
                var resposta = Console.ReadLine()?.ToUpper();

                if (resposta == "SIM" || resposta == "S") // Salva se a resposta for "Sim"
                {
                    armazenamento.SaveAll(produtos);
                    Funcao.txt("Dados salvos em CSV.!");
                }
                // Sai do programa
                Funcao.txt("Saindo do programa...");
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
        Funcao.txt($"Ocorreu um erro inesperado: {ex.Message}");
        Funcao.txt("Verifique se os arquivos CSV estão acessíveis e se os dados são válidos.");
        Console.ReadKey();
    }
}