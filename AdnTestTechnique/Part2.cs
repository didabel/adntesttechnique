using System;
using NUnit.Framework;

namespace AdnTestTechnique
{

    public class Part2
    {
        private static int[] InitiliseTab(int taille)
        {
            int[] result = new int[taille];

            for(int i = 0; i < taille; i++)
            {
                result[i] = 0;
            }

            return result;
        }


        private static int[] FindSubtotals(int[] values)
        {
            int sumSucc, sumPre;

            int[] result = InitiliseTab(values.Length);


            int x, y, nbrSucc, nbrPred;

            for (int i = 0, j = values.Length - 1; i < values.Length && j >= 0; i++, j--)
            {
                sumSucc = sumPre = nbrSucc = nbrPred = 0;

                x = i + 1;
                y = j - 1;

                if (x > values.Length || y < 0)
                {
                    break;
                }

                while ((sumSucc < values[i] && x < values.Length) || (sumPre < values[j] && y >= 0))
                {
                    sumSucc += values[x];
                    x++;
                    nbrSucc++;

                    sumPre += values[y];
                    y--;
                    nbrPred++;
                }

                if ( sumSucc == values[i] && nbrSucc >= 2)
                {
                    result[i] = x - 1 - i;
                }

                if (sumPre == values[j] && nbrPred >= 2)
                {
                    result[j] = -(j - (y + 1));
                }

            }
            return result;
        }




        private static int[] EmptySubtotals(int[] values, int[] codage)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (codage[i] != 0)
                {
                    values[i] = 0;
                }
            }
            return values;
        }

       

        private static int[] CalculateSubtotals(int[] values, int[] codage)
        {
            int j, sum;
            int[] result = new int[values.Length];

            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == 0)
                {
                    j = 1;
                    sum = 0;

                    if (codage[i] > 0)
                    {
                        while (j <= codage[i])
                        {
                            sum += values[i + j];
                            j++;
                        }
                    }

                    else if(codage[i] < 0)
                    {
                        while (j <= Math.Abs(codage[i]))
                        {
                            sum += values[i - j];
                            j++;
                        }
                    }

                    result[i] = sum;
                }

                else
                {
                    result[i] = values[i];
                }

            }

            return result;
        }

       
        [Test]
        public void FindSubtotalsTest()
        {
            var values = new[] { 8, 3, 5, 9, 3, 6 };
            CollectionAssert.AreEqual(FindSubtotals(values), new[] { 2, 0, 0, 2, 0, 0 });

            values = new[] { 3, 5, 8, 3, 6, 9 };
            CollectionAssert.AreEqual(FindSubtotals(values), new[] { 0, 0, -2, 0, 0, -2 });

            values = new[] { 10, 20, 40, 80 };
            CollectionAssert.AreEqual(FindSubtotals(values), new[] { 0, 0, 0, 0 });

            values = new int[0];
            CollectionAssert.AreEqual(FindSubtotals(values), new int[0]);

            values = new[] { 4, 2, 1, 1 };
            CollectionAssert.AreEqual(FindSubtotals(values), new[] { 3, 2, 0, 0 });

            values = new[] { 1, 1, 2, 4 };
            CollectionAssert.AreEqual(FindSubtotals(values), new[] { 0, 0, -2, -3 });

            values = new[] { 10, 5, 0, 5, 10 };
            CollectionAssert.AreEqual(FindSubtotals(values), new[] { 3, 2, 0, -2, -3 });

            values = new[] { 16, 0, 8, 4, 4 };
            CollectionAssert.AreEqual(FindSubtotals(values), new[] { 4, 0, 2, 0, 0 });
        }

        [Test]
        public void EmptySubtotalsTest()
        {
            var values = new[] { 7, 2, 5, 10, 9, 1 };
            var codes = new[] { 2, 0, 0, 2, 0, 0 };
            CollectionAssert.AreEqual(EmptySubtotals(values, codes), new[] { 0, 2, 5, 0, 9, 1 });

            values = new[] { 4, 2, 1, 1 };
            codes = new[] { 3, 2, 0, 0 };
            CollectionAssert.AreEqual(EmptySubtotals(values, codes), new[] { 0, 0, 1, 1 });

            values = new[] { 10, 20, 40, 80 };
            codes = new[] { 0, 0, 0, 0 };
            CollectionAssert.AreEqual(EmptySubtotals(values, codes), new[] { 10, 20, 40, 80 });

            values = new[] { 2, 3, 5 };
            codes = new[] { 0, 0, -2 };
            CollectionAssert.AreEqual(EmptySubtotals(values, codes), new[] { 2, 3, 0 });
        }

        [Test]
        public void CaculateSubtotalsTest()
        {
            var values = new[] { 0, 2, 5, 0, 9, 1 };
            var codes = new[] { 2, 0, 0, 2, 0, 0 };
            CollectionAssert.AreEqual(CalculateSubtotals(values, codes), new[] { 7, 2, 5, 10, 9, 1 });

            values = new[] { 2, 3, 0 };
            codes = new[] { 0, 0, -2 };
            CollectionAssert.AreEqual(CalculateSubtotals(values, codes), new[] { 2, 3, 5 });

            values = new[] { 2, 3, 10 };
            codes = new[] { 0, 0, 0 };
            CollectionAssert.AreEqual(CalculateSubtotals(values, codes), new[] { 2, 3, 10 });
        }
    }
}
