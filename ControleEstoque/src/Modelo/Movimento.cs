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


/*
🎓 Explicação para Iniciantes - Dois Arquivos CSV
Vamos pensar em uma loja que vende produtos. Você precisa de 2 cadernos diferentes:

📗 Caderno 1: produtos.csv (Lista dos produtos da loja)
Id;Produto;Categoria;Quantidade
1;Notebook;Informática;35
2;Mouse;Informática;80
3;Teclado;Informática;50
```

**O que significa:**
- Linha 1: Você tem o produto "Notebook" com ID número 1, e tem 35 unidades
- Linha 2: Você tem o produto "Mouse" com ID número 2, e tem 80 unidades
- Linha 3: Você tem o produto "Teclado" com ID número 3, e tem 50 unidades

**Pensa assim:** É a **prateleira** da loja mostrando o que tem e quanto tem de cada coisa.

---

## 📘 **Caderno 2: movimentos.csv** (Histórico de entradas e saídas)
```
Id;ProdutoId;Tipo;Quantidade;Data;Observacao
1;1;ENTRADA;50;30/10/2025 14:00;Comprei do fornecedor
2;1;SAIDA;10;30/10/2025 15:00;Vendi para João
3;2;ENTRADA;100;30/10/2025 16:00;Comprei do fornecedor
4;1;SAIDA;5;30/10/2025 17:00;Vendi para Maria
```

**O que significa cada coluna:**

### **Id** = Número da movimentação (cada linha é uma ação diferente)
- Movimentação 1 = primeira ação que aconteceu
- Movimentação 2 = segunda ação que aconteceu
- Movimentação 3 = terceira ação...

### **ProdutoId** = Qual produto foi movimentado
- ProdutoId = 1 significa "Notebook" (olha no caderno 1!)
- ProdutoId = 2 significa "Mouse" (olha no caderno 1!)

---

## 🎬 **Vamos ver linha por linha:**

### **Linha 1 do movimentos.csv:**
```
1;1;ENTRADA;50;30/10/2025 14:00;Comprei do fornecedor
```

**Tradução:**
- **Id = 1**: É a movimentação número 1 (primeira ação registrada)
- **ProdutoId = 1**: É sobre o produto ID 1 (Notebook)
- **Tipo = ENTRADA**: Coloquei mais produtos no estoque
- **Quantidade = 50**: Coloquei 50 notebooks
- **Data**: Aconteceu no dia 30/10 às 14h
- **Observacao**: Anotei que comprei do fornecedor

### **Linha 2 do movimentos.csv:**
```
2;1;SAIDA;10;30/10/2025 15:00;Vendi para João
```

**Tradução:**
- **Id = 2**: É a movimentação número 2 (segunda ação)
- **ProdutoId = 1**: É sobre o produto ID 1 de novo (Notebook de novo!)
- **Tipo = SAIDA**: Tirei produtos do estoque
- **Quantidade = 10**: Vendi 10 notebooks
- **Observacao**: Cliente João comprou

### **Linha 4 do movimentos.csv:**
```
4;1;SAIDA;5;30/10/2025 17:00;Vendi para Maria
```

**Tradução:**
- **Id = 4**: É a movimentação número 4
- **ProdutoId = 1**: De novo sobre o Notebook!
- **Tipo = SAIDA**: Vendi
- **Quantidade = 5**: 5 notebooks

---

## 🤔 **Agora você pergunta: "Por que ProdutoId repete?"**

Porque o **mesmo produto** pode ter **várias movimentações**!

Olha só o Notebook (ID 1):
- Movimentação 1: +50 notebooks (ENTRADA)
- Movimentação 2: -10 notebooks (SAIDA)
- Movimentação 4: -5 notebooks (SAIDA)

**Total do Notebook:** 50 - 10 - 5 = **35 notebooks** ✅

Olha no `produtos.csv` → A quantidade do Notebook é 35! Bateu! 🎯

---

## 📊 **Analogia Visual:**

Imagine sua **agenda escolar**:

### **Caderno de Matérias (produtos.csv):**
```
1. Matemática - 3 provas feitas
2. Português - 5 provas feitas
3. História - 2 provas feitas
```

### **Histórico de Provas (movimentos.csv):**
```
Prova 1: Matéria 1 (Matemática) - Nota 8
Prova 2: Matéria 1 (Matemática) - Nota 7
Prova 3: Matéria 2 (Português) - Nota 9
Prova 4: Matéria 1 (Matemática) - Nota 10
Prova 5: Matéria 2 (Português) - Nota 6
```

Viu? **Matemática (Matéria 1)** aparece 3 vezes no histórico!

---

## ✅ **Resumo Final:**

### **Id** da movimentação:
- Número único de CADA ação
- Nunca repete
- Serve para identificar aquela ação específica

### **ProdutoId**:
- Mostra QUAL produto foi movimentado
- Pode repetir várias vezes (porque o mesmo produto tem várias entradas/saídas)
- É tipo um "apelido" que aponta para o produto lá no `produtos.csv`

---

## 🎯 **Exemplo do Mundo Real:**

Quando você vai no mercado e pega o extrato do caixa:
```
Item 1: Arroz (Produto código 123) - R$ 20
Item 2: Feijão (Produto código 456) - R$ 10  
Item 3: Arroz (Produto código 123) - R$ 20
Percebeu? Você comprou Arroz 2 vezes (Item 1 e Item 3)!

Item = Id da movimentação
Produto código = ProdutoId


Agora ficou claro? 😊 Me diz se ainda tem dúvida!
 
 */