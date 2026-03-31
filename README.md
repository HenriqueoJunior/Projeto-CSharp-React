# Sistema de Controle de Gastos Residenciais

Um sistema completo de controle de gastos desenvolvido com **C# .NET** no backend e **React com TypeScript** no frontend. O sistema permite gerenciar pessoas, categorias, transações e visualizar relatórios de receitas e despesas.

## 📋 Funcionalidades Implementadas

### ✅ Backend (C# .NET)

- **Cadastro de Pessoas**: CRUD completo com validações
  - Criação, leitura, atualização e deleção
  - Exclusão em cascata de transações ao deletar pessoa
  - Validação de nome (máximo 200 caracteres) e idade

- **Cadastro de Categorias**: Criação e listagem
  - Descrição (máximo 400 caracteres)
  - Finalidade: Despesa, Receita ou Ambas
  - Validação de enum

- **Cadastro de Transações**: Criação e listagem
  - Descrição (máximo 400 caracteres)
  - Valor (número positivo)
  - Tipo: Despesa ou Receita
  - Validação: Menores de 18 anos só podem registrar despesas
  - Validação: Categoria deve ser compatível com o tipo de transação

- **Relatórios**: Consulta de totais
  - Totais por pessoa (receitas, despesas, saldo)
  - Totais por categoria (receitas, despesas, saldo)
  - Total geral do sistema

- **CORS**: Configurado para aceitar requisições do frontend

### ✅ Frontend (React + TypeScript)

- **Layout Principal**: Navegação com sidebar
- **Página de Pessoas**: CRUD com modal de criação/edição
- **Página de Categorias**: Criação e listagem com badges de finalidade
- **Página de Transações**: Criação com validações e listagem
- **Página de Relatórios**: Visualização de totais por pessoa e categoria
- **Home**: Página inicial com instruções de uso
- **Integração com API**: Todas as páginas conectadas ao backend

## 🛠️ Tecnologias Utilizadas

### Backend
- **.NET 10.0**
- **Entity Framework Core 10.0.5**
- **SQLite**
- **ASP.NET Core Web API**

### Frontend
- **React 19**
- **TypeScript 5.6**
- **Tailwind CSS 4**
- **shadcn/ui**
- **Axios**
- **Wouter** (routing)
- **Sonner** (toasts)

## 📁 Estrutura do Projeto

### Backend
```
ControleGastos/
├── ControleGastos.API/
│   ├── Controllers/
│   │   ├── PessoasController.cs
│   │   ├── CategoriasController.cs
│   │   ├── TransacoesController.cs
│   │   └── RelatoriosController.cs
│   ├── Models/
│   │   ├── Pessoa.cs
│   │   ├── Categoria.cs
│   │   ├── Transacao.cs
│   │   └── Enums/
│   ├── Services/
│   │   ├── PessoaService/
│   │   ├── CategoriaService/
│   │   ├── TransacaoService/
│   │   └── RelatorioService/
│   ├── DTOs/
│   │   ├── PessoaDTO.cs
│   │   ├── CategoriaDTO.cs
│   │   ├── TransacaoDTO.cs
│   │   └── RelatorioTotaisDTO.cs
│   ├── Data/
│   │   └── AppDbContext.cs
│   └── Program.cs
```

### Frontend
```
controle-gastos-frontend/
├── client/
│   ├── src/
│   │   ├── pages/
│   │   │   ├── Home.tsx
│   │   │   ├── Pessoas.tsx
│   │   │   ├── Categorias.tsx
│   │   │   ├── Transacoes.tsx
│   │   │   ├── Relatorios.tsx
│   │   │   └── NotFound.tsx
│   │   ├── components/
│   │   │   ├── Layout.tsx
│   │   │   └── ui/ (shadcn/ui components)
│   │   ├── lib/
│   │   │   └── api.ts
│   │   ├── hooks/
│   │   │   └── useApi.ts
│   │   ├── App.tsx
│   │   └── index.css
│   ├── index.html
│   └── public/
```

## 🚀 Como Executar

### Pré-requisitos
- .NET 10.0 SDK
- Node.js 22+
- npm ou pnpm

### Backend

1. Navegue para a pasta do backend:
```bash
cd "C:\Trabalho\Projeto C# React API\ControleGastos\ControleGastos.API"
```

2. Execute a aplicação:
```bash
dotnet run
```

A API estará disponível em:
- HTTP: `http://localhost:5289`
- HTTPS: `https://localhost:7096`

O banco de dados SQLite será criado automaticamente na raiz do projeto como `controle_gastos.db`.

### Frontend

1. Navegue para a pasta do frontend:
```bash
cd controle-gastos-frontend
```

2. Instale as dependências:
```bash
pnpm install
```

3. Inicie o servidor de desenvolvimento:
```bash
pnpm dev
```

O frontend estará disponível em: `http://localhost:3000`

