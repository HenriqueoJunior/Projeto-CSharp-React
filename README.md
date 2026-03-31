# Controle de Gastos

Sistema completo de controle de gastos residenciais. Backend em C# .NET e Frontend em React + TypeScript.

## Estrutura do Projeto

```
Projeto C# React API/
├── ControleGastos/              # Backend C# .NET
│   └── ControleGastos.API/
├── controle-gastos-frontend/    # Frontend React
│   └── client/src/
└── README.md                     # Este arquivo
```

## Como Rodar

### Backend (C# .NET)

1. **Navegue até a pasta do backend:**
```bash
cd "C:\Trabalho\Projeto C# React API\ControleGastos\ControleGastos.API"
```

2. **Execute o projeto:**
```bash
dotnet run
```

3. **Verifique se tá rodando:**
```
Now listening on: https://localhost:7096
Now listening on: http://localhost:5289
```

O backend vai criar um banco de dados SQLite automaticamente em `controle_gastos.db`.

### Frontend (React)

1. **Abra outro terminal e navegue até a pasta do frontend:**
```bash
cd "C:\Trabalho\Projeto C# React API\controle-gastos-frontend"
```

2. **Instale as dependências (primeira vez):**
```bash
pnpm install
```

Se não tiver pnpm instalado:
```bash
npm install -g pnpm
```

3. **Inicie o servidor:**
```bash
pnpm dev
```

4. **Abra no navegador:**
```
http://localhost:3000
```

## Configuração

### URL da API no Frontend

Se o backend estiver em outra porta, edite:
```
controle-gastos-frontend/client/src/lib/api.ts
```

Procure por:
```typescript
const API_BASE_URL = 'https://localhost:7096/api';
```

E mude a porta se necessário.

## Funcionalidades

### Backend
- CRUD de Pessoas (criar, ler, atualizar, deletar)
- CRUD de Categorias (criar, ler)
- CRUD de Transações (criar, ler)
- Relatórios de totais por pessoa e categoria
- Validações de negócio (menores só podem despesas, etc)

### Frontend
- Página inicial com instruções
- Gerenciamento de pessoas
- Gerenciamento de categorias
- Registro de transações
- Visualização de relatórios

## Testes da API

Tem um arquivo `ControleGastos.API.http` na pasta do backend com todas as requisições pra testar.

Abre no VS Code com a extensão REST Client e testa tudo lá.

## Resetar o Banco de Dados

Se quiser começar do zero, delete o arquivo:
```
C:\Trabalho\Projeto C# React API\ControleGastos\ControleGastos.API\controle_gastos.db
```

Na próxima vez q rodar o backend, um novo banco será criado vazio.

## Troubleshooting

### Backend não inicia
- Verifique se tem .NET 10.0 instalado
- Tente: `dotnet build` pra compilar

### Frontend não inicia
- Verifique se tem Node.js 22+ instalado
- Tente: `pnpm install` novamente
- Se a porta 3000 tá em uso: `pnpm dev -- --port 3001`

### Erro de CORS ou conexão recusada
- Verifique se o backend tá rodando em `https://localhost:7096`
- Verifique a URL em `client/src/lib/api.ts`

## Tecnologias

### Backend
- .NET 10.0
- Entity Framework Core
- SQLite

### Frontend
- React 19
- TypeScript
- Tailwind CSS
- shadcn/ui
- Axios
- Wouter

---

Qualquer dúvida, só chamar!
