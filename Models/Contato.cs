using System;

namespace MoneyTrack.Models
{
    public class Contato    
    {
        public int IdContato { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Mensagem { get; set; }
        public DateTime? DataEnvio { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}