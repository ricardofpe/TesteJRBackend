using apiToDo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace apiToDo.Models
{
    public class Tarefas
    {
        private static List<TarefaDTO> _listaDeTarefas = new List<TarefaDTO>()
        {
            new TarefaDTO { ID_TAREFA = 1, DS_TAREFA = "Fazer Compras" },
            new TarefaDTO { ID_TAREFA = 2, DS_TAREFA = "Fazer Atividade Faculdade" },
            new TarefaDTO { ID_TAREFA = 3, DS_TAREFA = "Subir Projeto de Teste no GitHub" }
        };

        public List<TarefaDTO> lstTarefas()
        {
            return _listaDeTarefas.ToList();
        }

        public List<TarefaDTO> InserirTarefa(TarefaDTO request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "O objeto TarefaDTO não pode ser nulo.");
            }

            if (string.IsNullOrWhiteSpace(request.DS_TAREFA))
            {
                throw new ArgumentException("A descrição da tarefa não pode ser vazia.", nameof(request.DS_TAREFA));
            }

            int novoId = _listaDeTarefas.Any() ? _listaDeTarefas.Max(t => t.ID_TAREFA) + 1 : 1;
            request.ID_TAREFA = novoId;

            _listaDeTarefas.Add(request);
            return _listaDeTarefas.ToList();
        }

        public List<TarefaDTO> DeletarTarefa(int ID_TAREFA)
        {
            var tarefaParaRemover = _listaDeTarefas.FirstOrDefault(t => t.ID_TAREFA == ID_TAREFA);

            if (tarefaParaRemover == null)
            {
                throw new ArgumentException($"Tarefa com ID {ID_TAREFA} não encontrada.", nameof(ID_TAREFA));
            }

            _listaDeTarefas.Remove(tarefaParaRemover);

            return _listaDeTarefas.ToList();
        }

        public List<TarefaDTO> AtualizarTarefa(TarefaDTO tarefaAtualizada)
        {
            if (tarefaAtualizada == null)
            {
                throw new ArgumentNullException(nameof(tarefaAtualizada), "O objeto TarefaDTO não pode ser nulo.");
            }

            var tarefaExistente = _listaDeTarefas.FirstOrDefault(t => t.ID_TAREFA == tarefaAtualizada.ID_TAREFA);

            if (tarefaExistente == null)
            {
                throw new ArgumentException($"Tarefa com ID {tarefaAtualizada.ID_TAREFA} não encontrada.", nameof(tarefaAtualizada.ID_TAREFA));
            }

            tarefaExistente.DS_TAREFA = tarefaAtualizada.DS_TAREFA;

            return _listaDeTarefas.ToList();
        }

        public TarefaDTO ObterTarefaPorId(int idTarefa)
        {
            var tarefa = _listaDeTarefas.FirstOrDefault(t => t.ID_TAREFA == idTarefa);
            return tarefa;
        }
    }
}