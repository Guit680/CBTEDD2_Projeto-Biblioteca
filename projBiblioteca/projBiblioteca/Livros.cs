using System;
using System.Collections.Generic;

namespace projBiblioteca
{
    internal class Livros
    {
        private List<Livro> acervo = new List<Livro>();

        public void adicionar(Livro livro)
        {
            acervo.Add(livro);
        }

        // Busca pelo ISBN (Equals de Livro). Retorna null se não achar.
        public Livro pesquisar(Livro livro)
        {
            int i = acervo.IndexOf(livro);
            return i >= 0 ? acervo[i] : null;
        }
    }
}
