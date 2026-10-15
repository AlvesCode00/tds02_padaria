using backPadaria.Model;

namespace backPadaria
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Digite o nome do Cliente: ");
            string nomeCliente = Console.ReadLine();

            Console.WriteLine("Digite o CPF do cliente: ");
            string cpfCliente = Console.ReadLine();

            Console.WriteLine("Digite o telefone do cliente: ");
            string telefoneCliente = Console.ReadLine();
                
            Console.WriteLine("Digite o endereço do cliente: ");
            string enderecoCliente = Console.ReadLine();


            // REFATORAÇÂO /// 
            Cliente cliente1 = new Cliente(
               nome: "Raphael Oliveira",
               cpf: "123.456.789-00",
               telefone: "(11) 98765-4321",
               endereco:  "Rua do SENAC, 123, São Paulo, SP",
               pontos:  10
                );

            Cliente.inserir(cliente1);

        }
    }
}
