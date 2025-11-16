//Pasta src: Pasta Servico: Funcao.cs

namespace ControleEstoque.src.Servico
{
    public class Funcao
    {
        // Mantém igual — centraliza títulos
        public static void txt(string texto)
        {
            int larguraConsole = Console.WindowWidth;
            int posicaoInicial = Math.Max(0, (larguraConsole - texto.Length) / 2);

            Console.SetCursorPosition(posicaoInicial, Console.CursorTop);
            Console.WriteLine(texto);
        }

        // AGORA recebe o tamanho fixo da tabela e centraliza corretamente
        public static void txtLeft(string texto, int tabelaWidth)
        {
            int larguraConsole = Console.WindowWidth;
            int margem = Math.Max(0, (larguraConsole - tabelaWidth) / 2);

            Console.WriteLine(new string(' ', margem) + texto);
        }
    }
}



