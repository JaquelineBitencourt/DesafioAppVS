using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CNG;
using MDL;
using CEF;

namespace WS
{
    public class Usuario : Hub
    {
        public async Task BuscaUsuario()
        {
            CNG.UsuarioNG listaUsuario = new UsuarioNG();
            await Clients.All.SendAsync("RespostaBuscaUsuario", listaUsuario.BuscaUsuarios());
        }
    }
}
