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

        public CEF.Modelos.Usuarios BuscaUsuarioUnico(CEF.Modelos.Usuarios usuario)
        {
            using (var db = new CEF.Modelos.XimasAPPContext())
            {
                Usuarios user = (from a in db.Usuarios
                                 where a.IdUsuario == usuario.IdUsuario
                                 select a).FirstOrDefault();

                return user;
            }


        }

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

        //public bool VerificaChimarreando()
        //{
        //    bool mudou = false;
        //    using (var db = new CEF.Modelos.XimasAPPContext())
        //    {
        //        List<Usuarios> listaChimarreandoNaoLogado = (from a in db.Usuarios
        //                                            where a.Logado == false && a.Chimarreando == true
        //                                            select a).ToList();


        //        if (listaChimarreandoNaoLogado.Count() >= 1)
        //        {
        //            listaChimarreandoNaoLogado.ForEach(x => x.Chimarreando = false);
        //            db.SaveChanges();
        //        }

        //        List<Usuarios> listaChimarreandoLogado = (from b in db.Usuarios
        //                                                  where b.Logado == true && b.Chimarreando == true
        //                                                  select b).ToList();

        //        if (listaChimarreandoLogado.Count() == 0)
        //        {
        //            listaChimarreandoLogado.ForEach(x => x.Chimarreando = false);
        //            db.SaveChanges();
        //            mudou = true;
        //        }


        //    }
        //    return mudou;
        //}

        public Usuarios LogaUsuario(Usuarios usuario)
        {
            try
            {
                using (var db = new CEF.Modelos.XimasAPPContext())
                {

                    List<Usuarios> listaGeral = (from b in db.Usuarios
                                                 select b).ToList();
                    //pega todos os nomes do banco
                    List<Usuarios> lista = (from a in db.Usuarios
                                            where a.Logado == true && a.Chimarreando == true
                                            select a).ToList();

                    //consulta se o nome digitado está na tabela
                    Usuarios logado = (from l in db.Usuarios
                                       where l.NomeDoUsuario == usuario.NomeDoUsuario
                                       select l).FirstOrDefault();

                    //pega a última posição da tabela ordem
                    int? ordem = (from o in db.Usuarios
                                  where o.Logado == true
                                  && o.Ordem.HasValue
                                  select o.Ordem).OrderByDescending(x => x.Value).FirstOrDefault();

                    //if(logado != null && lista.Count() == 0)
                    //{
                    //    listaGeral.ForEach(x => x.Chimarreando = false);
                    //    logado.Chimarreando = true;
                    //}

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

                //Usuarios ProximoChimarreando = (from b in db.Usuarios
                //                                where b.Ordem > UsuarioChimarreando.Ordem
                //                                && b.Logado == true
                //                                orderby b.Ordem
                //                                select b).Take(1)
                //                                     .Union(from b in db.Usuarios
                //                                            where b.Logado == true
                //                                            orderby b.Ordem
                //                                            select b).Take(1).FirstOrDefault();

                Usuarios ProximoChimarreando = (from b in db.Usuarios
                                                where b.Ordem > UsuarioChimarreando.Ordem
                                                && b.Logado == true
                                                orderby b.Ordem
                                                select b).FirstOrDefault();

                if (ProximoChimarreando == null)
                {
                    ProximoChimarreando = (from a in db.Usuarios
                                           where a.Logado == true
                                           orderby a.Ordem
                                           select a).FirstOrDefault();
                }

                lista.ForEach(x => x.Chimarreando = false);
                ProximoChimarreando.Chimarreando = true;
                db.SaveChanges();

                //.Where(x => x.Ordem > AtualChimarreando)
                //.OrderBy(x => x.Ordem)
                //.Select(x => x.IdUsuario)
                //.FirstOrDefault().ToString());
            }
        }

        public bool DeslogaUsuario(CEF.Modelos.Usuarios usuario)
        {
            try
            {
                using (var db = new CEF.Modelos.XimasAPPContext())
                {
                    Usuarios user = (from a in db.Usuarios
                                     where a.Logado == true && a.IdUsuario == usuario.IdUsuario
                                     select a).FirstOrDefault();

                    if (user.Chimarreando == true)
                    {
                        user.Chimarreando = false;
                    }

                    user.Logado = false;
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }

        }

        public void ReafirmaLogados(int id)
        {
            using (var db = new CEF.Modelos.XimasAPPContext())
            {
                Usuarios NomeDoUsuario = (from a in db.Usuarios
                                          where a.IdUsuario == id
                                          select a).FirstOrDefault();

                NomeDoUsuario.Logado = true;
                db.SaveChanges();

            }
        }

        public void AtualizaDeslogados()
        {
            using (var db = new CEF.Modelos.XimasAPPContext())
            {
                List<Usuarios> listachimarreando = (from b in db.Usuarios
                                                    where b.Logado == true && b.Chimarreando == true
                                                    select b).ToList();

                List<Usuarios> lista = (from a in db.Usuarios
                                        where a.Logado == true
                                        select a).ToList();

                lista.ForEach(x => x.Logado = false);
                db.SaveChanges();
            }
        }
    }
}
