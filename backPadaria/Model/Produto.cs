using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Model
{
    internal class Produto : Marca
    {
        private int id;
        private string nome;
        private double preco;
        private string categoria;
        private double fabricado;
        private Marca marcaId;

        public Produto(int id)
        {
            this.id = id;
        }

        public Produto(string nome, double preco, string categoria, double fabricado, Marca marcaId)
        {
            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(categoria))
            {
                throw new ArgumentException("Todos os campos devem ser preenchidos.");
            }
            if (preco == 0 & fabricado == 0 )
            {
                throw new ArgumentException("O preço não pode ser 0.");

            }
            this.nome = nome;
            this.preco = preco;
            this.categoria = categoria;
            this.fabricado = fabricado;
            this.marcaId = marcaId;
        }
        ,

        public Produto(int id, string nome, double preco, string categoria, double fabricado, Marca marcaId)
        {
            this.id = id;
            this.nome = nome;
            this.preco = preco;
            this.categoria = categoria;
            this.fabricado = fabricado;
            this.marcaId = marcaId;
        }

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public double Preco { get => preco; set => preco = value; }
        public string Categoria { get => categoria; set => categoria = value; }
        public double Fabricado { get => fabricado; set => fabricado = value; }
        internal Marca MarcaId { get => marcaId; set => marcaId = value; }


        public static int inserirProduto(Produto produto)
        { 
         string sql = "INSERT INTO `Produto`(`Nome`, `Preco`, `Categoria`, `Fabricado`, `MarcaId`) VALUES (@Nome, @Preco, @Categoria, @Fabricado, @MarcaId)";

            using (var cmd = new MySqlCommand(sql))
            {

              

                cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                cmd.Parameters.AddWithValue("@Categoria", produto.Categoria);
                cmd.Parameters.AddWithValue("@Fabricado", produto.Fabricado);
                cmd.Parameters.AddWithValue("@MarcaId", produto.MarcaId);

                return Banco.executarComando(cmd);

            }

        }
        public static int Deletar(Produto produto)
        {
            string sql = "DELETE FROM `Produto` WHERE `Id`  = @id";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", produto.Id);

                return Banco.executarComando(cmd);
            }
        }

        public static List<Produto> ListarTodos()
        {
            string sql = "SELECT * FROM `Cliente`";

            List<Produto> listaProdutos = new();

            using (var cmd = new MySqlCommand(sql))
            {

                using (MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd))
                {
                   
                    while (reader.Read())
                    {
                        
                        int id = reader.GetInt32("Id");
                        string nome = validarCampo(reader["Nome"]);
                        double preco = reader.GetInt32("Preço");
                        string categoria = validarCampo(reader["Categoria"]);
                        string fabricado = validarCampo(reader["Fabricado"]);
                        int marcaId = reader.GetInt32("MarcaId");

                        Produto produto = new Produto(id, nome, preco, categoria, fabricado, marcaId);

                        listaProdutos.Add(produto);


                    }
                }
                // Retorna a lista completa de clientes.
                return listaClientes;
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

    }
}
