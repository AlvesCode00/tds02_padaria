using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria
{
    internal class Banco
    {
        private static string stringDeConexao = "Server=10.91.45.41;Port=3306;Database=padaria_bd;Uid=admin;Pwd=123456";

        public static void connectAndTest()
        {
            using(var connection = new MySqlConnection(stringDeConexao))
            {
                try
                {
                    Console.WriteLine("Tentando conectar ao banco de dados.");

                    connection.Open();

                    Console.WriteLine("Conexão bem sucedida");

                    using (var command = new MySqlCommand("SELECT VERSION();", connection))
                    {
                        string? version = command.ExecuteScalar()?.ToString();
                        Console.WriteLine($"Versão do MariaDB {version}");
                    }

                }
                catch ( MySqlException ex)
                {
                    Console.WriteLine("Erro ao conectar no banco de dados.");

                    Console.WriteLine(ex.Message);

                }


            }
        }


       
        //Declaração do método: público e estático, retornando um número inteiro (Linhas afetadas)
        // e recebe um objeto MySqlCommand com a instrução SQL já configurada com parâmetro.
        public static int executarComando(MySqlCommand command)
        {
            // O bloco 'using' garante que a conexão será automaticamente fechada e liberada
            // da memória ao final do bloco, mesmo que ocorram erros no meio do processo.

            using (var conexao = new MySqlConnection(stringDeConexao))
            {
                command.Connection = conexao;

                // Tratamento de erros: Inicia um bloco de tentativa para proteger o programa de falhas abruptas.
                try
                {
                    conexao.Open();

                    /*
                     Execução: o ExecuteNonQuery dispara comandos de ação (INSERT, UPDATE, DELETE).
                    Ele não retorna registros, mas sim o número inteiro de linha que foram alterados.
                    e p 'return' devolve esse número para quem chamou o método.
                     */

                    return command.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"Erro de Banco de Dados{ex.Message}");

                    return -1; // Retorna -1 para indicar que houve um erro na execução do comando.
                }
            }
            

        }


        // Declaracão do método: publico e estático, projetado especificamente para executar comandos
        // (geralmente INSERTs seguidos de SELECT LAST_INSERT_ID()) e retornar o ID gerado pelo banco.
        public static int ExecutarComandoComRetornoId(MySqlCommand command)
        {
            using (var conexao = new MySqlConnection(stringDeConexao))
            {
                command.Connection = conexao;

                try
                {
                    conexao.Open();

                    // 0 ExecuteScalar executa o comando e retorna APENAS a primeira coluna da primeira
                    // linha do resultado. É perfeito para pegar um unico valor (como um ID recem-gerado).
                    // Ele retorna um tipo 'object' porque nao sabe de antemao qual o tipo do dado no banco.

                    object objRetorno = command.ExecuteScalar();

                    int retorno = 0;

                    /* Verificação de segurança: Checa se o banco realmente retornou algo.
                    Verifica tanto se o objeto C# naão e nulo (null) quanto se
                    nao e um nulo do proprio banco de dados (DBNull.Value).
                    */

                    if (objRetorno != null && objRetorno != DBNull.Value)
                    {
                        /* Se passou na verificacão, converte o objeto (que veio como texto/objeto)
                        para um numero inteiro (int)
                        */

                        retorno = int.Parse(objRetorno.ToString());
                    }

                    return retorno;
                }
                catch(MySqlException ex) 
                {
                    Console.WriteLine($"Erro de Banco de Dados{ex.Message}");
                    return -1;  
                }
            }
        }

        // Metodo central para executar comandos de consulta (SELECT) no banco de dados.
        // Ele retorna um MySqLDataReader, que é uma espécie de "ponteiro" para ler os dados linha por linha.

        public static MySqlDataReader executarComRetornoDeDados(MySqlCommand command)
        {

            // Cria a conexão com o banco.
            // ATENÇÃO: Aqui NÃO usamos o bloco 'using' para a conexão. Se usássemos, a conexão
            // seria fechada antes de retornar os dados, e o programa quebraria ao tentar ler a tabela.
            var conexao = new MySqlConnection(stringDeConexao);

            try
            {
                // Abre a porta de comunicação com o banco MuSQL.
                conexao.Open();

                // Executa a consulta e retorna o "leitor" de dados.
                // O CommandBehavior. CloseConnection e a peca-chave deste metodo: ele avisa ao leitor
                // que assin que ele (o leitor) for fechado ou descartado la na tela/classe que o chamou,
                // a conexão com o banco deve ser fechada automaticamente junto con ele.
                return command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
            catch (MySqlException ex) {

                // 0 Dispose() ja executa o Close() e libera a memoria.
                conexao.Dispose();

                Console.WriteLine($"Errona leitura de Banco de Dados: {ex.Message}");

                //Ao inves de retornar null, relanca o erro(throw) para que o metodo que
                // chamou este ExecuteReader saiba que a pesquisa falhou e possa tratar a tela do usuário,
                throw;


            }
        }

    }
}
