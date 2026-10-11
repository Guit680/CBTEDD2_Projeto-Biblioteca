using System;
using System.Collections.Generic;

namespace projBiblioteca
{
    internal class Exemplar
    {
        private int tombo;
        private List<Emprestimo> emprestimos = new List<Emprestimo>();

        public int Tombo { get => tombo; set => tombo = value; }
        public List<Emprestimo> Emprestimos { get => emprestimos; }

        public Exemplar(int tombo)
        {
            this.tombo = tombo;
        }

        public Exemplar() : this(0)
        { }

        // Disponível = não há empréstimo em aberto.
        public bool disponivel()
        {
            foreach (Emprestimo e in emprestimos)
                if (e.EmAberto)
                    return false;
            return true;
        }

        // Registra novo empréstimo; false se já estiver emprestado.
        public bool emprestar()
        {
            if (!disponivel())
                return false;
            emprestimos.Add(new Emprestimo(DateTime.Now));
            return true;
        }

        // Fecha o empréstimo em aberto; false se não estava emprestado.
        public bool devolver()
        {
            foreach (Emprestimo e in emprestimos)
            {
                if (e.EmAberto)
                {
                    e.DtDevolucao = DateTime.Now;
                    return true;
                }
            }
            return false;
        }

        public int qtdeEmprestimos()
        {
            return emprestimos.Count;
        }

        public override string ToString()
        {
            return string.Format("Tombo {0} - {1}", tombo, disponivel() ? "disponível" : "emprestado");
        }

        public override bool Equals(object obj)
        {
            Exemplar outro = obj as Exemplar;
            return outro != null && this.tombo == outro.tombo;
        }

        public override int GetHashCode()
        {
            return tombo.GetHashCode();
        }
    }
}
