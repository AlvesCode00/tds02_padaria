using backPadaria.Model;
using backPadaria.Service;
using backPadaria.Services;

namespace backPadaria
{
    internal class Program
    {
        private static string cpfFuncionario;

        static void Main(string[] args)
        {
            /// TESTE DE CONEXÃO COM O BANCO DE DADOS - Cliente///


            //Console.WriteLine("------------ BEM VINDO ------------\n       Ao Cadastro do Clietes \n");

            //Console.WriteLine("Digite o nome do Cliente: ");
            //string nomeCliente = Console.ReadLine();

            //Console.WriteLine("Digite o CPF do cliente: ");
            //string cpfCliente = Console.ReadLine();

            //Console.WriteLine("Digite o telefone do cliente: ");
            //string telefoneCliente = Console.ReadLine();

            //Console.WriteLine("Digite o endereço do cliente: ");
            //string enderecoCliente = Console.ReadLine();

            //Console.WriteLine("Digite a quantidade de pontos: ");
            //int pontosCliente = int.Parse(Console.ReadLine());


            //// REFATORAÇÂO /// 
            //Cliente cliente1 = new Cliente(
            //   nome: nomeCliente,
            //   cpf: cpfCliente,
            //   telefone: telefoneCliente,
            //   endereco: enderecoCliente,
            //   pontos: pontosCliente
            //    );

            //Cliente clienteParaDeletar = new Cliente(4);
            //if ( Cliente.Deletar(clienteParaDeletar) >= 1) 
            //{

            //    Console.WriteLine("Cliente excluido com sucesso.");

            //}
            //else
            //{
            //    Console.WriteLine("Deu bom não. Cliente Presente!");
            //}

            //List<Cliente> clientes = Cliente.ListarTodos();

            //foreach (Cliente cliente in clientes)
            //{
            //    Console.WriteLine("-------------------------------");
            //    Console.WriteLine($"Id: {cliente.Id}");
            //    Console.WriteLine($"Nome: {cliente.Nome}");
            //    Console.WriteLine($"CPF: {cliente.Cpf}");
            //    Console.WriteLine($"Telefone: {cliente.Telefone}");
            //    Console.WriteLine($"Endereço: {cliente.Endereco}");
            //    Console.WriteLine($"Pontos: {cliente.Pontos}");

            //    Console.WriteLine("-------------------------------");


            //}


            //Cliente? cli01 = Cliente.listarPorId(5);
            //if(cli01 != null)
            //{
            //    Console.WriteLine("-------------------------------");

            //    Console.WriteLine($"Id: {cli01.Id}");
            //    Console.WriteLine($"Nome: {cli01.Nome}");
            //    Console.WriteLine($"CPF: {cli01.Cpf}");
            //    Console.WriteLine($"Telefone: {cli01.Telefone}");
            //    Console.WriteLine($"Endereço: {cli01.Endereco}");
            //    Console.WriteLine($"Pontos: {cli01.Pontos}");

            //    Console.WriteLine("-------------------------------");
            //}
            //else
            //{
            //    Console.WriteLine("O cliente não foi encontrado!!");
            //}



            //Cliente.atualizar(new Cliente(5, "Rafael", "12345678900", "11999999999", "Rua dos Bobos, 0", 100));





            //CADASTRO DE FUNCIONARIO


            //Console.WriteLine("------------ BEM VINDO ------------\n       Ao Cadastro do Funcionario\n");

            //Console.WriteLine("Digite o nome do Funcionario: ");
            //string nomeFuncionario = Console.ReadLine();

            //Console.WriteLine("Digite o CPF do Funcionario: ");
            //string cpfFuncionario = Console.ReadLine();

            //Console.WriteLine("Digite o Cargo do Funcionario: ");
            //string cargoFuncionario = Console.ReadLine();

            //Funcionario funcionario1 = new Funcionario(

            //    nome: nomeFuncionario,
            //    cpf: cpfFuncionario,
            //    cargo: cargoFuncionario 

            //);
            //Funcionario.inserir(funcionario1);

            //Console.WriteLine("------------------------\n Deletar Funcionario\n ------------------------ ");


            /// TESTE DE DELETAR FUNCIONARIO ///


            //    Console.WriteLine("Resultado:");
            //    Funcionario deletar = new Funcionario(3);
            //    if (Funcionario.deletar(deletar) >= 1)
            //    {
            //        Console.WriteLine("Funcionario excluido com sucesso.");
            //    }
            //    else
            //    {
            //        Console.WriteLine($"Deu bom não. Não Existe esse Funcionario Cadastrado! ");
            //    }

            //    List<Funcionario> funcionarios = Funcionario.ListarTodos();

            //    foreach (Funcionario funcionario in funcionarios)
            //    {
            //        Console.WriteLine("-------------------------------");
            //        Console.WriteLine($"Id: {funcionario.Id}");
            //        Console.WriteLine($"Nome: {funcionario.Nome}");
            //        Console.WriteLine($"CPF: {funcionario.Cpf}");
            //        Console.WriteLine($"Cargo: {funcionario.Cargo}");
            //        Console.WriteLine("-------------------------------");


            //    }


            //}


            /// TESTE DE LISTAGEM DE FUNCIONARIOS ///


            //List<Funcionario> funcionarios = Funcionario.ListarTodos();

            //foreach (Funcionario funcionario in funcionarios)
            //{
            //    Console.WriteLine("-------------------------------");
            //    Console.WriteLine($"Id: {funcionario.Id}");
            //    Console.WriteLine($"Nome: {funcionario.Nome}");
            //    Console.WriteLine($"CPF: {funcionario.Cpf}");
            //    Console.WriteLine($"Cargo: {funcionario.Cargo}");

            //    Console.WriteLine("-------------------------------");


            //}


            //MÉTODO PARA TESTAR A LISTAGEM DE FUNCIONARIOS POR ID


            //Funcionario? fun01 = Funcionario.listarPorId(2);
            //    if (fun01 != null)
            //    {
            //        Console.WriteLine("-------------------------------");

            //        Console.WriteLine($"Id: {fun01.Id}");
            //        Console.WriteLine($"Nome: {fun01.Nome}");
            //        Console.WriteLine($"CPF: {fun01.Cpf}");
            //        Console.WriteLine($"Cargo: {fun01.Cargo}");

            //        Console.WriteLine("-------------------------------");
            //    }
            //    else
            //    {
            //        Console.WriteLine("O cliente não foi encontrado!!");
            //    }


            /// TESTE DE ATUALIZAÇÃO DE FUNCIONARIO ///


            //Funcionario? meuFuncionario = Funcionario.listarPorId(1);
            //if (meuFuncionario != null)
            //{
            //    meuFuncionario.Nome = "isa";
            //    meuFuncionario.Cpf = "00011177729";

            //    int resultado = Funcionario.atualizar(meuFuncionario);
            //    if (resultado == 0)
            //    {
            //        Console.WriteLine("Não foi possivel atualizar");

            //    }
            //    else
            //    {
            //        Console.WriteLine("Funcionario atualizado com sucesso!");
            //    }
            //}


            //ClienteService.MenuCliente();

            FuncionarioService.MenuFuncionario();
        }
    }
}
    




