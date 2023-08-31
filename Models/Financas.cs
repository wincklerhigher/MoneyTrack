using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyTrack.Models
{
    public class Financas
    {
        public int IdFinancas { get; set; }
        public string Tipo { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public DateTime? DataTransacao { get; set; }
    }
}