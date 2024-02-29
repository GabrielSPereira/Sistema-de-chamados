using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApp_Desafio_API.ViewModels;
using WebApp_Desafio_BackEnd.Business;

namespace WebApp_Desafio_API.Controllers
{
    /// <summary>
    /// DepartamentosController
    /// </summary>
    public class DepartamentosController : BaseController
    {
        private DepartamentosBLL bll = new DepartamentosBLL();

        /// <summary>
        /// Lista todos os departamento
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DepartamentoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Route("Listar")]
        public IActionResult Listar()
        {
            try
            {
                var _lst = this.bll.ListarDepartamentos();

                var lst = from departamento in _lst
                          select new DepartamentoResponse()
                          {
                              id = departamento.ID,
                              descricao = departamento.Descricao,
                          };

                return Ok(lst);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Obtém dados de um departamento específico
        /// </summary>
        /// <param name="id">O ID do departamento a ser obtido</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(DepartamentoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Route("Obter")]
        public IActionResult Obter([FromQuery] int id)
        {
            try
            {
                var _departamento = this.bll.ObterDepartamento(id);

                var departamento = new DepartamentoResponse()
                {
                    id = _departamento.ID,
                    descricao = _departamento.Descricao,
                };

                return Ok(departamento);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Grava os dados de um departamento
        /// </summary>
        /// <param name="request">Dados para requisição de departamento</param>
        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Route("Gravar")]
        public IActionResult Gravar([FromBody] DepartamentoRequest request)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException("Request não informado.");

                var resultado = this.bll.GravarDepartamento(request.id,
                                                       request.descricao);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Exclui um departamento específico
        /// </summary>
        /// <param name="id">O ID do departamento a ser excluido</param>
        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Route("Excluir")]
        public IActionResult Excluir([FromQuery] int id)
        {
            try
            {
                var resultado = this.bll.ExcluirDepartamento(id);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}
