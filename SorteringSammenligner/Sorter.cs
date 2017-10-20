/*
Forfatter:  Lars Vågenes lv
Historie:   Når         Hvem   Hva
            20171016    lv     Opprettet

Innhold:
            BubbleSort
            BubbleSort med pointers, kopiert fra jmj
            SelectionSort
            InsertionSort
            ShellSort
            MergeSort 2 stk. iterative varianter
            MergeSort 1 stk. rekursiv variant
            QuickSort, Rekursiv 
             
 */


using System;

namespace SorteringSammenligner
{
    static class Sorter
    {
        public static void ArrayDotSort(int[] tabell, int size)
        {
            Array.Sort(tabell);
        }

        //Bubble sort
        public static void BubbleSort(int[] tabell, int size)
        {
            bool bytter;
            int temp;
            size--;
            do
            {
                bytter = false;
                for (int i = 0; i < size; i++)
                {
                    if (tabell[i] > tabell[i + 1])
                    {
                        temp = tabell[i];
                        tabell[i] = tabell[i + 1];
                        tabell[i + 1] = temp;
                        bytter = true;
                    }
                }
            } while (bytter);
        }

        //unsafe BubbleSort, kopiert fra jmj sin implementasjon
        public static unsafe void BubbleSortUnsafe(int[] tabell, uint antall)
        {
            //int count = 0;
            int temp;
            int* o;
            int* b;

            fixed (int* nestSist = &tabell[antall - 2])
            {
                o = nestSist;
                b = nestSist;
                fixed (int* tStart = &tabell[0])
                {
                    fixed (int* tSlutt = &tabell[antall - 1])
                    {
                        while (b >= tStart)
                        {
                            while (o < tSlutt && o >= tStart && *o > *(o + 1))
                            {
                                // swap:
                                temp = *(o + 1);
                                *(o + 1) = *o;
                                *o = temp;
                                o++;
                                //count++;
                            }
                            o = --b;
                        }
                    }
                }
            }
        }

        //SelectionSort
        public static void SelectionSort(int[] tabell, int size)
        {
            int min;
            int indexMin;
            int i = 0;
            while (i < size)
            {
                indexMin = i;
                i++;
                for (int j = i; j < size; j++)
                {
                    if (tabell[j] < tabell[indexMin])
                    {
                        indexMin = j;
                    }
                }
                min = tabell[i - 1];
                tabell[i - 1] = tabell[indexMin];
                tabell[indexMin] = min;
            }
        }

