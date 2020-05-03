using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyPoints_Bayes
{
    class KeyPoints
    {
        public void AddMask()
        {
            Bitmap btm = new Bitmap(@"img_before/1.jpg");
            Bitmap newimage = new Bitmap(btm.Width, btm.Height);
            double[][] filtr = new double[3][];
            filtr[0] = new double[] { -1, -1, -1 };
            filtr[1] = new double[] { -1, 8, -1 };
            filtr[2] = new double[] { -1, -1, -1 };
            for (int i = 1; i < btm.Width - 1; i++)
            {
                for (int j = 1; j < btm.Height - 1; j++)
                {
                    int RValue = 0;
                    int GValue = 0;
                    int BValue = 0;
                    for (int k = -1; k < 2; k++)
                    {
                        for (int l = -1; l < 2; l++)
                        {
                            int mask = (int)filtr[k + 1][l + 1];
                            RValue += btm.GetPixel(i + k, j + l).R * mask;
                            GValue += btm.GetPixel(i + k, j + l).G * mask;
                            BValue += btm.GetPixel(i + k, j + l).B * mask;
                        }
                    }
                    if (RValue > 255)
                    { RValue = 255; }
                    else if (RValue < 0)
                    { RValue = 0; }

                    if (GValue > 255)
                    { GValue = 255; }
                    else if (GValue < 0)
                    { GValue = 0; }

                    if (BValue > 255)
                    { BValue = 255; }
                    else if (BValue < 0)
                    { BValue = 0; }

                    newimage.SetPixel(i, j, Color.FromArgb(RValue, GValue, BValue));
                }
            }
            newimage.Save(@"img_after/1_mask.jpg");
        }
        public void PotencialPoints(int numberOfPoints)
        {
            Bitmap btm = new Bitmap(@"img_before/1.jpg");
            Bitmap result = new Bitmap(btm.Width, btm.Height);
            List<List<double>> potencialPoints = new List<List<double>>();

            for (int i = 1; i < btm.Width - 1; i++)
            {
                for (int k = 1; k < btm.Height - 1; k++)
                {
                    potencialPoints.Add(
                        new List<double>
                        {
                            Convert.ToDouble(i), Convert.ToDouble(k),Convert.ToDouble(BrightnessCalculate(btm, i, k))
                        }
                        ); ;
                }
            }

            potencialPoints = potencialPoints.OrderByDescending(k => k[2]).ToList();
            potencialPoints.RemoveRange(numberOfPoints, potencialPoints.Count - numberOfPoints);

            for (int i = 0; i < result.Width; i++)
            {
                for (int k = 0; k < result.Height; k++)
                {
                    CopyPixel(ref result, i, k, btm, i, k);
                }
            }

            foreach (var p in potencialPoints)
            {
                result.SetPixel(Convert.ToInt32(p[0]), Convert.ToInt32(p[1]), Color.FromArgb(255, 0, 0));
            }

            result.Save(@"img_after/1_keyPoints.jpg");

        }
        public void CopyPixel(ref Bitmap dest, int x, int y, Bitmap source, int a, int b)
        {
            Color pxl = source.GetPixel(a, b);
            dest.SetPixel(x, y, pxl);
        }
        public float BrightnessCalculate(Bitmap img, int x, int y)
        {
            float result = 0;

            for (int a = -1; a < 2; a++)
            {
                for (int b = -1; b < 2; b++)
                {
                    result += img.GetPixel(x + a, y + b).GetBrightness();
                }
            }
            return result / 9;
        }
    }
}
