using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace QCMApp.bll
{
    public class DALUtilisateur
    {
        public static void InsertUtilisateur(Utilisateurs utilisateur)
        {

            using (var context = new QCMAppBDDEntities())
            {
                try
                {
                    context.Utilisateurs.Add(utilisateur);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
            }
        }
        public static void UpdateUtilisateur(Utilisateurs utilisateur)
        {
            using (var context = new QCMAppBDDEntities())
            {
                try
                {
                    context.Utilisateurs.AddOrUpdate(utilisateur);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    
                }
            }
        }

        public static Utilisateurs FindUtilisateurByNomPrenom(String nom, String prenom)
        {
            Utilisateurs utilisateur = new Utilisateurs();
            using (var context = new QCMAppBDDEntities())
            {
                utilisateur = context.Utilisateurs.Where(u => u.nom == nom && u.prenom == prenom).
                    Select(u => u).
                    FirstOrDefault();
            }

            return utilisateur;
        }

        public static List<Utilisateurs> SelectAllUtilisateurs()
        {
            List<Utilisateurs> utilisateurs = new List<Utilisateurs>();
            using (var context = new QCMAppBDDEntities())
            {
                utilisateurs = context.Utilisateurs.Select(u => u).ToList();
            }

            return utilisateurs;
        }


    }
}