using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Program2020
{
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
