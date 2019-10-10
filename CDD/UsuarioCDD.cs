using System;
using System.Collections.Generic;
using CEF.Modelos;
using CEF;
using System.Linq;


namespace CDD
{
    public class UsuarioCDD
    {
        /*
         *Caso não esteja instanciado, instancia. 
         * Para chamar qualquer metodo tem que usar a classe static Instancia. 
         * Exemplo: UsuarioCDD.Instancia.ValidaPessoa
        */
        public static UsuarioCDD instancia;
        public UsuarioCDD() { }
        public static UsuarioCDD Instancia
        {
            get
            {
                if (instancia == null)
                    instancia = new UsuarioCDD();
                return instancia;
            }
        }
        //public Usuarios ValidaUsuario(Usuarios Usuario)
        //{
        //    Usuario.NomeDoUsuario = "Luis " + Usuario.NomeDoUsuario;
        //    return Usuario;
        //}


        public IEnumerable<Usuarios> BuscaUsuarios()
        {
            List<Usuarios> lista = new List<Usuarios>();

            using (var db = new CEF.Modelos.XimasAPPContext())
            {
                lista = (from a in db.Usuarios
                         where a.IdUsuario >= 1
                         select new Usuarios
                         {
                             IdUsuario = 0,
                             NomeDoUsuario = a.NomeDoUsuario,
                             Logado = a.Logado,
                             Chimarreando = a.Chimarreando,


                         }).ToList();
            }

            return lista;
        }


        /*

            - usuário loga -> insere no banco que logou
            - usuário está chimarreando -> atualiza no banco

    */

        /// <summary>
        /// Seta logado no BD o usuário
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        /// Grava a posição / a ordem do usuário no banco

        public bool LogaUsuario(Usuarios usuario)
        {
            try
            {
                using (var db = new CEF.Modelos.XimasAPPContext())
                {
                    //consulta se o nome digitado está na tabela
                    Usuarios logado = (from l in db.Usuarios
                                       where l.NomeDoUsuario == usuario.NomeDoUsuario
                                       select l).FirstOrDefault();

                    //pega a última posição da tabela ordem
                    int? ordem = (from o in db.Usuarios
                                 where o.Logado == true
                                 && o.Ordem.HasValue
                                 select o.Ordem).OrderByDescending(x => x.Value).FirstOrDefault();

                    if (!ordem.HasValue || ordem == 0)
                    {
                        ordem = 1;
                    }
                    else
                    {
                        ordem++;
                    }

                    if (logado != null)
                    {
                        logado.Logado = true;
                        logado.Ordem = ordem;
                        db.SaveChanges();

                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool SetaChimarreando(Usuarios usuario)
        {

            
            // seta chimarreando para UM e remove de todos os outros da mesma fila
            try
            {
                using (var db = new CEF.Modelos.XimasAPPContext())
                {
                    Usuarios logado = (from l in db.Usuarios
                                       where l.NomeDoUsuario == usuario.NomeDoUsuario
                                       && l.Logado == true
                                       select l).FirstOrDefault();

                    if (logado != null )
                    {
                        List<Usuarios> Lista = (from l in db.Usuarios
                                                where l.Logado == true
                                                select l).ToList();


                        Lista.ForEach(x => { x.Chimarreando = false; });

                        logado.Chimarreando = true;
                        db.SaveChanges();

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// Busca lista de usuários logados
        /// </summary>
        /// <returns></returns>
        public List<Usuarios> BuscaFila()
        {
            List<Usuarios> lista = new List<Usuarios>();

            using (var db = new CEF.Modelos.XimasAPPContext())
            {
                lista = (from a in db.Usuarios
                         where a.Logado == true
                         select a).OrderBy(x => x.IdUsuario).ToList();
            }

            return lista;
        }


        //este método são testes ignore ele mas não exclua por enquanto
        public Usuarios UsuarioLogado(Usuarios usuario)
        {
            LogaUsuario(usuario);

            //Usuarios logado = new Usuarios();

            //List<Usuarios> fila = new List<Usuarios>();

            //using (var db = new CEF.Modelos.XimasAPPContext())
            //{

            //    logado = (from l in db.Usuarios
            //              where l.NomeDoUsuario == usuario.NomeDoUsuario
            //              select l).FirstOrDefault();
            //    logado.Logado = 1;
            //    logado.Chimarreando = 1;
            //    fila.Add(logado);




            //    db.SaveChanges();

            //}
            return usuario;


        }


    }
}
