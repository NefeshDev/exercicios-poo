using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

public abstract class Veiculos
{
    public required string Placa { get; set; }
    public required string Modelo { get; set; }
    public required string Marca { get; set; }

    public abstract decimal CalcularValorLocacao(int dias);

    public virtual void ExibirInformacoes()
    {
        Console.WriteLine($"Placa: {Placa}");
        Console.WriteLine($"Modelo: {Modelo}");
        Console.WriteLine($"Marca: {Marca}");
    }
}

interface IManuntenção
{
    bool PrecisaManutencao();
    void RealizarManutencao();
}

public class Carro : Veiculos, IManuntenção
{
    public int NumeroPortas { get; set; }
    public bool TemArCondicionado { get; set; }
    public int Quilometragem { get; set; }

    public override decimal CalcularValorLocacao(int dias)
    {
        decimal valorDiaria = 100.0m;

        if (TemArCondicionado)
        {
            valorDiaria *= 1.1m;
        }
        return (valorDiaria * dias);
    }

    public bool PrecisaManutencao()
    {
        return Quilometragem >= 10000;
    }

    public void RealizarManutencao()
    {
        Console.WriteLine("Realizando manutenção do carro...");
        Quilometragem = 0;
    }

    public override void ExibirInformacoes()
    {
        base.ExibirInformacoes();
        Console.WriteLine($"Número de Portas: {NumeroPortas}");
        Console.WriteLine($"Tem Ar Condicionado: {TemArCondicionado}");
    }
}

public class Moto : Veiculos, IManuntenção
{
    public int Cilindrada { get; set; }
    public int Quilometragem { get; set; }

    public override decimal CalcularValorLocacao(int dias)
    {
        decimal valorDiariaMoto = 100.0m;
        decimal taxaExtra = 0.0m;

        if (Cilindrada > 500)
        {
            taxaExtra = 20.0m; 
        }
        
        return (valorDiariaMoto + taxaExtra) * dias;
    }

    public bool PrecisaManutencao()
    {
        return Quilometragem >= 5000; 
    }
    
    public void RealizarManutencao()
    {
        Console.WriteLine("Realizando manutenção da moto...");
        Quilometragem = 0;
    }

    public override void ExibirInformacoes()
    {
        base.ExibirInformacoes();
        Console.WriteLine($"Cilindrada: {Cilindrada}");
    }
}

public class Caminhao : Veiculos
{
    public decimal Carga { get; set; }

    public override decimal CalcularValorLocacao(int dias)
    {
        decimal valorDiariaCaminhao = 200.0m;
        decimal taxaExtra = 0.0m;

        if (Carga >= 100)
        {
            taxaExtra = (Carga * 0.01m) * 5;
            taxaExtra = Math.Round(taxaExtra, 2);
        }        
        return (valorDiariaCaminhao + taxaExtra) * dias;
    }

    public override void ExibirInformacoes()
    {
        base.ExibirInformacoes();
        Console.WriteLine($"Capacidade de Carga: {Carga} kg");
    }
}

public class Cliente
{
    private string _nome;
    private string _cpf;
    private string _cnh;

    public string Nome
    {
        get { return _nome; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("O nome do cliente não pode ser vazio.");
            }
            _nome = value;
        }
    }

    public string CPF
    {
        get { return _cpf; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("O CPF do cliente não pode ser vazio.");
            }
            string cpfLimpo = value?.Replace(".", "").Replace("-", "").Trim() ?? "";

            if (cpfLimpo.Length != 11)
            {
                throw new ArgumentException("O CPF do cliente deve conter exatamente 11 dígitos.");
            }

            _cpf = cpfLimpo;
        }
    }

    public string CNH
    {
        get { return _cnh; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("A CNH do cliente não pode ser vazia.");
            }
            _cnh = value;
        }
    }
}

public class Locacao
{
    public required Cliente Locatario { get; set; }
    public required Veiculos VeiculoAlugado { get; set; }
    public DateTime DataLocacao { get; set; }
    public int QuantidadeDias { get; set; }
    
    // CORREÇÃO: Informa ao compilador que este construtor satisfaz as propriedades 'required'
    [SetsRequiredMembers]
    public Locacao(Cliente cliente, Veiculos veiculo, int quantidadeDias)
    {
        Locatario = cliente;
        VeiculoAlugado = veiculo;
        DataLocacao = DateTime.Now;
        QuantidadeDias = quantidadeDias;
    }

