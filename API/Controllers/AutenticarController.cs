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

        //[HttpPost]
        //[Route("UsuarioLogado")]
        //public ActionResult<Usuarios> UsuarioLogado([FromBody]CEF.Modelos.Usuarios usuario)
        //{

        //    CNG.UsuarioNG usuariong = new UsuarioNG();
        //    CEF.Modelos.Usuarios usuarioCEF = new CEF.Modelos.Usuarios();

        //    usuarioCEF = usuariong.UsuarioLogado(usuario);

        //    return Ok(usuarioCEF);
        //}

        [HttpGet]
        [Route("ProximoChimarreando")]
        public void ProximoChimarreando()
        {
            UsuarioNG.Instancia.ProximoChimarreando();
        }


        [HttpPost]
        [Route("LogaUsuario")]
        public ActionResult<Usuarios> LogaUsuario([FromBody]CEF.Modelos.Usuarios usuario)
        {

            CNG.UsuarioNG logaUsuarioNG = new UsuarioNG();
            CEF.Modelos.Usuarios logaUsuarioCEF = new CEF.Modelos.Usuarios();

            logaUsuarioCEF = logaUsuarioNG.LogaUsuario(usuario);

            return Ok(logaUsuarioCEF);
        }

        [HttpPost]
        [Route("SetaChimarreando")]
        public ActionResult<Usuarios> SetaChimarreando([FromBody]CEF.Modelos.Usuarios usuario) {

            CNG.UsuarioNG usuarioChimarreandoNG = new UsuarioNG();
            CEF.Modelos.Usuarios usuarioChimarreandoCEF = new CEF.Modelos.Usuarios();

            usuarioChimarreandoCEF = usuarioChimarreandoNG.SetaChimarreando(usuario);

            return Ok(usuarioChimarreandoCEF);

        }


    }
}
