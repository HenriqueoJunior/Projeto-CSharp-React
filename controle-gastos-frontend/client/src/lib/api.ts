import axios from 'axios';

// API Base URL - Adjust based on environment
const API_BASE_URL = 'http://localhost:5289/api';

// Create axios instance
const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Types
export interface Pessoa {
  id: string;
  nome: string;
  idade: number;
}

export interface Categoria {
  id: string;
  descricao: string;
  finalidade: 1 | 2 | 3;
}

export interface Transacao {
  id: string;
  descricao: string;
  valor: number;
  tipo: 1 | 2;
  categoriaId: string;
  pessoaId: string;
  categoria?: Categoria;
  pessoa?: Pessoa;
}

export interface TotalPorPessoa {
  pessoaId: string;
  nome: string;
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
}

export interface RelatorioTotaisPessoas {
  pessoas: TotalPorPessoa[];
  totalGeralReceitas: number;
  totalGeralDespesas: number;
  saldoGeralLiquido: number;
}

export interface TotalPorCategoria {
  categoriaId: string;
  descricao: string;
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
}

export interface RelatorioTotaisCategorias {
  categorias: TotalPorCategoria[];
  totalGeralReceitas: number;
  totalGeralDespesas: number;
  saldoGeralLiquido: number;
}

const finalidadeMap: Record<string, number> = {
  Despesa: 1,
  Receita: 2, 
  Ambas: 3,
};

const transacaoMap: Record<string, number> = {
  Despesa: 1,
  Receita: 2,
};

// Pessoas API
export const pessoasAPI = {
  listar: () => api.get<Pessoa[]>('/pessoas'),
  buscarPorId: (id: string) => api.get<Pessoa>(`/pessoas/${id}`),
  criar: (data: { nome: string; idade: number }) => api.post<Pessoa>('/pessoas', data),
  atualizar: (id: string, data: { nome: string; idade: number }) => api.put<Pessoa>(`/pessoas/${id}`, data),
  deletar: (id: string) => api.delete(`/pessoas/${id}`),
};

// Categorias API
export const categoriasAPI = {
  listar: () => api.get<Categoria[]>('/categorias'),

  buscarPorId: (id: string) => api.get<Categoria>(`/categorias/${id}`),

  criar: (data: { descricao: string; finalidade: string }) =>
    api.post<Categoria>('/categorias', {
      ...data,
      finalidade: finalidadeMap[data.finalidade],
    }),
};

// Transações API
export const transacoesAPI = {
  listar: () => api.get<Transacao[]>('/transacoes'),
  buscarPorId: (id: string) => api.get<Transacao>(`/transacoes/${id}`),
  criar: (data: {
    descricao: string;
    valor: number;
    tipo: string;
    categoriaId: string;
    pessoaId: string;
  }) => api.post<Transacao>('/transacoes', {
    ...data,
    tipo: transacaoMap[data.tipo]
  }),
};

// Relatórios API
export const relatoriosAPI = {
  obterTotaisPorPessoa: () => api.get<RelatorioTotaisPessoas>('/relatorios/pessoas'),
  obterTotaisPorCategoria: () => api.get<RelatorioTotaisCategorias>('/relatorios/categorias'),
};

export default api;
