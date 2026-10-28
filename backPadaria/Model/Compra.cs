using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Model
{
    internal class Compra
    {
        private int id;
        private DateTime dataHora;
        private int funcionarioId;
        private int clienteid;
        
        private List<ItemCompra> Itens = new List<ItemCompra>();

        public int Id { get => id; set => id = value; }
        public DateTime DataHora { get => dataHora; set => dataHora = value; }
        public int FuncionarioId { get => funcionarioId; set => funcionarioId = value; }
        public int Clienteid { get => clienteid; set => clienteid = value; }

        public Compra(DateTime dataHora, int funcionarioId, int clienteid)
        {
           if (funcionarioId <= 0) throw new ArgumentOutOfRangeException("O funcionário responsável é inválido.");
       

            this.DataHora = dataHora;
            this.FuncionarioId = funcionarioId;
            this.Clienteid = clienteid;
            
        }

        public Compra(int id, DateTime dataHora, int funcionarioId, int clienteid)
            : this(dataHora, funcionarioId, clienteid)
        { 
            if(id <= 0) throw new ArgumentOutOfRangeException("Id da Compra Inválido.");
            this.Id = id;
           
        }

        public decimal CalcularTotal()
        { 
            return Itens.Sum(item => item.CalcularSubTotal());
        }

        public void AdicionarItem(Produto produto, int quantidade)
        {
            if (produto == null) throw new Exception("Produto inexistente");

            ItemCompra novoItem = new ItemCompra(produto.Id, quantidade, produto.Preco);
            this.Itens.Add(novoItem);
        }
         
        public void RemoverItem(ItemCompra item)
        {
            this.Itens.Remove(item);
        }

        public static int Inserir(Compra compra)
        {
            if (compra.Clienteid == 0) compra.Clienteid = 10000;
            
            using(var cmd = new MySqlCommand("sp_inserirCompra"))
            {
                cmd.Parameters.AddWithValue("p_DataHora", compra.DataHora);
                cmd.Parameters.AddWithValue("p_FuncionarioId", compra.FuncionarioId);
                cmd.Parameters.AddWithValue("p_ClienteId", compra.Clienteid);

                object resultado = Banco.ExecutarComandoComRetornoId(cmd);

                if(resultado != null && int.TryParse(resultado.ToString(), out int idGerado))
                {

                    compra.Id = idGerado; 
                    return idGerado;
                }

                return -1;

            }
        }
    }
}