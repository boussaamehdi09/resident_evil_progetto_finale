namespace resident_evil_progetto_finale
{
    internal class Program
    {

        // Mostra lo status del giocatore
        static void MostraStatus(string nome, int puntiVita, int forzaAttacco, double probFuga, int indiceMappaCorrente, string[] mappa, bool bonusCavallo)
        {
            Random generatoreCasuale = new Random();
            bool statocavallo = bonusCavallo;
            Console.WriteLine("\n======================================");
            Console.WriteLine("xxxxxxxxxxxxxxx STATUS  xxxxxxxxxxxxxxx");
            Console.WriteLine("======================================");
            Console.WriteLine($"Personaggio: {nome}");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Punti Vita: {puntiVita}");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Danno Base: {forzaAttacco}");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Probabilità Fuga/Schivata: {probFuga}(%)");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Bonus Velocità (Adrenalina): {statocavallo}");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Posizione: {indiceMappaCorrente + 1} su {mappa.Length} ({mappa[indiceMappaCorrente]})");
            Console.WriteLine("--------------------------------------");
        }
        static int TiraDadi(int indiceMappaCorrente, int lunghezzaMappa, bool bonusCavallo)
        {
            Random generatoreCasuale = new Random();
            // Dado 
            int tiro = generatoreCasuale.Next(1, 5);
            int bonus = 0;
            // bonus velocità scelto da me ( se ti capita il bonus avanzi 2 caselle )
            if (bonusCavallo == true)
            {
                bonus = 2;
                Console.WriteLine("il bonus cavallo ti dà +2 al tiro!");
                Console.WriteLine(@"                           ___________ _        ");
                Console.WriteLine(@"                      __/   .::::.-'-(/-/)      ");
                Console.WriteLine(@"                    _/:  .::::.-' .-'\/\_`,     ");
                Console.WriteLine(@"                   /:  .::::./   -._-.  d\|     ");
                Console.WriteLine(@"                    /: (""""/    '.  (__/||     ");
                Console.WriteLine(@"                     \::).-'  -._  \/ \\/\|     ");
                Console.WriteLine(@"             __ _ .-'`)/  '-'. . '. |  (i_O     ");
                Console.WriteLine(@"         .-'      \       -'      '\|           ");
                Console.WriteLine(@"    _ _./      .-'|       '.  (    \\           ");
                Console.WriteLine(@" .-'   :      '_  \         '-'\  /|/           ");
                Console.WriteLine(@"/      )\_      '- )_________.-|_/^\            ");
                Console.WriteLine(@"(   .-'   )-._-:  /        \(/\'-._ `.          ");
                Console.WriteLine(@" (   )  _//_/|:  /          `\()   `\_\         ");
                Console.WriteLine(@"  ( (   \()^_/)_/             )/      \\        ");
                Console.WriteLine(@"   )     \\ \(_)             //        )\       ");
                Console.WriteLine(@"         _o\ \\\            (o_       |__\      ");
                Console.WriteLine(@"         \ /  \\\__          )_\                ");
                Console.WriteLine(@"               ^)__\                            ");

            }

            int avanzamento = tiro + bonus;
            Console.WriteLine($"Hai tirato: {tiro} (+{bonus} bonus). Avanzi di {avanzamento} caselle.");
            // indice mappa nuovo
            indiceMappaCorrente = indiceMappaCorrente + avanzamento;

            if (indiceMappaCorrente >= lunghezzaMappa - 1)
            {
                indiceMappaCorrente = lunghezzaMappa - 1;
            }

            Console.WriteLine($"Ti sposti alla posizione {indiceMappaCorrente + 1} su {lunghezzaMappa}.");
            return indiceMappaCorrente;
        }



        static void Combattimento(ref int puntiVita, ref int forzaAttacco, double probFuga)
        {
            Random generatoreCasuale = new Random();
            string[] nemici = { "Zombie", "Licker", "Cerberus" };// ti puo capitare un nemico anche piu volte
            string nomeMostro = nemici[generatoreCasuale.Next(0, nemici.Length)];
            string[] inventario = { "Pistola 9mm", "Fucile a Pompa", "Colt Python", "Erba Verde", "Coltello" };
            int vitaMostro = generatoreCasuale.Next(8, 16);
            int dannoMostro = generatoreCasuale.Next(2, 6);

            Console.WriteLine(" ATTENZIONE: Appare un " + nomeMostro );

            // Fuga iniziale 
            if (generatoreCasuale.Next(1, 101) <= (probFuga * 100))
            {
                Console.WriteLine("Sei riuscito a scappare da " + nomeMostro + " con la tua velocità");
                return;
            }

            while (puntiVita > 0 && vitaMostro > 0)
            {
                Console.WriteLine("Cosa utilizzi per combattere?");
                for (int i = 0; i < inventario.Length; i++)
                {
                    Console.Write("[" + (i + 1) + ")" + inventario[i] + "] ");
                }
                Console.WriteLine();

                int scelta = Convert.ToInt32(Console.ReadLine());
                int dannoExtra = 0;

                // scelta arma
                if (scelta == 1) // Pistola
                {
                    dannoExtra = 2; 
                }
                else if (scelta == 2) // Fucile
                {
                    dannoExtra = 4; 
                }
                else if (scelta == 3) // Colt
                {
                    dannoExtra = 7; 
                }
                else if (scelta == 4) // Erba Verde
                {
                    puntiVita = puntiVita + 6;

                    if (puntiVita > 30)
                    {
                        puntiVita = 30;
                    }
                    Console.WriteLine("hai usate l'erba verde ora la tua vita e : " + puntiVita);
                }
                else if (scelta == 5)
                {
                    dannoExtra = 0; // Coltello
                }

                // Attacco Giocatore
                int dannoTotale = forzaAttacco + dannoExtra + generatoreCasuale.Next(1, 3);
                vitaMostro = vitaMostro - dannoTotale;
                Console.WriteLine("Colpisci il " + nomeMostro + " Danno: " + dannoTotale);

                // Attacco Nemico
                if (vitaMostro > 0)
                {
                    // Schivata tiro mostro 
                    if (generatoreCasuale.Next(1, 101) <= (probFuga * 50))
                    {
                        Console.WriteLine("Schivata perfetta! Il " + nomeMostro + " colpisce a vuoto.");
                    }
                    // ti colpisce 
                    else
                    {
                        puntiVita = puntiVita - dannoMostro;
                        Console.WriteLine("Il " + nomeMostro + " ti morde! PVita rimanenti: " + puntiVita);
                    }
                }
            }

            if (puntiVita > 0)
            {
                Console.WriteLine("Hai sconfitto il " + nomeMostro );
               
            }
        }

        static void Main()
        {
            Random generatoreCasuale = new Random();
            // Array delle 20 caselle della mappa 
            string[] mappa = {
            "R.P.D. Ingresso", "Ufficio S.T.A.R.S.", "Corridoio Ovest", "Stanza Sicura (Ovest)",
            "Canile", "Cella Detenzione", "Fogne Livello Sup.", "Magazzino Esterno",
            "Giardino Botanico", "Serra", "Torre Orologio", "Linea Metropolitana",
            "Tunnel Inondati", "Laboratorio B2 (Ingresso)", "Stanza Dati", "Sala Contenimento",
            "Passaggio di Controllo", "Zona Caldaie", "Laboratorio NEST (Base)",
            "Fuga Finale"};// obbiettivo finale è arrivare a fuga finale
            //Introduzione
            Console.WriteLine("\n=======================================================================================================================");
            Console.WriteLine("Ti trovi nel cuore di Raccoon City, una metropoli un tempo viva, ora trasformata in un inferno di sangue e ruggine.\n" +
                  "Il letale Virus T della Umbrella Corporation ha trasformato i cittadini in mostri famelici e deformi.\n" +
                  "La tua missione è disperata: devi attraversare il distretto di polizia e i laboratori sotterranei della NEST\n" +
                  "per trovare una via di fuga prima che l'intera città venga rasa al suolo per contenere l'infezione.\n\n" +
                  "Lungo il cammino incontrerai nemici implacabili come i Licker o l'inarrestabile Tyrant che ti darà la caccia,\n" +
                  "ma potrai contare sul supporto di alleati come Barry Burton o Rebecca Chambers e trovare erbe medicinali.\n" +
                  "Ma fai attenzione... nelle ombre più profonde, il Nemesis sta già pronunciando il tuo nome.");
            Console.WriteLine("========================================================================================================================");
            // indice
            int puntiVita = 10;
            int forzaAttacco = 2;
            double probFuga = 0.1;
            int indiceMappaCorrente = 0;
            bool terminaGioco = false;
            bool bonusCavallo = false;
            string SceltaPersonaggio ;
            string nomePersonaggio = "Sopravvissuto";
            string[] inventario = { "Pistola 9mm", "Fucile a Pompa", "Colt Python", "Erba Verde", "Coltello" };
            // descrizione gioco
            Console.WriteLine("\n======================================");
            Console.WriteLine("************ Raccoon City  ***********");
            Console.WriteLine("======================================");
            Console.WriteLine("Il tuo obiettivo è raggiungere la Fuga Finale.");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Ogni casella che visiterai contiene un nemico!");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Totale Caselle: {mappa.Length}");
            Console.WriteLine("--------------------------------------");

            // Scelta del personaggio
            Console.WriteLine("\n======================================");
            Console.WriteLine("Scegli il personaggio: ");
            Console.WriteLine("======================================");
            Console.WriteLine(" 1) Leon S. Kennedy ");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine(" 2) Claire Redfield ");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine(" 3) Jill Valentine");
            Console.WriteLine("--------------------------------------");
            SceltaPersonaggio = Console.ReadLine();

            if (SceltaPersonaggio == "1")
            {

                puntiVita = puntiVita + 5;

                nomePersonaggio = "Leon S. Kennedy";
                Console.WriteLine("--------------------------------------");
            }
            else if (SceltaPersonaggio == "2")
            {
                Console.WriteLine("--------------------------------------");
                probFuga = probFuga + 0.15;
                Console.WriteLine("--------------------------------------");
                nomePersonaggio = "Claire Redfield";
                Console.WriteLine("--------------------------------------");
            }
            else if (SceltaPersonaggio == "3")
            {
                Console.WriteLine("--------------------------------------");
                forzaAttacco = forzaAttacco + 2;
                Console.WriteLine("--------------------------------------");
                nomePersonaggio = "Jill Valentine";
                Console.WriteLine("--------------------------------------");
            }
            else
            {
                nomePersonaggio = "Sopravvissuto Sconosciuto";
            }
            // output scelta giocatore 
            Console.WriteLine("\n======================================");
            Console.WriteLine($"Hai scelto: ");
            Console.WriteLine("======================================");
            Console.WriteLine($"Hai scelto: {nomePersonaggio}. ");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Vita: {puntiVita}");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Danno: {forzaAttacco}");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($" Fuga: {probFuga}(%)");
            Console.WriteLine("--------------------------------------");

            while (terminaGioco == false)
            {
                MostraStatus(nomePersonaggio, puntiVita, forzaAttacco, probFuga, indiceMappaCorrente, mappa, bonusCavallo);
                // scelta del utente( avanzare , mostrare staus , uscire)
                Console.WriteLine("\n======================================");
                Console.WriteLine("Scegli un'azione: ");
                Console.WriteLine("======================================");
                Console.WriteLine(" 1) Tira dado (Avanza) ");
                Console.WriteLine("--------------------------------------");
                Console.WriteLine(" 2) Status ");
                Console.WriteLine("--------------------------------------");
                Console.WriteLine(" 3) inventario");
                Console.WriteLine("--------------------------------------");
                Console.WriteLine(" 4) uscire");
                Console.WriteLine("--------------------------------------");
                string azionePrincipale = Console.ReadLine();

                if (azionePrincipale == "1")
                {

                    indiceMappaCorrente = TiraDadi(indiceMappaCorrente, mappa.Length, bonusCavallo);
                    bonusCavallo = false;

                    // Controlla se il giocatore e arrivato alla fine
                    if (indiceMappaCorrente == mappa.Length - 1)
                    {
                        Console.WriteLine("\nSei fuggito prima di incontrare l'ultimo mostro");
                    }
                    else
                    {


                        //  Incontro con il Mostro
                        Console.WriteLine("\n Un nemico infetto blocca il passaggio!");

                        // Il combattimento main
                        Combattimento(ref puntiVita, ref forzaAttacco, probFuga);

                        //  bonus dopo il Combattimento
                        int eventoBonus = generatoreCasuale.Next(0, 3);

                        if (eventoBonus == 0)
                        {
                            bonusCavallo = true;
                            Console.WriteLine("Trovata Adrenalina! Prossimo tiro avanzerai più velocemente.");
                        }
                        else if (eventoBonus == 1)
                        {
                            int cura = generatoreCasuale.Next(2, 5);
                            puntiVita = puntiVita + cura;

                            if (puntiVita > 30)
                            {
                                puntiVita = 30;
                            }

                            Console.WriteLine($"Hai trovato una Fiala di Primo Soccorso. Recuperi {cura} PVita. Vita attuale: {puntiVita}.");
                        }
                        else
                        {
                            Console.WriteLine("Trovate solo cartucce usate.");
                        }
                    }
                }
                else if (azionePrincipale == "2")
                {
                    MostraStatus(nomePersonaggio, puntiVita, forzaAttacco, probFuga, indiceMappaCorrente, mappa, bonusCavallo);
                }
                else if(azionePrincipale == "3") 
                {
                    for (int i = 0; i < inventario.Length; i++)
                    {
                        Console.Write("[" + inventario[i] + "]-------");
                    }                
                }
                else if (azionePrincipale == "4")
                {
                    terminaGioco = true;
                }

                // Sconfitta
                if (puntiVita <= 0)
                {
                    Console.WriteLine("Sei stato sconfitto! Game over.");
                    terminaGioco = true;
                }

                // Vittoria
                if (indiceMappaCorrente == mappa.Length - 1)
                {
                    Console.WriteLine("SEI ARRIVATO A FUGA FINALE. HAI VINTO!!!!!!!!. BRAVO ");
                    terminaGioco = true;
                }
            }

            Console.WriteLine("GIOCO FINITO . boussaa mehdi");
        }
    }
}

