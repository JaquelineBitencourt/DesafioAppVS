using System;
using System.Collections.Generic;

namespace CEF.Modelos
{
    public partial class Usuarios
    {
        public int IdUsuario { get; set; }
        public string NomeDoUsuario { get; set; }
        public byte? Logado { get; set; }
        public byte? Chimarreando { get; set; }
    }
}
