using System;
using System.IO; // para poder operar con archivos
using System.Drawing; // para poder operar con fotos

namespace PROYECTO_IO
{
    /***********************************************************************************************************
     * Nombre de la clase: CPixel
     * Funcionalidad: almacena los valores R, G y B, correspondientes al color de un pixel de una imagen
     * Parametros de entrada:
     *  --> R: int. Valor numérico de rojo de 0 a 255
     *  --> G: int. Valor numérico de verde de 0 a 255
     *  --> B: int. Valor numérico de azul de 0 a 255 roc
     ***********************************************************************************************************/
    class CPixel
    {
        public int R;
        public int G;
        public int B;

        public CPixel(int red, int green, int blue)
        {
            R = red;
            G = green;
            B = blue;
        }
    }

    /***********************************************************************************************************
     * Nombre de la clase: CUsuario
     * Funcionalidad: Almacena el nombre, correo y contraseña que el usuario introduzca en la clase usuario.
     * Parametros de entrada:
     *  --> nombre: string nombre
     *  --> correo: string correo
     *  --> contraseña: string contraseña
     ***********************************************************************************************************/
    public class CUsuario // Creamos la clase en la que almacenamos datos de los usuarios
    {
        public string nombre;
        public string correo;
        public string contraseña;

    }

    /***********************************************************************************************************
     * Nombre de la clase: CLista
     * Funcionalidad: 
     * Parametros de entrada:
     *  --> 
     *  --> 
     *  --> 
     ***********************************************************************************************************/
    public class CLista
    {
        public CUsuario[] usuarios;
    }

    internal class Program
    {
        /************************************************************************************************************************
         * Nombre de la función: PNGtoMATRIZ
         * Funcionalidad: abre una imagen en formato .png o .jpg y la convierte a una matriz de clase Pixel
         * Parametros de entrada:
         *  --> fotoelegida: String que contiene el nombre de la imagen a abrir. Debe incluir el .png o .jpg
         *  Devuelve:
         *  --> Matriz de clase Pixel: donde cada Pixel contiene los valores R, G y B de cada pixel de la imagen original. 
         *         La forma de la matriz es [anchura, altura]. La posición [0,0] representa el valor de arriba a la izquierda y 
         *         la posición [anchura,altura] el valor de abajo a la derecha de la imagen original.
         *  --> Valor null si la imagen no se encuentra.
         * **********************************************************************************************************************/
        static CPixel[,] PNGtoMATRIZ(string fotoelegida) //De .png o .jpg a matriz inimage
        {
            if (fotoelegida != null)
            {
                string inputimagename = fotoelegida;
                Bitmap inimage = new Bitmap(inputimagename); // Abrimos la imagen como Bitmap
                CPixel[,] outimage = new CPixel[inimage.Width, inimage.Height]; //Matriz de Pixeles que devolveremos

                for (int i = 0; i < inimage.Width; i++) //Rellenamos el vector con los valores de la imagen
                {
                    for (int j = 0; j < inimage.Height; j++)
                    {
                        Color color = inimage.GetPixel(i, j);
                        outimage[i, j] = new CPixel(color.R, color.G, color.B);
                    }
                }
                return outimage; //matriz de pixeles
            }
            return null;
        }

        /***********************************************************************************************************
         * Nombre de la función: MATRIZtoPNG
         * Funcionalidad: recibe una matriz de clase Pixel y un nombre de archivo y guarda la imagen en formato .png o .jpg
         * Parametros de entrada:
         *  --> inimage: Matriz de clase Pixel. Cada Pixel debe contener los valores R, G y B que se visualizarán en la imagen a guardar.
         *              La forma de la matriz debe ser [anchura, altura], donde anchura y altura son el tamaño de la imagen en los ejes X e Y.
         *              La posición [0,0] representa el valor de arriba a la izquierda y 
         *              la posición [anchura,altura] el valor de abajo a la derecha de la imagen original.
         *  --> filename: String que contiene el nombre con el que se guardará la imagen. Debe incluir el .png o .jpg
         *  Devuelve:
         *  --> True: Si se ha podido guardar la imagen
         *  --> False: Si no se ha podido guardar la imagen.
         ***********************************************************************************************************/
        static bool MATRIZtoPNG(CPixel[,] inimage, string filename) //De matriz inimage a .png
        {
            if (inimage != null && filename != null)
            {
                Bitmap outimage = new Bitmap(inimage.GetLength(0), inimage.GetLength(1));

                for (int i = 0; i < inimage.GetLength(0); i++) // Recorrido por la imagen para guardar los valores
                {
                    for (int j = 0; j < inimage.GetLength(1); j++)
                    {
                        outimage.SetPixel(i, j, Color.FromArgb(inimage[i, j].R, inimage[i, j].G, inimage[i, j].B));
                    }
                }
                outimage.Save("editada " + filename); // Guardar la imagen
                return true;
            }
            return false;
        }

