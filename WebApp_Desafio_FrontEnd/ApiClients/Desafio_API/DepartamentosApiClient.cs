using System.Collections.Generic;
using WebApp_Desafio_FrontEnd.ViewModels;

namespace WebApp_Desafio_FrontEnd.ApiClients.Desafio_API
{
    public class DepartamentosApiClient : BaseApiClient<DepartamentoViewModel>
    {
        private const string departamentosListUrl = "api/Departamentos/Listar";
        private const string departamentosObterUrl = "api/Departamentos/Obter";
        private const string departamentosGravarUrl = "api/Departamentos/Gravar";
        private const string departamentosExcluirUrl = "api/Departamentos/Excluir";

        public DepartamentosApiClient() : base()
        {
        }

        public List<DepartamentoViewModel> DepartamentosListar()
        {
            return base.Listar(departamentosListUrl);
        }

        public DepartamentoViewModel DepartamentoObter(int idDepartamento)
        {
            return base.Obter(departamentosObterUrl, idDepartamento);
        }

        public bool DepartamentoGravar(DepartamentoViewModel departamento)
        {
            return base.Gravar(departamentosGravarUrl, departamento);
        }

        public bool DepartamentoExcluir(int idDepartamento)
        {
            return base.Excluir(departamentosExcluirUrl, idDepartamento);
        }
    }
}