## 📡 Endpoints da API

### Pessoas
- `GET /api/pessoas` - Listar todas as pessoas
- `GET /api/pessoas/{id}` - Buscar pessoa por ID
- `POST /api/pessoas` - Criar nova pessoa
- `PUT /api/pessoas/{id}` - Atualizar pessoa
- `DELETE /api/pessoas/{id}` - Deletar pessoa

### Categorias
- `GET /api/categorias` - Listar todas as categorias
- `GET /api/categorias/{id}` - Buscar categoria por ID
- `POST /api/categorias` - Criar nova categoria

### Transações
- `GET /api/transacoes` - Listar todas as transações
- `GET /api/transacoes/{id}` - Buscar transação por ID
- `POST /api/transacoes` - Criar nova transação

### Relatórios
- `GET /api/relatorios/pessoas` - Obter totais por pessoa
- `GET /api/relatorios/categorias` - Obter totais por categoria

## 🔍 Exemplos de Requisições

### Criar Pessoa
```bash
POST https://localhost:7096/api/pessoas
Content-Type: application/json

{
  "nome": "João Silva",
  "idade": 30
}
```

### Criar Categoria
```bash
POST https://localhost:7096/api/categorias
Content-Type: application/json

{
  "descricao": "Alimentação",
  "finalidade": "Ambas"
}
```

### Criar Transação
```bash
POST https://localhost:7096/api/transacoes
Content-Type: application/json

{
  "descricao": "Compra no supermercado",
  "valor": 150.50,
  "tipo": "Despesa",
  "categoriaId": "00000000-0000-0000-0000-000000000001",
  "pessoaId": "00000000-0000-0000-0000-000000000001"
}
```

## ✅ Regras de Negócio Implementadas

1. **Menores de idade (< 18 anos)**: Só podem registrar despesas
2. **Compatibilidade de Categoria**: A categoria deve aceitar o tipo de transação
   - Se a transação é "Despesa", a categoria deve ter finalidade "Despesa" ou "Ambas"
   - Se a transação é "Receita", a categoria deve ter finalidade "Receita" ou "Ambas"
3. **Exclusão em Cascata**: Ao deletar uma pessoa, todas as suas transações são removidas
4. **Validações de Tamanho**:
   - Nome da pessoa: máximo 200 caracteres
   - Descrição da categoria: máximo 400 caracteres
   - Descrição da transação: máximo 400 caracteres
5. **Valores Positivos**: Transações devem ter valores maiores que zero

## 🧪 Testando o Sistema

### Fluxo Básico de Teste

1. **Criar Pessoas**:
   - Acesse "Pessoas"
   - Clique em "Nova Pessoa"
   - Preencha nome e idade
   - Clique em "Criar"

2. **Criar Categorias**:
   - Acesse "Categorias"
   - Clique em "Nova Categoria"
   - Preencha descrição e selecione finalidade
   - Clique em "Criar"

3. **Criar Transações**:
   - Acesse "Transações"
   - Clique em "Nova Transação"
   - Selecione tipo (Despesa/Receita)
   - Selecione pessoa (note: menores não podem ter receitas)
   - Selecione categoria (apenas categorias compatíveis aparecem)
   - Preencha descrição e valor
   - Clique em "Criar"

4. **Visualizar Relatórios**:
   - Acesse "Relatórios"
   - Veja totais por pessoa ou categoria
   - Visualize o saldo geral

## 📝 Comentários no Código

Todo o código possui comentários explicativos em português:
- **Backend**: Comentários em classes, métodos e regras de negócio
- **Frontend**: Comentários em componentes e funções principais

## 🔐 Configuração de Banco de Dados

O projeto usa SQLite com arquivo local `controle_gastos.db`. Para usar outro banco:

1. Abra `Program.cs`
2. Modifique a string de conexão em `AddDbContext`
3. Instale o provider apropriado (ex: `Microsoft.EntityFrameworkCore.SqlServer`)

## 📦 Deployment

### Backend
1. Publique a aplicação: `dotnet publish -c Release`
2. Configure as variáveis de ambiente
3. Execute em um servidor (IIS, Docker, etc)

### Frontend
1. Execute: `pnpm build`
2. Faça upload dos arquivos em `dist/` para um servidor web
3. Configure a URL da API em `client/src/lib/api.ts`

## 🐛 Troubleshooting

### CORS Error
Se receber erro de CORS, verifique se o backend tem CORS configurado em `Program.cs`.

### Conexão Recusada
Certifique-se de que o backend está rodando na porta correta (5289 ou 7096).

### Banco de Dados Não Criado
O banco é criado automaticamente na primeira execução. Se houver erro, verifique permissões de escrita.

## 📄 Licença

Este projeto foi desenvolvido como teste técnico.

## 👨‍💻 Autor

Desenvolvido como solução completa do teste técnico de Controle de Gastos.
