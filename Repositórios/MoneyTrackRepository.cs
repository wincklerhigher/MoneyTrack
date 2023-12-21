using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;
using MoneyTrack.Models;

namespace MoneyTrack
{
    public class MoneyTrackRepository
    {
        private const string DadosConexao = "Database=moneytrack; Data Source=localhost; User Id=root; AllowZeroDateTime=True; Allow User Variables=True;";

        public void TesteConexao()
        {
            MySqlConnection conexao = new MySqlConnection(DadosConexao);

            conexao.Open();

            Console.WriteLine("Conectado com sucesso ao banco de dados!");

            conexao.Close();
        }

        public void Remover (Contato contato)
        {
            MySqlConnection conexao = new MySqlConnection(DadosConexao);
            conexao.Open();

            string query = "DELETE FROM Contato WHERE IdContato = @IdContato";

            MySqlCommand comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@IdContato", contato.IdContato);

            comando.ExecuteNonQuery();

            conexao.Close();
        }

        public void InserirContato(Contato contato)
        {
            MySqlConnection conexao = new MySqlConnection(DadosConexao);
            conexao.Open();

            string query = "INSERT INTO Contato(Nome, Email, Mensagem, DataEnvio, Login, Senha) VALUES (@Nome, @Email, @Mensagem, @DataEnvio, @Login, @Senha)";

            MySqlCommand comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@Nome", contato.Nome);
            comando.Parameters.AddWithValue("@Email", contato.Email);
            comando.Parameters.AddWithValue("@Mensagem", (object)contato.Mensagem ?? DBNull.Value);
            comando.Parameters.AddWithValue("@DataEnvio", contato.DataEnvio);
            comando.Parameters.AddWithValue("@Login", contato.Login);
            comando.Parameters.AddWithValue("@Senha", contato.Senha);

            comando.ExecuteNonQuery();

            conexao.Close();
        }

       public void InserirFinancas(Financas financas)
{
    MySqlConnection conexao = new MySqlConnection(DadosConexao);
    conexao.Open();

    string query = "INSERT INTO Financas(IdContato, Tipo, Valor, Descricao, DataTransacao) VALUES (@IdContato, @Tipo, @Valor, @Descricao, @DataTransacao)";

    MySqlCommand comando = new MySqlCommand(query, conexao);
    
    comando.Parameters.AddWithValue("@Tipo", financas.Tipo);
    comando.Parameters.AddWithValue("@Valor", financas.Valor);
    comando.Parameters.AddWithValue("@Descricao", financas.Descricao);
    comando.Parameters.AddWithValue("@DataTransacao", financas.DataTransacao);

    comando.ExecuteNonQuery();

    conexao.Close();
}

        public Contato ValidarLogin(Contato contato)
        {
             using MySqlConnection conexao = new MySqlConnection(DadosConexao);
             
        conexao.Open();

        string query = "SELECT * FROM Contato WHERE Login = @Login AND Senha = @Senha";

        using MySqlCommand command = new MySqlCommand(query, conexao);
        command.Parameters.AddWithValue("@Login", contato.Login);
        command.Parameters.AddWithValue("@Senha", contato.Senha);

        using MySqlDataReader reader = command.ExecuteReader();

        Contato contatoEncontrado = null;

        if (reader.Read())
        {
            contatoEncontrado = new Contato();
            contatoEncontrado.IdContato = reader.GetInt32("IdContato");

            
            if (!reader.IsDBNull(reader.GetOrdinal("Nome")))
                contatoEncontrado.Nome = reader.GetString("Nome");

            
            if (!reader.IsDBNull(reader.GetOrdinal("Senha")))
                contatoEncontrado.Senha = reader.GetString("Senha");
        }

        return contatoEncontrado;
        }

        public List<Contato> Listar()
        {
           using MySqlConnection conexao = new MySqlConnection(DadosConexao);

    conexao.Open();

    string query = "SELECT * FROM Contato";

    MySqlCommand command = new MySqlCommand(query, conexao);

    using MySqlDataReader reader = command.ExecuteReader();

    List<Contato> listaContatos = new List<Contato>();

    while (reader.Read())
    {
        Contato contato = new Contato();
        contato.IdContato = reader.GetInt32("IdContato");

        if (!reader.IsDBNull(reader.GetOrdinal("Nome")))
            contato.Nome = reader.GetString("Nome");

        if (!reader.IsDBNull(reader.GetOrdinal("Email")))
            contato.Email = reader.GetString("Email");

        if (!reader.IsDBNull(reader.GetOrdinal("Mensagem")))
            contato.Mensagem = reader.GetString("Mensagem");

        if (!reader.IsDBNull(reader.GetOrdinal("DataEnvio")))
            contato.DataEnvio = reader.GetDateTime("DataEnvio");

        listaContatos.Add(contato);
    }
    return listaContatos;
        }

