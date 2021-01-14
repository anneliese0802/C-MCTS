using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Program2020
{
     public abstract class JoueurS
    {
        public abstract int Jouer(PositionS p, bool asj1);
        //Initialiser l'instance avant une nouvelle partie
        public virtual void NouvellePartie() { }
        //Afficher des informations sur le dernier coup joué
        public virtual void Des() { }
    }

    public class JoueurHumainTron : JoueurS
    {


        public override int Jouer(PositionS p, bool asj1)
        { //Re-ecrire méthode Jouer
            PositionTron pT = (PositionTron)p;
            int choix1 = 0;
            int choix0 = 0; ;
            string c = "";
            if (asj1 == true)
            {
                Console.WriteLine("Vous avez " + pT.NbCoups1 + " coups possibles à choisir." + "Vous pouvez choisir entre 0 et " + (pT.NbCoups1 - 1) + ".");
                Console.WriteLine("sens comme dessous:");
                if (pT.direc1[0, 0] == -1) { Console.WriteLine("Vous pouvez bouger vers le haut"); }
                if (pT.direc1[1, 1] == -1) { Console.WriteLine("Vous pouvez bouger à gauche"); }
                if (pT.direc1[2, 1] == 1) { Console.WriteLine("Vous pouvez bouger à droit"); }
                if (pT.direc1[3, 0] == 1) { Console.WriteLine("Vous pouvez bouger vers le bas"); }
                Console.WriteLine("Joueur 1, entrez votre choix !");
                c = Console.ReadLine();
                while (!Int32.TryParse(c, out choix1) || choix1 > pT.NbCoups1 - 1)
                {
                    Console.WriteLine("Erreur Input!!!entrez votre choix correcte!!!");
                    c = Console.ReadLine();
                    Int32.TryParse(c, out choix1);
                }
            }
            else if (asj1 == false)
            {
                Console.WriteLine("Vous avez " + pT.NbCoups0 + " coups possibles à choisir." + "Vous pouvez choisir entre 0 et " + (pT.NbCoups0 - 1) + ".");
                Console.WriteLine("sens comme dessous:");
                if (pT.direc0[0, 0] == -1)
                {
                    Console.WriteLine("Vous pouvez bougez vers le haut");
                }
                if (pT.direc0[1, 1] == -1)
                {
                    Console.WriteLine("Vous pouvez bougez vers  à gauche");
                }
                if (pT.direc0[2, 1] == 1)
                {
                    Console.WriteLine("Vous pouvez bougez vers à droit");
                }
                if (pT.direc0[3, 0] == 1)
                {
                    Console.WriteLine("Vous pouvez bougez le bas");
                }
                Console.WriteLine("Joueur 0, donnez votre choix !");
                c = Console.ReadLine();
                while (!Int32.TryParse(c, out choix0) || choix1 > pT.NbCoups0 - 1)
                {
                    Console.WriteLine("Erreur Input!!!entrez votre choix correcte!!!");
                    c = Console.ReadLine();
                    Int32.TryParse(c, out choix0);
                }
            }
            return asj1 ? choix1 : choix0;

        }

    }

    public class JH : JoueurS
    {
        Random gen = new Random();

        public override int Jouer(PositionS p, bool asj1)
        {
            return asj1 ? gen.Next(p.NbCoups1) : gen.Next(p.NbCoups0);
        }
    }
