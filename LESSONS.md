# Plano de Aulas — Nível Básico (C# Console + Arquivos)

## Aula 1 — Introdução e Struct
- Objetivo: entender `Main`, `Console.ReadLine/WriteLine`, tipos básicos e `struct`.
- Atividade:
  1. `dotnet new console` e Hello World.
  2. Criar `struct Contact { Id, Nome, Telefone, Email }` (record struct).
  3. Criar 2 contatos em memória e imprimir.

## Aula 2 — Arquivos (CSV)
- Objetivo: ler e gravar texto.
- Conceitos: `File`, `StreamWriter`, `Encoding.UTF8`, separador `;`.
- Atividade:
  1. Criar pasta `data/` e arquivo `contatos.csv` com cabeçalho.
  2. Gravar 2 linhas.
  3. Ler e exibir.

## Aula 3 — CRUD (Criar, Listar, Buscar)
- Objetivo: construir menu e funções.
- Atividade: implementar opções 1–3 do menu.

## Aula 4 — Atualizar e Excluir
- Objetivo: localizar por ID e editar/remover.
- Atividade: opções 4–5, validações simples, mensagens de erro amigáveis.

## Aula 5 — Serviços e Persistência
- Objetivo: separar responsabilidades.
- Atividade: mover leitura/gravação para `FileStorage`; adicionar opção 6 (Salvar) e 7 (Backup).

## Aula 6 — Desafios e Revisão
- Objetivo: consolidar conhecimento.
- Desafios sugeridos:
  - Ordenar por Nome/Email com opção do usuário.
  - Buscar por prefixo (começa com).
  - Exportar TXT com um relatório de contatos.
  - Validação básica de email (contém '@').
  - Contador de backups e limpar backups antigos.
- Apresentação final: cada grupo demonstra seu CRUD.

## Avaliação (sugestão)
- Funcionalidades (40%): CRUD completo e persistência.
- Qualidade (30%): organização, nomes, tratamento de erros.
- Usabilidade (20%): mensagens claras, menu.
- Extra (10%): desafios implementados.
