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


 }
