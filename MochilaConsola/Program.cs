using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;


//prueba de para github
namespace MochilaConsola
{
    class Program
    {
        static int[] peso;
        static int[] ganancia;
        static int capacidad_mochila = 0;
        static string[] pobBin;
        static int[] GanMod;
        static void Main(string[] args)
        {



            double sumafx = 0;

            int capacidad = 0, numPoblacion = 0, numArticulos = 0, vueltas = 0;

            Console.WriteLine("Escribe la cantidad de individuos");
            string p = Console.ReadLine();
            numPoblacion = int.Parse(p);

            Console.WriteLine("Escribe la capacidad de la mochina");
            string cap = Console.ReadLine();
            capacidad_mochila = int.Parse(cap);

            Console.WriteLine("ingresa el numero de vueltas");
            string vu = Console.ReadLine();
            vueltas = int.Parse(vu);

            /*
            Console.WriteLine("Escribe la cantidad numero de articulos");
            string numa = Console.ReadLine();
            numArticulos = int.Parse(numa);
            */

            //Metodo se agrega el peso y beneficio de la mochila 
            load_data();

            pobBin = new string[numPoblacion];
            Random r = new Random();

            //Individuos aleatorios binario
            for (int i = 0; i < numPoblacion; i++)
            {

                String binario = "";

                for (int j = 0; j < numPoblacion; j++)
                {
                    int rand = r.Next(0, 2);
                    binario = binario + Convert.ToString(rand);
                }

                //Asigna los valores binarios a los miembros del arreglo
                pobBin[i] = binario;

                Console.WriteLine(pobBin[i]);
            }


            

            for (int a = 0; a < vueltas; a++)
            {

                restricion();
                solicuonNoValida(pobBin);

            }

            


            Console.ReadKey();
        }


        public static void penX(string[] poblacion)
        {



        }




        public static void cruze()
        {



        }


        public static void load_data()
        {

            string rutaListPesos = "C:/Users/Marcos Pedraza/Desktop/cosaDeLaEscuela/sistemas_inteligentes/p05_w.txt";
            string rutaListBen = "C:/Users/Marcos Pedraza/Desktop/cosaDeLaEscuela/sistemas_inteligentes/p05_p.txt";

            //string rutaListPesos = "C:/Users/vladi/Documents/txt/p07/pesos.txt";
            //string rutaListBen = "C:/Users/vladi/Documents/txt/p07/beneficios.txt";

            // string rutaListPesos = "C:/Users/vladi/Documents/txt/p08/pesos.txt";
            // string rutaListBen = "C:/Users/vladi/Documents/txt/p08/beneficios.txt";

            try
            {

                StreamReader sr = new StreamReader(rutaListPesos);
                StreamReader sr_ben = new StreamReader(rutaListBen);

                String linea;


                List<int> arrayListPesos = new List<int>();
                List<int> arrayListBeneficio = new List<int>();

                using (StreamReader reader = System.IO.File.OpenText(rutaListPesos))
                {

                    while ((linea = reader.ReadLine()) != null)
                    {
                        arrayListPesos.Add(Convert.ToInt16(linea));
                    }
                }

                using (StreamReader reader_ben = System.IO.File.OpenText(rutaListBen))
                {

                    while ((linea = reader_ben.ReadLine()) != null)
                    {
                        arrayListBeneficio.Add(Convert.ToInt16(linea));
                    }
                }

                peso = arrayListPesos.ToArray();
                ganancia = arrayListBeneficio.ToArray();

            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }





        public static void solicuonNoValida(string[] poblacion)
        {

            Console.WriteLine("capacidad de la mochila: " + 104);
            int totalMochila = 104;
            int[] sumaPesos = new int[poblacion.Length];
            GanMod = new int[poblacion.Length]; 

            for (int a = 0; a < poblacion.Length; a++)
            {

                
                int sumPeso = 0;
                int sumGam = 0;

                string mochila = poblacion[a];
              
                for (int m = 0; m < 8; m++)
                {
                    //Console.WriteLine("--------------------");
                    Console.WriteLine("* " + sumPeso);
                    sumPeso = sumPeso + (Convert.ToInt16(mochila.Substring(m, 1)) * peso[m]);
                    Console.WriteLine("objeto: "+mochila[m]+" * " +peso[m] +" = "+ " peso: " + sumPeso);
                    //Console.WriteLine("articulo:" + mochila[m] + " * " + "peso: " + peso[m]);
                    //Console.WriteLine("peso acumulado: " + sumPeso);
                    sumGam = sumGam + (Convert.ToInt16(mochila.Substring(m, 1)) * ganancia[m]);
                    
                }

                sumaPesos[a] = sumPeso;
                Console.WriteLine("------- peso para esta vuelta: " + sumaPesos[a]);
                if (sumPeso > totalMochila)
                {
                    GanMod[a] = 0;
                }
                else
                {
                    GanMod[a] = sumGam;
                }

            }

            Console.WriteLine("el mayor del peso es: " + sumaPesos.Max());
            Console.WriteLine("Nuevas ganancias-------");

            for (int j = 0; j < poblacion.Length; j++)
            {
                //Console.WriteLine(GanMod[j]);
            }

            Console.WriteLine("Ganancia Maxima: " + GanMod.Max());

        }


        public static int penalizar(string mochila)
        {
            int pen = 0;
            int maxPen = 0;
            int p = 0;

            for (int c = 0; c < mochila.Length; c++)
            {
                if (mochila[c] == '1')
                {
                    if (maxPen < (ganancia[c] / peso[c]))
                    {
                        maxPen = ganancia[c] / peso[c];
                    }

                }
            }

            p = maxPen;

            for (int r = 0; r < mochila.Length; r++)
            {
                pen = p * (mochila[r] * peso[r] - capacidad_mochila);
            }
            return pen;
        }

        public static int funcion(String item)
        {
            int ganancia_s = 0;
            int cont = 0;
            for(int i = 0; i < item.Length; i++)
            {
                if (item[i] == '1')
                {
                    ganancia_s += ganancia[cont];
                }
                cont++;
            }
            return ganancia_s;
        }


        public static void restricion()
        {
            for (int d = 0; d < ganancia.Length; d++)
            {

                int ganancia_aux = 0;

                if (peso[d] > capacidad_mochila)
                {
                    ganancia_aux = (funcion(pobBin[d]) - penalizar(pobBin[d]));
                    ganancia[d] = ganancia_aux;
                }


            }
        }
    }

  
}
