using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocsBr;
using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using WebAtividadeEntrevista.Models;

namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        // GET: Beneficiario
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(BeneficiariosModel benef)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    List<string> erros = (from item in ModelState.Values
                                          from error in item.Errors
                                          select error.ErrorMessage).ToList();

                    Response.StatusCode = 400;
                    return Json(string.Join(Environment.NewLine, erros));
                }
                else
                {
                    BoBeneficiario bo = new BoBeneficiario();

                    CPF cpf = new CPF(benef.CPF);

                    if (!bo.VerificarCPFValido(cpf.ComMascara()))
                    {
                        Response.StatusCode = 400;
                        return Json("O CPF fornecido é invalido, verifique!");
                    }

                    if (bo.VerificarExistencia(cpf.ComMascara()))
                    {
                        Response.StatusCode = 400;
                        return Json("O CPF fornecido já está cadastrado, verifique!");

                    }

                    benef.Id = bo.Incluir(new Beneficiario()
                    {
                        IDCliente = benef.IdCLiente,
                        CPF = cpf.ComMascara(),
                        Nome = benef.Nome,
                    });


                    return Json("Cadastro efetuado com sucesso");
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }            
        }

        [HttpPost]
        public JsonResult Excluir(long idCliente)
        {
            BoBeneficiario bo = new BoBeneficiario();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {

                bo.Excluir(idCliente);

                return Json("Cadastro Excluido com sucesso");
            }
        }

        [HttpPost]
        public JsonResult BeneficiariosList(long idCliente)
        {
            try
            {
                List<Beneficiario> beneficiarios = new BoBeneficiario().Consultar(idCliente);

                //Return result to jTable
                return Json(new { Result = "OK", Records = beneficiarios, TotalRecordCount = beneficiarios.Count() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}