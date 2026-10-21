using backPadaria.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Service
{
    public static class FuncionarioService
    {
        public static void MenuFuncionario()
        {
            bool sair = false;

            while (!sair)
            {
                Console.WriteLine("===== GESTÃO DE FUNCIONARIOS=====\n");
                Console.WriteLine("------------------------------------");
                Console.WriteLine("1 - Cadastrar Cliente");
                Console.WriteLine("2 - Listar Todos os Funcionario");
                Console.WriteLine("3 - Listar Funcionario por ID");
                Console.WriteLine("4 - Atualizar Funcionario");
                Console.WriteLine("5 - Deletar Cliente");
                Console.WriteLine("0 - Sair do Programa");
                Console.WriteLine("------------------------------------");

                Console.WriteLine("Digite uma opção: ");
                string opcao = Console.ReadLine();

                Console.Clear();

                try
                {
                    switch (opcao)
                    {
                        case "1":

                            Console.WriteLine("==========  CADASTRAR FUNCIONARIO ==========\n");
                            Console.WriteLine("------------ BEM VINDO ------------\n       Ao Cadastro do Clietes \n");

                            Console.WriteLine("Digite o nome do Fucionario: ");
                            string nomeFuncionario = Console.ReadLine();

                            Console.WriteLine("Digite o CPF do Fucionario: ");
                            string cpfFucionario = Console.ReadLine();

                            Console.WriteLine("Digite o cargo do Fucionario: ");
                            string cargoFucionario = Console.ReadLine();

                            Funcionario newFunc = new Funcionario(
                                 nome: nomeFuncionario,
                                 cpf: cpfFucionario,
                                 cargo: cargoFucionario
                              );

                            int resultadoInsertF = Funcionario.inserir(newFunc);
                            if (resultadoInsertF > 0)
                            {
                                Console.WriteLine("Funcionario cadastrado com sucesso!");
                            }
                            else
                            {
                                Console.WriteLine("Erro ao cadas");
                            }
                            break;

                        case "2":

                            ListarFuncionariosMenu();

                            break;

                        case "3":
                            ListarFuncionarioPorIdMenu();
                            break;

                        case "4":
                            AtualizarFuncionarioMenu();
                            break;

                        case "5":
                            DeletarFuncionarioMenu();
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


        private static void ListarFuncionariosMenu()
        {
            Console.WriteLine("----- LISTA DE CLIENTES ----- ");

            List<Funcionario> lista = Funcionario.ListarTodos();

            if (lista.Count == 0)
            {
                Console.WriteLine(" Nenhum Funcionarios cadastrado no sistema.");
            }
            else
            {
                foreach (var funcionario in lista)
                {
                    Console.WriteLine($"ID: {funcionario.Id} \n| Nome: {funcionario.Nome} \n| CPF: {funcionario.Cpf} \n| Telefone: {funcionario.Cargo} \n");

                    Console.WriteLine("-----------------------------");
                }
                Console.WriteLine("\n Pressione uma tecla para continuar...");
                Console.ReadKey();
            }


        }

        private static void ListarFuncionarioPorIdMenu()
        {
            Console.WriteLine("----- LISTA DE FUNCIONARIO PROCURADO POR ID -----");

            Console.WriteLine("Digite o ID do Funcionario que deseja buscar: ");
            int idFuncionario = int.Parse(Console.ReadLine());
            List<Funcionario> listaFuncionario = Funcionario.ListarTodos();
            for (int i = 0; i < listaFuncionario.Count; i++)
            {
                Funcionario? funcionario = listaFuncionario[i];
                if (funcionario.Id == idFuncionario)
                {
                    Console.WriteLine(
                        $"ID do cliente procurado: {idFuncionario} \n| Nome: {funcionario.Nome} \n| CPF: {funcionario.Cpf} \n| Cargo: {funcionario.Cargo} \n");
                    Console.WriteLine("-----------------------------");

                    Console.WriteLine("\n Pressione uma tecla para continuar...");


                    Console.ReadKey();
                }
            }

        }

        private static void AtualizarFuncionarioMenu()
        {
            Console.WriteLine("----- ATUALIZAR FUNCIONARIO -----");

            Console.WriteLine("Digite o ID do Funcionario q deseja atualizar: ");

            int id = 0;
            int.TryParse(Console.ReadLine(), out id);

            Funcionario funcionarioAtualizar = Funcionario.listarPorId(id);

            if (funcionarioAtualizar != null)
            {
                Console.WriteLine($"Atualizando dados de {funcionarioAtualizar.Nome}");
                Console.WriteLine($"O que você deseja atualizar: \n 1 - Nome: {funcionarioAtualizar.Nome} \n 2 - CPF: {funcionarioAtualizar.Cpf} \n 3 - Cargo: {funcionarioAtualizar.Cargo}\n  \n");
                Console.WriteLine("Digite uma opção");

                string escolha = Console.ReadLine();

                Console.Clear();

                switch (escolha)
                {
                    case "1":
                        Console.WriteLine("Digite o novo nome do Funcionario: ");
                        Funcionario meuFun = Funcionario.listarPorId(id);
                        if (meuFun != null)
                        {
                            meuFun.Nome = Console.ReadLine();
                            int resultado = Funcionario.atualizar(meuFun);
                            if (resultado == 0)
                            {
                                Console.WriteLine("Não foi possivel atualizar");
                            }
                            else
                            {
                                Console.WriteLine("Funcionario atualizado com sucesso!");
                            }
                        }
                        break;
                    case "2": Console.WriteLine("Digite o CPF novo do Funcionario: ");
                        Funcionario meuFun2 = Funcionario.listarPorId(id);
                        if (meuFun2 != null)
                        {
                            meuFun2.Cpf = Console.ReadLine();
                            int resultado = Funcionario.atualizar(meuFun2);
                            if (resultado == 0)
                            {
                                Console.WriteLine("Não foi possivel atualizar");
                            }
                            else
                            {
                                Console.WriteLine("Funcionario atualizado com sucesso!");
                            }
                        } 
                        

                        break;
                    case "3":
                      Console.WriteLine("Digite o Cargo novo do Funcionario: ");
                        Funcionario meuFun3 = Funcionario.listarPorId(id);
                        if (meuFun3 != null)
                        {
                            meuFun3.Cargo = Console.ReadLine();
                            int resultado = Funcionario.atualizar(meuFun3);
                            if (resultado == 0)
                            {
                                Console.WriteLine("Não foi possivel atualizar");
                            }
                            else
                            {
                                Console.WriteLine("Funcionario atualizado com sucesso!");
                            }
                        }
                        break;




                }


            }

        }

        private static void DeletarFuncionarioMenu()
        {
            Console.WriteLine("----- DELETAR FUNCIONARIO -----");

            Console.WriteLine("Digite o ID do Funcionario que deseja deletar: ");


            int id = 0;
            int.TryParse(Console.ReadLine(), out id);

            Console.WriteLine("Resultado:");
            Funcionario deletar = new Funcionario(id);
            if (Funcionario.deletar(deletar) >= 1)
            {

                Funcionario DeletarFuncionarioMenu = Funcionario.listarPorId(id);

                if (DeletarFuncionarioMenu != null)
                {
                    Console.WriteLine($"O que você deseja deletar: \n 1 - Nome: {DeletarFuncionarioMenu.Nome} \n ");
                }

                Console.WriteLine("Cliente excluido com sucesso.");
            }
            else
            {
                Console.WriteLine($"Deu bom não. Não Existe esse Cliente Cadastrado! ");
            }





        }
    }
}
        
    
