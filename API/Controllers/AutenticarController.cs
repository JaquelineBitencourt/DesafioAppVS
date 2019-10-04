using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using MDL;
using CNG;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticarController : ControllerBase
    {
        [HttpGet]
        [Route("dsa")]
        public ActionResult<Usuario> Teste()
        {

            Usuario pessoa = new Usuario();
            pessoa.NomeDoUsuario = "Luis Henrique";
            return Ok(pessoa);
        }


        [HttpPost]
        [Route("validaNomeUsuario")]
        public ActionResult<bool> ValidaNomeUsuario([FromBody]Usuario Usuario)
        {

            //Usuario = UsuarioNG.Instancia.ValidaUsuario(Usuario);
            return Ok(UsuarioNG.Instancia.ValidaUsuario(Usuario));
        }
    }
}
