/* 
Filnavn:    SorteringsSammenligner.cs
Kommentar:  Inspirert av Jan Martin Johannessens kode for sammenligning av sorteringsalgortimer.
            Kopierer her deler av hans utgangspunkt, og fyller inn nye algoritmer.
Forfatter:  Lars Vågenes lv
Historie:   Når         Hvem   Hva
            20171016    lv     Opprettet
            20172016    lv     Lagg til MergeSort og QuickSort

Innhold:
            BubbleSort
            BubbleSort med pointers, kopiert fra jmj
            SelectionSort
            InsertionSort
            ShellSort
            MergeSort 2 stk. iterative varianter
            MergeSort 1 stk. rekursiv variant
            QuickSort, Rekursiv

Det meste av koden er basert på min forståelse av de ulike algoritmene,
så jeg kan ikke garantere at noe av dette er den beste måten å skrive dem på.

Build/Kjør i Release mode for best mulig sammenligning
*/


using System;

namespace SorteringSammenligner
{
    class Program
    {
        const int ANTALL_ELEMENTER =  20000;       // Velg tabellstørrelse. 
        const int MAX_GRENSE_RANDOM = 20000;       // Velg variasjonsbredde.
        //Kommenter ut bubble-, selection- og Insertion- sort, om du vil teste med veldig store tabeller
        static Random r = new Random();