        /***********************************************************************************************************
         * Nombre de la función: reverse
         * Funcionalidad: recibe una matriz de clase Pixel y devuelve una matriz de clase Pixel con los valores espejo.
         * Parametros de entrada:
         *  --> inimage: Matriz de clase Pixel. Cada Pixel debe contener los valores R, G y B.
         *              La forma de la matriz debe ser [anchura, altura], donde anchura y altura son el tamaño de la imagen en los ejes X e Y.
         *              La posición [0,0] representa el valor de arriba a la izquierda y 
         *              la posición [anchura,altura] el valor de abajo a la derecha de la imagen original.         
         *  Devuelve:
         *  --> Matriz de clase Pixel. Cada Pixel contiene los valores R, G y B.
         *              La forma de la matriz es la misma que la matriz de entrada
         *              La posición [0,0] representa el valor de arriba a la derecha de la matriz original y 
         *              la posición [anchura,altura] el valor de abajo a la izquierda de la matriz original
         *  --> null: Si no se ha pasado un parámetro de entrada != null.
         ***********************************************************************************************************/
        static CPixel[,] reverse(CPixel[,] inimage)
        {
            CPixel[,] result = null;
            int width, height;

            if (inimage != null)
            {
                width = inimage.GetLength(0);
                height = inimage.GetLength(1);
                result = new CPixel[width, height];

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                    // igualar al lado opuesto
                    result[i, j] = inimage[width - i - 1, j];
                    }
                }
                return result;
            }
            return result;
        }

        /***********************************************************************************************************
          * Nombre de la función: enmarcar
          * Funcionalidad: 
          * Parametros de entrada:
          *  --> inimage: Matriz de clase Pixel. Cada Pixel debe contener los valores R, G y B que se visualizarán en la imagen a guardar.
          *              La forma de la matriz debe ser [anchura, altura], donde anchura y altura son el tamaño de la imagen en los ejes X e Y.
          *              La posición [0,0] representa el valor de arriba a la izquierda y 
          *              la posición [anchura,altura] el valor de abajo a la derecha de la imagen original.
          *  Devuelve:
          *  --> 
          *  --> 
          ***********************************************************************************************************/
        static CPixel[,] enmarcar(CPixel[,] inimage)
        {
            CPixel[,] result = null;
            if (inimage != null)
            {
                int width = inimage.GetLength(0);
                int height = inimage.GetLength(1);
                int j, i;
                result = inimage;
                CPixel colorRojo = new CPixel(155, 100, 50);

                for (i = 0; i < width; i++)
                {
                    for (j = 0; j < height; j++)
                    {
                        if (i <= 10 || i >= width - 11 || j <= 10 || j >= height - 11) // verificamos si estamos en donde queremos enmarcar
                        {
                            result[i, j] = new CPixel(colorRojo.R, colorRojo.G, colorRojo.B);
                            //Hacer que calcule la altura de la imagen y que el marco opcupe el 20% de los pixeles de la altura
                        }
                    }
                }
                return result;
            }
            return result;
        }

        /***********************************************************************************************************
         * Nombre de la función: cambiocolor
         * Funcionalidad: Cambiar el valor de los pixeles de la foto
         * Parametros de entrada:
         *  --> inimage: Matriz de clase Pixel. Cada Pixel debe contener los valores R, G y B que se visualizarán en la imagen a guardar.
         *              La forma de la matriz debe ser [anchura, altura], donde anchura y altura son el tamaño de la imagen en los ejes X e Y.
         *              La posición [0,0] representa el valor de arriba a la izquierda y 
         *              la posición [anchura,altura] el valor de abajo a la derecha de la imagen original.
         *  --> color: rojo, verde o azul
         *  Devuelve:
         *  --> matriz result con los pixeles cambiados
         ***********************************************************************************************************/

        static CPixel[,] cambiocolor(CPixel[,] inimage, string color)
        {
            CPixel[,] result = null;

            if (inimage != null)
            {
                int width, height, rojo, verde, azul;
                width = inimage.GetLength(0);
                height = inimage.GetLength(1);
                result = inimage;
                //inimage = new CPixel[width, height]; //hace falta?

                if(color == "rojo")
                {
                    for(int i  = 0; i < width; i++)
                    {
                        for(int j = 0;j < height; j++)
                        {
                            CPixel pix = result[i, j];
                            
                            rojo = pix.R;
                            verde = pix.G;
                            azul = pix.B;

                            if (rojo <= 205)
                            {
                                result[i, j] = new CPixel(rojo + 50, verde, azul);
                            }
                        }
                    }
                }
                if (color == "verde")
                {
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            CPixel pix = result[i, j];

                            rojo = pix.R;
                            verde = pix.G;
                            azul = pix.B;

                            if (verde <= 205)
                            {
                                result[i, j] = new CPixel(rojo, verde + 50, azul);
                            }
                        }
                    }
                }
                if (color == "azul")
                {
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            CPixel pix = result[i, j];

                            rojo = pix.R;
                            verde = pix.G;
                            azul = pix.B;

                            if (azul <= 205)
                            {
                                result[i, j] = new CPixel(rojo, verde, azul + 50);
                            }
                        }
                    }
                }
                return result;
            }
            return result;
        }

        /***********************************************************************************************************
         * Nombre de la función: collage
         * Funcionalidad: Crear un collage con cuatro fotos
         * Parametros de entrada:
         *  --> inimage: Matriz de clase Pixel. Cada Pixel debe contener los valores R, G y B que se visualizarán en la imagen a guardar.
         *              La forma de la matriz debe ser [anchura, altura], donde anchura y altura son el tamaño de la imagen en los ejes X e Y.
         *              La posición [0,0] representa el valor de arriba a la izquierda y 
         *              la posición [anchura,altura] el valor de abajo a la derecha de la imagen original.
         *  --> inimage2
         *  --> inimage3
         *  --> inimage4
         *  Devuelve:
         *  --> la matriz resultado
         ***********************************************************************************************************/
        public static CPixel[,] collage(CPixel[,] inimage, CPixel[,] inimage2, CPixel[,] inimage3, CPixel[,] inimage4)
        {
            CPixel[,] result = null;

            if (inimage != null || inimage2 != null || inimage3 != null || inimage4 != null)
            {
                // Verifica que las dimensiones de todas las imágenes sean iguales
                int height = inimage.GetLength(0);
                int width = inimage.GetLength(1);

                if (height == inimage2.GetLength(0) && width == inimage2.GetLength(1) &&
                    height == inimage3.GetLength(0) && width == inimage3.GetLength(1) &&
                    height == inimage4.GetLength(0) && width == inimage4.GetLength(1))
                {
                    result = new CPixel[height * 2, width * 2];

                    // Combinar las imágenes en el collage
                    for (int i = 0; i < height; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            result[i, j] = inimage[i, j];
                            result[i + width, j] = inimage2[i, j];
                            result[i, j + height] = inimage3[i, j];
                            result[i + width, j + height] = inimage4[i, j];
                        }
                    }
                    return result;
                }
            }
            return result;
        }



        public static bool ComprobarCuadrado(CPixel[,] inimage, CPixel[,] inimage2, CPixel[,] inimage3, CPixel[,] inimage4)
        {
           if(inimage.GetLength(0) == inimage.GetLength(1) && inimage2.GetLength(0) == inimage2.GetLength(1) 
              && inimage3.GetLength(0) == inimage3.GetLength(1) && inimage4.GetLength(0) == inimage4.GetLength(1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /***********************************************************************************************************
         * Nombre de la función: menu
         * Funcionalidad: Mostrar al usuario las opciones que tiene disponibles y sus respectivos codigos
         * Devuelve:
         *  --> 1 linea en la consola con la pregunta de qué operacion se quiere realizar
         *  --> 5 líneas en la consola, cada una con una operacion
         ***********************************************************************************************************/
        static void menu()
        {
            Console.WriteLine();
            Console.WriteLine("*********************************************************************************");
            Console.WriteLine(" Qué operación quieres realizar?");
            Console.WriteLine();
            Console.WriteLine(" 0: Terminar");
            Console.WriteLine();
            Console.WriteLine(" 1: Enmarcar la foto");
            Console.WriteLine();
            Console.WriteLine(" 2: Hacer un collage con 4 fotos");
            Console.WriteLine();
            Console.WriteLine(" 3: Cambiar la tonalidad de la foto");
            Console.WriteLine();
            Console.WriteLine(" 4: Invertir la imagen");
            Console.WriteLine();
            Console.WriteLine("*********************************************************************************");
        }

        static void Main(string[] args)
        {
            string fotoelegida, fotoelegida2, fotoelegida3, fotoelegida4, colorelegido;
            string usuario, correo, contraseña, opcion;
            
            bool guardada;

            string fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); // Obtiene la fecha y hora actuales y las formatea
            CUsuario us = new CUsuario();
            Console.WriteLine("Introduce usuario");
            usuario = Console.ReadLine();
            us.nombre = usuario;

            try
            {
                StreamReader lectura = new StreamReader(usuario + ".txt");
            }
            catch(FileNotFoundException)    
            {      
                
            }
            
            Console.WriteLine("Introduce correo");
            correo = Console.ReadLine();
            us.correo = correo;
            Console.WriteLine("Contraseña, por favor");
            contraseña = Console.ReadLine();
            us.contraseña = contraseña;         

            StreamWriter escritura = new StreamWriter(usuario + ".txt");
            escritura.WriteLine("Fecha de la edición: " + fecha);
            escritura.WriteLine("Usuario: " + usuario);
            escritura.WriteLine("Correo: " + correo);
            escritura.WriteLine("Contraseña: " + contraseña);
            escritura.WriteLine("");
            
            Console.WriteLine("Qué imagen quieres manipular?");
            fotoelegida = Console.ReadLine();
            CPixel[,] inimage;

            bool existe = false;
            while (!existe)
            {
                try // Probamos si existe la imagen
                {                    
                    inimage = PNGtoMATRIZ(fotoelegida);
                    existe = true; // Si no hay excepción, establecemos existe a true para salir del bucle
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Error al cargar la imagen, introduce nombre de imagen de nuevo:");
                    existe = false;
                    fotoelegida = Console.ReadLine();
                }
                
            }
            escritura.WriteLine("Foto elegida: " + fotoelegida);
            inimage = PNGtoMATRIZ(fotoelegida);
            
            escritura.WriteLine();
            escritura.WriteLine("Historial de operaciones:");

            if (inimage != null)
            {
                menu();
                opcion = Console.ReadLine();

                while (opcion != "0")
                {
                    switch (opcion)
                    {
                        case "0":
                            escritura.WriteLine("Terminar");
                            Console.WriteLine("Terminando Programa");
                            break;

                        case "1":
                            escritura.WriteLine("Enmarcar");
                            Console.WriteLine("Enmarcando de color rojo...");
                            enmarcar(inimage);
                            if (enmarcar(inimage) != null)
                            {
                                guardada = MATRIZtoPNG(enmarcar(inimage), fotoelegida);
                                if (guardada == true)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Foto guardada tio");
                                }
                            }
                            break;

                        case "2":
                            escritura.WriteLine("Collage");
                            Console.WriteLine("Para el collage necesito 3 fotos más tío (tienen que ser cuadradas)");
                            CPixel[,] inimage2, inimage3, inimage4;

                            Console.WriteLine("El nombre de la segunda foto tio");
                            fotoelegida2 = Console.ReadLine();
                            existe = false;
                            while (!existe)
                            {
                                try // Probamos si existe la imagen
                                {                    
                                    inimage2 = PNGtoMATRIZ(fotoelegida2);
                                    existe = true; // Si no hay excepción, establecemos existe a true para salir del bucle
                                    escritura.WriteLine("4 foto elegida: " + fotoelegida2);
                                }
                                catch (ArgumentException)
                                {
                                    Console.WriteLine("Error al cargar la imagen, introduce nombre de la segunda foto de nuevo:");
                                    existe = false;
                                    fotoelegida2 = Console.ReadLine();
                                }
                                
                            }
                            
                            Console.WriteLine("El nombre de la tercera foto tio");
                            fotoelegida3 = Console.ReadLine();
                            existe = false;
                            while (!existe)
                            {
                                try // Probamos si existe la imagen
                                {                    
                                    inimage3 = PNGtoMATRIZ(fotoelegida3);
                                    existe = true; // Si no hay excepción, establecemos existe a true para salir del bucle
                                    escritura.WriteLine("4 foto elegida: " + fotoelegida3);
                                }
                                catch (ArgumentException)
                                {
                                    Console.WriteLine("Error al cargar la imagen, introduce nombre de la tercera foto de nuevo:");
                                    existe = false;
                                    fotoelegida3 = Console.ReadLine();
                                }
                                
                            }

                            Console.WriteLine("El nombre de la cuarta foto tio");
                            fotoelegida4 = Console.ReadLine();
                            existe = false;
                            while (!existe)
                            {
                                try // Probamos si existe la imagen
                                {                    
                                    inimage4 = PNGtoMATRIZ(fotoelegida4);
                                    existe = true; // Si no hay excepción, establecemos existe a true para salir del bucle
                                    escritura.WriteLine("4 foto elegida: " + fotoelegida4);
                                }
                                catch (ArgumentException)
                                {
                                    Console.WriteLine("Error al cargar la imagen, introduce nombre de la cuarta foto de nuevo:");
                                    existe = false;
                                    fotoelegida4 = Console.ReadLine();
                                }
                                
                            }
                            
                            escritura.WriteLine("");

                            
                            
                            if(ComprobarCuadrado(inimage, inimage2, inimage3, inimage4) == true)
                            {
                                CPixel[,] fotocollage = collage(inimage, inimage2, inimage3, inimage4);

                                if (fotocollage != null)
                                {
                                    string collg = "collage.png";
                                    guardada = MATRIZtoPNG(fotocollage, collg);

                                    if (guardada == true)
                                    {
                                        Console.WriteLine("Foto guardada como collage.png tio");
                                    }
                                }
                                
                            }
                            else
                            {
                                Console.WriteLine("No tienen son cuadrados");
                            }
                            break;


                        case "3":
                            escritura.WriteLine("Cambiar color");
                            Console.WriteLine("¿Quieres cambiar a rojo, verde o azul?: ");
                            colorelegido = Console.ReadLine().ToLower();

                            Console.WriteLine("Perfecto, " + colorelegido + ", manos a la obra");
                            cambiocolor(inimage, colorelegido);
                            if (cambiocolor(inimage, colorelegido) != null)
                            {
                                guardada = MATRIZtoPNG(inimage, fotoelegida);
                                if (guardada == true)
                                {
                                    Console.WriteLine("Foto guardada tio");
                                }
                            }
                            break;

                        case "4":
                            escritura.WriteLine("Inversión");
                            Console.WriteLine("Espera un segundín mientras invierto la foto...");
                            reverse(inimage);
                            if (reverse(inimage) != null)
                            {
                                guardada = MATRIZtoPNG(reverse(inimage), fotoelegida);
                                if (guardada == true)
                                {
                                    Console.WriteLine("Foto guardada tio");
                                }
                            }
                            break;

                        default:
                            escritura.WriteLine("Error en código de operación");
                            Console.WriteLine("Código de operacion incorrecto chavalín");
                            break;
                    }
                    menu();
                    opcion = Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("No se pudo cargar la imagen jodeeeeeer");
            }
            escritura.Close();
            Console.WriteLine("Hasta otra tío! :D");
        }
    }
}

