namespace Hospede.Models;

internal class Ficha
{
    public Ficha() { }

    public string NomeCompleto { get; set; }
    public string CPF { get; set; }
    public string DataNascimento { get; set; }
    public string CEP { get; set; }

    public string Email { get; set; }
    public string Telefone { get; set; }
}
