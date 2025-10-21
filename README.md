# AgendaConsoleBasico (C# Console — Nível Básico)

Projeto didático de agenda de contatos com **CRUD** em **arquivo CSV**.

## Como executar
```bash
dotnet run
```

## Plano de Aulas (sugestão)
- **Aula 1**: Introdução ao C#, Console, tipos, `struct` Contact.
- **Aula 2**: Leitura e escrita de **arquivos** (`File`, `StreamWriter`, `StreamReader`).
- **Aula 3**: **CRUD** (criar, listar, buscar).
- **Aula 4**: **Atualizar** e **Excluir**, validações e mensagens.
- **Aula 5**: Organização do código (Services), tratamento de erros e **backup** simples.
- **Aula 6**: Desafios: ordenação, busca por prefixo, exportar TXT.

## Estrutura
- `src/Models/Contact.cs` — `struct` do contato.
- `src/Services/FileStorage.cs` — leitura/gravação CSV.
- `src/Program.cs` — menu e interface.
- `data/contatos.csv` — arquivo de dados.
