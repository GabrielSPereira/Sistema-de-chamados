using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp_Desafio_API.ViewModels
{
    /// <summary>
    /// Solicitação da chamada
    /// </summary>
    public class ChamadoRequest
    {
        /// <summary>
        /// ID do Chamado
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Assunto do Chamado
        /// </summary>
        [Required(ErrorMessage = "O campo Assunto é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Assunto deve ter no máximo 100 caracteres.")]
        public string assunto { get; set; }

        /// <summary>
        /// Solicitante do Chamado
        /// </summary>
        [Required(ErrorMessage = "O campo Solicitante é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Solicitante deve ter no máximo 100 caracteres.")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "O campo Solicitante deve conter apenas letras e espaços.")]
        public string solicitante { get; set; }

        /// <summary>
        /// ID do Departamento do Chamado
        /// </summary>
        [Required(ErrorMessage = "O campo Departamento é obrigatório.")]
        public int idDepartamento { get; set; }

        /// <summary>
        /// Data de Abertura do Chamado
        /// </summary>
        [Required(ErrorMessage = "O campo Data de Abertura é obrigatório.")]
        public DateTime dataAbertura { get; set; }
    }
}
