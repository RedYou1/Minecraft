using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.World_System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting
{
    public static class Helper
    {

        public static bool goodimage = false;

        public static Entities.Player player = new Entities.Player(new Location(-20, 15, 20, -(float)Math.PI / 6, (float)Math.PI - (float)Math.PI / 4, new World()));





        public static double Width;
        public static double Height;

        public static Model3DHandeler group;

        public static String ImageFile = Directory.GetCurrentDirectory() + "\\..\\..\\Images\\";

        public static float Dist(Location loc1, Location loc2)
        {
            return Dist(loc1.X, loc2.X, loc1.Y, loc2.Y, loc1.Z, loc2.Z);
        }
        public static float Dist(float x1, float x2, float y1, float y2, float z1, float z2)
        {
            return (float)Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(z1 - z2, 2) + Math.Pow(y1 - y2, 2));
        }

        /// <summary>
        /// transform un model 3D brute en model 3D utilisable
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static GeometryModel3D Model(Game_Model a)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();

            PointCollection pc = new PointCollection();
            for (int i = 0; i < a.model.Length; i++)
            {
                KeyValuePair<double[], double[]> paire = a.model[i];
                double[] vertex = paire.Key;
                double[] tex = paire.Value;
                mesh.Positions.Add(new Point3D(vertex[0], vertex[1], vertex[2]));
                pc.Add(new Point(tex[0], tex[1]));
                mesh.TriangleIndices.Add(i);
            }
            mesh.TextureCoordinates = pc;

            GeometryModel3D mGeometry = new GeometryModel3D(mesh, a.mat);
            Transform3DGroup trans = new Transform3DGroup();
            mGeometry.Transform = trans;
            return mGeometry;
        }

        public static double[,] MultiplyMatrix(double[,] A, double[,] B)
        {
            int rA = A.GetLength(0);
            int cA = A.GetLength(1);
            int rB = B.GetLength(0);
            int cB = B.GetLength(1);
            double temp = 0;
            double[,] kHasil = new double[rA, cB];
            if (cA != rB)
            {
                throw new ArgumentException("matrik can't be multiplied !!");
            }
            else
            {
                for (int i = 0; i < rA; i++)
                {
                    for (int j = 0; j < cB; j++)
                    {
                        temp = 0;
                        for (int k = 0; k < cA; k++)
                        {
                            temp += A[i, k] * B[k, j];
                        }
                        kHasil[i, j] = temp;
                    }
                }
                return kHasil;
            }
        }
    }
}
