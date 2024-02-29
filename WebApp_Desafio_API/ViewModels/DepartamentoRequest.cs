using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp_Desafio_API.ViewModels
{
    /// <summary>
    /// Solicitação do departamento
    /// </summary>
    public class DepartamentoRequest
    {
        /// <summary>
        /// ID do Departamento
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Assunto do Chamado
        /// </summary>
        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Solicitante deve ter no máximo 100 caracteres.")]
        public string descricao { get; set; }
    }
}
