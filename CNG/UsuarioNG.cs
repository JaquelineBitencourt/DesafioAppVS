﻿using System;
using MDL;
using CDD;
using CEF;
using System.Collections.Generic;

namespace CNG
{
    public class UsuarioNG
    {
        /*
         *Caso não esteja instanciado, instancia. 
         * Para chamar qualquer metodo tem que usar a classe static Instancia. 
         * Exemplo: UsuarioCDD.Instancia.ValidaPessoa
         */
        public static UsuarioNG instancia;
        public UsuarioNG() { }
        public static UsuarioNG Instancia
        {
            get
            {
                if (instancia == null)
                    instancia = new UsuarioNG();
                return instancia;
            }
        }
        //public Usuarios ValidaUsuario(CEF.Modelos.Usuarios Pessoa)
        //{
        //    return UsuarioCDD.Instancia.ValidaUsuario(Pessoa);
        //}

        public IEnumerable<CEF.Modelos.Usuarios> BuscaUsuarios()
        {
            UsuarioCDD usuarioCDD = new UsuarioCDD();

            return UsuarioCDD.Instancia.BuscaUsuarios();


        }
        public CEF.Modelos.Usuarios UsuarioLogado(CEF.Modelos.Usuarios usuario)
        {
            //UsuarioCDD usuarioCDD = new UsuarioCDD();




            return UsuarioCDD.Instancia.UsuarioLogado(usuario);
        }


    }
}
