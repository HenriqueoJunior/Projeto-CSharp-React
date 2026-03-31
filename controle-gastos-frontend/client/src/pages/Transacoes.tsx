import { useState, useEffect } from 'react';
import Layout from '@/components/Layout';
import { Button } from '@/components/ui/button';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogTrigger } from '@/components/ui/dialog';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select';
import { Plus, Loader2 } from 'lucide-react';
import { transacoesAPI, categoriasAPI, pessoasAPI, Transacao, Categoria, Pessoa } from '@/lib/api';
import { toast } from 'sonner';

export default function Transacoes() {
  const [transacoes, setTransacoes] = useState<Transacao[]>([]);
  const [categorias, setCategorias] = useState<Categoria[]>([]);
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [loading, setLoading] = useState(true);
  const [open, setOpen] = useState(false);
  const [tipo, setTipo] = useState('Despesa');
  const [formData, setFormData] = useState({
    descricao: '',
    valor: '',
    tipo: 'Despesa',
    categoriaId: '',
    pessoaId: '',
  });

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    try {
      setLoading(true);
      const [transResponse, catResponse, pesResponse] = await Promise.all([
        transacoesAPI.listar(),
        categoriasAPI.listar(),
        pessoasAPI.listar(),
      ]);
      setTransacoes(transResponse.data);
      setCategorias(catResponse.data);
      setPessoas(pesResponse.data);
    } catch (error) {
      toast.error('Erro ao carregar dados');
      console.error(error);
    } finally {
      setLoading(false);
    }
  };

  const handleTipoChange = (newTipo: string) => {
    setTipo(newTipo);
    setFormData({ ...formData, tipo: newTipo, categoriaId: '' });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!formData.descricao.trim() || !formData.valor || !formData.categoriaId || !formData.pessoaId) {
      toast.error('Preencha todos os campos');
      return;
    }

    const valor = parseFloat(formData.valor);
    if (valor <= 0) {
      toast.error('O valor deve ser positivo');
      return;
    }

    try {
      await transacoesAPI.criar({
        descricao: formData.descricao,
        valor,
        tipo: formData.tipo,
        categoriaId: formData.categoriaId,
        pessoaId: formData.pessoaId,
      });
      toast.success('Transação criada com sucesso');
      setFormData({
        descricao: '',
        valor: '',
        tipo: 'Despesa',
        categoriaId: '',
        pessoaId: '',
      });
      setOpen(false);
      loadData();
    } catch (error: any) {
      toast.error(error.response?.data || 'Erro ao criar transação');
    }
  };

  const getCategoriasDisponiveis = () => {
    return categorias.filter((cat) => {
      if (tipo === 'Despesa') {
        return cat.finalidade === 1 || cat.finalidade === 3;
      } else {
        return cat.finalidade === 2 || cat.finalidade === 3;
      }
    });
  };

  const getPessoaPermitida = (pessoa: Pessoa) => {
    if (tipo === 'Receita' && pessoa.idade < 18) {
      return false;
    }
    return true;
  };

  const formatCurrency = (value: number) => {
    return new Intl.NumberFormat('pt-BR', {
      style: 'currency',
      currency: 'BRL',
    }).format(value);
  };

  const getTransacaoColor = (tipo: any) => {
    return tipo === 1 ? 'text-red-600' : 'text-green-600';
  };

  return (
    <Layout>
      <div className="space-y-6">
        <div className="flex justify-between items-center">
          <h1 className="text-3xl font-bold text-foreground">Transações</h1>
          <Dialog open={open} onOpenChange={setOpen}>
            <DialogTrigger asChild>
              <Button>
                <Plus size={20} className="mr-2" />
                Nova Transação
              </Button>
            </DialogTrigger>
            <DialogContent className="max-w-md">
              <DialogHeader>
                <DialogTitle>Nova Transação</DialogTitle>
              </DialogHeader>
              <form onSubmit={handleSubmit} className="space-y-4">
                <div>
                  <Label htmlFor="tipo">Tipo</Label>
                  <Select value={tipo} onValueChange={handleTipoChange}>
                    <SelectTrigger id="tipo">
                      <SelectValue />
                    </SelectTrigger>
                    <SelectContent>
                      <SelectItem value="Despesa">Despesa</SelectItem>
                      <SelectItem value="Receita">Receita</SelectItem>
                    </SelectContent>
                  </Select>
                </div>

                <div>
                  <Label htmlFor="pessoa">Pessoa</Label>
                  <Select value={formData.pessoaId} onValueChange={(value) => setFormData({ ...formData, pessoaId: value })}>
                    <SelectTrigger id="pessoa">
                      <SelectValue placeholder="Selecione uma pessoa" />
                    </SelectTrigger>
                    <SelectContent>
                      {pessoas.map((pessoa) => (
                        <SelectItem key={pessoa.id} value={pessoa.id} disabled={!getPessoaPermitida(pessoa)}>
                          {pessoa.nome} {!getPessoaPermitida(pessoa) ? '(Menor - Apenas Despesas)' : ''}
                        </SelectItem>
                      ))}
                    </SelectContent>
                  </Select>
                </div>

                <div>
                  <Label htmlFor="categoria">Categoria</Label>
                  <Select value={formData.categoriaId} onValueChange={(value) => setFormData({ ...formData, categoriaId: value })}>
                    <SelectTrigger id="categoria">
                      <SelectValue placeholder="Selecione uma categoria" />
                    </SelectTrigger>
                    <SelectContent>
                      {getCategoriasDisponiveis().map((categoria) => (
                        <SelectItem key={categoria.id} value={categoria.id}>
                          {categoria.descricao}
                        </SelectItem>
                      ))}
                    </SelectContent>
                  </Select>
                </div>

                <div>
                  <Label htmlFor="descricao">Descrição</Label>
                  <Input
                    id="descricao"
                    value={formData.descricao}
                    onChange={(e) => setFormData({ ...formData, descricao: e.target.value })}
                    placeholder="Descrição da transação"
                    maxLength={400}
                  />
                </div>

                <div>
                  <Label htmlFor="valor">Valor</Label>
                  <Input
                    id="valor"
                    type="number"
                    step="0.01"
                    value={formData.valor}
                    onChange={(e) => setFormData({ ...formData, valor: e.target.value })}
                    placeholder="0.00"
                    min="0"
                  />
                </div>

                <Button type="submit" className="w-full">
                  Criar
                </Button>
              </form>
            </DialogContent>
          </Dialog>
        </div>

        {loading ? (
          <div className="flex justify-center items-center py-12">
            <Loader2 className="animate-spin text-primary" size={32} />
          </div>
        ) : transacoes.length === 0 ? (
          <Card>
            <CardContent className="py-12 text-center">
              <p className="text-muted-foreground">Nenhuma transação registrada</p>
            </CardContent>
          </Card>
        ) : (
          <div className="grid gap-4">
            {transacoes.map((transacao) => (
              <Card key={transacao.id}>
                <CardContent className="py-4">
                  <div className="flex justify-between items-start">
                    <div className="flex-1">
                      <h3 className="font-semibold text-foreground">{transacao.descricao}</h3>
                      <p className="text-sm text-muted-foreground">
                        {transacao.pessoa?.nome} • {transacao.categoria?.descricao}
                      </p>
                    </div>
                    <div className="text-right">
                      <p className={`font-bold text-lg ${getTransacaoColor(transacao.tipo)}`}>
                        {transacao.tipo === 1 ? '-' : '+'} {formatCurrency(transacao.valor)}
                      </p>
                      <p className="text-xs text-muted-foreground">{transacao.tipo === 1 ? 'Despesa' : 'Receita'}</p>
                    </div>
                  </div>
                </CardContent>
              </Card>
            ))}
          </div>
        )}
      </div>
    </Layout>
  );
}
