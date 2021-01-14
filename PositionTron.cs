using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Program2020
{
    public abstract class PositionS   // Etat du jeu à un moment
    {
        //Gain du joueur1 à la fin de la partie
        public float Eval { get; protected set; }
        //Nb de coups possibles pour joueur1 à partir de position courante
        public int NbCoups1 { get; protected set; }
        //Nb de coups possibles pour joueur0 à partir de position courante
        public int NbCoups0 { get; protected set; }
        //Modifier les positions de joueur1 et de joueur0
        public abstract void EffectuerCoup(int i, int j);
        //Renvoyer la position courante
        public abstract PositionS Clone();
        //Afficher une représentation 
        public abstract void Affiche();
        public int[,] direc1 = new int[4, 2];
        //coordonnees de coup possible de joueur1
        public int[,] direc0 = new int[4, 2];
        //coordonnees de coup possible de joueur0
    }

    public class PositionTron : PositionS
    {       
        public int[,] plateau; //grille/plateau de jeux Tron
        public int[] pJ1; //position courante de joueur1
        public int[] pJ0; //position courante de joueur0
        public int[] pJ1D; //case depart de joueur 1
        public int[] pJ0D; //case depart de joueur 0
        static int nbL = 7; //nombre de ligne du plateau
        static int nbC = 7; //nombre de colonnes du plateau  
        public char[,] etatdejeu;

        public PositionTron(int pJ10, int pJ11, int pJ00, int pJ01)
        //Construire PositionTron avec les cases de depart de deux joueurs
        {
            this.etatdejeu = new char[6, 7];
            this.plateau = new int[nbL + 2, nbC + 2];
            for (int i = 0; i < nbL + 2; i++)
            {
                for (int j = 0; j < nbC + 2; j++)
                {
                    if (i == 0 || i == (nbL + 1) || j == 0 || j == (nbC + 1))
                    {
                        plateau[i, j] = 8;//remplir les bords avec le numéro 8
                    }
                }
            }
            this.pJ1 = new int[2] { pJ10 + 1, pJ11 + 1 };//modifier les coordonées des cases courante
            this.pJ0 = new int[2] { pJ00 + 1, pJ01 + 1 };//modifier les coordonées des cases de courante
            this.pJ1D = new int[2] { pJ10 + 1, pJ11 + 1 };// modifier les coordonées des cases de départ
            this.pJ0D = new int[2] { pJ00 + 1, pJ01 + 1 };//modifier les coordonées des cases de départ
            plateau[pJ1[0], pJ1[1]] = 1;
            plateau[pJ0[0], pJ0[1]] = 2;
            this.Eval = 0;

            int nbcoup1 = 0;
            int t = 0;
            for (int p = -1; p < 2; p++)
            {
                for (int q = -1; q < 2; q++)
                {
                    if (Math.Abs(p + q) == 1 && this.plateau[this.pJ1[0] + p, this.pJ1[1] + q] == 0)
                    {
                        nbcoup1++;
                        direc1[t, 0] = p;
                        direc1[t, 1] = q;

                    }
                    if (Math.Abs(p + q) == 1)
                    { t++; }

                }
            }
            this.NbCoups1 = nbcoup1;

            int nbcoup0 = 0;
            int m = 0;
            for (int p = -1; p < 2; p++)
            {
                for (int q = -1; q < 2; q++)
                {
                    if (Math.Abs(p + q) == 1 && this.plateau[this.pJ0[0] + p, this.pJ0[1] + q] == 0)
                    {
                        nbcoup0++;
                        direc0[m, 0] = p;
                        direc0[m, 1] = q;
                    }
                    if (Math.Abs(p + q) == 1)
                    { m++; }
                }
            }
            this.NbCoups0 = nbcoup0;

        }

        //Constructeur de PositionTron avec un autre PositionTron
        public PositionTron(PositionTron p)
        {

            this.plateau = new int[nbL + 2, nbC + 2];
            for (int i = 0; i < nbL + 2; i++)
            {
                for (int j = 0; j < nbC + 2; j++)
                {
                    if (i == 0 || i == (nbL + 1) || j == 0 || j == (nbC + 1))
                    {
                        plateau[i, j] = 8;
                    }
                }
            }
            this.pJ1 = new int[2] { p.pJ1[0], p.pJ1[1] };
            this.pJ0 = new int[2] { p.pJ0[0], p.pJ0[1] };
            this.pJ1D = new int[2] { p.pJ1D[0], p.pJ1D[1] };
            this.pJ0D = new int[2] { p.pJ0D[0], p.pJ0D[1] };
            plateau[pJ1[0], pJ1[1]] = 1;
            plateau[pJ0[0], pJ0[1]] = 2;
            Eval = p.Eval;
            NbCoups1 = p.NbCoups1;
            NbCoups0 = p.NbCoups0;
            direc1 = p.direc1;
            direc0 = p.direc0;
        }

        public override void EffectuerCoup(int i, int j)
        {
            //Conversion
            int c1 = 0;
            int c0 = 0;
            int[] choix1 = new int[4];
            int[] choix0 = new int[4];
            int i1 = 0;
            int i0 = 0;
            int ii1 = 0;
            int ii0 = 0;
            for (int p = -1; p < 2; p++)
            {
                for (int q = -1; q < 2; q++)
                {
                    if (Math.Abs(p + q) == 1)
                    {
                        ii1++;
                        if (this.plateau[this.pJ1[0] + p, this.pJ1[1] + q] == 0)
                        {
                            choix1[i1] = ii1;
                            i1++;
                        }
                    }
                }
            }
            c1 = choix1[i];


            for (int p = -1; p < 2; p++)
            {
                for (int q = -1; q < 2; q++)
                {
                    if (Math.Abs(p + q) == 1)
                    {
                        ii0++;
                        if (this.plateau[this.pJ0[0] + p, this.pJ0[1] + q] == 0)
                        {
                            choix0[i0] = ii0;
                            i0++;
                        }
                    }
                }
            }
            c0 = choix0[j];



            //Renouveler pJ1
            if (c1 == 1 && plateau[this.pJ1[0] - 1, this.pJ1[1]] == 0)
            {
                plateau[this.pJ1[0], this.pJ1[1]] = 3;
                this.pJ1[0] = this.pJ1[0] - 1;
                this.pJ1[1] = this.pJ1[1];
            }
            else if (c1 == 3 && plateau[this.pJ1[0], this.pJ1[1] + 1] == 0)
            {
                plateau[this.pJ1[0], this.pJ1[1]] = 3;
                this.pJ1[0] = this.pJ1[0];
                this.pJ1[1] = this.pJ1[1] + 1;
            }
            else if (c1 == 4 && plateau[this.pJ1[0] + 1, this.pJ1[1]] == 0)
            {
                plateau[this.pJ1[0], this.pJ1[1]] = 3;
                this.pJ1[0] = this.pJ1[0] + 1;
                this.pJ1[1] = this.pJ1[1];
            }
            else if (c1 == 2 && plateau[this.pJ1[0], this.pJ1[1] - 1] == 0)
            {
                plateau[this.pJ1[0], this.pJ1[1]] = 3;
                this.pJ1[0] = this.pJ1[0];
                this.pJ1[1] = this.pJ1[1] - 1;
            }
            //Renouveler pJ2
            if (c0 == 1 && plateau[this.pJ0[0] - 1, this.pJ0[1]] == 0)
            {
                plateau[this.pJ0[0], this.pJ0[1]] = 4;
                plateau[this.pJ0[0] - 1, this.pJ0[1]] = 1;
                this.pJ0[0] = this.pJ0[0] - 1;
                this.pJ0[1] = this.pJ0[1];
            }
            else if (c0 == 3 && plateau[this.pJ0[0], this.pJ0[1] + 1] == 0)
            {
                plateau[this.pJ0[0], this.pJ0[1]] = 4;
                this.pJ0[0] = this.pJ0[0];
                this.pJ0[1] = this.pJ0[1] + 1;
            }
            else if (c0 == 4 && plateau[this.pJ0[0] + 1, this.pJ0[1]] == 0)
            {
                plateau[this.pJ0[0], this.pJ0[1]] = 4;
                this.pJ0[0] = this.pJ0[0] + 1;
                this.pJ0[1] = this.pJ0[1];
            }
            else if (c0 == 2 && plateau[this.pJ0[0], this.pJ0[1] - 1] == 0)
            {
                plateau[this.pJ0[0], this.pJ0[1]] = 4;
                this.pJ0[0] = this.pJ0[0];
                this.pJ0[1] = this.pJ0[1] - 1;
            }

            if (pJ1[0] == pJ0[0] && pJ1[1] == pJ0[1])
            {
                plateau[pJ1[0], pJ1[1]] = 0;
                pJ1[0] = pJ1D[0];
                pJ1[1] = pJ1D[1];
                pJ0[0] = pJ0D[0];
                pJ0[1] = pJ0D[1];
                plateau[pJ1[0], pJ1[1]] = 1;
                plateau[pJ0[0], pJ0[1]] = 2;
            }
            else
            {
                plateau[pJ1[0], pJ1[1]] = 1;
                plateau[pJ0[0], pJ0[1]] = 2;
            }



            // Mettre à jour NbCoup1 et NbCoup0   
            int nbcoup1 = 0;
            int t = 0;
            int[,] direc1 = new int[4, 2] { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } };
            for (int p = -1; p < 2; p++)
            {
                for (int q = -1; q < 2; q++)
                {
                    if (Math.Abs(p + q) == 1 && this.plateau[this.pJ1[0] + p, this.pJ1[1] + q] == 0)
                    {
                        nbcoup1++;
                        direc1[t, 0] = p;
                        direc1[t, 1] = q;
                    }
                    if (Math.Abs(p + q) == 1)
                    { t++; }
                }
            }
            this.NbCoups1 = nbcoup1;
            this.direc1 = direc1;

            int nbcoup0 = 0;
            int m = 0;
            int[,] direc0 = new int[4, 2] { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } };
            for (int p = -1; p < 2; p++)
            {
                for (int q = -1; q < 2; q++)
                {
                    if (Math.Abs(p + q) == 1 && this.plateau[this.pJ0[0] + p, this.pJ0[1] + q] == 0)
                    {
                        nbcoup0++;
                        direc0[m, 0] = p;
                        direc0[m, 1] = q;
                    }
                    if (Math.Abs(p + q) == 1)
                    { m++; }
                }
            }
            this.NbCoups0 = nbcoup0;
            this.direc0 = direc0;

            // Mettre à jour Eval
            if (NbCoups0 > 0 && NbCoups1 == 0)
            {
                Eval = -1;
            }
            else if (NbCoups0 == 0 && NbCoups1 > 0)
            {
                Eval = 1;
            }
            else
            {
                Eval = 0; //La partie est nulle  ou pas fini
            }

        }

        public override void Affiche()
        {

            for (int i = 1; i < nbL + 1; i++)
            {
                string s = "|";
                for (int j = 1; j < nbC + 1; j++)
                {
                    s = s + plateau[i, j] + "|";
                }
                Console.WriteLine(s);
            }
            Console.WriteLine();
        }

        public override PositionS Clone()
        {
            return new PositionTron(this);
        }
        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
                return false;
            PositionTron position = (PositionTron)obj;
            bool equal = ((this.NbCoups1 == position.NbCoups1) && (this.Eval == position.Eval)) || (this.NbCoups0 == position.NbCoups0) && (this.Eval == position.Eval);
            for (int i = 0; i < nbL; i++)
            {
                for (int j = 0; j < nbC; j++)
                {
                    if (this.plateau[i, j] != position.plateau[i, j])
                    {
                        equal = false;
                        break;
                    }
                }
                if (!equal)
                    break;
            }
            return equal;
        }
        public override int GetHashCode()
        {
            return this.NbCoups1;
        }





    }
