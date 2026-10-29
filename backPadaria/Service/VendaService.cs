using backPadaria.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backPadaria;

namespace backPadaria.Service
{
    public static class VendaService
    {

        public static void MenuVendas()
        {
            //
            bool sair = false;

            while (!sair)
            {
                Console.Clear();
                Console.WriteLine("====== PDV - Padaria Pão & Cia ======");
                Console.WriteLine("1 - Iniciar nova venda");
                Console.WriteLine("0 - Voltar ao menu principal");
                Console.WriteLine("==========================================");
                Console.Write("Digite uma opção: ");

                string opcao = Console.ReadLine();
                Console.Clear();

                try
                {
                    switch (opcao)
                    {
                        case "1":

                            iniciarNovaVenda();

                            break;
                        case "0":
                            sair = true;
                            break;
                        default:
                            Console.WriteLine("Opção Inválida");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nErro na Venda. {ex.Message}");
                    Console.WriteLine("\nPrecione uma tecla para continuar...");
                    Console.ReadKey();
                }


            }
        }

        public static void iniciarNovaVenda()
        {

            Console.WriteLine("------ NOVA VENDA ------");

            // 1 - IDENTIFAÇÂO DO FUNCIONÁRIO

            int funcionarioID = 2;

            // 1 - Identificação do Cliente
            Console.WriteLine("ID do cliente (Pressione ENTER se não quiser se identificar)");

            int idCliente = 0;

            int.TryParse(Console.ReadLine(), out idCliente);

            Cliente clienteVenda = Cliente.listarPorId(idCliente);


            // -- Inicializando o objeto compra
            Compra compraAtiva = new Compra(DateTime.Now, funcionarioID, clienteVenda.Id);

            // Loop do carrinho de Compras

            bool adicionarMais = true;

            while (adicionarMais)

            {

                Console.WriteLine("\n-- Adicionar Produto");

                Console.WriteLine("-- Id do Produto ou '0' para finalizar/pagar");

                int.TryParse(Console.ReadLine(), out int idProduto);

                if (idProduto == 0)

                {

                    adicionarMais = false;

                    continue;

                    // Ele ignora e vai para a próxima volta

                }

                Produto produto = Produto.listarPorId(idProduto);

                if (produto == null)

                {

                    Console.WriteLine("Produto não encontrado...Tente novamente");

                    continue;

                    // O Break finaliza o laço, enquanto o Continue seguirá para a próxima etapa

                }

                Console.Write($"Quantidade de {produto.Nome} | Preço {produto.Preco:C}");

                int.TryParse(Console.ReadLine(), out int qtdProduto);

                if (qtdProduto >= 0)

                {

                   compraAtiva.AdicionarItem(produto, qtdProduto);
                    Console.WriteLine($"Subtotal: {produto.Preco * qtdProduto}");
                }

            }//ENCERRA O LAÇO DE PRODUTOS

                // 3. Checkout
                if(compraAtiva.Itens.Count == 0)
                {

                Console.WriteLine("\n Venda cancelada (Nenhum item foi adicionado).");

            }
            else
            {
                Console.WriteLine("\n-------- RESUMO DA VENDA --------");
                foreach(var item in compraAtiva.Itens)
                    Console.WriteLine($"ID: {item.ProdutoId} | Quantidade: {item.Quantidade} | Preço Unit: {item.PrecoCobrado} | Subtotal: {item.CalcularSubTotal():C}");


            }

            decimal totalFinal = compraAtiva.CalcularTotal();

            Console.WriteLine("---------------------------------");
            Console.WriteLine($"TOTAL A PAGAR: {totalFinal:C}");
            Console.WriteLine("---------------------------------");

            Console.ReadKey();
        }

    }
}
