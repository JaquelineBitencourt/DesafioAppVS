using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MDL;
using CNG;
using CEF;

namespace WS
{
    public class WebSocket : Hub
    {
        #region Timer
        private static int tempoAtual = 0;
        private static DateTime tempoInicial;
        private static int tempoMaximoSegundos = 300;

        public async Task ResetaCronometro()
        {
            tempoAtual = 0;
            tempoInicial = DateTime.MinValue;

            await Clients.All.SendAsync("CR_RecebeTempoAtualizado", tempoAtual);
        }

        public async Task AtualizaCronometro()
        {
            if (tempoAtual == 0 || DateTime.Now >= tempoInicial.AddSeconds(tempoMaximoSegundos))
            {
                tempoInicial = DateTime.Now;
                tempoAtual = tempoMaximoSegundos;
            }
            else
            {
                tempoAtual = tempoMaximoSegundos - Convert.ToInt16(DateTime.Now.Subtract(tempoInicial).TotalSeconds);
            }

            await Clients.All.SendAsync("CR_RecebeTempoAtualizado", tempoAtual);
        }
        #endregion

        #region Usuario
        public async Task GetConnectionId()
        {
            await Clients.Caller.SendAsync("RespostaConnectionId", Context.ConnectionId);
            await Clients.All.SendAsync("Conectou");
        }

        public override async Task OnConnectedAsync()

        {
            string teste = Context.ConnectionId;
            
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception) // se na exception vier null = disconectou, se vier algo = caiu
        {
            UsuarioNG.Instancia.DisconectaUsuario(Context.ConnectionId);
            await Clients.All.SendAsync("Disconectou");
            await base.OnDisconnectedAsync(exception);
        }

        public async Task BuscaUsuario()
        {
            CNG.UsuarioNG listaUsuario = new UsuarioNG();
            await Clients.All.SendAsync("RespostaBuscaUsuario", listaUsuario.BuscaUsuarios());
        }
        #endregion
    }
}
