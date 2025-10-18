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
            Console.WriteLine("\n==== Controle de Estoque ====");
            Console.WriteLine("1 - Listar");
            Console.WriteLine("2 - Cadastrar Produto");
            Console.WriteLine("3 - Buscar por nome do produto");
            Console.WriteLine("4 - Atualizar Cadastro");
            Console.WriteLine("5 - Excluir Produto");
            Console.WriteLine("6 - Salvar Cadastro");
            Console.WriteLine("7 - Backup Geral");
            Console.WriteLine("8 - Sair\n");
            Console.WriteLine("Integrantes:");
            Console.WriteLine("Octavio Henrique Knupp Lucio");
            Console.WriteLine("Eduardo da Cunha");
            Console.WriteLine("Nícolas Joly Mussi");
            Console.WriteLine("Alexandre...\n");

            Console.WriteLine("Informe sua opção:");
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
                    Console.WriteLine("Hello word!");
                    break;
                case 2:
                    Console.WriteLine("Hello word!");
                    break;
                case 3:
                    Console.WriteLine("Hello word!");
                    break;
                case 4:
                    Console.WriteLine("Hello word!");
                    break;
                case 5:
                    Console.WriteLine("Hello word!");
                    break;
                case 6:
                    Console.WriteLine("Hello word!");
                    break;
                case 7:
                    Console.WriteLine("Hello word!");
                    break;
                case 8:
                    Console.WriteLine("Hello word!");
                    break;

                default:
                    Console.WriteLine("Hello word!");
                    break;
            }

            Console.ReadKey();
        }

    }
}
