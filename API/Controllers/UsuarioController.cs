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
    public class UsuarioController : ControllerBase
    {
        [HttpGet]
        [Route("BuscaUsuarios")]
        public ActionResult<Usuarios> BuscaUsuarios()
        {
            CNG.UsuarioNG listaUsuario = new UsuarioNG();
            return Ok(listaUsuario.BuscaUsuarios());
        }

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

        [HttpPost]
        [Route("DeslogaUsuario")]
        public ActionResult<bool> DeslogaUsuario([FromBody] CEF.Modelos.Usuarios usuario)
        {
            return Ok(UsuarioNG.Instancia.DeslogaUsuarios(usuario));
        }

        [HttpPost]
        [Route("SetaConnectionId")]
        public void SetaConnectionId([FromBody] CEF.Modelos.Usuarios usuario)
        {
            UsuarioNG.Instancia.SetaConnectionId(usuario);
        }


    }
}
