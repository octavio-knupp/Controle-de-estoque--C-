using System;
using ControleEstoque.src.Servico;
using ControleEstoque.src.Modelo;



class Program
{
    static void Main()
    {
        int menu;
        

        while (true)
        {
            Console.Clear();
            Funcao.txt("");
            Funcao.txt("==== Controle de Estoque ====");
            Funcao.txt("1 - Listar");
            Funcao.txt("2 - Cadastrar Produto");
            Funcao.txt("3 - Buscar por nome do produto");
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
            bool opV = int.TryParse(opcao, out int op);
            Console.WriteLine(opV);

            if (!opV)
            {
                Console.WriteLine("\nInforme um número válido");
                Console.ReadKey();
            }

            switch (op)
            {
                case 1:
                    Funcao.txt("Hello word!");
                    break;
                case 2:
                    Funcao.txt("Hello word!");
                    break;
                case 3:
                    Funcao.txt("Hello word!");
                    break;
                case 4:
                    Funcao.txt("Hello word!");
                    break;
                case 5:
                    Funcao.txt("Hello word!");
                    break;
                case 6:
                    Funcao.txt("Hello word!");
                    break;
                case 7:
                    Console.WriteLine("Hello word!");
                    break;
                case 8:
                    Funcao.txt("Hello word!");
                    break;

                default:
                    Funcao.txt("Hello word!");
                    break;
            }

            Console.ReadKey();
        }

    }
}