        static void Main(string[] args)
        {
            try
            {
                while (true)
                    Utfør();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void Utfør()
        {
            DateTime tid0, tid1;                // For tidtaking.
            TimeSpan intervall;                 // "

            int[] minTabell  = new int[ANTALL_ELEMENTER];        // Oppretter en tabell.
            int[] kopiTabell = new int[ANTALL_ELEMENTER];        // Oppretter en tabell til.

            Console.Clear();
            Console.WriteLine("Genererer ...");
            GenererTabell(minTabell, ANTALL_ELEMENTER);         // Fyller tabellen med tilfeldige tall.
            Console.WriteLine();
            
            Console.WriteLine("kopierer..");
            kopierTabell(minTabell, kopiTabell, ANTALL_ELEMENTER);
            Console.WriteLine("Sorterer med Array.Sort()...");
            tid0 = DateTime.Now;		                        // Notér starttidspunkt        
            Array.Sort(kopiTabell);
            tid1 = DateTime.Now;		                        // Notér stopptidspunkt
            intervall = tid1 - tid0;                            // Bergegn tidsforbruk.
            if (!SjekkSortering(kopiTabell, ANTALL_ELEMENTER)) Console.Write("Feilsortert!!  ");
            Console.WriteLine(SkrivResultat("Array.Sort", intervall));
            Console.WriteLine();
            
            Console.WriteLine("kopierer..");
            kopierTabell(minTabell, kopiTabell, ANTALL_ELEMENTER);
            Console.WriteLine("Sorterer med ShellSort...");
            tid0 = DateTime.Now;		                        // Notér starttidspunkt        
            Sorter.ShellSort(kopiTabell, ANTALL_ELEMENTER);
            tid1 = DateTime.Now;		                        // Notér stopptidspunkt
            intervall = tid1 - tid0;                            // Bergegn tidsforbruk.
            if (!SjekkSortering(kopiTabell, ANTALL_ELEMENTER)) Console.Write("Feilsortert!!  ");
            Console.WriteLine(SkrivResultat("ShellSort", intervall));
            Console.WriteLine();

            Console.WriteLine("kopierer..");             
            kopierTabell(minTabell, kopiTabell, ANTALL_ELEMENTER);
            Console.WriteLine("Sorterer med MergeSort 1...");
            tid0 = DateTime.Now;		                        // Notér starttidspunkt        
            Sorter.MergeSorter(kopiTabell, ANTALL_ELEMENTER);
            tid1 = DateTime.Now;		                        // Notér stopptidspunkt
            intervall = tid1 - tid0;                            // Bergegn tidsforbruk.
            if (!SjekkSortering(kopiTabell, ANTALL_ELEMENTER)) Console.Write("Feilsortert!!  ");
            Console.WriteLine(SkrivResultat("MergeSort 1 (iterativ)", intervall));
            Console.WriteLine();

            Console.WriteLine("kopierer..");             
            kopierTabell(minTabell, kopiTabell, ANTALL_ELEMENTER);
            Console.WriteLine("Sorterer med MergeSort 2...");
            tid0 = DateTime.Now;		                        // Notér starttidspunkt        
            Sorter.MergeSorter2(kopiTabell, ANTALL_ELEMENTER);
            tid1 = DateTime.Now;		                        // Notér stopptidspunkt
            intervall = tid1 - tid0;                            // Bergegn tidsforbruk.
            if (!SjekkSortering(kopiTabell, ANTALL_ELEMENTER)) Console.Write("Feilsortert!!  ");
            Console.WriteLine(SkrivResultat("MergeSort 2 (iterativ)", intervall));
            Console.WriteLine();

            Console.WriteLine("kopierer..");              
            kopierTabell(minTabell, kopiTabell, ANTALL_ELEMENTER);
            Console.WriteLine("Sorterer med MergeSort 3...");
            tid0 = DateTime.Now;		                        // Notér starttidspunkt        
            Sorter.RecursiveMergeSort(kopiTabell, ANTALL_ELEMENTER);
            tid1 = DateTime.Now;		                        // Notér stopptidspunkt
            intervall = tid1 - tid0;                            // Bergegn tidsforbruk.
            if (!SjekkSortering(kopiTabell, ANTALL_ELEMENTER)) Console.Write("Feilsortert!!  ");
            Console.WriteLine(SkrivResultat("MergeSort 3 (Rekursiv)", intervall));
            Console.WriteLine();

            Console.WriteLine("kopierer..");
            kopierTabell(minTabell, kopiTabell, ANTALL_ELEMENTER);
            Console.WriteLine("Sorterer med QuickSort...");
            tid0 = DateTime.Now;		                        // Notér starttidspunkt        
            Sorter.QuickSort(kopiTabell, ANTALL_ELEMENTER);
            tid1 = DateTime.Now;		                        // Notér stopptidspunkt
            intervall = tid1 - tid0;                            // Bergegn tidsforbruk.
            if (!SjekkSortering(kopiTabell, ANTALL_ELEMENTER)) Console.Write("Feilsortert!!  ");
            Console.WriteLine(SkrivResultat("QuickSort", intervall));
            Console.WriteLine();

            Console.WriteLine("kopierer..");
            kopierTabell(minTabell, kopiTabell, ANTALL_ELEMENTER);
            Console.WriteLine("Sorterer med Bubble sort ...");
            tid0 = DateTime.Now;		                        // Notér starttidspunkt        
            Sorter.BubbleSort(kopiTabell, ANTALL_ELEMENTER);
            tid1 = DateTime.Now;		                        // Notér stopptidspunkt
            intervall = tid1 - tid0;
            if (!SjekkSortering(kopiTabell, ANTALL_ELEMENTER)) Console.Write("Feilsortert!!  ");
            Console.WriteLine(SkrivResultat("BubbleSort", intervall));
            Console.WriteLine();

            Console.WriteLine("kopierer..");
            kopierTabell(minTabell, kopiTabell, ANTALL_ELEMENTER);
            Console.WriteLine("Sorterer med Unsafe BubbleSort...");
            tid0 = DateTime.Now;		                        // Notér starttidspunkt        
            Sorter.BubbleSortUnsafe(kopiTabell, ANTALL_ELEMENTER);
            tid1 = DateTime.Now;		                        // Notér stopptidspunkt
            intervall = tid1 - tid0;                            // Bergegn tidsforbruk.
            if (!SjekkSortering(kopiTabell, ANTALL_ELEMENTER)) Console.Write("Feilsortert!!  ");
            Console.WriteLine(SkrivResultat("Unsafe Bubblesort", intervall));
            Console.WriteLine();
            
            Console.WriteLine("kopierer..");
            kopierTabell(minTabell, kopiTabell, ANTALL_ELEMENTER);
            Console.WriteLine("Sorterer med SelectionSort ...");
            tid0 = DateTime.Now;		                        // Notér starttidspunkt        
            Sorter.SelectionSort (kopiTabell, ANTALL_ELEMENTER);
            tid1 = DateTime.Now;		                        // Notér stopptidspunkt
            intervall = tid1 - tid0;                            // Bergegn tidsforbruk.
            if (!SjekkSortering(kopiTabell, ANTALL_ELEMENTER)) Console.Write("Feilsortert!!  ");
            Console.WriteLine(SkrivResultat("SelectionSort", intervall));
            Console.WriteLine();

            Console.WriteLine("kopierer..");
            kopierTabell(minTabell, kopiTabell, ANTALL_ELEMENTER);
            Console.WriteLine("Sorterer med InsertionSort ...");
            tid0 = DateTime.Now;		                        // Notér starttidspunkt        
            Sorter.InsertionSort(kopiTabell, ANTALL_ELEMENTER);
            tid1 = DateTime.Now;		                        // Notér stopptidspunkt
            intervall = tid1 - tid0;                            // Bergegn tidsforbruk.
            if (!SjekkSortering(kopiTabell, ANTALL_ELEMENTER)) Console.Write("Feilsortert!!  ");
            Console.WriteLine(SkrivResultat("InsertionSort", intervall));
            Console.WriteLine();
            

            Console.WriteLine("\nTrykk en tast for å kjøre igjen...");
            Console.ReadKey();

        }

        public static void TestOgVisTid(string typeSorter, int[] minTabell, int[] kopiTabell, ref DateTime tid0, ref DateTime tid1, ref TimeSpan intervall)
        {
            Console.WriteLine("kopierer..");
            kopierTabell(minTabell, kopiTabell, ANTALL_ELEMENTER);
            Console.WriteLine("Sorterer med " + typeSorter +"..");
            tid0 = DateTime.Now;		                        // Notér starttidspunkt        
            Sorter.RecursiveMergeSort(kopiTabell, ANTALL_ELEMENTER);
            tid1 = DateTime.Now;		                        // Notér stopptidspunkt
            intervall = tid1 - tid0;                            // Bergegn tidsforbruk.
            if (!SjekkSortering(kopiTabell, ANTALL_ELEMENTER)) Console.Write("Feilsortert!!  ");
            Console.WriteLine(SkrivResultat(typeSorter, intervall));
        }

        public static string SkrivResultat(string algoritme, TimeSpan tid)
        {
            return String.Format("Algoritme: " + algoritme + " Tidsforbruk: " + tid.TotalMilliseconds + " millisekunder");
        }
        public static string SkrivResultat(string algoritme, TimeSpan tid, int snitt)
        {
            return String.Format("Algoritme: " + algoritme + " Tidsforbruk: " + tid.TotalMilliseconds + " Snitt: " + snitt);
        }
        static void GenererTabell(int[] tabell, uint antallElementer)
        {
            for (uint i = 0; i < antallElementer; i++)
            {
                tabell[i] = r.Next(0, MAX_GRENSE_RANDOM);	    // Genererer heltall i området 0 - MAX_GRENSE_RANDOM.
            }                                                   // Alternativt kan NextDouble() brukes.
        }
        static void VisTabell(int[] tabell, uint antallElementer)
        {
            for (uint i = 0; i < antallElementer; i++)
            {
                if ((i % 8) == 0)
                    Console.WriteLine();	                    // Linjeskift etter 8 elementer.
                Console.Write("{0}", tabell[i]);
                if (i < (antallElementer - 1))
                    Console.Write(", ");				        // Skiller alle verdiene med komma.
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        static double Snitt(int[] tabell, uint antallElementer)
        {
            double sum = 0.0;
            for (uint i = 0; i < antallElementer; ++i)
                sum += tabell[i];
            return sum / antallElementer;
        }

        static void kopierTabell(int[] tabell1, int[] tabell2, int ANTALL_ELEMENTER)
        {
            for(int i=0; i< ANTALL_ELEMENTER; i++)
                tabell2[i] = tabell1[i];
        }

        //Sjekker at tabellen er i stigende rekkefølge
        static bool SjekkSortering(int[] tabell, int size)
        {
            for (int i = 1; i < size; i++)
            {
                if (tabell[i - 1] > tabell[i])
                    return false;
            }
            return true;
        }
    }
}
