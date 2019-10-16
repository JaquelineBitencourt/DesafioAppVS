using System;
using System.Collections.Generic;
using CEF.Modelos;
using CEF;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
                         where a.Logado == true
                         orderby a.Ordem
                         select new Usuarios
                         {
                             NomeDoUsuario = a.NomeDoUsuario,
                             Logado = a.Logado,
                             Chimarreando = a.Chimarreando,
                             Ordem = a.Ordem,
                             IdUsuario = a.IdUsuario
                         }).AsNoTracking().ToList();
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

        public Usuarios LogaUsuario(Usuarios usuario)
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

                        return logado;
                    }
                    else
                    {
                        return null;
                    }

                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        public Usuarios SetaChimarreando(Usuarios usuario)
        {


            try
            {
                using (var db = new CEF.Modelos.XimasAPPContext())
                {
                    //consulta se o nome digitado está na tabela
                    Usuarios logado = (from l in db.Usuarios
                                       where l.IdUsuario == usuario.IdUsuario
                                       select l).FirstOrDefault();


                    if (logado != null)
                    {
                        List<Usuarios> Lista = (from l in db.Usuarios
                                                where l.Logado == true && l.Chimarreando == true
                                                select l).ToList();

                        Lista.ForEach(x => x.Chimarreando = false);

                        logado.Chimarreando = true;

                        db.SaveChanges();

                        return logado;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                return null;
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

        public void ProximoChimarreador()
        {
            using (var db = new CEF.Modelos.XimasAPPContext())
            {
                List<Usuarios> lista = (from x in db.Usuarios
                                        select x).ToList();

                Usuarios UsuarioChimarreando = (from l in db.Usuarios
                                                where l.Chimarreando == true && l.Logado == true
                                                select l).FirstOrDefault();

                Usuarios ProximoChimarreando = (from b in db.Usuarios
                                                where b.Ordem > UsuarioChimarreando.Ordem
                                                && b.Logado == true
                                                orderby b.Ordem
                                                select b).Take(1)
                                                     .Union(from b in db.Usuarios
                                                            where b.Logado == true
                                                            orderby b.Ordem
                                                            select b).Take(1).FirstOrDefault();

                //if (ProximoChimarreando == null)
                //{
                //    ProximoChimarreando = (from a in db.Usuarios
                //                           where a.Logado == true
                //                           orderby a.Ordem
                //                           select a).FirstOrDefault();
                //}

                lista.ForEach(x => x.Chimarreando = false);
                ProximoChimarreando.Chimarreando = true;
                db.SaveChanges();

                //.Where(x => x.Ordem > AtualChimarreando)
                //.OrderBy(x => x.Ordem)
                //.Select(x => x.IdUsuario)
                //.FirstOrDefault().ToString());
            }
        }


        //este método são testes
        //public Usuarios UsuarioLogado(Usuarios usuario)
        //{
        //    //LogaUsuario(usuario);
        //    UsuarioChimarreando(usuario);
        //    return usuario;


        //}


    }
}
