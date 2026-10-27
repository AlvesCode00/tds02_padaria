using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Model
{
    internal class Marca
    {
        private int id;
        private string nome;
        private string descricao;

        public Marca(string nome, string descricao)
        {
            this.nome = nome;
            this.descricao = descricao;
        }

        public Marca(int id, string nome, string descricao)
        {
            this.id = id;
            this.nome = nome;
            this.descricao = descricao;
        }

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Descricao { get => descricao; set => descricao = value; }

        public static int inserirMarca(Marca marca)
        {
            string sql = "INSERT INTO `Marca`(`nome`, `Descricao`) VALUES (@Nome, @Descricao)";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("id", marca.Id);
                cmd.Parameters.AddWithValue("nome", marca.Nome);
                cmd.Parameters.AddWithValue("Descricao", marca.Descricao);
                return Banco.executarComando(cmd);

            }

        }

        public static int DeletarMarca(Marca marca)
        {
            string sql = "DELETE FROM `Marca` WHERE `Id` =@id";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", marca.id);

                return Banco.executarComando(cmd);
            }
        }

        public static List<Marca> ListarTodos()
        {
            string sql = "SELECT * FROM `Marca`";

            List<Marca> listaMarcas = new();

            using (var cmd = new MySqlCommand(sql))
            {
                using (MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd))
                {

                    while (reader.Read())
                    {

                        int id = reader.GetInt32("id");
                        string nome = validarCampo(reader["Nome"]);
                        string telefone = validarCampo(reader["Telefone"]);

                        Marca marca = new Marca(id, nome, telefone);

                        listaMarcas.Add(marca);

                    }

                }

                return listaMarcas;

            }
        }

        public static Marca listaPorId(int id)
        {
            string sql = "SELECT * FROM `Marca` WHERE `id` =@id";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd);

                Marca marca = null;

                while (reader.Read())
                {

                    int idBanco = reader.GetInt32("id");
                    string nome = validarCampo(reader["Nome"]);
                    string telefone = validarCampo(reader["Telefone"]);

                    marca = new Marca(id, nome, telefone);

                }
                return marca;
            }
        }

        public static int atualizar(Marca marca)
        {
            string sql = "UPDATE `Marca` SET `Nome` = @Nome, `Descricao` = @Descricao WHERE `Id` = @Id";

            using (var cmd = new MySqlCommand(sql))
            {

                cmd.Parameters.AddWithValue("id", marca.Id);
                cmd.Parameters.AddWithValue("nome", marca.Nome);
                cmd.Parameters.AddWithValue("Descricao", marca.Descricao);

                return Banco.executarComando(cmd);
            }
        }
        private static string validarCampo(object campo)
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

        public static implicit operator Marca(int v)
        {
            throw new NotImplementedException();
        }
    }
}
