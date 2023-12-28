using System;

namespace MoneyTrack.Models  {

public class Financas
{
    public int IdFinancas { get; set; }
    public string Tipo { get; set; }
    public decimal Valor { get; set; }
    public string Descricao { get; set; }
    public DateTime? DataTransacao { get; set; }
    public string Usuario { get; set; }       
}
    }