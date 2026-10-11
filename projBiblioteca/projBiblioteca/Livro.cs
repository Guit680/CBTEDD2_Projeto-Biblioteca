using System;
using System.Collections.Generic;

namespace projBiblioteca
{
    internal class Livro
    {
        private int isbn;
        private string titulo;
        private string autor;
        private string editora;
        private List<Exemplar> exemplares = new List<Exemplar>();

        public int Isbn { get => isbn; set => isbn = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public string Autor { get => autor; set => autor = value; }
        public string Editora { get => editora; set => editora = value; }
        public List<Exemplar> Exemplares { get => exemplares; }

        public Livro(int isbn, string titulo, string autor, string editora)
        {
            this.isbn = isbn;
            this.titulo = titulo;
            this.autor = autor;
            this.editora = editora;
        }

        public Livro(int isbn) : this(isbn, "", "", "")
        { }

        public Livro() : this(0, "", "", "")
        { }

        public void adicionarExemplar(Exemplar exemplar)
        {
            exemplares.Add(exemplar);
        }

        public int qtdeExemplares()
        {
            return exemplares.Count;
        }

        public int qtdeDisponiveis()
        {
            int n = 0;
            foreach (Exemplar e in exemplares)
                if (e.disponivel())
                    n++;
            return n;
        }

        public int qtdeEmprestimos()
        {
            int n = 0;
            foreach (Exemplar e in exemplares)
                n += e.qtdeEmprestimos();
            return n;
        }

        // Percentual (0 a 100) de exemplares disponíveis; 0 se não há exemplares.
        public double percDisponibilidade()
        {
            if (exemplares.Count == 0)
                return 0;
            return 100.0 * qtdeDisponiveis() / exemplares.Count;
        }

        // Auxiliares (não constam no diagrama)
        public Exemplar buscarExemplar(int tombo)
        {
            int i = exemplares.IndexOf(new Exemplar(tombo));
            return i >= 0 ? exemplares[i] : null;
        }

        public Exemplar primeiroDisponivel()
        {
            foreach (Exemplar e in exemplares)
                if (e.disponivel())
                    return e;
            return null;
        }

        public override string ToString()
        {
            return string.Format("ISBN {0} - \"{1}\" - {2} - {3}", isbn, titulo, autor, editora);
        }

        public override bool Equals(object obj)
        {
            Livro outro = obj as Livro;
            return outro != null && this.isbn == outro.isbn;
        }

        public override int GetHashCode()
        {
            return isbn.GetHashCode();
        }
    }
}
