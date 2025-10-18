using System;
using ControleEstoque.src.Servico;
using ControleEstoque.src.Modelo;



class Program
{
    static void Main()
    {
        int menu;
        

        do
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
            Funcao.txt("8 - Sair\n");
            Funcao.txt("Integrantes:\n");
            Funcao.txt("Octavio Henrique Knupp Lucio");
            Funcao.txt("Alexandre Aielo Lima");
            Funcao.txt("Nícolas Joly Mussi");
            Funcao.txt("Eduardo da Cunha\n");
            
            

            Funcao.txt("Informe sua opção:");
            string opcao = Console.ReadLine();
            bool opV = int.TryParse(opcao, out  menu);

            if (!opV)
            {
                Funcao.txt("\nInforme um número válido");
                Console.ReadKey();
            }

            switch(menu)
            {
                case 1:
                    Console.Clear();
                    Funcao.txt("");
                    Funcao.txt("==== Cadastrar Produtos ====");

                    Console.ReadKey();
                    break;
                case 2:
                    Console.Clear();
                    Funcao.txt("");
                    Funcao.txt("==== Listar Produtos ====");

                    Console.ReadKey();
                    break;
                case 3:
                    Console.Clear();
                    Funcao.txt("");
                    Funcao.txt("==== Buscar Produtos ====");

                    Console.ReadKey();
                    break;
                case 4:
                    Console.Clear();
                    Funcao.txt("");
                    Funcao.txt("==== Atualizar Cadastro ====");

                    Console.ReadKey();
                    break;
                case 5:
                    Console.Clear();
                    Funcao.txt("");
                    Funcao.txt("==== Excluir Produto ====");

                    Console.ReadKey();
                    break;
                case 6:
                    Console.Clear();
                    Funcao.txt("");
                    Funcao.txt("==== Salvar Cadastro ====");

                    Console.ReadKey();
                    break;
                case 7:
                    Console.Clear();
                    Funcao.txt("");
                    Funcao.txt("==== Backup Geral ====");

                    Console.ReadKey();
                    break;
                case 8:
                    Console.Clear();
                    Funcao.txt("");
                    Funcao.txt("Saindo do sistema...");
                    break;

                default:
                    Funcao.txt("Digite um valor dentro do intervalo de (1 - 8)");

                    break;
            }
            Console.ReadKey();
        }while (menu != 8);

    }
}
