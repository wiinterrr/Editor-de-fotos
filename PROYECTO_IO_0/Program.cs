using System;
using System.Linq;
using System.IO; // para poder operar con archivos
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices; // para poder operar con fotos

namespace PROYECTO_IO
{
    /***********************************************************************************************************
     * Nombre de la clase: CPixel
     * Funcionalidad: almacena los valores R, G y B, correspondientes al color de un pixel de una imagen
     * Parametros de entrada:
     *  --> R: int. Valor numérico de rojo de 0 a 255
     *  --> G: int. Valor numérico de verde de 0 a 255
     *  --> B: int. Valor numérico de azul de 0 a 255
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
        public string contraseña;
        // public string foto;
    }

    /***********************************************************************************************************
     * Nombre de la clase: CLista
     * Funcionalidad: Es un vector que almacena un vector por usuario con su nombre y contraseña.
     ***********************************************************************************************************/
    public class CLista
    {
        public CUsuario[] usuarios = new CUsuario[3333];
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
        FUNCIONES DE TXTaVECTOR / INICIO DE SESIÓN / REGISTER:
        * --> Login: Pide los datos al usuario y comprueba si existen.
        * --> TXTaVECTOR: Creamos un vector con los datos del usuario que hay en el archivo.
        * --> Register: Pide los datos al usuario y los pone en el archivo.
        * --> VECTORaTXT: Pasa el vector creado anteriormente al archivo.
        ***********************************************************************************************************/

        static int Login(CLista lista_usuarios) // Mira si existe el usuario introducido en el archivo.
        {
            try
            {
                Console.WriteLine("Introduzca su nombre de usuario");
                string usuario = Console.ReadLine();
                Console.WriteLine("Introduzca su contraseña");
                string contraseña = Console.ReadLine();  
                
                for(int i = 0; i < 3333; i++)
                {
                    if(lista_usuarios.usuarios[i].nombre == usuario )
                    {
                        return 1; // existe el usuario, proceder con la edicion de fotos
                    }
                }
                return 2;
            }
            catch(Exception)
            {
               return 0; // Lista vacia
            }
            return 2; // Sale del bucle, por lo tanto no existe el usuario entonces retorna 2
        }

        static int Register(CLista lista) //Pedimos nombre y contraseña al usuario y lo metemos en el archivo.
        {
            Console.WriteLine("Introduzca su nombre de usuario");
            string usuario = Console.ReadLine();
            Console.WriteLine("Introduzca su contraseña");
            string contraseña = Console.ReadLine();
            
            if(lista != null && lista.usuarios != null)
            {            
                for(int j = 0; j < 3333; j++)
                {
                    if(lista.usuarios[j].nombre == null && lista.usuarios[j].contraseña == null) // miramos si hay algun hueco vacio
                    {
                        lista.usuarios[j].nombre = usuario;
                        lista.usuarios[j].contraseña = contraseña;
                        return 2; //podemos guardar el user en este espacio, proceder a escribir el user en ese vector y a la edicion de fotos
                    }
                }
                return 0; //no tenemos espacio lo sentimos, cerrar programa
            }
            return 3; // la lista es null, no hay nada en el fichero
        }
        
        static CLista TXTaVECTOR() 
        {   
            StreamReader leer = new StreamReader("usuarios.txt");
            CLista lista = new CLista();

            string linea = leer.ReadLine();
            for(int i = 0; linea != null; i++)
            {
                string[] trozos = linea.Split(' ');
                if(trozos.Length == 2)
                {
                    CUsuario usuario = new CUsuario
                    {
                        nombre = trozos[0],
                        contraseña = trozos[1]
                    };
                    lista.usuarios[i] = usuario;
                }
                linea = leer.ReadLine();
            }
            leer.Close();
            return lista;
        }

