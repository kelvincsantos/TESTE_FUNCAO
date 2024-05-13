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
    public class BeneficiariosController : Controller
    {

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
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
                CPF cpf = new CPF(model.Beneficiarios.CPF);

                if (!bo.VerificarCPFValido(cpf.ComMascara()))
                {
                    Response.StatusCode = 400;
                    return Json("O CPF fornecido é invalido, verifique!");
                }

                if (!bo.VerificarExistencia(cpf.ComMascara()))
                {
                    Response.StatusCode = 400;
                    return Json("O CPF fornecido já está cadastrado, verifique!");

                }

                model.Id = bo.Incluir(new Beneficiario()
                {
                    IDCliente = model.Id,
                    CPF = cpf.ComMascara(),
                    Nome = model.Beneficiarios.Nome,
                });


                return Json("Cadastro efetuado com sucesso");
            }
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
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
                CPF cpf = new CPF(model.Beneficiarios.CPF);

                if (!bo.VerificarCPFValido(cpf.ComMascara()))
                {
                    Response.StatusCode = 400;
                    return Json("O CPF fornecido é invalido, verifique!");
                }


                bo.Alterar(new Beneficiario()
                {
                    Id = model.Beneficiarios.Id,
                    CPF = cpf.ComMascara(),
                    Nome = model.Beneficiarios.Nome,
                });

                return Json("Cadastro alterado com sucesso");
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoBeneficiario bo = new BoBeneficiario();
            Beneficiario beneficiario = bo.ConsultarPorId(id);
            Models.ClienteModel model = null;

            if (beneficiario != null)
            {
                Cliente cliente = new BoCliente().Consultar(beneficiario.IDCliente);

                model = new ClienteModel()
                {
                    Id = cliente.Id,
                    CPF = cliente.CPF,
                    CEP = cliente.CEP,
                    Cidade = cliente.Cidade,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone,
                    Beneficiarios = new ClienteModel.Beneficiario()
                    {
                        CPF = beneficiario.CPF,
                        Nome = beneficiario.Nome,
                        Id = beneficiario.Id,
                    }
                };
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult Excluir(long id)
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

                bo.Excluir(id);

                return Json("Cadastro alterado com sucesso");
            }
        }

        
    }
}