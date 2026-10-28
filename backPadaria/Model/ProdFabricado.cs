
using MySqlConnector;
using System;
using System.Collections.Generic;

namespace backPadaria.Model
{
    internal class ProdFabricado 
    {
        private int produtoId;
        private string descricaoProducao;

        public ProdFabricado(int id, string nome, double preco, string descricaoProducao)
            
        {
            if (string.IsNullOrEmpty(descricaoProducao))
            {
                throw new ArgumentException("Todos os campos devem ser preenchidos.");
            }

            this.produtoId = id;
            this.descricaoProducao = descricaoProducao;
        }

        public ProdFabricado(int idProduto, string? descricaoProducao)
        {
            IdProduto = idProduto;
            this.descricaoProducao = descricaoProducao;
        }

        public int IdProduto { get => produtoId; set => produtoId = value; }
        public string DescricaoProduto { get => descricaoProducao; set => descricaoProducao = value; }

        public static int inserirDescricao(ProdFabricado prodFabricado)
        {
            string sql = "INSERT INTO `ProdFabricado` (`idProduto`, `descricaoProducao`) VALUES (@idProduto, @descricaoProducao)";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@idProduto", prodFabricado.produtoId);
                cmd.Parameters.AddWithValue("@descricaoProducao", prodFabricado.descricaoProducao);

                return Banco.executarComando(cmd);
            }
        }

        public static int Deletar(ProdFabricado prodFabricado)
        {
            string sql = "DELETE FROM `ProdFabricado` WHERE `idProduto` = @idProduto";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@idProduto", prodFabricado.produtoId);

                return Banco.executarComando(cmd);
            }
        }

        public static List<ProdFabricado> ListarTodos()
        {
            string sql = "SELECT * FROM `ProdFabricado`";

            List<ProdFabricado> listaDeProdFabricado = new();

            using (var cmd = new MySqlCommand(sql))
            {
                using (MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd))
                {
                    while (reader.Read())
                    {
                        int produtoId = reader.GetInt32("idProduto");
                        string descricaoProducao = validarCampo(reader["descricaoProducao"]);

                        ProdFabricado prodFabricado = new ProdFabricado(produtoId, "", 0, descricaoProducao);

                        listaDeProdFabricado.Add(prodFabricado);
                    }
                }
            }

            return listaDeProdFabricado;
        }

        public static ProdFabricado ListarProdFPorId(int produtoId)
        {
            string sql = "SELECT * FROM `ProdFabricado` WHERE `idProduto` = @produtoId";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@produtoId", produtoId);

                using (MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd))
                {
                    if (reader.Read())
                    {
                        int produtoIds = reader.GetInt32("idProduto");
                        string descricaoProducao = validarCampo(reader["descricaoProducao"]);

                        return new ProdFabricado(produtoIds, "", 0, descricaoProducao);
                    }
                }
            }

            return null;
        }

        private static string validarCampo(object campo)
        {
            if (campo == DBNull.Value || campo == null)
            {
                return "";
            }

            return campo.ToString();
        }
    }
}
