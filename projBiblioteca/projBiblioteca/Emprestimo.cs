using System;

namespace projBiblioteca
{
    internal class Emprestimo
    {
        private DateTime dtEmprestimo;
        private DateTime dtDevolucao;   // DateTime.MinValue = ainda não devolvido

        public DateTime DtEmprestimo { get => dtEmprestimo; set => dtEmprestimo = value; }
        public DateTime DtDevolucao { get => dtDevolucao; set => dtDevolucao = value; }
        public bool EmAberto { get => dtDevolucao == DateTime.MinValue; }

        public Emprestimo(DateTime dtEmprestimo)
        {
            this.dtEmprestimo = dtEmprestimo;
            this.dtDevolucao = DateTime.MinValue;
        }

        public Emprestimo() : this(DateTime.Now)
        { }

        public override string ToString()
        {
            return string.Format("Empréstimo em {0:dd/MM/yyyy HH:mm} - {1}",
                dtEmprestimo,
                EmAberto ? "ainda não devolvido" : "devolvido em " + dtDevolucao.ToString("dd/MM/yyyy HH:mm"));
        }
    }
}
