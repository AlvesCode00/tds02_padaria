using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Model
{
    internal class Fornecedor
    {
        private int id;
        private string nome;
        private string cnpj;
        private string telefone;

       
        public Fornecedor(int id, string nome, string cnpj, string telefone)
        {
            this.id = id;
            this.nome = nome;
            this.cnpj = cnpj;
            this.telefone = telefone;
        }
        public Fornecedor(int id)
        {
            this.Id = id;
        }

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Cnpj { get => cnpj; set => cnpj = value; }
        public string Telefone { get => telefone; set => telefone = value; }


        public Fornecedor(string nome, string cnpj, string telefone)
        {
            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(cnpj) || string.IsNullOrEmpty(telefone) )
            {
                throw new ArgumentException("Todos os campos devem ser preenchidos.");
            }

            this.Nome = nome;
            this.Cnpj = cnpj;
            this.Telefone = telefone;
           
        }
        public static int InserirFornecedor(Fornecedor fornecedor)
        {
            string sql = "INSERT INTO `Fornecedor`(`Nome`,`CNPJ`,`Telefone`) VALUES (@Nome, @CNPJ, @Telefone)";
            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("id", fornecedor.Id);
                cmd.Parameters.AddWithValue("Nome", fornecedor.nome);
                cmd.Parameters.AddWithValue("CNPJ", fornecedor.cnpj);
                cmd.Parameters.AddWithValue("Telefone", fornecedor.telefone);

                return Banco.executarComando(cmd);
            }
        }
        public static int deletarFornecedor(Fornecedor fornecedor)
        {
            string sql = "DELETE FROM `Fornecedor` WHERE `Id` =@id";
            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("id", fornecedor.id);

                return Banco.executarComando(cmd);
            }
        }

        public static List<Fornecedor> ListarFornecedores()
        {
            string sql = "SELECT * FROM `Fornecedor`";

            List<Fornecedor> listaFornecedores = new();

            using (var cmd = new MySqlCommand(sql)) 
            {
                using (MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd))
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        string nome = validarCampo(reader["Nome"]);
                        string cnpj = validarCampo(reader["CNPJ"]);
                        string telefone = validarCampo(reader["Telefone"]);
                         
                        Fornecedor fornecedor = new Fornecedor(id, nome, cnpj, telefone);

                        listaFornecedores.Add(fornecedor);
                    }
                }
                return listaFornecedores;
            }

        }

        public static Fornecedor listarPorId (int id)
        {
            string sql = "SELECT * FROM `Fornecedor` WHERE `id` =@id";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("id", id);

                MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd);

                Fornecedor fornecedor = null;

                while (reader.Read())
                {   
                    int idFornecedor = reader.GetInt32("id");
                    string nome = validarCampo(reader["Nome"]);
                    string cnpj = validarCampo(reader["CNPJ"]);
                    string telefone = validarCampo(reader["Telefone"]);


                    fornecedor = new Fornecedor(id,nome, cnpj,telefone);
                }

                return fornecedor;
            }


        }

        public static int atualizar(Fornecedor fornecedor
            )
        {
            string sql = "UPDATE `Fornecedor` SET `Nome` = @Nome, `Cnpj` = @CNPJ, `Telefone` = @Telefone WHERE `Id` = @Id";

            using (var cmd = new MySqlCommand(sql)) {

                cmd.Parameters.AddWithValue("id", fornecedor.Id);
                cmd.Parameters.AddWithValue("Nome", fornecedor.nome);
                cmd.Parameters.AddWithValue("CNPJ", fornecedor.cnpj);
                cmd.Parameters.AddWithValue("Telefone", fornecedor.telefone);

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
        internal static int atualizarFornecedor(Fornecedor for1)
        {
            throw new NotImplementedException();
        }
    }
}
