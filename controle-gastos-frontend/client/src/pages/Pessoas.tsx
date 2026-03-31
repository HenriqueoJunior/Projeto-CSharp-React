import { useState, useEffect } from 'react';
import Layout from '@/components/Layout';
import { Button } from '@/components/ui/button';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogTrigger } from '@/components/ui/dialog';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';
import { Trash2, Edit2, Plus, Loader2 } from 'lucide-react';
import { pessoasAPI, Pessoa } from '@/lib/api';
import { toast } from 'sonner';

export default function Pessoas() {
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [loading, setLoading] = useState(true);
  const [open, setOpen] = useState(false);
  const [editingId, setEditingId] = useState<string | null>(null);
  const [formData, setFormData] = useState({ nome: '', idade: '' });

  // Load pessoas on mount
  useEffect(() => {
    loadPessoas();
  }, []);

  const loadPessoas = async () => {
    try {
      setLoading(true);
      const response = await pessoasAPI.listar();
      setPessoas(response.data);
    } catch (error) {
      toast.error('Erro ao carregar pessoas');
      console.error(error);
    } finally {
      setLoading(false);
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!formData.nome.trim() || !formData.idade) {
      toast.error('Preencha todos os campos');
      return;
    }

    const idade = parseInt(formData.idade);
    if (idade < 0) {
      toast.error('Idade não pode ser negativa');
      return;
    }

    try {
      if (editingId) {
        await pessoasAPI.atualizar(editingId, {
          nome: formData.nome,
          idade,
        });
        toast.success('Pessoa atualizada com sucesso');
      } else {
        await pessoasAPI.criar({
          nome: formData.nome,
          idade,
        });
        toast.success('Pessoa criada com sucesso');
      }
      setFormData({ nome: '', idade: '' });
      setEditingId(null);
      setOpen(false);
      loadPessoas();
    } catch (error: any) {
      toast.error(error.response?.data || 'Erro ao salvar pessoa');
    }
  };

  const handleEdit = (pessoa: Pessoa) => {
    setFormData({ nome: pessoa.nome, idade: pessoa.idade.toString() });
    setEditingId(pessoa.id);
    setOpen(true);
  };

  const handleDelete = async (id: string) => {
    if (!confirm('Tem certeza que deseja deletar esta pessoa? Todas as suas transações serão removidas.')) {
      return;
    }

    try {
      await pessoasAPI.deletar(id);
      toast.success('Pessoa deletada com sucesso');
      loadPessoas();
    } catch (error: any) {
      toast.error(error.response?.data || 'Erro ao deletar pessoa');
    }
  };

  const handleOpenChange = (newOpen: boolean) => {
    if (!newOpen) {
      setFormData({ nome: '', idade: '' });
      setEditingId(null);
    }
    setOpen(newOpen);
  };

  return (
    <Layout>
      <div className="space-y-6">
        <div className="flex justify-between items-center">
          <h1 className="text-3xl font-bold text-foreground">Pessoas</h1>
          <Dialog open={open} onOpenChange={handleOpenChange}>
            <DialogTrigger asChild>
              <Button>
                <Plus size={20} className="mr-2" />
                Nova Pessoa
              </Button>
            </DialogTrigger>
            <DialogContent>
              <DialogHeader>
                <DialogTitle>
                  {editingId ? 'Editar Pessoa' : 'Nova Pessoa'}
                </DialogTitle>
              </DialogHeader>
              <form onSubmit={handleSubmit} className="space-y-4">
                <div>
                  <Label htmlFor="nome">Nome</Label>
                  <Input
                    id="nome"
                    value={formData.nome}
                    onChange={(e) => setFormData({ ...formData, nome: e.target.value })}
                    placeholder="Digite o nome"
                    maxLength={200}
                  />
                </div>
                <div>
                  <Label htmlFor="idade">Idade</Label>
                  <Input
                    id="idade"
                    type="number"
                    value={formData.idade}
                    onChange={(e) => setFormData({ ...formData, idade: e.target.value })}
                    placeholder="Digite a idade"
                    min="0"
                  />
                </div>
                <Button type="submit" className="w-full">
                  {editingId ? 'Atualizar' : 'Criar'}
                </Button>
              </form>
            </DialogContent>
          </Dialog>
        </div>

        {loading ? (
          <div className="flex justify-center items-center py-12">
            <Loader2 className="animate-spin text-primary" size={32} />
          </div>
        ) : pessoas.length === 0 ? (
          <Card>
            <CardContent className="py-12 text-center">
              <p className="text-muted-foreground">Nenhuma pessoa cadastrada</p>
            </CardContent>
          </Card>
        ) : (
          <div className="grid gap-4">
            {pessoas.map((pessoa) => (
              <Card key={pessoa.id}>
                <CardContent className="py-4">
                  <div className="flex justify-between items-center">
                    <div>
                      <h3 className="font-semibold text-foreground">{pessoa.nome}</h3>
                      <p className="text-sm text-muted-foreground">{pessoa.idade} anos</p>
                    </div>
                    <div className="flex gap-2">
                      <Button
                        variant="outline"
                        size="sm"
                        onClick={() => handleEdit(pessoa)}
                      >
                        <Edit2 size={16} />
                      </Button>
                      <Button
                        variant="destructive"
                        size="sm"
                        onClick={() => handleDelete(pessoa.id)}
                      >
                        <Trash2 size={16} />
                      </Button>
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
