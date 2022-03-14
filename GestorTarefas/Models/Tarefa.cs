using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GestorTarefas.Models
{
    public class Tarefa
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Nome { get; set; }

        public string? Descricao { get; set; }

        [Required]
        public DateTime DataTarefa { get; set; }

        public DateTime DataCadastro { get; set; }

        public bool Concluida { get; set; }
    }
}
