using System;

namespace SistemaBiblioteca
{
    class Program
    {
        static void Main(string[] args)
        {
            // Estado do sistema (tipos de valor primitivos e constantes), regras da biblioteca e variáveis de estado
            int emprestimosAtivos = 0;
            const int LIMITE_EMPRESTIMOS = 3;
            const int PRAZO_DEVOLUCAO_DIAS = 14;
            decimal valorMultaDiaria = 2.50m;
            bool podeEmprestar = true;

            int opcao = -1; // sentinela

            do
            {
                Console.WriteLine("\n=== SISTEMA DE GESTÃO DE BIBLIOTECA (STANDALONE TP1) ===");
                Console.WriteLine("1 - Simular Empréstimo de Exemplar");
                Console.WriteLine("2 - Processar Devolução e Calcular Multa");
                Console.WriteLine("3 - Consultar Status do Membro");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");

                // aceita valor string ou nulo
                string? entradaOpcao = Console.ReadLine();

                // validação de entrada com operador de negação (!) e TryParse; segue o fluxo de exceção
                if (!int.TryParse(entradaOpcao, out opcao))
                {
                    Console.WriteLine("\n[ERRO]: Opção inválida! Digite apenas números inteiros.");
                    opcao = -1; // garante que o loop não encerre, pois o TryParse zera 'opcao' quando falha
                    continue;
                }

                // regra booleana de confirmação antes de processar as opções
                podeEmprestar = emprestimosAtivos < LIMITE_EMPRESTIMOS;

                switch (opcao)
                {
                    case 1:
                        Console.WriteLine("\n--- SIMULAÇÃO DE EMPRÉSTIMO ---");
                        if (!podeEmprestar)
                        {
                            Console.WriteLine($"[BLOQUEADO]: O membro atingiu o limite máximo de {LIMITE_EMPRESTIMOS} empréstimos simultâneos.");
                        }
                        else
                        {
                            emprestimosAtivos++;
                            Console.WriteLine($"[SUCESSO]: Empréstimo registrado! Prazo: {PRAZO_DEVOLUCAO_DIAS} dias.");
                            Console.WriteLine($"Total de empréstimos ativos do membro: {emprestimosAtivos}/{LIMITE_EMPRESTIMOS}");
                        }
                        break;

                    case 2:
                        Console.WriteLine("\n--- PROCESSAR DEVOLUÇÃO ---");
                        if (emprestimosAtivos == 0)
                        {
                            Console.WriteLine("[AVISO]: O membro não possui nenhum empréstimo ativo para devolver.");
                            break;
                        }

                        Console.Write("Informe os dias de atraso na devolução (0 para no prazo): ");
                        string? entradaDias = Console.ReadLine();

                        if (!int.TryParse(entradaDias, out int diasAtraso) || diasAtraso < 0)
                        {
                            Console.WriteLine("[ERRO]: Quantidade de dias inválida. Informe um valor inteiro maior ou igual a zero.");
                        }
                        else
                        {
                            emprestimosAtivos--;
                            if (diasAtraso > 0)
                            {
                                decimal totalMulta = diasAtraso * valorMultaDiaria;
                                Console.WriteLine($"[DEVOLUÇÃO COM ATRASO]: {diasAtraso} dia(s) de atraso. Multa gerada: R$ {totalMulta:F2}");
                            }
                            else
                            {
                                Console.WriteLine("[DEVOLUÇÃO NO PRAZO]: Exemplar devolvido com sucesso! Nenhuma multa aplicada.");
                            }
                        }
                        break;

                    case 3:
                        Console.WriteLine("\n--- STATUS DO MEMBRO ---");
                        podeEmprestar = emprestimosAtivos < LIMITE_EMPRESTIMOS;
                        Console.WriteLine($"Empréstimos Ativos: {emprestimosAtivos}");
                        Console.WriteLine($"Vagas Restantes: {LIMITE_EMPRESTIMOS - emprestimosAtivos}");
                        Console.WriteLine($"Liberado para novos empréstimos: {(podeEmprestar ? "Sim" : "Não")}");
                        break;

                    case 0:
                        Console.WriteLine("\nEncerrando o sistema de registro.");
                        break;

                    default:
                        Console.WriteLine("\n[ERRO]: Opção não encontrada no menu.");
                        break;
                }

            } while (opcao != 0);
        }
    }
}