        static void VECTORaTXT(CLista lista_usuarios)
        {
            StreamWriter escritura  = new StreamWriter("usuarios.txt");

            for (int i = 0; i < lista_usuarios.usuarios.Length; i++)
            {
                escritura.WriteLine("{0} {1}", lista_usuarios.usuarios[i].nombre, lista_usuarios.usuarios[i].contraseña);
            }
            escritura.Close();
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
          *  --> Matriz de pixeles
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
                //inimage = new CPixel[width, height]; //hace falta tio?

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
        // Comprueba si las cuatro imagenes son cuadradas.
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
        
        public static bool comprobarcuad(CPixel[,] inimage) // Comprueba si la primera foto es cuarada.
        {
            if(inimage.GetLength(0) == inimage.GetLength(1))
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
            Console.WriteLine("*********************************************************************************");
            Console.WriteLine();
        }

        static void minimenu()
        {
            Console.WriteLine();
            Console.WriteLine("*********************************************************************************");
            Console.WriteLine("Elije: ");
            Console.WriteLine("0. Salir");
            Console.WriteLine("1. Iniciar sesión");
            Console.WriteLine("2. Registrarse");
            Console.WriteLine("*********************************************************************************");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            string fotoelegida, fotoelegida2, fotoelegida3, fotoelegida4, colorelegido;
            string usuario, contraseña, opcion;
            int opcion_minimenu;
            bool guardada;
            bool salir = false;
            CPixel[,] inimage2, inimage3, inimage4;
            
            
            try
            {
                StreamReader lectura = new StreamReader("usuarios.txt");
                lectura.Close();
            }
            catch(FileNotFoundException)    
            {      
                Console.WriteLine("Aun no existe el archivo usuarios.txt, vamos a crearlo");
                StreamWriter creartxt = new StreamWriter("usuarios.txt");
                creartxt.Close();
            }

            CLista lista = TXTaVECTOR();
            
            for(int i = 0; i<3333; i++)
            {
                CUsuario usuarios = new CUsuario();
                lista.usuarios[i].contraseña = ".";
                lista.usuarios[i].nombre = ".";
                lista.usuarios[i] = usuarios;
            }
            
            Console.WriteLine("Buenas!! Qué desea hacer?: ");
            minimenu();
            string opcion_usuario = Console.ReadLine();

            while(opcion_usuario != "0" && !salir)
            {
                switch(opcion_usuario)
                { 
                    case "0":
                        Console.WriteLine("Saliendo chavalin");
                        break;

                    case "1":
                        Console.WriteLine("Perfecto, iniciando sesión...");
                        int login = Login(lista);
                        if(login == 1)
                        {
                            Console.WriteLine("Bienvenido de nuevo guapete");
                            opcion_minimenu = 1;
                            salir = true;
                        }
                        else if(login == 2)
                        {
                            Console.WriteLine("Datos no encontrados o datos introducidos incorrectos");
                            minimenu();
                            opcion_usuario = Console.ReadLine();
                        }
                        else if(login == 0)
                        {
                            Console.WriteLine("Error, La lista de usuarios esta vacia, no puedes iniciar sesion");
                            Console.WriteLine("Regístrese, porfavor");
                            minimenu();
                            opcion_usuario = Console.ReadLine();
                        }
                        break;

                    case "2":
                        Console.WriteLine("Perfecto, registrando usuario...");
                        int register = Register(lista);
                        if(register == 1)
                        {
                            Console.WriteLine("Este usuario ya existe");
                            minimenu();
                            opcion_usuario = Console.ReadLine();
                        }
                        else if(register == 2)
                        {
                            Console.WriteLine("Bienvenido al editor de fotos, has sido guardado");
                            opcion_minimenu = 2;

                        }
                        else if(register == 0)
                        {
                            Console.WriteLine("Lo sentimos pero no hay espacio suficiente, no podemos almacenar a mas gente!!");
                        }
                        else
                        {
                            Console.WriteLine("Vaya vaya tío, error inesperado");
                        }
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("Codigo incorrecto chavalín... elije de nuevo");
                        minimenu();
                        opcion_usuario = Console.ReadLine();
                        break;
                }
            }        
            
            Console.WriteLine("Qué imagen quieres manipular?");
            fotoelegida = Console.ReadLine();
            CPixel[,] inimage;

            bool existe = false;
            while (!existe)
            {
                try // Probamos si existe la imagen
                {                    
                    inimage = PNGtoMATRIZ(fotoelegida);
                    existe = true; // Si no hay excepción, establecemos que existe a true para salir del bucle
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Error al cargar la imagen, introduce nombre de imagen de nuevo:");
                    existe = false;
                    fotoelegida = Console.ReadLine();
                }
            }

            inimage = PNGtoMATRIZ(fotoelegida);

            if (inimage != null)
            {
                menu();
                opcion = Console.ReadLine();

                while (opcion != "0")
                {
                    switch (opcion)
                    {
                        case "0":
                            Console.WriteLine("Terminando Programa");
                            break;

                        case "1":
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
                            Console.WriteLine();
                            Console.WriteLine("Comprobando que la primera foto sea cuadrada...");
                            bool compr = comprobarcuad(inimage);

                            if(compr == true)
                            {
                                Console.WriteLine("La foto es cuadrada :))))");
                                Console.WriteLine();
                                Console.WriteLine("Para el collage necesito 3 fotos más tío (tienen que ser cuadradas)");
                                
                                existe = false;
                                Console.WriteLine("Por favor ingrese la segunda foto: ");
                                fotoelegida2 = Console.ReadLine();
                                Console.WriteLine("Por favor ingrese la tercera foto: ");
                                fotoelegida3 = Console.ReadLine();
                                Console.WriteLine("Por favor ingrese la cuarta foto: ");
                                fotoelegida4 = Console.ReadLine();

                                while(true)
                                {                                    
                                    try
                                    {
                                        inimage2 = PNGtoMATRIZ(fotoelegida2);
                                        inimage3 = PNGtoMATRIZ(fotoelegida3);
                                        inimage4 = PNGtoMATRIZ(fotoelegida4);
                                        break;
                                    }
                                    catch(ArgumentException)
                                    {
                                        Console.WriteLine("No existe alguna de las imagenes");
                                    }
                                    Console.WriteLine("Por favor ingrese la segunda foto: ");
                                    fotoelegida2 = Console.ReadLine();
                                    Console.WriteLine("Por favor ingrese la tercera foto: ");
                                    fotoelegida3 = Console.ReadLine();
                                    Console.WriteLine("Por favor ingrese la cuarta foto: ");
                                    fotoelegida4 = Console.ReadLine();
                                }
                                inimage2 = PNGtoMATRIZ(fotoelegida2);
                                inimage3 = PNGtoMATRIZ(fotoelegida3);
                                inimage4 = PNGtoMATRIZ(fotoelegida4);

                                Console.WriteLine("Vamos a comprobar si son cuadradas...");
                                
                                bool comprobar = ComprobarCuadrado(inimage, inimage2, inimage3, inimage4);
                                if(comprobar == true)
                                {
                                    Console.WriteLine("Perfecto, son cuadradas");
                                    Console.WriteLine();
                                    Console.WriteLine("Vamos a empezar con el collage");

                                    if(collage(inimage, inimage2, inimage3, inimage4) != null)
                                    {
                                        string nom = "collage.png";
                                        CPixel[,] result = collage(inimage, inimage2, inimage3, inimage4);
                                        guardada = MATRIZtoPNG(result, nom);
                                        if(guardada == true)
                                        {
                                            Console.WriteLine("Foto guardada tio");
                                        }
                                        else
                                        {
                                            Console.WriteLine("No se ha guardado tio");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("La foto dada no es cuadrada por lo tanto no se puede hacer el collage, ingrese otra operación");
                            }
                            break;
                            
                        case "3":
                            Console.WriteLine("¿Quieres cambiar a rojo, verde o azul?: ");
                            colorelegido = Console.ReadLine().ToLower();

                            if(colorelegido == "rojo" || colorelegido ==  "verde" || colorelegido ==  "azul")
                            {
                                Console.WriteLine("Perfecto, " + colorelegido + ", manos a la obra");
                                cambiocolor(inimage, colorelegido);
                                if (cambiocolor(inimage, colorelegido) != null)
                                {
                                    guardada = MATRIZtoPNG(cambiocolor(inimage, colorelegido), fotoelegida);
                                    if (guardada == true)
                                    {
                                        Console.WriteLine("Foto guardada tio");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error al elegir un color Albert, lee bien...");
                            }
                            break;

                        case "4":
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
            Console.WriteLine("Hasta otra tío! :D");
        }
    }
}

/*

COSAS QUE HAY QUE ACABAR PARA QUE NO HAYAN ERRORES PARA EL USUARIO: 

1. CAMBIAR LA GESTION DE USUARIOS Y QUE SOLO SE GUARDE EN EL .TXT SU NOMBRE / CORREO / CONTRASEÑA EN ESTE FORMATO: {1}-{2}-{3}

2. DANI!!! EN EL PRIMER SWITCH DE (LOGIN/REGISTER) SE GUARDA EN UN .TXT EL NOMBRE DE USUARIO, CORREO Y CONTRASEÑA, EN EL SEGUNDO SWITCH SE ALMACENA LOKE YA TENEMOS
EL ALBERT ME HA DICHO QUE ESTA BIEN LO DE LOS DOS SWITCH 

3. BASICMENTE HAY QUE ARREGLAR LAS FUNCIONES REGISTER Y LOGIN DE MANERA QUE NO DEN ERROR AL USUARIO

*/