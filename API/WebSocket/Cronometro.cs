using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.WebSocket
{
    public class Cronometro : Hub
    {
        private static int tempoAtual = 0;
        private static List<string> pessoas = new List<string>();
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
            //if (pessoas.Count > 1)
            //{
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
    }
}
