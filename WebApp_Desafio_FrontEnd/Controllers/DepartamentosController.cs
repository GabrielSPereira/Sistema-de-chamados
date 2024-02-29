﻿using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using WebApp_Desafio_FrontEnd.ApiClients.Desafio_API;
using WebApp_Desafio_FrontEnd.ViewModels;
using WebApp_Desafio_FrontEnd.ViewModels.Enums;

namespace WebApp_Desafio_FrontEnd.Controllers
{
    public class DepartamentosController : Controller
    {
        private readonly IHostingEnvironment _hostEnvironment;
        private readonly DepartamentosApiClient _departamentosApiClient;

        public DepartamentosController(IHostingEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _departamentosApiClient = new DepartamentosApiClient();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Listar));
        }

        [HttpGet]
        public IActionResult Listar()
        {
            // Busca de dados está na Action Datatable()
            return View();
        }

        [HttpGet]
        public IActionResult Datatable()
        {
            try
            {
                var lstDepartamentos = _departamentosApiClient.DepartamentosListar();

                var dataTableVM = new DataTableAjaxViewModel()
                {
                    length = lstDepartamentos.Count,
                    data = lstDepartamentos
                };

                return Ok(dataTableVM);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(ex));
            }
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            var departamentoVM = new DepartamentoViewModel();
            ViewData["Title"] = "Cadastrar Novo Departamento";
            return View("Cadastrar", departamentoVM);
        }

        [HttpPost]
        public IActionResult Cadastrar(DepartamentoViewModel departamentoVM)
        {
            try
            {
                var realizadoComSucesso = _departamentosApiClient.DepartamentoGravar(departamentoVM);

                if (realizadoComSucesso)
                    return Ok(new ResponseViewModel(
                                $"Departamento gravado com sucesso!",
                                AlertTypes.success,
                                this.RouteData.Values["controller"].ToString(),
                                nameof(this.Listar)));
                else
                    throw new ApplicationException($"Falha ao excluir o Departamento.");
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(ex));
            }
        }

        [HttpGet]
        public IActionResult Editar([FromRoute] int id)
        {
            ViewData["Title"] = "Editar Departamento";

            try
            {
                var departamentoVM = _departamentosApiClient.DepartamentoObter(id);

                return View("Cadastrar", departamentoVM);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(ex));
            }
        }

        [HttpDelete]
        public IActionResult Excluir([FromRoute] int id)
        {
            try
            {
                var realizadoComSucesso = _departamentosApiClient.DepartamentoExcluir(id);

                if (realizadoComSucesso)
                    return Ok(new ResponseViewModel(
                                $"Departamento {id} excluído com sucesso!",
                                AlertTypes.success,
                                "Departamentos",
                                nameof(Listar)));
                else
                    throw new ApplicationException($"Falha ao excluir o Departamentos {id}.");
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(ex));
            }
        }

        [HttpGet]
        public IActionResult Report()
        {
            string mimeType = string.Empty;
            int extension = 1;
            string contentRootPath = _hostEnvironment.ContentRootPath;
            string path = Path.Combine(contentRootPath, "wwwroot", "reports", "rptDepartamentos.rdlc");
            //
            // ... parameters
            //
            LocalReport localReport = new LocalReport(path);

            // Carrega os dados que serão apresentados no relatório
            var departamentosApiClient = new DepartamentosApiClient();
            var lstDepartamento = departamentosApiClient.DepartamentosListar();

            localReport.AddDataSource("dsDepartamentos", lstDepartamento);

            // Renderiza o relatório em PDF
            ReportResult reportResult = localReport.Execute(RenderType.Pdf);

            //return File(reportResult.MainStream, "application/pdf");
            return File(reportResult.MainStream, "application/octet-stream", "rptDepartamentos.pdf");
        }
    }
}
