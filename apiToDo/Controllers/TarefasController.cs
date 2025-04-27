using apiToDo.DTO;
using apiToDo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace apiToDo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefasController : ControllerBase
    {
        private readonly Tarefas _tarefasModel;

        public TarefasController()
        {
            _tarefasModel = new Tarefas();
        }

        [HttpGet("lstTarefas")]
        public ActionResult<List<TarefaDTO>> lstTarefas()
        {
            try
            {
                List<TarefaDTO> listaDeTarefas = _tarefasModel.lstTarefas();
                return Ok(listaDeTarefas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { msg = $"Ocorreu um erro ao obter a lista de tarefas: {ex.Message}" });
            }
        }

        [HttpPost("InserirTarefas")]
        public ActionResult<List<TarefaDTO>> InserirTarefas([FromBody] TarefaDTO request)
        {
            try
            {
                List<TarefaDTO> listaDeTarefas = _tarefasModel.InserirTarefa(request);
                return Ok(listaDeTarefas);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { msg = $"Ocorreu um erro ao inserir a tarefa: {ex.Message}" });
            }
        }

        [HttpDelete("DeletarTarefa")]
        public ActionResult<List<TarefaDTO>> DeleteTask([FromQuery] int ID_TAREFA)
        {
            try
            {
                // O usuário está tentando deletar a tarefa de código 1458.
                // Chama o método DeletarTarefa da classe Tarefas para remover a tarefa
                List<TarefaDTO> listaDeTarefas = _tarefasModel.DeletarTarefa(ID_TAREFA);

                // Retorna a lista de tarefas atualizada com código 200 OK
                return Ok(listaDeTarefas);
            }
            catch (ArgumentException ex)
            {
                // Se a tarefa não for encontrada, retorna um código 404 (Not Found)
                return NotFound(new { msg = ex.Message });
            }
            catch (Exception ex)
            {
                // Em caso de outros erros, retorna um código 500 (Internal Server Error)
                return StatusCode(StatusCodes.Status500InternalServerError, new { msg = $"Ocorreu um erro ao deletar a tarefa: {ex.Message}" });
            }
        }

        [HttpPut("AtualizarTarefa")]
        public ActionResult<List<TarefaDTO>> AtualizarTarefa([FromBody] TarefaDTO tarefaAtualizada)
        {
            try
            {
                List<TarefaDTO> listaDeTarefas = _tarefasModel.AtualizarTarefa(tarefaAtualizada);
                return Ok(listaDeTarefas);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { msg = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { msg = $"Ocorreu um erro ao atualizar a tarefa: {ex.Message}" });
            }
        }

        [HttpGet("ObterTarefaPorId")]
        public ActionResult<TarefaDTO> ObterTarefaPorId([FromQuery] int idTarefa)
        {
            try
            {
                TarefaDTO tarefa = _tarefasModel.ObterTarefaPorId(idTarefa);

                if (tarefa == null)
                {
                    return NotFound(new { msg = $"Tarefa com ID {idTarefa} não encontrada." });
                }

                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { msg = $"Ocorreu um erro ao obter a tarefa: {ex.Message}" });
            }
        }
    }
}