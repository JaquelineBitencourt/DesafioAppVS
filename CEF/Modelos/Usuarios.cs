using System;
using System.Collections.Generic;

namespace CEF.Modelos
{
    public partial class Usuarios
    {
        public int IdUsuario { get; set; }
        public string NomeDoUsuario { get; set; }
        public bool? Logado { get; set; }
        public bool? Chimarreando { get; set; }
        public int? Ordem { get; set; }
    }
}
