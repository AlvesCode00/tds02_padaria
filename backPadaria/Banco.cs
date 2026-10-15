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


        // Executando método executar um comando
        //Declarãção do método: público e estático, retornando um número inteiro (Linhas afetadas)
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
    }
}
