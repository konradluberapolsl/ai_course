using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lab02
{
    class Program
    {
        static void Main(string[] args)
        {
            Bitmap btm = new Bitmap(@"nowy.jpg");
            Bitmap btmF = new Bitmap(btm.Width, btm.Height);
            double[][] kernel = new double[3][];
            kernel[0] = new double[] { -1, -1, -1 };
            kernel[1] = new double[] { -1, 8, -1 };
            kernel[2] = new double[] { -1, -1, -1 };
            double pR = 0, pG =0 , pB = 0;
            double k = 0;
            for (int i = 0; i < kernel.Length; i++)
            {
                for (int j = 0; j < kernel[i].Length; j++)
                {
                    k += kernel[i][j];
                }
            }
            for (int i = 0; i < btm.Width - kernel.Length; i++)
            {
                for (int j = 0; j < btm.Height - kernel[0].Length; j++)
                {
                    
                    pR = 0; pG = 0; pG = 0;
                    for (int x = kernel.Length - 1; x > -1 ; x--)
                    {
                        for (int y = kernel[0].Length -1; y > -1; y--)
                        {
                            Color pxl = btm.GetPixel(i + kernel.Length -  1 - x, j + kernel[0].Length -1 -y);
                            pR += (pxl.R * kernel[x][y]);   
                            pG += (pxl.G * kernel[x][y]);
                            pB += (pxl.B * kernel[x][y]);
                            
                        }
                    }
                    //pR *= k;
                    //pG *= k;
                    //pB *= k;
                    if (pR < 0)
                        pR = 0;
                    if (pG < 0)
                        pG = 0;
                    if (pB < 0)
                        pB = 0;
                    if (pR > 255)
                        pR = 255;
                    if (pG > 255)
                        pG = 255;
                    if (pB > 255)
                        pB = 255;
                    //Console.Write("R: " + pR + " G: " + pG + " B: " + pB + " \n");
                    //Color pxl = btm.GetPixel(i, j);
                    //int newColor = (pxl.R+pxl.G+pxl.B)/3;
                    btm.SetPixel(i, j,Color.FromArgb( (int)pR, (int)pG, (int)pB));
                }
            }
            btm.Save(@"qwe.jpg");
        }
    }
}
