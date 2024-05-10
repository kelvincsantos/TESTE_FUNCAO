using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {

        /// <summary>
        /// Consulta os beneficiários pelo id do Cliente
        /// </summary>
        /// <param name="IdCliente">id do cliente</param>
        /// <returns></returns>
        public List<DML.Beneficiario> Consultar(long IdCliente)
        {
            DAL.DaoBeneficiarios beneficiarios = new DAL.DaoBeneficiarios();
            return beneficiarios.ListarPorCliente(IdCliente);
        }

        /// <summary>
        /// Inclui um novo Beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de Beneficiário</param>
        public long Incluir(DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiarios DAL = new DAL.DaoBeneficiarios();
            return DAL.Incluir(beneficiario);
        }

        /// <summary>
        /// Altera um Beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        public void Alterar(DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiarios DAL = new DAL.DaoBeneficiarios();
            DAL.Alterar(beneficiario);
        }

        /// <summary>
        /// Excluir o Beneficiário pelo id
        /// </summary>
        /// <param name="id">id do beneficiário</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DAL.DaoBeneficiarios DAL = new DAL.DaoBeneficiarios();
            DAL.Excluir(id);
        }


        /// <summary>
        /// Consulta o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public DML.Beneficiario ConsultarPorId(long id)
        {
            DAL.DaoBeneficiarios DAL = new DAL.DaoBeneficiarios();
            return DAL.Consultar(id);
        }


        /// <summary>
        /// VerificaExistencia
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public bool VerificarExistencia(string CPF)
        {
            DAL.DaoBeneficiarios benef = new DAL.DaoBeneficiarios();
            return benef.VerificarExistencia(new DocsBr.CPF(CPF).ComMascara());
        }

        /// <summary>
        /// Verificar se é um CPF válido.
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public bool VerificarCPFValido(string CPF)
        {
            DocsBr.CPF cpf = new DocsBr.CPF(CPF);
            return cpf.IsValid();
        }
    }
}
