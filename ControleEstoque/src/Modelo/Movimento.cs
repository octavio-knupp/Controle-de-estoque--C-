//Pasta src: Pasta Modelo: Movimento.cs

namespace ControleEstoque.src.Modelo;

public readonly record struct Movimentos(
    int Id,
    int ProdutoId,      // ← Apenas o ID, não o nome!
    string Tipo,        // ← "ENTRADA" ou "SAIDA"
    int Quantidade,
    DateTime Data,      // ← Data da movimentação
    string Observacao   // ← Motivo (ex: "Venda", "Compra")
);