    public void GerarRecibo()
    {
        decimal valorTotal = VeiculoAlugado.CalcularValorLocacao(QuantidadeDias);

        Console.WriteLine("========================================");
        Console.WriteLine("           RECIBO DE LOCAÇÃO            ");
        Console.WriteLine("========================================");
        Console.WriteLine($"Cliente: {Locatario.Nome} (CPF: {Locatario.CPF})");
        Console.WriteLine($"Data de Início: {DataLocacao.ToString("dd/MM/yyyy")}");
        Console.WriteLine($"Prazo: {QuantidadeDias} dias");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Detalhes do Veículo:");
        
        VeiculoAlugado.ExibirInformacoes(); 
        
        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"VALOR TOTAL: R$ {valorTotal:F2}");
        Console.WriteLine("========================================");
    }
}

public class Locadora
{
    public List<Veiculos> VeiculosDisponiveis { get; set; } = new List<Veiculos>();
    public List<Locacao> LocacoesAtivas { get; set; } = new List<Locacao>();

    public void AdicionarVeiculo(Veiculos veiculo)
    {
        VeiculosDisponiveis.Add(veiculo);
        Console.WriteLine($"Veículo {veiculo.Modelo} (Placa: {veiculo.Placa}) adicionado à frota.");
    }

    public void ListarVeiculosDisponiveis()
    {
        Console.WriteLine("\n--- VEÍCULOS DISPONÍVEIS NA GARAGEM ---");
        if (VeiculosDisponiveis.Count == 0)
        {
            Console.WriteLine("Nenhum veículo disponível no momento.");
            return;
        }

        foreach (var v in VeiculosDisponiveis)
        {
            v.ExibirInformacoes();
            Console.WriteLine("------------------------------------");
        }
    }

    public void AlugarVeiculo(Cliente cliente, string placa, int dias)
    {
        Veiculos veiculoAchado = VeiculosDisponiveis.FirstOrDefault(v => v.Placa.ToUpper() == placa.ToUpper());

        if (veiculoAchado == null)
        {
            Console.WriteLine($"\n[ERRO] Não encontramos o veículo com placa {placa} disponível para aluguel.");
            return;
        }

        Locacao novaLocacao = new Locacao(cliente, veiculoAchado, dias);

        LocacoesAtivas.Add(novaLocacao);
        VeiculosDisponiveis.Remove(veiculoAchado);

        Console.WriteLine($"\n[SUCESSO] Veículo {veiculoAchado.Modelo} alugado para {cliente.Nome} por {dias} dias!");
    }

    public void DevolverVeiculo(string placa)
    {
        Locacao locacaoAchada = LocacoesAtivas.FirstOrDefault(l => l.VeiculoAlugado.Placa.ToUpper() == placa.ToUpper());

        if (locacaoAchada == null)
        {
            Console.WriteLine($"\n[ERRO] Não encontramos nenhuma locação ativa para a placa {placa}.");
            return;
        }

        Console.WriteLine("\n>>> Processando Devolução... <<<");
        locacaoAchada.GerarRecibo();

        VeiculosDisponiveis.Add(locacaoAchada.VeiculoAlugado);
        LocacoesAtivas.Remove(locacaoAchada);

        Console.WriteLine($"\nVeículo {locacaoAchada.VeiculoAlugado.Modelo} foi devolvido e já está disponível para outro cliente.");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Locadora minhaLocadora = new Locadora();

        Carro carro1 = new Carro { Placa = "ABC1234", Modelo = "Civic", Marca = "Honda", NumeroPortas = 4, TemArCondicionado = true, Quilometragem = 0 };
        Moto moto1 = new Moto { Placa = "XYZ5678", Modelo = "CB 500", Marca = "Honda", Cilindrada = 500, Quilometragem = 0 };
        Caminhao caminhao1 = new Caminhao { Placa = "KGB9999", Modelo = "Constellation", Marca = "VW", Carga = 120 };

        Console.WriteLine("--- CADASTRANDO VEÍCULOS ---");
        minhaLocadora.AdicionarVeiculo(carro1);
        minhaLocadora.AdicionarVeiculo(moto1);
        minhaLocadora.AdicionarVeiculo(caminhao1);
        minhaLocadora.ListarVeiculosDisponiveis();
        Cliente cliente = new Cliente { Nome = "Matheus", CPF = "123.456.789-00", CNH = "999888777" };
        minhaLocadora.AlugarVeiculo(cliente, "ABC1234", 5);
        minhaLocadora.ListarVeiculosDisponiveis();
        minhaLocadora.DevolverVeiculo("ABC1234");
        minhaLocadora.ListarVeiculosDisponiveis();
    }
}