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
        [Route("dsa")]
        public ActionResult<Usuarios> BuscaUsuarios()
        {

            CNG.UsuarioNG listaUsuario = new UsuarioNG();

            return Ok(listaUsuario.BuscaUsuarios());
        }


        //    //[HttpPost]
        //    //[Route("validaNomeUsuario")]
        //    //public ActionResult<string> ValidaNomeUsuario([FromBody]Usuario Usuario)
        //    //{
        //    //    Usuario = UsuarioNG.Instancia.ValidaUsuario(Usuario);
        //    //    return Ok(Usuario.NomeDoUsuario);
        //    //}


        }
    }
