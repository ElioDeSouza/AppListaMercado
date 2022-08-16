using AppListaMercado.Model;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppListaMercado.Helper
{
    /**
     * Definição da classe SQLiteDatabaseHelper que funciona como uma abstração de acesso ao arquivo db3
     * do SQLite. A classe contém as informações de "conexão" e os métodos para realizar o CRUD (Create,
     * Read, Update e Delete).
     * Observe que na classe todos os métodos são Async, isso significa que todos são executados via Threads
     * o que, em teoria) não trava a interface do app enquanto os dados são lidos/gravados no arquivo db3.
     */
    public class SQLiteDatabaseHelper
    {
        /**
         * Campo da classe que armazena a "conexão" com o arquivo db3.
         * Isso siginifica que o arquivo db3 é aberto e armazenado aqui para que
         * essa classe possa usar os métodos da classe do SQLite para gravar
         * e ler dados do arquivo.
         */
        readonly SQLiteAsyncConnection _conn;


        /**
         * Método construtor da classe que recebe um parâmetro chamado path para
         * "conectar" ao arquivo db3.
         */
        public SQLiteDatabaseHelper(string path)
        {
            
            _conn = new SQLiteAsyncConnection(path);

            _conn.CreateTableAsync<Produto>().Wait();
        }


      
        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p);
        }


        
        public Task<List<Produto>> Update(Produto p)
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE id= ? ";
            return _conn.QueryAsync<Produto>(sql, p.Descricao, p.Quantidade, p.Preco, p.Id);
        }


        
        public Task<List<Produto>> GetAll()
        {
            return _conn.Table<Produto>().ToListAsync();
        }


       
        public Task<int> Delete(int id)
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }


       
        public Task<List<Produto>> Search(string q)
        {
            string sql = "SELECT * FROM Produto WHERE Descricao LIKE '%" + q + "%' ";

            return _conn.QueryAsync<Produto>(sql);
        }
    }
}