# 📦 Controle de Estoque – Sistema em C#

Sistema de controle de estoque desenvolvido em C#, utilizando armazenamento em arquivos CSV para registrar produtos e movimentações de entrada e saída.  
Projeto criado como exercício acadêmico para prática de lógica de programação, manipulação de arquivos, modularização e boas práticas em C#.

---

## 🚀 Funcionalidades

### ✔ Gestão de Produtos
- Cadastro de produtos  
- Consulta de produtos cadastrados  
- Edição de informações  
- Definição de estoque mínimo  
- Visualização do saldo atual  

### ✔ Movimentações de Estoque
- Registrar **ENTRADA** de produtos  
- Registrar **SAÍDA** de produtos  
- Validação automática de saldo (não permite saldo negativo)  
- Registro completo com data/hora e observação  
- Geração de extrato por produto  

### ✔ Armazenamento em Arquivos CSV
- `produtos.csv` → Lista de produtos  
- `movimentos.csv` → Histórico de movimentações  
- Criação automática dos arquivos e da pasta `data/`  
- Resiliência contra linhas inválidas  
- Sistema de gravação com arquivo temporário para evitar corrupção  

### ✔ Relatórios
- Extrato de movimentações por produto  
- Backup automático dos arquivos de produtos  

---

## 🏗 Estrutura do Projeto

- /ControleEstoque
- │
- ├── Program.cs
- ├── /src
- │ ├── /Modelo
- │ │ ├── Produtos.cs
- │ │ └── Movimentos.cs
- │ ├── /Servico
- │ │ ├── CsvArmazenamento.cs
- │ │ └── InventarioServico.cs
- │
- └── /data
- ├── produtos.csv
- └── movimentos.csv

  ---

## ⚙ Tecnologias Utilizadas
- C# (.NET 8)
- Programação estruturada e modularizada
- Records (`record struct`)
- Manipulação de arquivos CSV

  ---
  
#Autores

Octavio Henrique Knupp Lucio – Desenvolvedor
- Github: octavio-knupp

Alexandre Aielo Lima – Desenvolvedor
- Github: xandy67

Nícolas Joly Mussi – Desenvolvedor
- Github: Nícolas Mussi

Eduardo da Cunha – Desenvolvedor
- Github: cunhaxdudu

