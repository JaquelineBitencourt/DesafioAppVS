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
        private static List<string> pessoas = new List<string>();
        private static DateTime tempoInicial;
        private static int tempoMaximoSegundos = 60;

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
            //}
            //else
            //{
            //    tempoAtual = 0;
            //    tempoInicial = DateTime.MinValue;
            //}


            await Clients.All.SendAsync("CR_RecebeTempoAtualizado", tempoAtual);
        }
        #endregion
        #region Usuario
        public void ReafirmaLogado(string Identificador)
        {
            if (!pessoas.Contains(Identificador))
            {
                pessoas.Add(Identificador);
            }
        }

        public async Task EstouLogado(string Identificador)
        {
            //string socketId = Context.ConnectionId;
            if (!pessoas.Contains(Identificador))
            {
                pessoas.Clear();
                pessoas.Add(Identificador);

                await Clients.All.SendAsync("CR_SolicitaLogados");
            }
        }
        

        
        public async Task BuscaUsuario()
        {
            CNG.UsuarioNG listaUsuario = new UsuarioNG();
            await Clients.All.SendAsync("RespostaBuscaUsuario", listaUsuario.BuscaUsuarios());
        }

        public async Task AtualizaDeslogados()
        {
            UsuarioNG.Instancia.AtualizaDeslogados();
            await Clients.All.SendAsync("RetornoDeslogados");
        }

        public async Task ReafirmaLogados(int id)
        {
            UsuarioNG.Instancia.ReafirmaLogados(id);
            await Clients.All.SendAsync("ReafirmouLogados");
        }
        #endregion
    }
}
