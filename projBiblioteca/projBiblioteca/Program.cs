using System;

namespace projBiblioteca
{
    internal class Program
    {
        static Livros livros = new Livros();

        static void Main(string[] args)
        {
            int op;
            do
            {
                Console.WriteLine();
                Console.WriteLine("===== BIBLIOTECA =====");
                Console.WriteLine("0. Sair");
                Console.WriteLine("1. Adicionar livro");
                Console.WriteLine("2. Pesquisar livro (sintético)");
                Console.WriteLine("3. Pesquisar livro (analítico)");
                Console.WriteLine("4. Adicionar exemplar");
                Console.WriteLine("5. Registrar empréstimo");
                Console.WriteLine("6. Registrar devolução");
                op = lerInt("Opção: ");

                switch (op)
                {
                    case 0: Console.WriteLine("Encerrando..."); break;
                    case 1: adicionarLivro(); break;
                    case 2: pesquisarSintetico(); break;
                    case 3: pesquisarAnalitico(); break;
                    case 4: adicionarExemplar(); break;
                    case 5: registrarEmprestimo(); break;
                    case 6: registrarDevolucao(); break;
                    default: Console.WriteLine("Opção inválida!"); break;
                }
            } while (op != 0);
        }

        static void adicionarLivro()
        {
            int isbn = lerInt("ISBN: ");
            if (livros.pesquisar(new Livro(isbn)) != null)
            {
                Console.WriteLine("Já existe livro com esse ISBN!");
                return;
            }
            Console.Write("Título: ");
            string titulo = Console.ReadLine();
            Console.Write("Autor: ");
            string autor = Console.ReadLine();
            Console.Write("Editora: ");
            string editora = Console.ReadLine();

            livros.adicionar(new Livro(isbn, titulo, autor, editora));
            Console.WriteLine("Livro adicionado.");
        }

        static void pesquisarSintetico()
        {
            Livro l = pedirLivro();
            if (l == null) return;
            mostraSintetico(l);
        }

        static void pesquisarAnalitico()
        {
            Livro l = pedirLivro();
            if (l == null) return;
            mostraSintetico(l);
            Console.WriteLine("--- Exemplares ---");
            if (l.qtdeExemplares() == 0)
                Console.WriteLine("(nenhum exemplar)");
            foreach (Exemplar e in l.Exemplares)
            {
                Console.WriteLine("  " + e);
                foreach (Emprestimo emp in e.Emprestimos)
                    Console.WriteLine("      " + emp);
            }
        }

        static void adicionarExemplar()
        {
            Livro l = pedirLivro();
            if (l == null) return;

            int tombo = lerInt("Tombo do novo exemplar: ");
            if (l.buscarExemplar(tombo) != null)
            {
                Console.WriteLine("Este livro já tem um exemplar com o tombo {0}!", tombo);
                return;
            }
            l.adicionarExemplar(new Exemplar(tombo));
            Console.WriteLine("Exemplar adicionado. Total: {0}", l.qtdeExemplares());
        }

        // Empresta o primeiro exemplar disponível do livro.
        static void registrarEmprestimo()
        {
            Livro l = pedirLivro();
            if (l == null) return;

            Exemplar e = l.primeiroDisponivel();
            if (e == null)
            {
                Console.WriteLine(l.qtdeExemplares() == 0
                    ? "O livro não tem exemplares cadastrados."
                    : "Nenhum exemplar disponível no momento.");
                return;
            }
            e.emprestar();
            Console.WriteLine("Empréstimo registrado (tombo {0}).", e.Tombo);
        }

        static void registrarDevolucao()
        {
            Livro l = pedirLivro();
            if (l == null) return;

            int tombo = lerInt("Tombo do exemplar devolvido: ");
            Exemplar e = l.buscarExemplar(tombo);
            if (e == null)
            {
                Console.WriteLine("Exemplar não encontrado neste livro!");
                return;
            }
            Console.WriteLine(e.devolver()
                ? "Devolução registrada."
                : "Este exemplar não estava emprestado!");
        }

        // ---------- auxiliares ----------
        static Livro pedirLivro()
        {
            int isbn = lerInt("ISBN: ");
            Livro l = livros.pesquisar(new Livro(isbn));
            if (l == null)
                Console.WriteLine("Livro não encontrado!");
            return l;
        }

        static void mostraSintetico(Livro l)
        {
            Console.WriteLine(l);
            Console.WriteLine("Exemplares: {0} | Disponíveis: {1} | Empréstimos: {2} | Disponibilidade: {3:F1}%",
                l.qtdeExemplares(), l.qtdeDisponiveis(), l.qtdeEmprestimos(), l.percDisponibilidade());
        }

        static int lerInt(string msg)
        {
            int v;
            Console.Write(msg);
            while (!int.TryParse(Console.ReadLine(), out v))
            {
                Console.Write("Valor inválido. " + msg);
            }
            return v;
        }
    }
}
