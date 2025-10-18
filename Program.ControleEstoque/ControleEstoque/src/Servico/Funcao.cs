using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstoque.src.Servico
{
    public class Funcao
    {
        public static void txt(string texto)
        {
        // Calcula a posição inicial para centralizar o texto
        int larguraConsole = Console.WindowWidth;
        int posicaoInicial = Math.Max(0, (larguraConsole - texto.Length) / 2);

        // Move o cursor para a posição inicial e escreve o texto
        Console.SetCursorPosition(posicaoInicial, Console.CursorTop);
        Console.WriteLine(texto);
        }
    }
    
}
