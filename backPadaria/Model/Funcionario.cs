using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Model
{
    internal class Funcionario
    {
        private int id;
        private string nome;
        private string cpf;
        private string cargo;
        private static Funcionario funcionario;

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Cpf { get => cpf; set => cpf = value; }
        public string Cargo { get => cargo; set => cargo = value; }

        public Funcionario(int id, string nome, string cpf, string cargo)
        {
            this.Id = id;
            this.Nome = nome;
            this.Cpf = cpf;
            this.Cargo = cargo;
        }


        public Funcionario(int id)
        {
            this.Id = id;
        }

        public Funcionario(string? nome, string? cpf, string? cargo)
        {
            this.nome = nome;
            this.cpf = cpf;
            this.cargo = cargo;
        }

        public static int inserir (Funcionario funcionario)
        {
            string sql = $"INSERT INTO Funcionario (nome, cpf, cargo) VALUES (@Nome, @Cpf, @Cargo)";



            using (var cmd = new MySqlCommand(sql))
            {

                cmd.Parameters.AddWithValue("@Nome", funcionario.Nome); 
                cmd.Parameters.AddWithValue("@Cpf", funcionario.Cpf);
                cmd.Parameters.AddWithValue("@Cargo", funcionario.Cargo);

                return Banco.executarComando(cmd);

            }
        }

        public static int deletar(Funcionario funcionario)
        {
            string sql = "DELETE FROM `Funcionario` WHERE `id` = @id";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", funcionario.Id);

                return Banco.executarComando(cmd);
            }
        }



        public static List<Funcionario> ListarTodos()
        {


            string sql = "SELECT * FROM `Funcionario`";

            List<Funcionario> listaFuncionarios = new();

            using (var cmd = new MySqlCommand(sql))
            {
                using (MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd))
                {

                    while (reader.Read())
                    {
                        int id = reader.GetInt32("Id");
                        string nome = ValidarCampo(reader["Nome"]); 
                        string cpf = ValidarCampo(reader["CPF"]);
                        string cargo = ValidarCampo(reader["Cargo"]);

                        Funcionario funcionario = new Funcionario(id, nome, cpf, cargo);

                        listaFuncionarios.Add(funcionario);


                    }
                }
                return listaFuncionarios;
                }
        }

        public static Funcionario? listarPorId(int id)
        {
            string sql = "SELECT * FROM `Funcionario` WHERE `id` = @id";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd);

                Funcionario? funcionário = null;

                while (reader.Read())
                {

                    int idBanco = reader.GetInt32("Id");
                    string nome = ValidarCampo(reader["Nome"]);
                    string cpf = ValidarCampo(reader["CPF"]);
                    string cargo = ValidarCampo(reader["Cargo"]);


                    funcionário = new Funcionario(id, nome, cpf, cargo);
                }
                return funcionário;
            }
        }

        public static int atualizar(Funcionario funcionario)
        {
            string sql = "UPDATE `Funcionario` SET `Nome` = @Nome, `CPF` = @CPF, `Cargo` = @Cargo  WHERE `Id` = @Id";

            using (var cmd = new MySqlCommand(sql))
            {

               
                cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
                cmd.Parameters.AddWithValue("@CPF", funcionario.Cpf);
                cmd.Parameters.AddWithValue("@Cargo", funcionario.Cargo);


                cmd.Parameters.AddWithValue("@Id", funcionario.Id);

                
                return Banco.executarComando(cmd);

            }

        }







        private static string ValidarCampo(object campo)
        {
            if (campo == DBNull.Value || campo == null)
            {
                return "";
            }
            else
            {
                return campo.ToString();
            }

        }



    }
}
