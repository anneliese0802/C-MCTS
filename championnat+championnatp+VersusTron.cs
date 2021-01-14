using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Program2020
{
    
    
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
    


 }
