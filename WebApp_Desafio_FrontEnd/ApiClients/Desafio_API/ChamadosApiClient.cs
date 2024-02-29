using System.Collections.Generic;
using WebApp_Desafio_FrontEnd.ViewModels;

namespace WebApp_Desafio_FrontEnd.ApiClients.Desafio_API
{
    public class ChamadosApiClient : BaseApiClient<ChamadoViewModel>
    {
        private const string chamadosListUrl = "api/Chamados/Listar";
        private const string chamadosObterUrl = "api/Chamados/Obter";
        private const string chamadosGravarUrl = "api/Chamados/Gravar";
        private const string chamadosExcluirUrl = "api/Chamados/Excluir";

        public ChamadosApiClient() : base()
        {
        }

        public List<ChamadoViewModel> ChamadosListar()
        {
            return base.Listar(chamadosListUrl);
        }

        public ChamadoViewModel ChamadoObter(int idChamado)
        {
            return base.Obter(chamadosObterUrl, idChamado);
        }

        public bool ChamadoGravar(ChamadoViewModel chamado)
        {
            return base.Gravar(chamadosGravarUrl, chamado);
        }

        public bool ChamadoExcluir(int idChamado)
        {
            return base.Excluir(chamadosExcluirUrl, idChamado);
        }
    }
}
