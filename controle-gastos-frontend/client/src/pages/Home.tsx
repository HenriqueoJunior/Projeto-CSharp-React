import Layout from '@/components/Layout';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { Users, Tag, TrendingUp, BarChart3 } from 'lucide-react';
import { Link } from 'wouter';

export default function Home() {
  const features = [
    {
      title: 'Gerenciar Pessoas',
      description: 'Crie, edite e delete pessoas do sistema',
      icon: Users,
      href: '/pessoas',
      color: 'bg-blue-100 text-blue-600',
    },
    {
      title: 'Categorias',
      description: 'Organize suas transações por categorias',
      icon: Tag,
      href: '/categorias',
      color: 'bg-green-100 text-green-600',
    },
    {
      title: 'Transações',
      description: 'Registre receitas e despesas',
      icon: TrendingUp,
      href: '/transacoes',
      color: 'bg-purple-100 text-purple-600',
    },
    {
      title: 'Relatórios',
      description: 'Visualize totais e saldos',
      icon: BarChart3,
      href: '/relatorios',
      color: 'bg-orange-100 text-orange-600',
    },
  ];

  return (
    <Layout>
      <div className="space-y-8">
        <div>
          <h2 className="text-3xl font-bold text-foreground mb-2">Bem-vindo ao Controle de Gastos</h2>
          <p className="text-muted-foreground">
            Gerencie suas finanças pessoais com facilidade. Controle receitas, despesas e categorias.
          </p>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
          {features.map((feature) => {
            const Icon = feature.icon;
            return (
              <Link key={feature.href} href={feature.href}>
                <a>
                  <Card className="h-full hover:shadow-lg transition-shadow cursor-pointer">
                    <CardHeader>
                      <div className="flex items-center gap-4">
                        <div className={`p-3 rounded-lg ${feature.color}`}>
                          <Icon size={24} />
                        </div>
                        <div>
                          <CardTitle>{feature.title}</CardTitle>
                          <CardDescription>{feature.description}</CardDescription>
                        </div>
                      </div>
                    </CardHeader>
                  </Card>
                </a>
              </Link>
            );
          })}
        </div>

        <Card>
          <CardHeader>
            <CardTitle>Como usar</CardTitle>
          </CardHeader>
          <CardContent className="space-y-4">
            <div>
              <h3 className="font-semibold text-foreground mb-2">1. Cadastre Pessoas</h3>
              <p className="text-muted-foreground">
                Comece adicionando as pessoas que utilizarão o sistema. Cada pessoa pode ter múltiplas transações.
              </p>
            </div>
            <div>
              <h3 className="font-semibold text-foreground mb-2">2. Crie Categorias</h3>
              <p className="text-muted-foreground">
                Organize suas transações em categorias (Alimentação, Salário, etc.). Defina se é para despesa, receita ou ambas.
              </p>
            </div>
            <div>
              <h3 className="font-semibold text-foreground mb-2">3. Registre Transações</h3>
              <p className="text-muted-foreground">
                Adicione suas receitas e despesas. O sistema valida automaticamente as regras de negócio.
              </p>
            </div>
            <div>
              <h3 className="font-semibold text-foreground mb-2">4. Visualize Relatórios</h3>
              <p className="text-muted-foreground">
                Acompanhe totais por pessoa e categoria. Veja o saldo geral do seu sistema.
              </p>
            </div>
          </CardContent>
        </Card>
      </div>
    </Layout>
  );
}
