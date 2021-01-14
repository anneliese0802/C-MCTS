using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Program2020
{
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


}
