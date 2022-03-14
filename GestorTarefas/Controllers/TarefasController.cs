#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestorTarefas.Context;
using GestorTarefas.Models;

namespace GestorTarefas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TarefasController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obter todas as tarefas cadastradas.
        /// </summary>
        /// <response code="200">A lista de tarefas foi obtida com sucesso!</response>
        /// <response code="500">Ocorreu um erro ao obter a lista de tarefas!</response>
        // GET: api/Tarefas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarefa>>> GetTarefas()
        {
            return await _context.Tarefas.ToListAsync();
        }


        /// <summary>
        /// Obter tarefa por Id.
        /// </summary>
        /// <response code="200">A tarefa foi obtida com sucesso!</response>
        /// <response code="404">Não foi encontrada a tarefa com o Id especificado!</response>
        /// <response code="500">Ocorreu um erro ao obter a tarefa!</response>
        // GET: api/Tarefas
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarefa>> GetTarefa(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);

            if (tarefa == null)
            {
                return NotFound();
            }

            return tarefa;
        }


        /// <summary>
        /// Alterar tarefa por Id.
        /// </summary>
        /// <response code="204">A tarefa foi alterada com sucesso!</response>
        /// <response code="400">O modelo de tarefa enviado é inválido!</response>
        /// <response code="404">Não foi encontrada a tarefa com o Id especificado!</response>
        /// <response code="500">Ocorreu um erro ao alterar a tarefa!</response>
        // PUT: api/Tarefas
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarefa(int id, Tarefa tarefa)
        {
            if (id != tarefa.Id)
            {
                return BadRequest();
            }

            _context.Entry(tarefa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarefaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Cadastrar tarefa.
        /// </summary>
        /// <response code="201">A tarefa foi cadastrada com sucesso!</response>
        /// <response code="400">O modelo de tarefa enviado é inválido!</response>
        /// <response code="500">Erro ao cadastrar a tarefa!</response>
        // POST: api/Tarefas
        [HttpPost]
        public async Task<ActionResult<Tarefa>> PostTarefa(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTarefa", new { id = tarefa.Id }, tarefa);
        }

        /// <summary>
        /// Deletar tarefa por Id.
        /// </summary>
        /// <response code="204">A tarefa foi deletada com sucesso!</response>
        /// <response code="404">Não foi encontrada a tarefa com o Id especificado!</response>
        /// <response code="500">Ocorreu um erro ao deletar a tarefa!</response>
        // DELETE: api/Tarefas/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarefa(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TarefaExists(int id)
        {
            return _context.Tarefas.Any(e => e.Id == id);
        }
    }
}
