using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Program2020
{

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
}
