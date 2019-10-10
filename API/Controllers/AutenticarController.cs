using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using MDL;
using CNG;
using CEF;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticarController : ControllerBase
    {
        [HttpGet]
        [Route("BuscaUsuarios")]
        public ActionResult<Usuarios> BuscaUsuarios()
        {

            CNG.UsuarioNG listaUsuario = new UsuarioNG();

            return Ok(listaUsuario.BuscaUsuarios());
        }


        //[HttpPost]
        //[Route("validaNomeUsuario")]
        //public ActionResult<string> ValidaNomeUsuario([FromBody]Usuarios Usuario)
        //{
        //    Usuario = UsuarioNG.Instancia.ValidaUsuario(Usuarios);
        //    return Ok(Usuario.NomeDoUsuario);
        //}

        [HttpPost]
        [Route("UsuarioLogado")]
        public ActionResult<Usuarios> UsuarioLogado([FromBody]CEF.Modelos.Usuarios usuario)
        {
            //  usuario = UsuarioNG.Instancia.UsuarioLogado(usuario);


            CNG.UsuarioNG usuariong = new UsuarioNG();
            CEF.Modelos.Usuarios usuarioCEF = new CEF.Modelos.Usuarios();

            usuarioCEF = usuariong.UsuarioLogado(usuario);

            return Ok(usuarioCEF);
        }



    }
}