       public List<Financas> ListarFinancas()
{
    using MySqlConnection conexao = new MySqlConnection(DadosConexao);
    conexao.Open();

    string query = "SELECT * FROM Financas";

    MySqlCommand command = new MySqlCommand(query, conexao);

    using MySqlDataReader reader = command.ExecuteReader();

    List<Financas> ListarFinancas = new List<Financas>();

    while (reader.Read())
    {
        Financas financas = new Financas();
        financas.IdFinancas = reader.GetInt32("IdFinancas");
        financas.Tipo = reader.GetString("Tipo");
        financas.Valor = reader.GetDecimal("Valor");
        financas.Descricao = reader.GetString("Descricao");
        financas.DataTransacao = reader.GetDateTime("DataTransacao");

        ListarFinancas.Add(financas);
    }
        return ListarFinancas;
}

    public List<Contato> ObterTodosContatos()
{
    List<Contato> listaContatos = new List<Contato>();

    using (MySqlConnection conexao = new MySqlConnection(DadosConexao))
    {
        conexao.Open();

        string query = "SELECT * FROM Contato";

        using (MySqlCommand command = new MySqlCommand(query, conexao))
        using (MySqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Contato contato = new Contato();
                contato.IdContato = reader.GetInt32("IdContato");

                if (!reader.IsDBNull(reader.GetOrdinal("Nome")))
                    contato.Nome = reader.GetString("Nome");

                if (!reader.IsDBNull(reader.GetOrdinal("Email")))
                    contato.Email = reader.GetString("Email");

                if (!reader.IsDBNull(reader.GetOrdinal("Mensagem")))
                    contato.Mensagem = reader.GetString("Mensagem");

                if (!reader.IsDBNull(reader.GetOrdinal("DataEnvio")))
                    contato.DataEnvio = reader.GetDateTime("DataEnvio");

                listaContatos.Add(contato);
            }
        }
    }
                return listaContatos;
}

        public void AtualizarContato(Contato contato)
{
    MySqlConnection conexao = new MySqlConnection(DadosConexao);
    conexao.Open();

    string query = "UPDATE Contato SET Nome = @Nome, Email = @Email, Mensagem = @Mensagem, Login = @Login, Senha = @Senha WHERE IdContato = @IdContato";

    MySqlCommand comando = new MySqlCommand(query, conexao);

    comando.Parameters.AddWithValue("@IdContato", contato.IdContato);
    comando.Parameters.AddWithValue("@Nome", contato.Nome);
    comando.Parameters.AddWithValue("@Email", contato.Email);
    comando.Parameters.AddWithValue("@Mensagem", (object)contato.Mensagem ?? DBNull.Value);    
    comando.Parameters.AddWithValue("@Login", contato.Login);
    comando.Parameters.AddWithValue("@Senha", contato.Senha);

    comando.ExecuteNonQuery();

    conexao.Close();
}

        public Contato BuscarPorId(int IdContato)
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();

            string Query = "Select * from Contato where IdContato=@IdContato";

            MySqlCommand Comando = new MySqlCommand(Query, Conexao);

            Comando.Parameters.AddWithValue("@IdContato", IdContato);

            MySqlDataReader Reader = Comando.ExecuteReader();

            Contato contatoLocalizado = new Contato();

            if (Reader.Read())
            {
                contatoLocalizado.IdContato = Reader.GetInt32("IdContato");

                if (!Reader.IsDBNull(Reader.GetOrdinal("Nome")))
                    contatoLocalizado.Nome = Reader.GetString("Nome");

                if (!Reader.IsDBNull(Reader.GetOrdinal("Login")))
                    contatoLocalizado.Login = Reader.GetString("Login");

                if (!Reader.IsDBNull(Reader.GetOrdinal("Senha")))
                    contatoLocalizado.Senha = Reader.GetString("Senha");
            }

            Conexao.Close();

            return contatoLocalizado;
        }
    }
}