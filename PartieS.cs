using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Program2020
{
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


}
