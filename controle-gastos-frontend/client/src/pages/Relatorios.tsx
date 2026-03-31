import { useState, useEffect } from 'react';
import Layout from '@/components/Layout';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Tabs, TabsContent, TabsList, TabsTrigger } from '@/components/ui/tabs';
import { Loader2 } from 'lucide-react';
import { relatoriosAPI, RelatorioTotaisPessoas, RelatorioTotaisCategorias } from '@/lib/api';
import { toast } from 'sonner';

export default function Relatorios() {
  const [relatoriosPessoas, setRelatoriosPessoas] = useState<RelatorioTotaisPessoas | null>(null);
  const [relatoriosCategorias, setRelatoriosCategorias] = useState<RelatorioTotaisCategorias | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    loadRelatorios();
  }, []);

  const loadRelatorios = async () => {
    try {
      setLoading(true);
      const [pessoasRes, categoriasRes] = await Promise.all([
        relatoriosAPI.obterTotaisPorPessoa(),
        relatoriosAPI.obterTotaisPorCategoria(),
      ]);
      setRelatoriosPessoas(pessoasRes.data);
      setRelatoriosCategorias(categoriasRes.data);
    } catch (error) {
      toast.error('Erro ao carregar relatórios');
      console.error(error);
    } finally {
      setLoading(false);
    }
  };

  const formatCurrency = (value: number) => {
    return new Intl.NumberFormat('pt-BR', {
      style: 'currency',
      currency: 'BRL',
    }).format(value);
  };

  const getSaldoColor = (saldo: number) => {
    if (saldo > 0) return 'text-green-600';
    if (saldo < 0) return 'text-red-600';
    return 'text-muted-foreground';
  };

  return (
    <Layout>
      <div className="space-y-6">
        <h1 className="text-3xl font-bold text-foreground">Relatórios</h1>

        {loading ? (
          <div className="flex justify-center items-center py-12">
            <Loader2 className="animate-spin text-primary" size={32} />
          </div>
        ) : (
          <Tabs defaultValue="pessoas" className="w-full">
            <TabsList>
              <TabsTrigger value="pessoas">Por Pessoa</TabsTrigger>
              <TabsTrigger value="categorias">Por Categoria</TabsTrigger>
            </TabsList>

            {/* Relatório por Pessoa */}
            <TabsContent value="pessoas" className="space-y-4">
              {relatoriosPessoas && (
                <>
                  {relatoriosPessoas.pessoas.length === 0 ? (
                    <Card>
                      <CardContent className="py-12 text-center">
                        <p className="text-muted-foreground">Nenhum dado disponível</p>
                      </CardContent>
                    </Card>
                  ) : (
                    <>
                      <div className="grid gap-4">
                        {relatoriosPessoas.pessoas.map((pessoa) => (
                          <Card key={pessoa.pessoaId}>
                            <CardContent className="py-4">
                              <div className="space-y-3">
                                <h3 className="font-semibold text-foreground text-lg">{pessoa.nome}</h3>
                                <div className="grid grid-cols-3 gap-4">
                                  <div>
                                    <p className="text-sm text-muted-foreground">Receitas</p>
                                    <p className="text-lg font-semibold text-green-600">
                                      {formatCurrency(pessoa.totalReceitas)}
                                    </p>
                                  </div>
                                  <div>
                                    <p className="text-sm text-muted-foreground">Despesas</p>
                                    <p className="text-lg font-semibold text-red-600">
                                      -{formatCurrency(pessoa.totalDespesas)}
                                    </p>
                                  </div>
                                  <div>
                                    <p className="text-sm text-muted-foreground">Saldo</p>
                                    <p className={`text-lg font-semibold ${getSaldoColor(pessoa.saldo)}`}>
                                      {formatCurrency(pessoa.saldo)}
                                    </p>
                                  </div>
                                </div>
                              </div>
                            </CardContent>
                          </Card>
                        ))}
                      </div>

                      {/* Total Geral */}
                      <Card className="bg-primary/10 border-primary/20">
                        <CardHeader>
                          <CardTitle className="text-primary">Total Geral</CardTitle>
                        </CardHeader>
                        <CardContent>
                          <div className="grid grid-cols-3 gap-4">
                            <div>
                              <p className="text-sm text-muted-foreground">Total Receitas</p>
                              <p className="text-2xl font-bold text-green-600">
                                {formatCurrency(relatoriosPessoas.totalGeralReceitas)}
                              </p>
                            </div>
                            <div>
                              <p className="text-sm text-muted-foreground">Total Despesas</p>
                              <p className="text-2xl font-bold text-red-600">
                                -{formatCurrency(relatoriosPessoas.totalGeralDespesas)}
                              </p>
                            </div>
                            <div>
                              <p className="text-sm text-muted-foreground">Saldo Líquido</p>
                              <p className={`text-2xl font-bold ${getSaldoColor(relatoriosPessoas.saldoGeralLiquido)}`}>
                                {formatCurrency(relatoriosPessoas.saldoGeralLiquido)}
                              </p>
                            </div>
                          </div>
                        </CardContent>
                      </Card>
                    </>
                  )}
                </>
              )}
            </TabsContent>

            {/* Relatório por Categoria */}
            <TabsContent value="categorias" className="space-y-4">
              {relatoriosCategorias && (
                <>
                  {relatoriosCategorias.categorias.length === 0 ? (
                    <Card>
                      <CardContent className="py-12 text-center">
                        <p className="text-muted-foreground">Nenhum dado disponível</p>
                      </CardContent>
                    </Card>
                  ) : (
                    <>
                      <div className="grid gap-4">
                        {relatoriosCategorias.categorias.map((categoria) => (
                          <Card key={categoria.categoriaId}>
                            <CardContent className="py-4">
                              <div className="space-y-3">
                                <h3 className="font-semibold text-foreground text-lg">{categoria.descricao}</h3>
                                <div className="grid grid-cols-3 gap-4">
                                  <div>
                                    <p className="text-sm text-muted-foreground">Receitas</p>
                                    <p className="text-lg font-semibold text-green-600">
                                      {formatCurrency(categoria.totalReceitas)}
                                    </p>
                                  </div>
                                  <div>
                                    <p className="text-sm text-muted-foreground">Despesas</p>
                                    <p className="text-lg font-semibold text-red-600">
                                      -{formatCurrency(categoria.totalDespesas)}
                                    </p>
                                  </div>
                                  <div>
                                    <p className="text-sm text-muted-foreground">Saldo</p>
                                    <p className={`text-lg font-semibold ${getSaldoColor(categoria.saldo)}`}>
                                      {formatCurrency(categoria.saldo)}
                                    </p>
                                  </div>
                                </div>
                              </div>
                            </CardContent>
                          </Card>
                        ))}
                      </div>

                      {/* Total Geral */}
                      <Card className="bg-primary/10 border-primary/20">
                        <CardHeader>
                          <CardTitle className="text-primary">Total Geral</CardTitle>
                        </CardHeader>
                        <CardContent>
                          <div className="grid grid-cols-3 gap-4">
                            <div>
                              <p className="text-sm text-muted-foreground">Total Receitas</p>
                              <p className="text-2xl font-bold text-green-600">
                                {formatCurrency(relatoriosCategorias.totalGeralReceitas)}
                              </p>
                            </div>
                            <div>
                              <p className="text-sm text-muted-foreground">Total Despesas</p>
                              <p className="text-2xl font-bold text-red-600">
                                -{formatCurrency(relatoriosCategorias.totalGeralDespesas)}
                              </p>
                            </div>
                            <div>
                              <p className="text-sm text-muted-foreground">Saldo Líquido</p>
                              <p className={`text-2xl font-bold ${getSaldoColor(relatoriosCategorias.saldoGeralLiquido)}`}>
                                {formatCurrency(relatoriosCategorias.saldoGeralLiquido)}
                              </p>
                            </div>
                          </div>
                        </CardContent>
                      </Card>
                    </>
                  )}
                </>
              )}
            </TabsContent>
          </Tabs>
        )}
      </div>
    </Layout>
  );
}