        //InsertionSort
        public static void InsertionSort(int[] tabell, int size)
        {
            int temp;
            for (int i = 0; i < size - 1; i++)
            {
                if (tabell[i] > tabell[i + 1])
                {
                    for (int j = i; j >= 0; j--)
                    {
                        if (tabell[j] > tabell[j + 1])
                        {
                            temp = tabell[j];
                            tabell[j] = tabell[j + 1];
                            tabell[j + 1] = temp;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

        }

        //ShellSort
        public static void ShellSort(int[] tabell, int size)
        {
            int temp;
            int t = size;
            do
            {
                t /= 2;
                for (int k = 0; k < t; k++)
                {
                    for (int i = 0 + k; i < size - t; i += t)
                    {
                        if (tabell[i] > tabell[i + t])
                        {
                            for (int j = i; j >= 0; j -= t)
                            {
                                if (tabell[j] > tabell[j + t])
                                {
                                    temp = tabell[j];
                                    tabell[j] = tabell[j + t];
                                    tabell[j + t] = temp;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            } while (t > 1);
        }


        // Iterativ MergeSort
        public static void MergeSorter(int[] tabell, int size)
        {
            int strl = 1;
            int[] tabell2 = new int[size];
            int steg = 0;

            while (strl < size)
            {
                //Siden vi kopierer til en ny tabell, alternerer jeg her hvilken tabell vi kopierer fra og til
                steg++;
                if (steg % 2 == 1)
                    Sort(tabell, tabell2, size, ref strl);
                else
                    Sort(tabell2, tabell, size, ref strl);

                strl *= 2;
            }
            if (steg % 2 == 1)
            {
                //så passer jeg til slutt på at vi har kopiert til riktig tabell
                kopierTabell(tabell2, tabell, size);
            }

        }

        //Hjelpefunksjon tim MergeSort1
        private static void Sort(int[] tab1, int[] tab2, int length, ref int st)
        {
            int ven = 0, høy = st, venf = høy - 1, høyf = venf + st;
            int vens;

            for (int i = 0; i < length / (2 * st); i++)
            {
                vens = ven;
                for (int j = 0; j < 2 * st - 1; j++)
                {
                    //Merge
                    int index = j + vens;
                    while ((ven <= venf) && (høy <= høyf))
                    {
                        if (tab1[ven] < tab1[høy])
                        {
                            tab2[index] = tab1[ven];
                            ven++;
                        }
                        else
                        {
                            tab2[index] = tab1[høy];
                            høy++;
                        }
                        index++;
                    }

                    while (ven <= venf)
                    {
                        tab2[index] = tab1[ven];
                        ven++;
                        index++;
                    }

                    while (høy <= høyf)
                    {
                        tab2[index] = tab1[høy];
                        høy++;
                        index++;
                    }
                }
                //setter verdier for neste merge
                ven += st;
                høy += st;
                venf = høy - 1;
                høyf = venf + st;
            }
            int mod = length % (2 * st);
            if (mod > 0)
            {
                if (mod > st)
                {
                    ven = st * 2 * (length / (st * 2));
                    høy = ven + st;
                    venf = høy - 1;
                    høyf = length - 1;
                    vens = ven;
                    for (int j = 0; j < mod - 1; j++)
                    {
                        //Merge
                        int index = j + vens;
                        while ((ven <= venf) && (høy <= høyf))
                        {
                            if (tab1[ven] < tab1[høy])
                            {
                                tab2[index] = tab1[ven];
                                ven++;
                            }
                            else
                            {
                                tab2[index] = tab1[høy];
                                høy++;
                            }
                            index++;
                        }

                        while (ven <= venf)
                        {
                            tab2[index] = tab1[ven];
                            ven++;
                            index++;
                        }

                        while (høy <= høyf)
                        {
                            tab2[index] = tab1[høy];
                            høy++;
                            index++;
                        }

                    }
                }
                else
                {
                    //kopier resten vist det ikke lar seg merge
                    for (int i = st * 2 * (length / (st * 2)); i < length; i++)
                    {
                        tab2[i] = tab1[i];
                    }
                }

            }

        }

        // Den raskeste av 2 MergeSorts
        public static void MergeSorter2(int[] tabell, int size)
        {
            int strl = 1;
            int[] tabell2 = new int[size];
            int steg = 0;

            while (strl < size)
            {
                //Siden vi kopierer til en ny tabell, alternerer jeg her hvilken tabell vi kopierer fra og til
                steg++;
                if (steg % 2 == 1)
                    Sort2(tabell, tabell2, size, ref strl);
                else
                    Sort2(tabell2, tabell, size, ref strl);

                strl *= 2;
            }
            if (steg % 2 == 1)
            {
                //så passer jeg til slutt på at vi har kopiert til riktig tabell
                kopierTabell(tabell2, tabell, size);
            }

        }



        //Hjelpefunksjon til MergeSort2
        private static void Sort2(int[] tab1, int[] tab2, int length, ref int st)
        {
            int ven = 0, høy = st, venf = høy - 1, høyf = venf + st;
            int vens;

            for (int i = 0; i < length / (2 * st); i++)
            {
                vens = ven;
                for (int j = 0; j < 2 * st - 1; j++)
                {
                    //Merge
                    if (tab1[ven] < tab1[høy])//om venstre er minst, legg til venstre i nye tabellen
                    {
                        tab2[j + vens] = tab1[ven];
                        ven++;
                    }
                    else //og vist ikke venstre er minst, tar vi høyre leddet inn i nye tabellen
                    {
                        tab2[j + vens] = tab1[høy];
                        høy++;

                    }
                    //så sjekker vi om noen av sidene er "tom"
                    if (ven > venf)
                    {
                        //tøm resten av høre side
                        while (høy <= høyf)
                        {
                            tab2[j + vens + 1] = tab1[høy];
                            høy++;
                            j++;
                        }
                    }
                    else if (høy > høyf)
                    {
                        //tøm resten av venstre side
                        while (ven <= venf)
                        {
                            tab2[j + vens + 1] = tab1[ven];
                            ven++;
                            j++;
                        }
                    }

                }
                //setter verdier for neste merge
                ven += st;
                høy += st;
                venf = høy - 1;
                høyf = venf + st;
            }
            //Merger deretter det som ikke er merget om det kan, ellers kopieres det
            int mod = length % (2 * st);
            if (mod > 0)
            {
                if (mod > st)
                {
                    ven = st * 2 * (length / (st * 2));
                    høy = ven + st;
                    venf = høy - 1;
                    høyf = length - 1;
                    vens = ven;
                    for (int j = 0; j < mod - 1; j++)
                    {
                        //Merge
                        if (tab1[ven] < tab1[høy])//om venstre er minst, legg til venstre i nye tabellen
                        {
                            tab2[j + vens] = tab1[ven];
                            ven++;
                        }
                        else //og vist ikke venstre er minst, tar vi høyre leddet inn i nye tabellen
                        {
                            tab2[j + vens] = tab1[høy];
                            høy++;

                        }
                        //så sjekker vi om noen av sidene er "tom"
                        if (ven > venf)
                        {
                            //tøm resten av høre side
                            while (høy <= høyf)
                            {
                                tab2[j + vens + 1] = tab1[høy];
                                høy++;
                                j++;
                            }
                        }
                        else if (høy > høyf)
                        {
                            //tøm resten av venstre side
                            while (ven <= venf)
                            {
                                tab2[j + vens + 1] = tab1[ven];
                                ven++;
                                j++;
                            }
                        }

                    }
                }
                else
                {
                    //kopier resten vist det ikke lar seg merge
                    for (int i = st * 2 * (length / (st * 2)); i < length; i++)
                    {
                        tab2[i] = tab1[i];
                    }
                }

            }

        }

        //Rekursiv MergeSort
        public static void RecursiveMergeSort(int[] tabell, int size)
        {
            MergeSortRecursive(tabell, 0, size - 1);

        }

        private static void MergeSortRecursive(int[] tabell, int lav, int høg)
        {
            if (lav < høg) // del opp så langt det går så lenge forskjellen mellom lav og høg > 0
            {
                int mid = (høg / 2) + (lav / 2);
                MergeSortRecursive(tabell, lav, mid);     //Del opp venstre side opp videre
                MergeSortRecursive(tabell, mid + 1, høg); // Del opp høyre side opp videre
                Merge(tabell, lav, mid, høg); //Når alt er delt opp, merge det sammen igjen
            }
        }

        private static void Merge(int[] tabell, int lav, int mid, int høg)
        {

            int[] tabell2 = new int[1 + høg - lav];

            int venstre = lav, høyre = 1 + mid;
            int index = 0;

            //Fyll inn begge sider frem til ene siden er ferdig
            while ((venstre <= mid) && (høyre <= høg))
            {
                if (tabell[venstre] < tabell[høyre])
                {
                    tabell2[index] = tabell[venstre];
                    venstre = venstre + 1;
                }
                else
                {
                    tabell2[index] = tabell[høyre];
                    høyre = høyre + 1;
                }
                index++;
            }

            //fyll inn resten av venstre side
            while (venstre <= mid)
            {
                tabell2[index] = tabell[venstre];
                venstre = venstre + 1;
                index = index + 1;
            }

            //Fyll inn resten av høyre side
            while (høyre <= høg)
            {
                tabell2[index] = tabell[høyre];
                høyre = høyre + 1;
                index = index + 1;
            }

            //Fyll tilbake til den originale tabellen
            for (int i = 0; i < tabell2.Length; i++)
            {
                tabell[lav + i] = tabell2[i];
            }

        }

        // QuickSort, rekursiv
        public static void QuickSort(int[] tabell, int size)
        {
            QuickSortH(tabell, 0, size - 1);
        }

        private static void QuickSortH(int[] tabell, int start, int slutt)
        {
            if (start < slutt)
            {
                int piv = start;
                int temp;
                for (int i = start; i < slutt; i++)
                {
                    if (tabell[i] <= tabell[slutt])
                    {
                        temp = tabell[i];
                        tabell[i] = tabell[piv];
                        tabell[piv] = temp;
                        piv++;
                    }
                }

                temp = tabell[piv];
                tabell[piv] = tabell[slutt];
                tabell[slutt] = temp;



                QuickSortH(tabell, start, piv - 1);
                QuickSortH(tabell, piv + 1, slutt);
            }
        }



        //Hjelpefunksjoner

        private static void kopierTabell(int[] tabell1, int[] tabell2, int ANTALL_ELEMENTER)
        {
            for (int i = 0; i < ANTALL_ELEMENTER; i++)
                tabell2[i] = tabell1[i];
        }


    }
}
