using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Program2020
{
    public static void Main()
    {
        //-----------------------------Deux joueurs randoms  -----------------------------------------------------------
        /*Random gen = new Random();
        PositionTron jeu = new PositionTron(1, 1, 3, 3);
        jeu.Affiche();
        do
        {
            int b1 = gen.Next(0, jeu.NbCoups1);
            int b0 = gen.Next(0, jeu.NbCoups0);
            jeu.EffectuerCoup(b1, b0);
            jeu.Affiche();
        } while (jeu.Eval == 0);
        Console.WriteLine(jeu.Eval);*/

        //------------------------------Deux joueurs humaines ---------------------------------------------------------     
        //JoueurS j1 = new JoueurHumainTron();
        //JoueurS j0 = new JoueurHumainTron();
        //PartieS par = new PartieS(j1, j0, new PositionTron(1, 1, 3, 3));
        //par.Commencer();


        //---------------1 joueur random J1 VS 1 joueur random J0--------------------------------------------------------
        //JoueurS j1 = new JH();
        //JoueurS j0 = new JH();
        //PartieS par = new PartieS(j1, j0, new PositionTron(1, 1, 3, 3));
        //par.Commencer();

        //----------------1 joueur humaine J1 VS 1 joueur random J0-------------------------------------------------------
        //JoueurS j1 = new JoueurHumainTron();
        //JoueurS j0 = new JH();
        //PartieS par = new PartieS(j1, j0, new PositionTron(0, 2, 3, 4));
        //par.Commencer();

        //----------------1 joueur humaine J1 VS 1 joueur JMCTSS J0-------------------------------------------------------
        //JoueurS j1 = new JoueurHumainTron();
        //JMCTSS j0 = new JMCTSS(2, 100);
        //PartieS partie = new PartieS(j1, j0, new PositionTron(1, 1, 3, 3));
        //partie.Commencer();


        //----------------1 joueur JMCTSSp J1 VS 1 joueur JMCTSSp J0-------------------------------------------------------
        //JMCTSSp j0 = new JMCTSSp(15, 100, 4);
        //JMCTSSp j1 = new JMCTSSp(40, 100, 2);
        //PartieS partie = new PartieS(j0, j1, new PositionTron(0, 2, 3, 3));
        //partie.Commencer();


        //-------------------1 joueur JMCTSS J1 VS 1 joueur random J0------------------------------------------------------------------
        //JMCTSSp j1 = new JMCTSSp(2,100,4);
        //JoueurS j0 = new JH();
        //PartieS partie = new PartieS(j1, j0, new PositionTron(1, 1, 3, 3));
        //partie.Commencer();


        //----------------------1 joueur JMCTSS J1 VS 1 joueur JMCTSSP J0------------------------------------------------------------------
        JMCTSSP j1 = new JMCTSSP(5, 100, 4);
        JMCTSSP j0 = new JMCTSSP(5, 100, 4);
        PartieS partie = new PartieS(j1, j0, new PositionTron(1, 1, 3, 3));
        partie.Commencer();


        //-----------------------Championnat de JMCTSS et Championnat de JMCTSSp------------------------------------------------------------------
        //Championnat(50);
        //Championnatp(50);

        //-----------------------Methode VersusTron entre JMCTSS et JMCTSSp----------------------------------------------

        //VersusTron("JMCTSS", "JMCTSSp", 100);

    }

    static void Championnat(int nombre_joueurs)
    {
        int N = nombre_joueurs;
        JMCTSS[] joueurs = new JMCTSS[N];
        for (int i = 0; i < N; i++)
            joueurs[i] = new JMCTSS(i + 1, 100);
        PositionTron position;
        PartieS partie;
        int[] victoires = new int[N];
        Stopwatch watch = new Stopwatch();
        watch.Start();
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (i != j)
                {
                    position = new PositionTron(1, 1, 3, 3);
                    partie = new PartieS(joueurs[i], joueurs[j], position);
                    partie.Commencer(false);
                    if (partie.r < 0)
                        victoires[j]++;
                    if (partie.r > 0)
                        victoires[i]++;
                }
            }
            Console.Write($"{i} ");
        }
        watch.Stop();
        Console.WriteLine();
        Console.WriteLine("Time elapsed: {0}", watch.Elapsed);
        for (int i = 0; i < N; i++)
            Console.WriteLine($"Parametre a = {i + 1}, nombre de victoires: {victoires[i]}.");
    }


    static void Championnatp(int nombre_joueurs)
    {
        int N = nombre_joueurs;
        JMCTSSp[] joueurs = new JMCTSSp[N];
        for (int i = 0; i < N; i++)
            joueurs[i] = new JMCTSSp(i + 1, 100, 4);
        PositionS position;
        PartieS partie;
        int[] victoires = new int[N];
        Stopwatch watch = new Stopwatch();
        watch.Start();
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (i != j)
                {
                    position = new PositionTron(1, 2, 3, 4);
                    partie = new PartieS(joueurs[i], joueurs[j], position);
                    partie.Commencer(false);
                    if (partie.r < 0)
                        victoires[j]++;
                    if (partie.r > 0)
                        victoires[i]++;
                }
            }
            Console.Write($"{i} ");
        }
        watch.Stop();
        Console.WriteLine();
        Console.WriteLine("Time elapsed: {0}", watch.Elapsed);
        for (int i = 0; i < N; i++)
            Console.WriteLine($"Parametre a = {i + 1}, nombre de victoires: {victoires[i]}.");
    }
    static void VersusTron(string Joueur1 = "Humain", string Joueur0 = "JMCTSS", int NbParties = 4)
    {
        int a1 = 22;
        int a2 = 22;
        int temps = 100;
        int NbThread = 2;
        JoueurS j1;
        JoueurS j0;
        switch (Joueur1)
        {
            case "JMCTSS":
                j1 = new JMCTSS(a1, temps);
                break;
            case "JMCTSSp":
                j1 = new JMCTSSp(a1, temps, NbThread);
                break;

            default:
                j1 = new JoueurHumainTron();
                break;
        }
        switch (Joueur0)
        {
            case "Humain":
                j0 = new JoueurHumainTron();
                break;
            case "JMCTSSp":
                j0 = new JMCTSSp(a2, temps, NbThread);
                break;
            default:
                j0 = new JMCTSS(a2, temps);
                break;
        }
        PositionTron p;
        PartieS partie;
        int score_j1 = 0;
        int score_j0 = 0;
        bool start = true;
        Console.WriteLine("Score : (J1 - J0)");
        for (int i = 0; i < NbParties; i++)
        {
            p = new PositionTron(1, 2, 3, 4);
            partie = new PartieS(j1, j0, p);
            partie.Commencer(true);

            if (partie.r < 0) { score_j1++; }
            else if (partie.r > 0) { score_j0++; }
            start = !start;
            Console.WriteLine($"{score_j1} - {score_j0}");
        }
        Console.WriteLine($"Joueur1 : {Joueur1}({a1},{temps}) VS Joueur0 : {Joueur0}({a2},{temps}) sur {NbParties} Parties, " +
            $"avec nombre de Thread {NbThread}.");
        Console.WriteLine($"Le joueur1 a gagné {score_j1} parties.");
        Console.WriteLine($"Le joueur0 a gagné {score_j0} parties.");

    }













    public abstract class JoueurS
    {
        public abstract int Jouer(PositionS p, bool asj1);
        //Initialiser l'instance avant une nouvelle partie
        public virtual void NouvellePartie() {  }
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
        public int[,] direc0 = new int[4, 2];
    }

    public class PositionTron : PositionS
    {

        //Attributs
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
            this.pJ1 = new int[2] { pJ10 + 1, pJ11 + 1 };//modifier les coordonées des cases de départ
            this.pJ0 = new int[2] { pJ00 + 1, pJ01 + 1 };//modifier les coordonées des cases de départ
            this.pJ1D = new int[2] { pJ10 + 1, pJ11 + 1 };
            this.pJ0D = new int[2] { pJ00 + 1, pJ01 + 1 };
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
            //Conversion entre i(j) et les coups prochains de deux joueurs. 
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
                Eval = 0; //La partie est nulle 
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
            bool equal = ((this.NbCoups1 == position.NbCoups1) && (this.Eval == position.Eval))|| (this.NbCoups0 == position.NbCoups0) && (this.Eval == position.Eval);
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
    public class PartieS
    {
        PositionS pCourante;
        JoueurS j1, j0;
        public float r;

        //Constructeur de la classe PartieS
        public PartieS(JoueurS j1, JoueurS j0, PositionS pInitiale)
        {
            this.j1 = j1;
            this.j0 = j0;
            pCourante = pInitiale.Clone();
        }

        public void NouveauMatch(PositionS pInitiale)
        {
            pCourante = pInitiale.Clone();
        }

        public void Commencer(bool affichage = true)
        {
            j1.NouvellePartie();
            j0.NouvellePartie();
            do
            {
                if (affichage) { pCourante.Affiche(); Console.WriteLine(); }

                Task<int> t1 = Task.Run(() => j1.Jouer(pCourante.Clone(), true));
                Task<int> t0 = Task.Run(() => j0.Jouer(pCourante.Clone(), false));
                t1.Wait(); t0.Wait();
                pCourante.EffectuerCoup(t1.Result, t0.Result);

                //int rep1 = j1.Jouer(pCourante.Clone(), true);
                //int rep0 = j0.Jouer(pCourante.Clone(), false);
                //pCourante.EffectuerCoup(rep1, rep0);

                j1.Des(); j0.Des();

            } while (pCourante.NbCoups1 > 0 && pCourante.NbCoups0 > 0);
            r = pCourante.Eval;
            if (affichage)
            {
                pCourante.Affiche();
                int re = 0; if (r < 0) re = -1; if (r > 0) re = 1;
                switch (re)
                {
                    case 1: Console.WriteLine("j1 {0} a gagné {1}.", j1, r); break;
                    case -1: Console.WriteLine("j0 {0} a gagné {1}.", j0, r); break;
                    case 0: Console.WriteLine("Partie nulle."); break;
                }
            }
        }
    }




    public class NoeudS
    {
        public PositionS p;
        public NoeudS pere;
        public NoeudS[,] fils;
        public int cross;
        public float win;
        public int indiceMeilleurFils1, indiceMeilleurFils0;

        public NoeudS(NoeudS pere, PositionS p)
        {
            this.pere = pere;
            this.p = p;
            fils = new NoeudS[this.p.NbCoups1, this.p.NbCoups0];
        }

        public void CalculMeilleurFils(Func<int, float, float> phi, Random g)
        {
            float s;
            float sM = 0;
            float sw = 0; int sc = 0;

            int i0 = g.Next(p.NbCoups1); indiceMeilleurFils1 = i0;
            sw = 0; sc = 0;
            for (int j = 0; j < p.NbCoups0; j++)
            {
                if (fils[i0, j] != null) { sc += fils[i0, j].cross; sw += fils[i0, j].win; }
            }
            sM = phi(sc, sw);

            for (int i = 0; i < p.NbCoups1; i++)
            {
                sw = 0; sc = 0;
                for (int j = 0; j < p.NbCoups0; j++)
                {
                    if (fils[i, j] != null) { sc += fils[i, j].cross; sw += fils[i, j].win; }
                }
                s = phi(sc, sw);
                if (s > sM) { sM = s; indiceMeilleurFils1 = i; }
            }

            int j0 = g.Next(p.NbCoups0); indiceMeilleurFils0 = j0;
            sw = 0; sc = 0;
            for (int i = 0; i < p.NbCoups1; i++)
            {
                if (fils[i, j0] != null) { sc += fils[i, j0].cross; sw += -fils[i, j0].win; }
            }
            sM = phi(sc, sw);
            for (int j = 0; j < p.NbCoups0; j++)
            {
                sw = 0; sc = 0;
                for (int i = 0; i < p.NbCoups1; i++)
                {
                    if (fils[i, j] != null) { sc += fils[i, j].cross; sw += -fils[i, j].win; }
                }
                s = phi(sc, sw);
                if (s > sM) { sM = s; indiceMeilleurFils0 = j; }
            }
        }

        public NoeudS MeilleurFils()
        {
            if (fils[indiceMeilleurFils1, indiceMeilleurFils0] != null)
            {
                return fils[indiceMeilleurFils1, indiceMeilleurFils0];
            }
            PositionS q = p.Clone();
            q.EffectuerCoup(indiceMeilleurFils1, indiceMeilleurFils0);
            fils[indiceMeilleurFils1, indiceMeilleurFils0] = new NoeudS(this, q);
            return fils[indiceMeilleurFils1, indiceMeilleurFils0];
        }

        public override string ToString()
        {
            string s = "";
            s = s + "indiceMF1 = " + indiceMeilleurFils1 + " indiceMF0 = " + indiceMeilleurFils0 + "\n";
            float sw = 0; int sc = 0;
            for (int j = 0; j < p.NbCoups0; j++)
            {
                if (fils[indiceMeilleurFils1, j] != null) { sc += fils[indiceMeilleurFils1, j].cross; sw += fils[indiceMeilleurFils1, j].win; }
            }
            float note1 = sw / sc;

            sw = 0; sc = 0;
            for (int i = 0; i < p.NbCoups1; i++)
            {
                if (fils[i, indiceMeilleurFils0] != null) { sc += fils[i, indiceMeilleurFils0].cross; sw += fils[i, indiceMeilleurFils0].win; }
            }
            float note0 = sw / sc;

            s += String.Format("note1= {0} note0={1} \n", note1, note0);
            s += "\n";
            return s;
        }
    }
    public class JMCTSS : JoueurS
    {
        Random gen = new Random();
        Stopwatch sw = new Stopwatch();

        protected float a;
        public int temps, iter;

        NoeudS racine;

        public JMCTSS(float a, int temps)
        {
            this.a = a;
            this.temps = temps;
        }

        public override string ToString()
        {
            return string.Format("JMCTSS[{0} - temps={1}]", a, temps);
        }

        public virtual float JeuHasard(PositionS p)
        {
            PositionS q = p.Clone();
            while (q.NbCoups1 > 0 && q.NbCoups0 > 0)
            {
                q.EffectuerCoup(gen.Next(0, q.NbCoups1), gen.Next(0, q.NbCoups0));
            }
            return q.Eval;
        }

        public override void NouvellePartie() { racine = null; }

        public override int Jouer(PositionS p, bool asj1)
        {
            sw.Restart();
            Func<int, float, float> phi = (C, W) => (a + W) / (a + C);
            racine = new NoeudS(null, p);
            iter = 0;
            while (sw.ElapsedMilliseconds < temps)
            {
                NoeudS no = racine;

                do // Sélection
                {
                    no.CalculMeilleurFils(phi, gen);
                    no = no.MeilleurFils();

                } while (no.cross > 0 && no.fils.Length > 0);

                float re = JeuHasard(no.p); // Simulation

                while (no != null) // Rétropropagation
                {
                    no.cross += 1;
                    no.win += re;
                    no = no.pere;
                }
                iter++;
            }
            racine.CalculMeilleurFils(phi, gen);
            int rep = (asj1) ? racine.indiceMeilleurFils1 : racine.indiceMeilleurFils0;
            return rep;
        }

        public override void Des()
        {
            Console.Write("{0} itérations  ", iter);
            Console.Write(racine);
        }

    }




    //5.
    public class JMCTSSp : JoueurS
    {
        private Random[] gen;
        static Stopwatch sw = new Stopwatch();
        private Object verrou = new Object();

        float a;
        int temps, iter;
        List<Task<int>> TaskList;
        NoeudS racine;
        int N;
        public JMCTSSp(float a, int temps, int N)
        {
            this.a = 2 * a;
            this.temps = temps;
            this.N = N;
            this.gen = new Random[N];
            for (int i = 0; i < N; i++)
            {
                this.gen[i] = new Random();
            }
        }

        public override string ToString()
        {
            return string.Format("JMCTS[{0} - temps={1}]", a, temps);
        }

        int JeuHasard(PositionS p, int i)
        {
            PositionS q = p.Clone();
            int re = 1;
            while (q.NbCoups1 > 0 && q.NbCoups0 > 0)
            {
                q.EffectuerCoup(gen[i].Next(0, q.NbCoups1), gen[i].Next(0, q.NbCoups0));
            }
            if (q.Eval < 0) { re = -1; }//gaile
            if (q.Eval > 0) { re = 1; }//gaile
            return re;
        }

        public override int Jouer(PositionS p, bool asj1)
        {
            sw.Restart();
            Func<int, float, float> phi = (W, C) => (a + W) / (a + C);
            racine = new NoeudS(null, p);
            int iter = 0;
            int totale_re;
            while (sw.ElapsedMilliseconds < temps)
            {
                this.TaskList = new List<Task<int>>();
                totale_re = 0;
                NoeudS no = racine;

                do // Sélection
                {
                    no.CalculMeilleurFils(phi, gen[N - 1]);
                    no = no.MeilleurFils();

                } while (no.cross > 0 && no.fils.Length > 0);


                for (int i = 0; i < this.N; i++)//simulation
                {
                    int j = i;
                    TaskList.Add(Task.Run(() => JeuHasard(no.p, j)));
                }
                Task.WaitAll(TaskList.ToArray());
                for (int i = 0; i < TaskList.Count; i++)
                {
                    totale_re += TaskList[i].Result;
                }

                while (no != null) // Rétropropagation
                {
                    no.cross += this.N * 2;
                    no.win += totale_re;
                    no = no.pere;
                }

                iter++;
            }
            racine.CalculMeilleurFils(phi, gen[N - 1]);
            int rep = (asj1) ? racine.indiceMeilleurFils1 : racine.indiceMeilleurFils0;
            return rep;
        }
        public override void Des()
        {
            Console.WriteLine("{0} itérations", iter);
            Console.WriteLine(racine);

        }
        public override void NouvellePartie()
        {
            this.racine = null;
        }
    }






    //7.

    public class NoeudSP
    {
        static Random gen = new Random();

        public PositionS p;
        public NoeudSP pere;
        public NoeudSP[,] fils;
        public int[] win, cross;
        public int N;
        public int[] indiceMeilleurFils1, indiceMeilleurFils0;
        public NoeudSP(NoeudSP pere, PositionS p, int nb_thread)
        {
            this.pere = pere;//
            this.p = p;//
            this.fils = new NoeudSP[this.p.NbCoups1, this.p.NbCoups0];//
            this.N = nb_thread;
            this.win = new int[nb_thread];
            this.cross = new int[nb_thread];
            for (int i = 0; i < N; i++)
            {
                this.win[i] = 0;
                this.cross[i] = 0;
            }
            this.indiceMeilleurFils1 = new int[nb_thread];
            this.indiceMeilleurFils0 = new int[nb_thread];
        }      
        public int totale_win
        {
            get
            {
                int totale_W = 0;
                for (int i = 0; i < this.N; i++)
                {
                    totale_W += win[i];
                }
                return totale_W;
            }
        }
        public int totale_cross
        {
            get
            {
                int totale_C = 0;
                for (int i = 0; i < this.N; i++)
                {
                    totale_C += cross[i];
                }
                return totale_C;
            }
        }

        public void CalculMeilleurFils(Func<int, float, float> phi, Random g)
        {

            float s;
            float sM = 0;
            float sw = 0; int sc = 0;

            int i0 = g.Next(p.NbCoups1); 
            indiceMeilleurFils1[0] = i0;
            sw = 0; sc = 0;
            for (int j = 0; j < p.NbCoups0; j++)
            {
                if (fils[i0,j] != null) { sc += fils[i0, j].totale_cross; sw += fils[i0, j].totale_win; }
            }
            sM = phi(sc, sw);

            for (int i = 0; i < p.NbCoups1; i++)
            {
                sw = 0; sc = 0;
                for (int j = 0; j < p.NbCoups0; j++)
                {
                    if (fils[i, j] != null) { sc += fils[i, j].totale_cross; sw += fils[i, j].totale_win; }
                }
                s = phi(sc, sw);
                if (s > sM) { sM = s; indiceMeilleurFils1[0] = i; }
            }

            int j0 = g.Next(p.NbCoups0); indiceMeilleurFils0[0] = j0;
            sw = 0; sc = 0;
            for (int i = 0; i < p.NbCoups1; i++)
            {
                if (fils[i, j0] != null) { sc += fils[i, j0].totale_cross; sw += -fils[i, j0].totale_win; }
            }
            sM = phi(sc, sw);
            for (int j = 0; j < p.NbCoups0; j++)
            {
                sw = 0; sc = 0;
                for (int i = 0; i < p.NbCoups1; i++)
                {
                    if (fils[i, j] != null) { sc += fils[i, j].totale_cross; sw += -fils[i, j].totale_win; }
                }
                s = phi(sc, sw);
                if (s > sM) { sM = s; indiceMeilleurFils0[0] = j; }
            }
        }






        public NoeudSP MeilleurFils(int j)
        {
            if (fils[indiceMeilleurFils1[j], indiceMeilleurFils0[j]] != null)
            {
                return fils[indiceMeilleurFils1[j], indiceMeilleurFils0[j]];
            }
            PositionS q = p.Clone();
            q.EffectuerCoup(indiceMeilleurFils1[j], indiceMeilleurFils0[j]);
            fils[indiceMeilleurFils1[j], indiceMeilleurFils0[j]] = new NoeudSP(this, q, this.N);
            return fils[indiceMeilleurFils1[j], indiceMeilleurFils0[j]];
        }
        public override string ToString()
        {
            string s = "";
            s = s + "indiceMF1 = " + indiceMeilleurFils1 + " indiceMF0 = " + indiceMeilleurFils0 + "\n";
            float sw = 0; int sc = 0;
            for (int j = 0; j < p.NbCoups0; j++)
            {
                 if (fils[indiceMeilleurFils1[0], j] != null) { sc += fils[indiceMeilleurFils1[0], j].totale_cross; sw += fils[indiceMeilleurFils1[0], j].totale_win; }
            }
            float note1 = sw / sc;

            sw = 0; sc = 0;
            for (int i = 0; i < p.NbCoups1; i++)
            {
                if (fils[i, indiceMeilleurFils0[0]] != null) { sc += fils[i, indiceMeilleurFils0[0]].totale_cross; sw += fils[i, indiceMeilleurFils0[0]].totale_win; }

            }
            float note0 = sw / sc;

            s += String.Format("note1= {0} note0={1} \n", note1, note0);
            s += "\n";
            return s;
        }

    }



    public class JMCTSSP : JoueurS
    {
        public Random[] gen;
        static Stopwatch sw = new Stopwatch();
        float a;
        int temps;
        List<Task> TaskList;
        NoeudSP racine;
        int N;
        int[] re;
        public JMCTSSP(float a, int temps, int N)
        {
            this.a = 2 * a;
            this.temps = temps;
            this.N = N;
            this.re = new int[N];
            this.gen = new Random[N];
            for (int i = 0; i < N; i++)
            {
                this.gen[i] = new Random();
            }
        }

        public override string ToString()
        {
            return string.Format("JMCTS[{0}- temps={1}]", a, temps);
        }

        int JeuHasard(PositionS p, int i)
        {
            PositionS q = p.Clone();
            int re = 1;
            while (q.NbCoups1 > 0 && q.NbCoups0 > 0)
            {
                q.EffectuerCoup(gen[i].Next(0, q.NbCoups1), gen[i].Next(0, q.NbCoups0));
            }
            if (q.Eval < 0) { re = -1; }
            if (q.Eval > 0) { re = 1; }
            return re;
        }

        public void Calcul(NoeudSP no, Func<int, float, float> phi, int i)
        {
            do // Sélection
            {
                no.CalculMeilleurFils(phi, gen[N - 1]);
                no = no.MeilleurFils(i);
            } while (no.cross[i] > 0 && no.fils.Length > 0);


            re[i] = JeuHasard(no.p, i); // Simulation

            while (no != null) // Rétropropagation
            {
                no.cross[i] += 2;//gaile
                no.win[i] += re[i];
                no = no.pere;
            }
        }

        public override int Jouer(PositionS p, bool asj1)
        {
            sw.Restart();
            Func<int, float, float> phi = (W, C) => (a + W) / (a + C);

            racine = new NoeudSP(null, p, this.N);

            if (racine == null)
            {
                racine = new NoeudSP(null, p, this.N);
            }
            else
            {
                racine = racine.MeilleurFils(0);
                for (int i = 0; i < racine.p.NbCoups1 - 1; i++)
                {
                    for (int j = 0; j < racine.p.NbCoups1 - 1; j++)
                    {
                        if ((racine.fils[i, j] != null) && (racine.fils[i, j].p.Equals(p)))
                        {
                            racine = racine.fils[i, j];
                            break;
                        }
                    }
                }
            }
            int iter = 0;
            while (sw.ElapsedMilliseconds < temps)
            {
                this.TaskList = new List<Task>();
                NoeudSP no = racine;
                for (int i = 0; i < this.N; i++)
                {
                    int j = i;
                    this.TaskList.Add(Task.Run(() => Calcul(no, phi, j)));
                    iter++;
                }

                Task.WaitAll(this.TaskList.ToArray());
            }
            Console.WriteLine("{0} itérations", iter);
            Console.WriteLine(racine);
            racine.CalculMeilleurFils(phi, gen[N - 1]);
            int rep = (asj1) ? racine.indiceMeilleurFils1[0] : racine.indiceMeilleurFils0[0];
            return rep;
        }
        public override void NouvellePartie()
        {
            this.racine = null;
        }
    }







}
