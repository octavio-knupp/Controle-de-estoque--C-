//Pasta src: Pasta Modelo: Produto.cs

namespace ControleEstoque.src.Modelo;

public readonly record struct Produtos(
    int Id,
    string Produto,
    string Categoria,
    int EstoqueMinimo,
    int Saldo
    );