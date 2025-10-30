namespace ControleEstoque.src.Modelo;

public readonly record struct Produtos(
    int Id,
    string Produto,
    string Categoria,
    int Quantidade
    );