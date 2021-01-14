using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Program2020
{
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
                if (fils[i0, j] != null) { sc += fils[i0, j].totale_cross; sw += fils[i0, j].totale_win; }
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


}
