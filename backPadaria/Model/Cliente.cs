using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Model
{
    internal class Cliente
    {
        //Variaveis
        private int id;
        private string nome;
        private string cpf;
        private string telefone;
        private string endereco;
        private int pontos;


        // Primeiro método Construtor (para falar q se um cliente n tiver id,
        // ele vai ser considerado cliente do msm jeito).
        public Cliente(string nome, string cpf, string telefone, string endereco, int pontos)
        {
            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(cpf) || string.IsNullOrEmpty(telefone) || string.IsNullOrEmpty(endereco))
            {
                throw new ArgumentException("Todos os campos devem ser preenchidos.");
            }

            this.Nome = nome;
            this.Cpf = cpf;
            this.Telefone = telefone;
            this.Endereco = endereco;
            this.Pontos = pontos;
        }

        //Gambiarra para testar
        public Cliente(int id)
        {
            this.Id = id;
        }
       
        // Método Construtor
        public Cliente(int id, string nome, string cpf, string telefone, string endereco, int pontos)
        {
            this.Id = id;
            this.Nome = nome;
            this.Cpf = cpf;
            this.Telefone = telefone;
            this.Endereco = endereco;
            this.Pontos = pontos;
        }

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Cpf { get => cpf; set => cpf = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        public string Endereco { get => endereco; set => endereco = value; }
        public int Pontos { get => pontos; set => pontos = value; }

        public void AdicionarPontos(int pontos)
        {
            this.Pontos += pontos;
        }


        public static int inserir(Cliente cliente)
        {

            // Instrução SQL: Define o comando de inserção (INSERT).
            // O uso de marcações com '@' (ex: @Nome) é uma prática de segurança crucial chamada de "Parametrização".
            // Ela impede ataques de SQL Injection, pois o banco entende o que é comando e o que é apenas texto do usuário.

            string sql = "INSERT INTO `Cliente`(`Nome`, `CPF`, `Telefone`, `Endereco`, `Pontos`) VALUES (@Nome, @CPF, @Telefone, @Endereco, @Pontos)";

            // Gerenciamento de recursos: Cria o objeto de comando já passando a instrução SQL.
            // O 'using' garante que os recursos de memória ocupados pelo comando sejam liberados ao final do bloco.

            using (var cmd = new MySqlCommand(sql))
            {

                // Vinculação de Dados: Pega as propriedades do objeto C# (Cliente.Nome, usuario.Email, etc.)
                // e substitui de forma segura as marcações '@' da string SQL com os valores reais.

                cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@CPF", cliente.Cpf);
                cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                cmd.Parameters.AddWithValue("@Endereco", cliente.Endereco);
                cmd.Parameters.AddWithValue("@Pontos", cliente.Pontos);

                // Execução: Como o comando de INSERT não retorna tabelas (apenas executa uma ação),
                // ele envia o comando configurado para o método genérico 'executarComando' da classe 'Banco'.
                // O método retorna 1 (se o usuário foi inserido com sucesso) ou -1 (se houver erro no banco).

                return Banco.executarComando(cmd);

            }

        }

        public static int Deletar(Cliente cliente)
        {
            string sql = "DELETE FROM `Cliente` WHERE `Id`  = @id";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", cliente.Id);
                return Banco.executarComando(cmd);
            }
        }

        public static List<Cliente> ListarTodos()
        {
            string sql = "SELECT * FROM `Cliente`";

            List<Cliente> listaClientes = new();

            using (var cmd = new MySqlCommand(sql))
            {



                // Executa a leitura. O bloco 'using' aqui e FUNDAMENTAL para garantir que o
                // reader seja fechado no final, o que por sua vez fecha a conexao com o banco.
                using (MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd))
                {
                    // O while vai rodar uma vez para CADA Linha que existir na tabela Cliente
                    while (reader.Read())
                    {
                        /// Extrai os dados da linha atual.
                        int id = reader.GetInt32("Id");
                        string nome = validarCampo(reader["Nome"]);
                        string cpf = validarCampo(reader["CPF"]);
                        string telefone = validarCampo(reader["Telefone"]);
                        string endereco = validarCampo(reader["Endereco"]);
                        int pontos = reader.GetInt32("Pontos");

                        /// Cria o objeto Cliente com os dados extraídos.
                        Cliente cliente = new Cliente(id, nome, cpf, telefone, endereco, pontos);

                        /// Adiciona o objeto Cliente à lista de clientes.
                        listaClientes.Add(cliente);


                    }
                }
                // Retorna a lista completa de clientes.
                return listaClientes;
            }
        }

        public static Cliente? listarPorId(int id)
        {
            string sql = "SELECT * FROM `Cliente` WHERE `id` = @id";


            using(var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd);

                Cliente? cliente = null;

                while (reader.Read())
                {
                    /// Extrai os dados da linha atual.
                    int idBanco = reader.GetInt32("Id");
                    string nome = validarCampo(reader["Nome"]);
                    string cpf = validarCampo(reader["CPF"]);
                    string telefone = validarCampo(reader["Telefone"]);
                    string endereco = validarCampo(reader["Endereco"]);
                    int pontos = reader.GetInt32("Pontos");

                    /// Cria o objeto Cliente com os dados extraídos.
                   cliente = new Cliente(id, nome, cpf, telefone, endereco, pontos);
                }

                return cliente;
            }
        }

        public static int atualizar(Cliente cliente)
        {
            string sql = "UPDATE `Cliente` SET `Nome` = @Nome, `Cpf` = @CPF, `Telefone` = @Telefone, `Endereco` = @Endereco, `Pontos` = @Pontos WHERE `Id` = @Id";

            using (var cmd = new MySqlCommand(sql))
            {

                // Vinculação de parâmetros: Protege contra SQL Injection e organiza os dados
                cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@CPF", cliente.Cpf);
                cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                cmd.Parameters.AddWithValue("@Endereco", cliente.Endereco);
                cmd.Parameters.AddWithValue("@Pontos", cliente.Pontos);

                // A chave de busca: Quem será atualizado é o cliente cujo Id seja igual ao Id do objeto cliente passado como parâmetro.
                cmd.Parameters.AddWithValue("@Id", cliente.Id);

                // Execução: O UPDATE também é uma instrução de ação (como INSERT e DELETE).
                // Ele vai retornar 1 (se encontrou o ID e atualizou com sucesso) ou 0 (se o ID nao existia).
                return Banco.executarComando(cmd);
              
            }

        }




        private static string validarCampo(object campo)
        {
            if(campo == DBNull.Value || campo == null)
            {
                return "";
            }
            else
            {
                return campo.ToString();
            }
       
        }

        internal static int atualizar(Funcionario fun1)
        {
            throw new NotImplementedException();
        }
    }
  
}
