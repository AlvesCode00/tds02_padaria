using backPadaria.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Services
{
    public static class ClienteService
    {
        public static void MenuCliente()
        {
            bool sair = false;

            while (!sair)
            {
                Console.WriteLine("===== GESTÃO DE CLIENTES =====\n");
                Console.WriteLine("------------------------");
                Console.WriteLine("1 - Cadastrar Cliente");
                Console.WriteLine("2 - Listar Todos os Clientes");
                Console.WriteLine("3 - Listar Cliente por ID");
                Console.WriteLine("4 - Atualizar Cliente");
                Console.WriteLine("5 - Deletar Cliente");
                Console.WriteLine("6 - Visualizar Pontuação");
                Console.WriteLine("0 - Sair do Programa");
                Console.WriteLine("------------------------");

                Console.WriteLine("Digite uma opção");

                string opcao = Console.ReadLine();

                Console.Clear();

                try
                {
                    switch (opcao)
                    {
                        case "1": // Cadastrar Cliente
                            Console.WriteLine(" ========== CADASTRAR CLIENTE ==========");

                            Console.WriteLine("------------ BEM VINDO ------------\n       Ao Cadastro do Clietes \n");

                            Console.WriteLine("Digite o nome do Cliente: ");
                            string nomeCliente = Console.ReadLine();

                            Console.WriteLine("Digite o CPF do cliente: ");
                            string cpfCliente = Console.ReadLine();

                            Console.WriteLine("Digite o telefone do cliente: ");
                            string telefoneCliente = Console.ReadLine();

                            Console.WriteLine("Digite o endereço do cliente: ");
                            string enderecoCliente = Console.ReadLine();

                            Console.WriteLine("Digite a quantidade de pontos: ");
                            int pontosCliente = int.Parse(Console.ReadLine());


                            // REFATORAÇÂO /// 
                            Cliente newClient = new Cliente(
                               nome: nomeCliente,
                               cpf: cpfCliente,
                               telefone: telefoneCliente,
                               endereco: enderecoCliente,
                               pontos: pontosCliente
                                );

                            int resultadoInsert = Cliente.Inserir(newClient);
                            if (resultadoInsert > 0)
                            {
                                Console.WriteLine("Cliente cadastrado com sucesso!");
                            }
                            else
                            {
                                Console.WriteLine("Erro ao cadastrar cliente.");
                            }


                            break;
                        case "2":
                            ListarClientesMenu();
                            break;
                        case "3":
                            ListarClientePorIdMenu();
                            break;
                        case "4":
                            AtualizarClienteMenu();

                            break;
                        case "5":
                            DeletarClienteMenu();
                            break;
                        case "6":
                            VisualizarPontuacaoMenu();
                            break;
                        case "0":
                            Console.WriteLine("Saindo do Sistema...");


                            sair = true;
                            break;

                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro de validação {ex.Message}");
                }
            }
        }


        private static void ListarClientesMenu()
        {
            Console.WriteLine("----- LISTA DE CLIENTES ----- ");

            List<Cliente> lista = Cliente.ListarTodos();

            if (lista.Count == 0)
            {
                Console.WriteLine(" Nenhum cliente cadastrado no sistema.");
            }
            else
            {
                foreach (var cliente in lista)
                {
                    Console.WriteLine($"ID: {cliente.Id} \n| Nome: {cliente.Nome} \n| CPF: {cliente.Cpf} \n| Telefone: {cliente.Telefone} \n| Endereço: {cliente.Endereco} \n| Pontos: {cliente.Pontos}\n");

                    Console.WriteLine("-----------------------------");
                }
                Console.WriteLine("\n Pressione uma tecla para continuar...");
                Console.ReadKey();
            }

        }


        private static void ListarClientePorIdMenu()
        {
            Console.WriteLine("----- LISTA DE CLIENTE PROCURADO POR ID -----");

            Console.WriteLine("Digite o ID do cliente que deseja buscar: ");
            int idCliente = int.Parse(Console.ReadLine());
            List<Cliente> listaClientes = Cliente.ListarTodos();
            for (int i = 0; i < listaClientes.Count; i++)
            {
                Cliente? cliente = listaClientes[i];
                if (cliente.Id == idCliente)
                {
                    Console.WriteLine(
                        $"ID do cliente procurado: {idCliente} \n| Nome: {cliente.Nome} \n| CPF: {cliente.Cpf} \n| Telefone: {cliente.Telefone} \n| Endereço: {cliente.Endereco} \n| Pontos: {cliente.Pontos}");
                    Console.WriteLine(
                        "-----------------------------");

                    Console.WriteLine("\n Pressione uma tecla para continuar...");


                    Console.ReadKey();
                }
            }
        }


        private static void AtualizarClienteMenu()
        {
            Console.WriteLine("----- ATUALIZAR CLIENTE -----");

            Console.WriteLine("Digite o ID do cliente q deseja atualizar: ");

            int id = 0;
            int.TryParse(Console.ReadLine(), out id);

            Cliente? clienteAtualizar = Cliente.listarPorId(id);

            if (clienteAtualizar != null)
            {
                Console.WriteLine($"Atualizando dados de {clienteAtualizar.Nome}");
                Console.WriteLine($"O que você deseja atualizar: \n 1 - Nome: {clienteAtualizar.Nome} \n 2 - Endereço: {clienteAtualizar.Endereco} \n 3 - Telefone: {clienteAtualizar.Telefone} \n 4 - CPF: {clienteAtualizar.Cpf} \n 5 - Pontos: {clienteAtualizar.Pontos} \n");
                Console.WriteLine("Digite uma opção");

                string escolha = Console.ReadLine();

                Console.Clear();

                switch (escolha)
                {
                    case "1":
                        Console.WriteLine("Digite o novo nome do cliente: ");
                        Cliente? meuCli = Cliente.listarPorId(id);
                        if (meuCli != null)
                        {
                            meuCli.Nome = Console.ReadLine();
                            int resultado = Cliente.Atualizar(meuCli);
                            if (resultado == 0)
                            {
                                Console.WriteLine("Não foi possivel atualizar");
                            }
                            else
                            {
                                Console.WriteLine("Cliente atualizado com sucesso!");
                            }
                        }
                        break;
                    case "2":
                        Console.WriteLine("Digite o Endereço novo do cliente: ");
                        Cliente? meuCli1 = Cliente.listarPorId(id);
                        if (meuCli1 != null)
                        {
                            meuCli1.Endereco = Console.ReadLine();
                            int resultado = Cliente.Atualizar(meuCli1);
                            if (resultado == 0)
                            {
                                Console.WriteLine("Não foi possivel atualizar");
                            }
                            else
                            {
                                Console.WriteLine("Cliente atualizado com sucesso!");
                            }
                        }

                        break;
                    case "3":
                        Console.WriteLine("Digite o novo telefone do cliente: ");
                        Cliente? meuCli2 = Cliente.listarPorId(id);
                        if (meuCli2 != null)
                        {
                            meuCli2.Telefone = Console.ReadLine();
                            int resultado = Cliente.Atualizar(meuCli2);
                            if (resultado == 0)
                            {
                                Console.WriteLine("Não foi possivel atualizar");
                            }
                            else
                            {
                                Console.WriteLine("Cliente atualizado com sucesso!");
                            }
                        }
                        break;
                    case "4":
                        Console.WriteLine("Digite o novo CPF do cliente: ");
                        Cliente? meuCli3 = Cliente.listarPorId(id);
                        if (meuCli3 != null)
                        {
                            meuCli3.Cpf = Console.ReadLine();
                            int resultado = Cliente.Atualizar(meuCli3);
                            if (resultado == 0)
                            {
                                Console.WriteLine("Não foi possivel atualizar");
                            }
                            else
                            {
                                Console.WriteLine("Cliente atualizado com sucesso!");
                            }
                        }
                        break;
                    case "5":
                        Console.WriteLine("Digite a nova quantidade de pontos do cliente: ");
                        Cliente? meuCli4 = Cliente.listarPorId(id);
                        if (meuCli4 != null)
                        {
                            meuCli4.Pontos = int.Parse(Console.ReadLine());
                            int resultado = Cliente.Atualizar(meuCli4);
                            if (resultado == 0)
                            {
                                Console.WriteLine("Não foi possivel atualizar");
                            }
                            else
                            {
                                Console.WriteLine("Cliente atualizado com sucesso!");
                            }
                        }
                        break;



                }

            }
            else
            {
                Console.WriteLine($"Cliente com ID {id} não encontrado.");
            }

        }


        private static void DeletarClienteMenu()
        {
            Console.WriteLine("----- DELETAR CLIENTE -----");

            Console.WriteLine("Digite o ID do Cliente que deseja deletar: ");


            int id = 0;
            int.TryParse(Console.ReadLine(), out id);

            Console.WriteLine("Resultado:");
            Cliente deletar = new Cliente(id);
            if (Cliente.Deletar(deletar) >= 1)
            {

                Cliente? DeletarClienteMenu = Cliente.listarPorId(id);

                if (DeletarClienteMenu != null)
                {
                    Console.WriteLine($"O que você deseja deletar: \n 1 - Nome: {DeletarClienteMenu.Nome} \n ");
                }

                Console.WriteLine("Cliente excluido com sucesso.");
            }
            else
            {
                Console.WriteLine($"Deu bom não. Não Existe esse Cliente Cadastrado! ");
            }





        }


        public static void VisualizarPontuacaoMenu()
        {
            Console.WriteLine("----- VISUALIZAR A PONTUAÇÃO -----");
            Console.WriteLine("Digita o ID que você deseja visualizar a pontuação: ");

            int idCliente = int.Parse(Console.ReadLine());
            List<Cliente> listaClientes = Cliente.ListarTodos();

            for (int i = 0; i < listaClientes.Count; i++)
            {
                Cliente? cliente = listaClientes[i];
                if (cliente.Id == idCliente)
                {
                    Console.WriteLine(
                        $"ID do cliente procurado: {idCliente} \n| Nome: {cliente.Nome} \n Pontos: {cliente.Pontos}");
                    Console.WriteLine(
                        "-----------------------------");

                    Console.WriteLine("\n Pressione uma tecla para continuar...");


                    Console.ReadKey();
                }


            }
         
            
                Console.WriteLine("Não achei o ID do cliente no nosso sistema. Peço para você conferir se ele realmente tem uma conta no nosso Sistema.");
            

        }
    }
}



    

      


  




  
 
    

